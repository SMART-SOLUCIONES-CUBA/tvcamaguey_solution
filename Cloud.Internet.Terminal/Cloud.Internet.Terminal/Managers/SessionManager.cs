using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Managers
{
    public class SessionManager
    {
        private const string LOGIN_KEY = "session-login";
        private const string IDENTITY_ID_KEY = "session-identity-id";
        private const string AUTHENTICATION_TYPE_KEY = "session-authentication-type";
        private const string AUTHENTICATION_TOKEN_KEY = "session-authentication-token";

        private static SessionManager instance;

        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionManager();
                }

                return instance;
            }
        }

        public bool IsAuthenticated => Xamarin.Forms.Application.Current.Properties.ContainsKey(LOGIN_KEY);

        public bool IsCompleteProfiled
        {
            get
            {
                return Identity.Name != Login;
            }
        }

        public SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity Identity
        {
            get
            {
                SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity identity = null;

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    identity = dbContext.PersonIdentities.Include(e => e.Image).First(e => e.Id == IdentityId);
                }

                return identity;
            }
        }

        public string Login => Xamarin.Forms.Application.Current.Properties[LOGIN_KEY]?.ToString();

        public System.Guid IdentityId => System.Guid.Parse(Xamarin.Forms.Application.Current.Properties[IDENTITY_ID_KEY]?.ToString());

        public string AuthenticationType => Xamarin.Forms.Application.Current.Properties[AUTHENTICATION_TYPE_KEY]?.ToString();

        public string AuthenticationToken => Xamarin.Forms.Application.Current.Properties[AUTHENTICATION_TOKEN_KEY]?.ToString();

        public async System.Threading.Tasks.Task<string> RegisterAsync(string login)
        {
            var securityApiClient = new Api.Connect.Clients.SecurityApiClient(Providers.HttpClientHelperProvider.Instance.HttpClientHelper);

            return await securityApiClient.RegisterAsync(new Api.Common.Models.RegistrationRequestModel
            {
                Login = login
            });
        }

        public async System.Threading.Tasks.Task<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity> AuthenticateAsync(string login, string token, string purpose)
        {
            var institutionInfoNomenclatorsApiClient = new Api.Connect.Clients.InstitutionInfoNomenclatorsApiClient(Providers.HttpClientHelperProvider.Instance);

            var institutionInfoNomenclatorModels = await institutionInfoNomenclatorsApiClient.GetAsync();

            var securityApiClient = new Api.Connect.Clients.SecurityApiClient(Providers.HttpClientHelperProvider.Instance.HttpClientHelper);

            var response = await securityApiClient.AuthenticateAsync(new Api.Common.Models.AuthenticationRequestModel
            {
                Login = login,
                Token = token,
                Purpose = purpose
            });

            SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity identity = null;

            using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
            {
                await Providers.DbContextProvider.Instance.InitializeAsync(dbContext);

                if (response.Identity.Image != null)
                {
                    await Helpers.ImageHandlingHelper.FetchRemoteImageFileAsync(response.Identity.Image.Id, response.Identity.Image.ContentType);
                }

                identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity
                {
                    Id = response.Identity.Id,
                    Name = response.Identity.Name,
                    LastName = response.Identity.LastName,
                    Alias = response.Identity.Alias,
                    BornDate = response.Identity.BornDate,
                    CivilStatus = response.Identity.CivilStatus,
                    Sex = response.Identity.Sex,
                    About = response.Identity.About,
                    Image = response.Identity.Image == null ? null : new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                    {
                        Id = response.Identity.Image.Id,
                        CreateAt = response.Identity.Image.CreateAt,
                        ContentType = response.Identity.Image.ContentType,
                        Length = response.Identity.Image.Length,
                        Width = response.Identity.Image.Width,
                        Height = response.Identity.Image.Height,
                        About = response.Identity.Image.About
                    }
                };

                await dbContext.PersonIdentities.AddAsync(identity);

                await dbContext.InstitutionInfoNomenclators.AddRangeAsync(institutionInfoNomenclatorModels.Select(m => new Internet.Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = m.Id,
                    Value = m.Value
                }));

                await dbContext.SaveChangesAsync(string.Empty, syncronize: false);
            }

            Xamarin.Forms.Application.Current.Properties[AUTHENTICATION_TYPE_KEY] = response.Type;
            Xamarin.Forms.Application.Current.Properties[AUTHENTICATION_TOKEN_KEY] = response.Token;
            Xamarin.Forms.Application.Current.Properties[LOGIN_KEY] = login;
            Xamarin.Forms.Application.Current.Properties[IDENTITY_ID_KEY] = response.Identity.Id;

            await Xamarin.Forms.Application.Current.SavePropertiesAsync();

            return identity;
        }

        public async System.Threading.Tasks.Task CloseAsync()
        {
            Xamarin.Forms.Application.Current.Properties.Clear();

            await Xamarin.Forms.Application.Current.SavePropertiesAsync();

            SAPRESSC.Middleware.Media.Persistence.Entities.Image[] images;

            using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
            {
                images = dbContext.Images.Include(e => e.Thumbnail).ToArray();
            }

            System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetDatabaseFilePath());

            foreach (var image in images)
            {
                System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetImageFilePath(image.Id, image.ContentType.ToImageExtension()));
            }
        }
    }
}

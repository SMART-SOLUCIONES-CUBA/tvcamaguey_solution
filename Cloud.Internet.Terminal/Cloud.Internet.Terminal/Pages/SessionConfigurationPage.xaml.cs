using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class SessionConfigurationPage : XFToolKit.Mvvm.Pages.CarouselPage
	{
        private bool profileChangesFlag;

        private Xamarin.Forms.ContentPage TargetPage;

        public SessionConfigurationPage()
		{
			InitializeComponent();

            profileChangesFlag = false;

            BindingContext = new Models.Page.SessionConfigurationPageModel();
		}

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void BeginButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            TargetPage = Children[1];

            CurrentPage = TargetPage;
        }

        private void RegisterButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (sender as Xamarin.Forms.Button).IsEnabled = false;

            var userRegistrationPageModel = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.Page.UserRegistrationPageModel;

            userRegistrationPageModel.IsBusy = true;

            Managers.SessionManager.Instance.RegisterAsync(userRegistrationPageModel.Login).ContinueWith((System.Threading.Tasks.Task<string> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    userRegistrationPageModel.IsBusy = false;

                    (sender as Xamarin.Forms.Button).IsEnabled = true;

                    if (task.Exception != null)
                    {
                        (Xamarin.Forms.Application.Current as App).HandleException(task.Exception);

                        return;
                    }

                    var userAuthenticationPageModel = (BindingContext as Models.Page.SessionConfigurationPageModel).UserAuthenticationPageModel;

                    userAuthenticationPageModel.Login = userRegistrationPageModel.Login;
                    userAuthenticationPageModel.Purpose = task.Result;

                    TargetPage = Children[2];

                    CurrentPage = TargetPage;
                });
            });
        }

        private void AuthenticateButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (sender as Xamarin.Forms.Button).IsEnabled = false;

            var userAuthenticationPageModel = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.Page.UserAuthenticationPageModel;

            userAuthenticationPageModel.IsBusy = true;

            Managers.SessionManager.Instance.AuthenticateAsync(userAuthenticationPageModel.Login, userAuthenticationPageModel.Token, userAuthenticationPageModel.Purpose).ContinueWith((System.Threading.Tasks.Task<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    userAuthenticationPageModel.IsBusy = false;

                    (sender as Xamarin.Forms.Button).IsEnabled = true;

                    if (task.Exception != null)
                    {
                        (Xamarin.Forms.Application.Current as App).HandleException(task.Exception);

                        return;
                    }

                    var userEssentialProfileEditionPageModel = (BindingContext as Models.Page.SessionConfigurationPageModel).UserEssentialProfileEditionPageModel;

                    if (task.Result.Name != Managers.SessionManager.Instance.Login)
                    {
                        userEssentialProfileEditionPageModel.Name = task.Result.Name;
                    }

                    if (task.Result.Image != null)
                    {
                        userEssentialProfileEditionPageModel.ProfileImageExtension = task.Result.Image.ContentType.ContentTypeToExtension();
                        userEssentialProfileEditionPageModel.ProfileImageId = task.Result.Image.Id;
                    }

                    userEssentialProfileEditionPageModel.PropertyChanged += UserEssentialProfileEditionPageModelPropertyChanged;

                    TargetPage = Children[3];

                    CurrentPage = TargetPage;
                });
            });
        }

        private void FinalizeButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            (sender as Xamarin.Forms.Button).IsEnabled = false;

            var userEssentialProfileEditionPageModel = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.Page.UserEssentialProfileEditionPageModel;

            if (string.IsNullOrEmpty(userEssentialProfileEditionPageModel.Name) || string.IsNullOrWhiteSpace(userEssentialProfileEditionPageModel.Name))
            {
                DisplayAlert("Aviso", "Es necesario que escribas tu nombre para poder continuar.", "Aceptar");

                (sender as Xamarin.Forms.Button).IsEnabled = true;

                return;
            }

            System.Threading.Tasks.Task.Run(async () =>
            {
                if (profileChangesFlag)
                {
                    userEssentialProfileEditionPageModel.IsBusy = true;

                    using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                    {
                        var entity = dbContext.PersonIdentities.Include(e => e.Image).First(e => e.Id == Managers.SessionManager.Instance.IdentityId);

                        entity.Name = userEssentialProfileEditionPageModel.Name;

                        if (userEssentialProfileEditionPageModel.ProfileImageId != System.Guid.Empty && userEssentialProfileEditionPageModel.ProfileImageId != entity.ImageId)
                        {
                            SAPRESSC.Middleware.Media.Persistence.Entities.Image oldImage = entity.Image;

                            var fileInfo = new System.IO.FileInfo(Helpers.LocalFileStorageHelper.GetImageFilePath(userEssentialProfileEditionPageModel.ProfileImageId, userEssentialProfileEditionPageModel.ProfileImageExtension));

                            entity.Image = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                Id = userEssentialProfileEditionPageModel.ProfileImageId,
                                ContentType = userEssentialProfileEditionPageModel.ProfileImageExtension.ToImageContentType(),
                                CreateAt = System.DateTime.Now,
                                Length = fileInfo.Length,
                                About = string.Format("Image for Identity : {0}", entity.Id)
                            };

                            if (oldImage != null)
                            {
                                dbContext.Images.Remove(oldImage);

                                System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetImageFilePath(oldImage.Id, oldImage.ContentType.ContentTypeToExtension()));
                            }
                        }

                        await dbContext.SaveChangesAsync(string.Empty);
                    }
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    userEssentialProfileEditionPageModel.PropertyChanged -= UserEssentialProfileEditionPageModelPropertyChanged;

                    await this.NavigationPopModalPageAsync();

                    ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).InitializeContent(true);
                });
            });            
        }

        private void PickImageButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            string[] fileSelector = null;

            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.Android:
                    fileSelector = new string[] { "image/*" };

                    break;

                case Xamarin.Forms.Device.UWP:
                    fileSelector = new string[] { ".jpg", ".jpeg", ".png", ".bmp" };

                    break;
            }

            var userEssentialProfileEditionPageModel = (sender as Xamarin.Forms.BindableObject).BindingContext as Models.Page.UserEssentialProfileEditionPageModel;

            userEssentialProfileEditionPageModel.IsBusy = true;

            Plugin.FilePicker.CrossFilePicker.Current.PickFile(fileSelector).ContinueWith((System.Threading.Tasks.Task<Plugin.FilePicker.Abstractions.FileData> pickFileTask) =>
            {
                if (pickFileTask.Result != null)
                {
                    var newImageExtension = System.IO.Path.GetExtension(pickFileTask.Result.FileName);

                    var newImageId = System.Guid.NewGuid();

                    using (var profileImageFileStream = System.IO.File.OpenWrite(Helpers.LocalFileStorageHelper.GetImageFilePath(newImageId, newImageExtension)))
                    {
                        var pickImageFileStream = pickFileTask.Result.GetStream();

                        pickImageFileStream.Position = 0;

                        pickImageFileStream.CopyTo(profileImageFileStream);
                    }

                    userEssentialProfileEditionPageModel.ProfileImageExtension = newImageExtension;
                    userEssentialProfileEditionPageModel.ProfileImageId = newImageId;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    userEssentialProfileEditionPageModel.IsBusy = false;
                });
            });
        }

        private void CarouselPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            if (TargetPage == null)
            {
                TargetPage = CurrentPage;

                return;
            }

            if (TargetPage != CurrentPage)
            {
                CurrentPage = TargetPage;
            }
        }

        private void UserEssentialProfileEditionPageModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            profileChangesFlag = true;
        }
    }
}
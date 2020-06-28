using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Syncronization.Handling.Persistence.EF.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Web.Common.Extensions;
using SmartSolucionesCuba.SAPRESSC.Syncronization.Web.Common.Extensions;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Options.MediaResizeConstraintOptions>(configuration.GetSection("MediaResizeConstraint"));

            services.AddDbContext<Persistence.EF.Context.ApplicationDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"), mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new System.Version(configuration.GetSection("MySQLSeverVersion").Value), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
            }));

            services.AddScoped<DbContext, Persistence.EF.Context.ApplicationDbContext>();

            services.PerformCoreWebSecurityServiceRegistration();
            services.PerformCoreWebSecurityLocalServiceRegistration();
            services.PerformMiddlewareIdentityWebCommonServiceRegistration();
            services.PerformSyncronizationWebCommonServiceRegistration();

            services.AddSingleton<SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Cloud.Persistence.EF.Providers.BusinessDbSchemaConfigurationProvider>();

            services.AddSingleton<SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper, Helpers.DataInitializationHelper>();

            services.AddScoped<IEntityRepository<Employ, System.Guid>, EmployRepository>();
            services.AddScoped<IEntityRepository<Blog, System.Guid>, BlogRepository>();
            services.AddScoped<IEntityRepository<Award, System.Guid>, AwardRepository>();
            services.AddScoped<IEntityRepository<Show, System.Guid>, ShowRepository>();
            services.AddScoped<IEntityRepository<ProductionTeam, System.Guid>, ProductionTeamRepository>();
            services.AddScoped<IEntityRepository<Presentation, System.Guid>, PresentationRepository>();

            #region Syncronization Delivery Services Configuration

            services.PerformMiddlewareIdentitySyncronizationHandlingPersistenceEFServiceRegistration();

            services.AddSingleton<SAPRESSC.Syncronization.Persistence.EF.Helpers.Facade.ISyncronizationDbChangeHandleHelperFacade, Helpers.Syncronization.SyncronizationDbChangeHandleHelper>();

            services.AddSingleton<SAPRESSC.Syncronization.Web.Api.Delivery.Helpers.Facade.ISyncronizationDeliveryHelperFacade, Helpers.Syncronization.SyncronizationDeliveryHelper>();

            #endregion

            services.AddSingleton<SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade, Managers.ImageFileManager>();
            services.AddSingleton<SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IThumbnailFileManagerFacade, Managers.ThumbnailFileManager>();

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(config =>
            {
                config.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/security/error/forbidden");
                config.LoginPath = new Microsoft.AspNetCore.Http.PathString("/security/account/login");
                config.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
                config.SlidingExpiration = true;
            });

            #region SAPRESSC.Core.Security concreter dependencies configuration
            services.AddSingleton<SAPRESSC.Core.Web.Security.Licensing.ILicenseValidator, Security.Licensing.Validator>();

            services.AddTransient<SAPRESSC.Core.Web.Security.Models.View.IUserProfileViewModel, Models.View.UserProfileViewModel>();

            services.AddSingleton<SAPRESSC.Core.Web.Common.ModelBinding.IConcretModelBindHelper, Helpers.ModelBinding.ConcretModelBindHelper>();

            services.AddScoped<SAPRESSC.Core.Web.Security.Providers.Facade.IIdentityProviderFacade, SAPRESSC.Core.Web.SecurityLocal.Managers.IdentityManager>();

            services.AddScoped<SAPRESSC.Core.Web.Security.Managers.Facade.IUserProfileManagerFacade, Managers.UserProfileManager>();
            #endregion

            #region Globalization configuration

            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders = new[] { new SAPRESSC.Core.Web.Common.Localization.UrlRequestCultureProvider() };
            });

            services.AddSingleton<Helpers.Localization.GlobalViewLocalizationHelper>();

            #endregion

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc(mvc =>
                mvc.ModelBinderProviders.Insert(0, new SAPRESSC.Core.Web.Common.ModelBinding.AbstractsModelBinderProvider())
            )
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResources));
                }
            )
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_0);

            services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(env.ContentRootPath, "dpkeys")));

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> requestLocalizationOptions, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, DbContext dbContext)
        {
            loggerFactory.AddSerilog(Log.Logger);

            app.UseRequestLocalization(requestLocalizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if (env.IsStaging())
            {
                dbContext.Database.Migrate();

                app.PerformDatabaseInitialization();

                if (!(dbContext as Persistence.EF.Context.ApplicationDbContext).Users.Any())
                {
                    var defaultUser = new SAPRESSC.Core.Security.Persistence.Entities.User
                    {
                        Id = configuration["DefaultUser:Id"],
                        Password = configuration["DefaultUser:Password"].ToHashedPassword(),
                        Enabled = true,
                        HasFullAccess = true,
                        PasswordChangeRequired = true
                    };

                    defaultUser.Signature = SAPRESSC.Core.Web.SecurityLocal.Helpers.Integrity.UserIntegrityHelper.GetSignature(defaultUser);

                    (dbContext as Persistence.EF.Context.ApplicationDbContext).Users.Add(defaultUser);

                    (dbContext as Persistence.EF.Context.ApplicationDbContext).SaveChanges("system");
                }
            }
            else
            {
                app.UseExceptionHandler("/error");

                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaultLocalizedAreaRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultLocalizedRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute("defaultAreaRoutes",
                    template: "{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultRoutes",
                    template: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}

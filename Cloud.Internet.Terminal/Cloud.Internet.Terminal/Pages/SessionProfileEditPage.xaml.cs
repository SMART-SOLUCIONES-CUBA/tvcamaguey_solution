using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class SessionProfileEditPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private bool changesFlag;

        private Models.Page.UserProfilePageModel reference;

        public SessionProfileEditPage(Models.Page.UserProfilePageModel reference)
		{
			InitializeComponent();

            changesFlag = false;

            this.reference = reference;

            var model = new Models.Page.UserProfilePageModel
            {
                IsBusy = false,
                Id = reference.Id,
                Name = reference.Name,
                LastName = reference.LastName,
                BornDate = reference.BornDate,
                Alias = reference.Alias,
                Sex = reference.Sex,
                CivilStatus = reference.CivilStatus,
                About = reference.About,
                ProfileImageId = reference.ProfileImageId,
                ProfileImageExtension = reference.ProfileImageExtension
            };

            model.PropertyChanged += PageModelPropertyChanged;

            BindingContext = model;
		}

        protected override bool OnBackButtonPressed()
        {
            if (changesFlag)
            {
                DisplayAlert("Confirmar", "Está seguro que desea regresar y omitir los cambios realizados?", "Regresar", "Continuar").ContinueWith((System.Threading.Tasks.Task<bool> alertTask) =>
                {
                    var newImageId = (BindingContext as Models.Page.UserProfilePageModel).ProfileImageId;

                    if (reference.ProfileImageId != newImageId && System.Guid.Empty != newImageId)
                    {
                        System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetImageFilePath(newImageId, (BindingContext as Models.Page.UserProfilePageModel).ProfileImageExtension));
                    }

                    if (alertTask.Result)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            this.NavigationPopPageAsync();
                        });
                    }
                });
            }
            else
            {
                this.NavigationPopPageAsync();
            }

            return true;
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

            (BindingContext as Models.Page.UserProfilePageModel).IsBusy = true;

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

                    (BindingContext as Models.Page.UserProfilePageModel).ProfileImageExtension = newImageExtension;
                    (BindingContext as Models.Page.UserProfilePageModel).ProfileImageId = newImageId;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.UserProfilePageModel).IsBusy = false;
                });
            });
        }

        private void ApplyToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (!changesFlag)
            {
                this.NavigationPopPageAsync();
            }
            else
            {
                var model = BindingContext as Models.Page.UserProfilePageModel;

                System.Threading.Tasks.Task.Run(async () =>
                {
                    using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                    {
                        var entity = dbContext.PersonIdentities.Include(e => e.Image).First(e => e.Id == Managers.SessionManager.Instance.IdentityId);

                        entity.Name = model.Name;
                        entity.LastName = model.LastName;
                        entity.BornDate = model.BornDate;
                        entity.Alias = model.Alias;
                        entity.Sex = model.Sex;
                        entity.CivilStatus = model.CivilStatus;
                        entity.About = model.About;

                        var entityImageId = entity.Image?.Id ?? System.Guid.Empty;

                        if (model.ProfileImageId != System.Guid.Empty && model.ProfileImageId != entityImageId)
                        {
                            var fileInfo = new System.IO.FileInfo(Helpers.LocalFileStorageHelper.GetImageFilePath(model.ProfileImageId, model.ProfileImageExtension));

                            entity.Image = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                Id = model.ProfileImageId,
                                ContentType = model.ProfileImageExtension.ToImageContentType(),
                                CreateAt = System.DateTime.Now,
                                Length = fileInfo.Length,
                                About = string.Format("Image for Identity : {0}", entity.Id)
                            };
                        }

                        dbContext.PersonIdentities.Update(entity);

                        string oldImageExtension = null;

                        if (entityImageId != System.Guid.Empty && model.ProfileImageId != entityImageId)
                        {
                            var oldImage = dbContext.Images.First(e => e.Id == entityImageId);

                            oldImageExtension = oldImage.ContentType.ToImageExtension();

                            dbContext.Images.Remove(dbContext.Images.First(e => e.Id == entityImageId));
                        }

                        await dbContext.SaveChangesAsync(string.Empty);

                        if (entityImageId != System.Guid.Empty && model.ProfileImageId != entityImageId)
                        {
                            System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetImageFilePath(entityImageId, oldImageExtension));
                        }
                    }

                    reference.Name = model.Name;
                    reference.LastName = model.LastName;
                    reference.BornDate = model.BornDate;
                    reference.Alias = model.Alias;
                    reference.Sex = model.Sex;
                    reference.CivilStatus = model.CivilStatus;
                    reference.About = model.About;
                    reference.ProfileImageExtension = model.ProfileImageExtension;
                    reference.ProfileImageId = model.ProfileImageId;

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        model.IsBusy = false;

                        this.NavigationPopPageAsync();

                        ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).InitializeContent(false);
                    });
                });
            }
        }

        private void CancelToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (changesFlag)
            {
                DisplayAlert("Confirmar", "Está seguro que desea cancelar y omitir los cambios realizados?", "Cancelar", "Continuar").ContinueWith((System.Threading.Tasks.Task<bool> alertTask) =>
                {
                    var newImageId = (BindingContext as Models.Page.UserProfilePageModel).ProfileImageId;

                    if (reference.ProfileImageId != newImageId && System.Guid.Empty != newImageId)
                    {
                        System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetImageFilePath(newImageId, (BindingContext as Models.Page.UserProfilePageModel).ProfileImageExtension));
                    }

                    if (alertTask.Result)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            this.NavigationPopPageAsync();
                        });
                    }
                });
            }
            else
            {
                this.NavigationPopPageAsync();
            }
        }

        private void PageModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            changesFlag = true;
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            (BindingContext as Models.Page.UserProfilePageModel).PropertyChanged -= PageModelPropertyChanged;

            reference = null;
        }
    }
}
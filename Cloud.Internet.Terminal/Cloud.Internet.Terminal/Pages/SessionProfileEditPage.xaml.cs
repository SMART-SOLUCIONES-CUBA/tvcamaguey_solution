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

        private byte[] imagePickedData;

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

        private async void PickImageButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var action = await DisplayActionSheet("Seleccionar Imágen de Perfil desde", "Cancelar", null, Helpers.ConstantProvisionHelper.ACTIONSHEET_PICKIMAGE_OPTION_CAMARA, Helpers.ConstantProvisionHelper.ACTIONSHEET_PICKIMAGE_OPTION_GALLERY);

            if (action == Helpers.ConstantProvisionHelper.ACTIONSHEET_PICKIMAGE_OPTION_CAMARA)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.UserProfilePageModel).IsBusy = true;
                    (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = "Capturando imagen para el perfil ...";
                });

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.UserProfilePageModel).IsBusy = false;
                        (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(delayTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
                        });
                    });
                });

                await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()).ContinueWith(PickedImageContinuationTask);
            }
            else if (action == Helpers.ConstantProvisionHelper.ACTIONSHEET_PICKIMAGE_OPTION_GALLERY)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.UserProfilePageModel).IsBusy = true;
                    (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = "Seleccionando imagen para el perfil ...";
                });

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Error", "This device is not supported to pick photo.", "OK");

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.UserProfilePageModel).IsBusy = false;
                        (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(delayTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
                        });
                    });
                });

                await Plugin.Media.CrossMedia.Current.PickPhotoAsync().ContinueWith(PickedImageContinuationTask);
            }
        }

        private void ApplyToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            if (!changesFlag)
            {
                this.NavigationPopPageAsync();
            }
            else
            {
                var pageBindingContext = BindingContext as Models.Page.UserProfilePageModel;

                System.Threading.Tasks.Task.Run(async () =>
                {
                    if (imagePickedData != null)
                    {
                        pageBindingContext.ProfileImageId = System.Guid.NewGuid();
                        pageBindingContext.ProfileImageExtension = ".jpg";

                        using (var profileImageFileStream = System.IO.File.OpenWrite(Helpers.LocalFileStorageHelper.GetImageFilePath(pageBindingContext.ProfileImageId, pageBindingContext.ProfileImageExtension)))
                        {
                            await profileImageFileStream.WriteAsync(imagePickedData, 0, imagePickedData.Length);
                        }

                        imagePickedData = null;
                    }

                    using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                    {
                        var entity = dbContext.PersonIdentities.Include(e => e.Image).First(e => e.Id == Managers.SessionManager.Instance.IdentityId);

                        entity.Name = pageBindingContext.Name;
                        entity.LastName = pageBindingContext.LastName;
                        entity.BornDate = pageBindingContext.BornDate;
                        entity.Alias = pageBindingContext.Alias;
                        entity.Sex = pageBindingContext.Sex;
                        entity.CivilStatus = pageBindingContext.CivilStatus;
                        entity.About = pageBindingContext.About;

                        var entityImageId = entity.Image?.Id ?? System.Guid.Empty;

                        if (pageBindingContext.ProfileImageId != System.Guid.Empty && pageBindingContext.ProfileImageId != entityImageId)
                        {
                            var fileInfo = new System.IO.FileInfo(Helpers.LocalFileStorageHelper.GetImageFilePath(pageBindingContext.ProfileImageId, pageBindingContext.ProfileImageExtension));

                            entity.Image = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                Id = pageBindingContext.ProfileImageId,
                                ContentType = pageBindingContext.ProfileImageExtension.ToImageContentType(),
                                CreateAt = System.DateTime.Now,
                                Length = fileInfo.Length,
                                About = string.Format("Image for Identity : {0}", entity.Id)
                            };
                        }

                        dbContext.PersonIdentities.Update(entity);

                        string oldImageExtension = null;

                        if (entityImageId != System.Guid.Empty && pageBindingContext.ProfileImageId != entityImageId)
                        {
                            var oldImage = dbContext.Images.First(e => e.Id == entityImageId);

                            oldImageExtension = oldImage.ContentType.ToImageExtension();

                            dbContext.Images.Remove(dbContext.Images.First(e => e.Id == entityImageId));
                        }

                        await dbContext.SaveChangesAsync(string.Empty);

                        if (entityImageId != System.Guid.Empty && pageBindingContext.ProfileImageId != entityImageId)
                        {
                            System.IO.File.Delete(Helpers.LocalFileStorageHelper.GetImageFilePath(entityImageId, oldImageExtension));
                        }
                    }

                    reference.Name = pageBindingContext.Name;
                    reference.LastName = pageBindingContext.LastName;
                    reference.BornDate = pageBindingContext.BornDate;
                    reference.Alias = pageBindingContext.Alias;
                    reference.Sex = pageBindingContext.Sex;
                    reference.CivilStatus = pageBindingContext.CivilStatus;
                    reference.About = pageBindingContext.About;
                    reference.ProfileImageExtension = pageBindingContext.ProfileImageExtension;
                    reference.ProfileImageId = pageBindingContext.ProfileImageId;

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        pageBindingContext.IsBusy = false;

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

        private void PickedImageCroppingComplete__Callback(SkiaSharp.SKBitmap result)
        {
            if (result != null)
            {
                using (var skImage = SkiaSharp.SKImage.FromBitmap(result))
                {
                    using (var skData = skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75))
                    {
                        imagePickedData = skData.ToArray();
                    }
                }

                result.Dispose();

                ProfileImage.Source = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(imagePickedData));
            }

            this.NavigationPopModalPageAsync();

            (BindingContext as Models.Page.UserProfilePageModel).IsBusy = false;
            (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = string.Empty;
        }

        private async System.Threading.Tasks.Task PickedImageContinuationTask(System.Threading.Tasks.Task<Plugin.Media.Abstractions.MediaFile> task)
        {
            if (task.Result == null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.UserProfilePageModel).IsBusy = false;
                    (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = string.Empty;
                });

                return;
            }

            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100));

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
            });

            SkiaSharp.SKBitmap croppingImageTarget = null;

            using (var imageFileStream = System.IO.File.OpenRead(task.Result.Path))
            {
                using (var skStream = new SkiaSharp.SKManagedStream(imageFileStream))
                {
                    croppingImageTarget = SkiaSharp.SKBitmap.Decode(skStream);
                }
            }

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                (BindingContext as Models.Page.UserProfilePageModel).BusyProgressMessage = "Recortando imagen ...";

                this.NavigationPushModalPageAsync(new Xamarin.Forms.NavigationPage(new SessionProfileImageCroppingPage(croppingImageTarget, PickedImageCroppingComplete__Callback)));
            });
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            (BindingContext as Models.Page.UserProfilePageModel).PropertyChanged -= PageModelPropertyChanged;

            reference = null;
        }
    }
}
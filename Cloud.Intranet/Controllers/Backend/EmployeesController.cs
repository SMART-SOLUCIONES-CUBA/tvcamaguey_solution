using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("business")]
    public class EmployeesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Employ, System.Guid, EmployInputViewModel, EmployDisplayViewModel>
    {
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;
        private readonly IEntityRepository<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository;
        private readonly SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManagerFacade;

        public EmployeesController(SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository, IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<Employ, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<EmployeesController> localizer, ILogger<EmployeesController> logger) : base(repository, localizer, logger)
        {
            this.identityRepository = identityRepository;
            this.imageRepository = imageRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployCreator)]
        public override IActionResult Create(EmployInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    modelInput.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployEditor)]
        public override IActionResult Edit(EmployInputViewModel modelInput)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    modelInput.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.EmployRemover)]
        public override IActionResult Delete(EmployDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Employ entity, EmployInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            var identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity
            {
                Name = modelInput.Name,
                LastName = modelInput.LastName,
                Alias = modelInput.Alias,
                BornDate = modelInput.BornDate,
                CivilStatus = modelInput.CivilStatus,
                Sex = modelInput.Sex,
                About = modelInput.About
            };

            if (modelInput.ImageFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.ImageFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            identity.Image = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.ImageFile.ContentType,
                                Length = modelInput.ImageFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height,
                                About = string.Format("Image for Employ :{0}", entity.Id)
                            };

                            imageFileManagerFacade.SaveImageFileContent(identity.Image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);
                        }
                    }
                }
            }

            identityRepository.Add(identity, User.Identity.Name);

            entity.IdentityId = identity.Id;

            foreach (var item in modelInput.SelectableAdministrativeRoles.Where(item => item.Selected))
            {
                entity.AdministrationRoleBridges.Add(new EmployAdministrationRoleBridge
                {
                    Employ = entity,
                    AdministrativeRole = (Cloud.Persistence.Enumerations.AdministrativeRole)item.Id
                });
            }            
        }
        protected override void PreEdit(Employ entity, EmployInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            entity.AdministrationRoleBridges.Clear();

            foreach (var item in modelInput.SelectableAdministrativeRoles.Where(item => item.Selected))
            {
                entity.AdministrationRoleBridges.Add(new EmployAdministrationRoleBridge
                {
                    Employ = entity,
                    AdministrativeRole = (Cloud.Persistence.Enumerations.AdministrativeRole)item.Id
                });
            }

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                {
                    using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                    {
                        entity.Identity.Image = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                        {
                            ContentType = modelInput.ImageFile.ContentType,
                            Length = modelInput.ImageFile.Length,
                            Width = imageSkBitmap.Width,
                            Height = imageSkBitmap.Height,
                            About = string.Format("Image for Employ :{0}", entity.Id)
                        };

                        imageFileManagerFacade.SaveImageFileContent(entity.Identity.Image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);
                    }
                }
            }
        }

        protected override void PostDelete(Employ entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);
        }

        private void PopulateModelInputForAvailableRoles(EmployInputViewModel modelInput)
        {
            var administrativeRoleValues = System.Enum.GetValues(typeof(Cloud.Persistence.Enumerations.AdministrativeRole));

            var administrativeRoleNames = System.Enum.GetNames(typeof(Cloud.Persistence.Enumerations.AdministrativeRole));

            for (int i = 0; i < administrativeRoleValues.Length; i++)
            {
                var value = administrativeRoleValues.GetValue(i);
                var name = administrativeRoleNames.GetValue(i);

                modelInput.SelectableAdministrativeRoles.Add(new SAPRESSC.Core.Common.Models.View.SelectionItemInputViewModel<int>
                {
                    Id = (int)value,
                    Selected = false,
                    Label = name.ToString(),
                });
            }
        }

        protected override void ConfigureCreate(EmployInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableRoles(modelInput);
        }

        protected override void ConfigureEdit(Employ entity, EmployInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableRoles(modelInput);

            foreach (var item in modelInput.SelectableAdministrativeRoles)
            {
                item.Selected = entity.AdministrationRoleBridges.Any(b => b.AdministrativeRole == (Cloud.Persistence.Enumerations.AdministrativeRole)item.Id);
            }
        }
    }
}
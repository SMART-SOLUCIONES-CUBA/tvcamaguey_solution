using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("business")]
    public class ShowsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Show, System.Guid, ShowInputViewModel, ShowDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository;

        public ShowsController(IEntityRepository<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Show, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ShowsController> localizer, ILogger<ShowsController> logger) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowCreator)]
        public override IActionResult Create(ShowInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        ModelState.ClearValidationState(formFile.Name);

                        switch (formFile.Name)
                        {
                            case "LogoFile":
                                modelInput.LogoFile = formFile;

                                break;

                            case "PosterFile":
                                modelInput.PosterFile = formFile;

                                break;
                        }
                    }
                }

                TryUpdateModelAsync(modelInput).Wait();
            }

            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowEditor)]
        public override IActionResult Edit(ShowInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        ModelState.ClearValidationState(formFile.Name);

                        switch (formFile.Name)
                        {
                            case "LogoFile":
                                modelInput.LogoFile = formFile;

                                break;

                            case "PosterFile":
                                modelInput.PosterFile = formFile;

                                break;
                        }
                    }
                }

                TryUpdateModelAsync(modelInput).Wait();
            }

            if (modelInput.LogoFile == null)
            {
                ModelState.ClearValidationState("LogoFile");
                ModelState.MarkFieldValid("LogoFile");
            }

            if (modelInput.PosterFile == null)
            {
                ModelState.ClearValidationState("PosterFile");

                ModelState.MarkFieldValid("PosterFile");
            }

            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowRemover)]
        public override IActionResult Delete(ShowDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Show entity, ShowInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            SAPRESSC.Middleware.Media.Persistence.Entities.Image logo = null;
            SAPRESSC.Middleware.Media.Persistence.Entities.Image poster = null;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.LogoFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                {
                    using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                    {
                        if (imageSkBitmap.Width != imageSkBitmap.Height)
                        {
                            throw new SAPRESSC.Core.Web.Frontend.InvalidModelException("LogoFile", localizer["CuadrateImageRequiredMessage"]);
                        }

                        logo = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                        {
                            ContentType = modelInput.LogoFile.ContentType,
                            Length = modelInput.LogoFile.Length,
                            Width = imageSkBitmap.Width,
                            Height = imageSkBitmap.Height
                        };
                    }
                }
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PosterFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                {
                    using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                    {
                        if (imageSkBitmap.Width <= imageSkBitmap.Height)
                        {
                            throw new SAPRESSC.Core.Web.Frontend.InvalidModelException("PosterFile", localizer["LandscapeImageRequiredMessage"]);
                        }

                        poster = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                        {
                            ContentType = modelInput.PosterFile.ContentType,
                            Length = modelInput.PosterFile.Length,
                            Width = imageSkBitmap.Width,
                            Height = imageSkBitmap.Height
                        };
                    }
                }
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.LogoFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                entity.Logo = logo;

                entity.Logo.About = string.Format("Show Logo :{0}", entity.Logo.Id);

                imageFileManagerFacade.SaveImageFileContent(entity.Logo.Id, memoryStream.ToArray(), modelInput.LogoFile.ContentType);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PosterFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                entity.Poster = poster;

                entity.Poster.About = string.Format("Show Poster :{0}", entity.Logo.Id);

                imageFileManagerFacade.SaveImageFileContent(entity.Poster.Id, memoryStream.ToArray(), modelInput.PosterFile.ContentType);
            }
        }

        protected override void PreEdit(Show entity, ShowInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            SAPRESSC.Middleware.Media.Persistence.Entities.Image newLogo = null;
            SAPRESSC.Middleware.Media.Persistence.Entities.Image newPoster = null;

            if (modelInput.LogoFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.LogoFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            if (imageSkBitmap.Width != imageSkBitmap.Height)
                            {
                                throw new SAPRESSC.Core.Web.Frontend.InvalidModelException("LogoFile", localizer["CuadrateImageRequiredMessage"]);
                            }

                            newLogo = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.LogoFile.ContentType,
                                Length = modelInput.LogoFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height
                            };
                        }
                    }
                }
            }

            if (modelInput.PosterFile != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PosterFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                    {
                        using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                        {
                            if (imageSkBitmap.Width <= imageSkBitmap.Height)
                            {
                                throw new SAPRESSC.Core.Web.Frontend.InvalidModelException("PosterFile", localizer["LandscapeImageRequiredMessage"]);
                            }

                            newPoster = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.PosterFile.ContentType,
                                Length = modelInput.PosterFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height
                            };
                        }
                    }
                }
            }

            if (newLogo != null)
            {
                var oldLogo = entity.Logo;

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.LogoFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    entity.Logo = newLogo;

                    entity.Logo.About = string.Format("Show Logo :{0}", entity.Logo.Id);

                    imageFileManagerFacade.SaveImageFileContent(entity.Logo.Id, memoryStream.ToArray(), modelInput.LogoFile.ContentType);
                }

                imageFileManagerFacade.DeleteImageFile(oldLogo);

                imageRepository.Delete(oldLogo, User.Identity.Name);
            }

            if (newPoster != null)
            {
                var oldPoster = entity.Poster;

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    modelInput.PosterFile.CopyTo(memoryStream);

                    memoryStream.Position = 0;

                    entity.Poster = newPoster;

                    entity.Poster.About = string.Format("Show Poster :{0}", entity.Poster.Id);

                    imageFileManagerFacade.SaveImageFileContent(entity.Poster.Id, memoryStream.ToArray(), modelInput.PosterFile.ContentType);
                }

                imageFileManagerFacade.DeleteImageFile(oldPoster);

                imageRepository.Delete(oldPoster, User.Identity.Name);
            }
        }

        protected override void PostDelete(Show entity)
        {
            base.PostDelete(entity);

            if (entity.Logo == null) return;

            imageRepository.Delete(entity.Logo, User.Identity.Name);
            imageRepository.Delete(entity.Poster, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Logo);
            imageFileManagerFacade.DeleteImageFile(entity.Poster);
        }
    }
}

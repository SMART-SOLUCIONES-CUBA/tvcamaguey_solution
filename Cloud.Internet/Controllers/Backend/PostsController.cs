using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("communication")]
    public class PostsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Post, string, PostInputViewModel, PostDisplayViewModel>
    {
        private readonly IEntityRepository<Category, string> categoryRepository;
        private readonly IEntityRepository<Tag, string> tagRepository;
        private readonly IEntityRepository<Employ, System.Guid> employRepository;
        private readonly IEntityRepository<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository;
        private readonly SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManagerFacade;

        public PostsController(SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid> imageRepository, IEntityRepository<Employ, System.Guid> employRepository, IEntityRepository<Tag, string> tagRepository, IEntityRepository<Category, string> categoryRepository, IEntityRepository<Post, string> repository, Microsoft.Extensions.Localization.IStringLocalizer<PostsController> localizer, ILogger<PostsController> logger) : base(repository, localizer, logger)
        {
            this.categoryRepository = categoryRepository;
            this.tagRepository = tagRepository;
            this.employRepository = employRepository;
            this.imageRepository = imageRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostCreator)]
        public override IActionResult Create(PostInputViewModel modelInput)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("PosterFile");

                    modelInput.PosterFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostEditor)]
        public override IActionResult Edit(string key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostEditor)]
        public override IActionResult Edit(PostInputViewModel modelInput)
        {
            ModelState.ClearValidationState("PosterFile");
            ModelState.MarkFieldValid("PosterFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    modelInput.PosterFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(modelInput);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostRemover)]
        public override IActionResult Delete(string key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PostRemover)]
        public override IActionResult Delete(PostDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(PostInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableCategories(modelInput);

            PopulateModelInputForAvailableEmployees(modelInput);

            PopulateModelInputForAvailableTags(modelInput);
        }

        protected override void ConfigureEdit(Post entity, PostInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableCategories(modelInput);

            PopulateModelInputForAvailableEmployees(modelInput);

            PopulateModelInputForAvailableTags(modelInput);

            foreach (var item in modelInput.SelectableTags)
            {
                item.Selected = entity.TagBridges.Any(b => b.TagId == item.Id);
            }
        }

        protected override void ConfigurePostFailCreateValidation(PostInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailableCategories(modelInput);

            PopulateModelInputForAvailableEmployees(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(PostInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateModelInputForAvailableCategories(modelInput);

            PopulateModelInputForAvailableEmployees(modelInput);
        }

        protected override void PreCreate(Post entity, PostInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Category = categoryRepository.Get(modelInput.CategoryId);

            foreach (var item in modelInput.SelectableTags.Where(item => item.Selected))
            {
                entity.TagBridges.Add(new PostTagBridge
                {
                    Post = entity,
                    TagId = item.Id
                });
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
                            entity.Poster= new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                            {
                                ContentType = modelInput.PosterFile.ContentType,
                                Length = modelInput.PosterFile.Length,
                                Width = imageSkBitmap.Width,
                                Height = imageSkBitmap.Height,
                                About = string.Format("Poster Image for Post :{0}", entity.Id)
                            };

                            imageFileManagerFacade.SaveImageFileContent(entity.Poster.Id, memoryStream.ToArray(), modelInput.PosterFile.ContentType);
                        }
                    }
                }
            }
        }

        protected override void PreEdit(Post entity, PostInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.Category.Id != modelInput.CategoryId)
            {
                entity.Category = categoryRepository.Get(modelInput.CategoryId);
            }

            entity.TagBridges.Clear();

            foreach (var item in modelInput.SelectableTags.Where(item => item.Selected))
            {
                entity.TagBridges.Add(new PostTagBridge
                {
                    Post = entity,
                    TagId = item.Id
                });
            }

            if (modelInput.PosterFile == null)
            {
                if (modelInput.ResetPoster && entity.Poster != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.Poster);

                    imageRepository.Delete(entity.Poster, User.Identity.Name);
                }

                return;
            }

            if (entity.Poster != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.Poster);

                imageRepository.Delete(entity.Poster, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.PosterFile.CopyTo(memoryStream);

                memoryStream.Position = 0;

                using (var imageSkData = SkiaSharp.SKData.Create(memoryStream))
                {
                    using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                    {
                        entity.Poster = new SAPRESSC.Middleware.Media.Persistence.Entities.Image
                        {
                            ContentType = modelInput.PosterFile.ContentType,
                            Length = modelInput.PosterFile.Length,
                            Width = imageSkBitmap.Width,
                            Height = imageSkBitmap.Height,
                            About = string.Format("Poster Image for Post :{0}", entity.Id)
                        };

                        imageFileManagerFacade.SaveImageFileContent(entity.Poster.Id, memoryStream.ToArray(), modelInput.PosterFile.ContentType);
                    }
                }
            }
        }

        protected override void PostDelete(Post entity)
        {
            base.PostDelete(entity);

            if (entity.Poster == null) return;

            imageRepository.Delete(entity.Poster, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Poster);
        }

        private void PopulateModelInputForAvailableCategories(PostInputViewModel inputModel)
        {
            var entities = categoryRepository.GetAll().OrderByDescending(e => e.ParentId).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }
        }

        private void PopulateModelInputForAvailableEmployees(PostInputViewModel inputModel)
        {
            var entities = employRepository.GetAll().ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableEmploySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = string.Format("{0} {1}", entity.Identity.Name, entity.Identity.LastName) });
            }
        }
        private void PopulateModelInputForAvailableTags(PostInputViewModel modelInput)
        {
            var tags = tagRepository.GetAll().ToArray();

            foreach (var tag in tags)
            {
                modelInput.SelectableTags.Add(new SAPRESSC.Core.Common.Models.View.SelectionItemInputViewModel<string>
                {
                    Id = tag.Id,
                    Selected = false,
                    Label = tag.Name,
                });
            }
        }
    }
}

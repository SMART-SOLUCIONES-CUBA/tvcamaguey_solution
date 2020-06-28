using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("communication")]
    public class CategoriesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Category, string, CategoryInputViewModel, CategoryDisplayViewModel>
    {
        public CategoriesController(IEntityRepository<Category, string> repository, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer, ILogger<CategoriesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryCreator)]
        public override IActionResult Create(CategoryInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryEditor)]
        public override IActionResult Edit(string key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryEditor)]
        public override IActionResult Edit(CategoryInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryRemover)]
        public override IActionResult Delete(string key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.CategoryRemover)]
        public override IActionResult Delete(CategoryDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(CategoryInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        protected override void ConfigureEdit(Category entity, CategoryInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(CategoryInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(CategoryInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateModelInputForAvailableParents(modelInput);
        }

        private void PopulateModelInputForAvailableParents(CategoryInputViewModel inputModel)
        {
            inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = "Ninguna"
            });

            var entities = Repository.FindBy(e => e.Id != inputModel.Id).OrderBy(e => e.ParentId).ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableCategorySelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }
        }
    }
}

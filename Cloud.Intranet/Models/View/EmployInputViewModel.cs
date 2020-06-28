using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class EmployInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Employ, System.Guid>
    {
        public EmployInputViewModel()
        {
            BornDate = System.DateTime.Now.AddYears(-30);
            SelectableAdministrativeRoles = new System.Collections.Generic.List<SAPRESSC.Core.Common.Models.View.SelectionItemInputViewModel<int>>();
        }

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "BornDate")]
        public System.DateTime BornDate { get; set; }

        [Display(Name = "CivilStatus")]
        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Sex")]
        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "AdministrativeRoles")]
        public System.Collections.Generic.List<SAPRESSC.Core.Common.Models.View.SelectionItemInputViewModel<int>> SelectableAdministrativeRoles { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 1048576, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public bool ResetImage { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public Cloud.Persistence.Entities.Employ Export()
        {
            return new Cloud.Persistence.Entities.Employ();
        }

        public void Import(Cloud.Persistence.Entities.Employ entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            Alias = entity.Identity.Alias;
            BornDate = entity.Identity.BornDate;
            CivilStatus = entity.Identity.CivilStatus;
            Sex = entity.Identity.Sex;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId ?? System.Guid.Empty;

            if (entity.Identity.ImageId != null && entity.Identity.ImageId != System.Guid.Empty)
            {
                ImageExtension = entity.Identity.Image.ContentType.ContentTypeToExtension();
            }
        }

        public void Mergue(Cloud.Persistence.Entities.Employ entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.LastName = LastName;
            entity.Identity.Alias = Alias;
            entity.Identity.BornDate = BornDate;
            entity.Identity.CivilStatus = CivilStatus;
            entity.Identity.Sex = Sex;
            entity.Identity.About = About;
        }
    }
}

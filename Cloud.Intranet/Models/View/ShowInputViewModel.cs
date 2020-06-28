using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class ShowInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Show, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Name { get; set; }

        [Display(Name = "PosterFile")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        public Microsoft.AspNetCore.Http.IFormFile PosterFile { get; set; }

        public System.Guid PosterId { get; set; }

        public string PosterExtension { get; set; }

        [Display(Name = "LogoFile")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        public Microsoft.AspNetCore.Http.IFormFile LogoFile { get; set; }

        public System.Guid LogoId { get; set; }

        public string LogoExtension { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        public Cloud.Persistence.Entities.Show Export()
        {
            var entity = new Cloud.Persistence.Entities.Show();

            Mergue(entity);

            return entity;
        }

        public void Import(Cloud.Persistence.Entities.Show entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PosterId = entity.Poster.Id;
            PosterExtension = entity.Poster.ContentType.ContentTypeToExtension();
            LogoId = entity.Logo.Id;
            LogoExtension = entity.Logo.ContentType.ContentTypeToExtension();
            About = entity.About;
        }

        public void Mergue(Cloud.Persistence.Entities.Show entity)
        {
            entity.Name = Name;
            entity.About = About;
        }
    }
}

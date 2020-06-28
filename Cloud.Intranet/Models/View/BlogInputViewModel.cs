using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class BlogInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Blog, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage = "RegularExpressionValidationErrorMessage")]
        public string Url { get; set; }

        public Cloud.Persistence.Entities.Blog Export()
        {
            var entity = new Cloud.Persistence.Entities.Blog();

            Mergue(entity);

            return entity;
        }

        public void Import(Cloud.Persistence.Entities.Blog entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Url = entity.Url;
        }

        public void Mergue(Cloud.Persistence.Entities.Blog entity)
        {
            entity.Title = Title;
            entity.Url = Url;
        }
    }
}

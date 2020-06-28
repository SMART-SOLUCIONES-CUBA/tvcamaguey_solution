using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class AwardInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Award, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Title { get; set; }

        [Display(Name = "Edition")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Edition { get; set; }

        [Display(Name = "IsInstitutional")]
        public bool IsInstitutional { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public Cloud.Persistence.Entities.Award Export()
        {
            var entity = new Cloud.Persistence.Entities.Award();

            Mergue(entity);

            return entity;
        }

        public void Import(Cloud.Persistence.Entities.Award entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Edition = entity.Edition;
            IsInstitutional = entity.IsInstitutional;
            Description = entity.Description;
        }

        public void Mergue(Cloud.Persistence.Entities.Award entity)
        {
            entity.Title = Title;
            entity.Edition = Edition;
            entity.IsInstitutional = IsInstitutional;
            entity.Description = Description;
        }
    }
}

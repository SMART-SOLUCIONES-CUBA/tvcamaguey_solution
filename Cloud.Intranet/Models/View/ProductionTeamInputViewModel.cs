using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class ProductionTeamInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.ProductionTeam, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public Cloud.Persistence.Entities.ProductionTeam Export()
        {
            var entity = new Cloud.Persistence.Entities.ProductionTeam();

            Mergue(entity);

            return entity;
        }

        public void Import(Cloud.Persistence.Entities.ProductionTeam entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Cloud.Persistence.Entities.ProductionTeam entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}

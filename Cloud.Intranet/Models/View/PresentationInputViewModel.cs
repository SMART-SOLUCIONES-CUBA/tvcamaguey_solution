using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class PresentationInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Presentation, System.Guid>
    {
        public PresentationInputViewModel()
        {
            var reference = System.DateTime.Now;

            At = reference.AddDays(1).AddMinutes(reference.Minute * -1);
            AvailableShowSelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            AvailableProductionTeamSelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid Id { get; set; }

        [Display(Name = "DateTime")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.DateTime At { get; set; }

        [Display(Name = "Show")]
        public System.Guid ShowId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableShowSelectListItems { get; set; }

        [Display(Name = "ProductionTeam")]
        public System.Guid ProductionTeamId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableProductionTeamSelectListItems { get; set; }

        public Cloud.Persistence.Entities.Presentation Export()
        {
            var entity = new Cloud.Persistence.Entities.Presentation();

            Mergue(entity);

            return entity;
        }

        public void Import(Cloud.Persistence.Entities.Presentation entity)
        {
            Id = entity.Id;
            At = entity.At;
            ShowId = entity.Show.Id;
            ProductionTeamId = entity.ProductionTeam.Id;
        }

        public void Mergue(Cloud.Persistence.Entities.Presentation entity)
        {
            entity.At = At;
        }
    }
}

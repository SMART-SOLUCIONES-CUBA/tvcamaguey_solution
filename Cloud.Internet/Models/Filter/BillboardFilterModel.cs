using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.Filter
{
    public class BillboardFilterModel
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Star")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "End")]
        public System.DateTime EndDate { get; set; }
    }
}

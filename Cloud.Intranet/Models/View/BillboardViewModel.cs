namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class BillboardViewModel
    {
        public BillboardViewModel()
        {
            DailyBillboards = new System.Collections.Generic.List<DailyBillboardViewModel>();
        }

        public Filter.BillboardFilterModel Filter { get; set; }

        public System.Collections.Generic.List<DailyBillboardViewModel> DailyBillboards { get; set; }
    }
}

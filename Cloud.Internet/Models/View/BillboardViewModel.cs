namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
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

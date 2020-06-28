namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class AwardEmployBridgeDisplayViewModel
    {
        public System.Guid AwardId { get; set; }

        public AwardDisplayViewModel Award { get; set; }

        public System.Guid EmployId { get; set; }

        public EmployDisplayViewModel Employ { get; set; }
    }
}

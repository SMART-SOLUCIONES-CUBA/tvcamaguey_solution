namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class EmployAwardBridge
    {
        public System.Guid EmployId { get; set; }

        public Employ Employ { get; set; }

        public System.Guid AwardId { get; set; }

        public Award Award { get; set; }
    }
}

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class EmployAdministrationRoleBridge
    {
        public System.Guid EmployId { get; set; }

        public Employ Employ { get; set; }

        public Enumerations.AdministrativeRole AdministrativeRole { get; set; }
    }
}

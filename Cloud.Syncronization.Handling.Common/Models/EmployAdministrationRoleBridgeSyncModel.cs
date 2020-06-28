namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class EmployAdministrationRoleBridgeSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.EmployAdministrationRoleBridge>
    {
        public System.Guid EmployId { get; set; }

        public Persistence.Enumerations.AdministrativeRole AdministrativeRole { get; set; }

        public System.Func<Persistence.Entities.EmployAdministrationRoleBridge, bool> Finder => e => e.EmployId == EmployId && e.AdministrativeRole == AdministrativeRole;

        public Persistence.Entities.EmployAdministrationRoleBridge Export()
        {
            var entity = new Persistence.Entities.EmployAdministrationRoleBridge
            {
                EmployId = EmployId,
                AdministrativeRole = AdministrativeRole
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.EmployAdministrationRoleBridge entity)
        {
            EmployId = entity.EmployId;
            AdministrativeRole = entity.AdministrativeRole;
        }

        public void Mergue(Persistence.Entities.EmployAdministrationRoleBridge entity)
        {
        }
    }
}

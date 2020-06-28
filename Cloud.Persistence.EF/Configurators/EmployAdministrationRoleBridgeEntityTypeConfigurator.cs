namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Configurators
{
    public class EmployAdministrationRoleBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.EmployAdministrationRoleBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.EmployAdministrationRoleBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(b => new { b.EmployId, b.AdministrativeRole });

            entityTypeBuilder.HasOne(b => b.Employ).WithMany(e => e.AdministrationRoleBridges).HasForeignKey(b => b.EmployId);
        }
    }
}

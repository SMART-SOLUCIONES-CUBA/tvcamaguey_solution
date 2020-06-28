namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Configurators
{
    public class EmployAwardBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.EmployAwardBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.EmployAwardBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(b => new { b.EmployId, b.AwardId });

            entityTypeBuilder.HasOne(b => b.Employ).WithMany(e => e.AwardBridges).HasForeignKey(b => b.EmployId);
            entityTypeBuilder.HasOne(b => b.Award).WithMany(e => e.EmployBridges).HasForeignKey(b => b.AwardId);
        }
    }
}

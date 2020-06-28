namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Configurators
{
    public class ProductionTeamEmployBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ProductionTeamEmployBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ProductionTeamEmployBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(b => new { b.EmployId, b.ProductionTeamId, b.ProductionRole });

            entityTypeBuilder.HasOne(b => b.Employ).WithMany().HasForeignKey(b => b.EmployId);
            entityTypeBuilder.HasOne(b => b.ProductionTeam).WithMany(e => e.EmployBridges).HasForeignKey(b => b.ProductionTeamId);
        }
    }
}

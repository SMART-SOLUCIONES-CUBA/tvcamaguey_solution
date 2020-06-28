namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Configurators
{
    public class EmployBlogBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.EmployBlogBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.EmployBlogBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(b => new { b.EmployId, b.BlogId });

            entityTypeBuilder.HasOne(b => b.Employ).WithMany(e => e.BlogBridges).HasForeignKey(b => b.EmployId);
            entityTypeBuilder.HasOne(b => b.Blog).WithMany(e => e.EmployBridges).HasForeignKey(b => b.BlogId);
        }
    }
}

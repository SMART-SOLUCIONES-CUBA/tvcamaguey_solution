namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Configurators
{
    public class PostTagBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.PostTagBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PostTagBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(e => new { e.PostId, e.TagId });

            entityTypeBuilder.HasOne(b => b.Post).WithMany(e => e.TagBridges).HasForeignKey(b => b.PostId);
            entityTypeBuilder.HasOne(b => b.Tag).WithMany().HasForeignKey(b => b.TagId);
        }
    }
}

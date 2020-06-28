namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Configurators
{
    public class TerminalUserEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.TerminalUser>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.TerminalUser> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(e => e.Identity).WithMany().HasForeignKey(e => e.IdentityId);
        }
    }
}

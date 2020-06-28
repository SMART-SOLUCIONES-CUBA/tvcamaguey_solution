using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Configurators
{
    public class EmployEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.Employ>
    {
        protected override void ApplayRules(EntityTypeBuilder<Employ> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Identity).WithMany().HasForeignKey(entity => entity.IdentityId);
        }
    }
}

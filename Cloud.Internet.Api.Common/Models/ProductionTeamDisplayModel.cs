using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class ProductionTeamDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.ProductionTeam, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductionTeamMemberModel[] Members { get; set; }

        public void Import(Cloud.Persistence.Entities.ProductionTeam entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Members = entity.EmployBridges.Select((Cloud.Persistence.Entities.ProductionTeamEmployBridge b) =>
            {
                var model = new ProductionTeamMemberModel
                {
                    Employ = new EmployDisplayModel(),
                    ProductionRole = b.ProductionRole
                };

                model.Employ.Import(b.Employ);

                return model;
            }).ToArray();
        }
    }
}

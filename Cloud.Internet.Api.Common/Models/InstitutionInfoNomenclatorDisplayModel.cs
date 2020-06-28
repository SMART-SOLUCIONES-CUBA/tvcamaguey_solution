using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Enumerators;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class InstitutionInfoNomenclatorDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<InstitutionInfoNomenclator, InstitutionInfoKind>
    {
        public InstitutionInfoKind Id { get; set; }

        public string Value { get; set; }

        public void Import(InstitutionInfoNomenclator entity)
        {
            Id = entity.Id;
            Value = entity.Value;
        }
    }
}

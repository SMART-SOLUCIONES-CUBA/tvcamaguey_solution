using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Enumerators;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class InstitutionInfoNomenclatorInputModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<InstitutionInfoNomenclator, InstitutionInfoKind>
    {
        public InstitutionInfoKind Id { get; set; }

        public InstitutionInfoNomenclator Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(InstitutionInfoNomenclator entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(InstitutionInfoNomenclator entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}

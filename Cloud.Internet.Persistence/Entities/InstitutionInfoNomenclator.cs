namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities
{
    public class InstitutionInfoNomenclator : SAPRESSC.Core.Persistence.Entities.IEntity<Enumerators.InstitutionInfoKind>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Enumerators.InstitutionInfoKind Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Value { get; set; }
    }
}

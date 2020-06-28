namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class AbstractUpdateRecord : SAPRESSC.Core.Persistence.Entities.IEntity<long>
    {
        public AbstractUpdateRecord()
        {
            At = System.DateTime.Now;
        }

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public System.DateTime At { get; set; }
    }
}

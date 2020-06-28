namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class EmploySyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.Employ>
    {
        public System.Guid Id { get; set; }

        public System.Guid IndentityId { get; set; }

        public System.Func<Persistence.Entities.Employ, bool> Finder => e => e.Id == Id;

        public Persistence.Entities.Employ Export()
        {
            var entity = new Persistence.Entities.Employ
            {
                Id = Id,
                IdentityId = IndentityId
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Employ entity)
        {
            Id = entity.Id;
            IndentityId = entity.IdentityId;
        }

        public void Mergue(Persistence.Entities.Employ entity)
        {
        }
    }
}

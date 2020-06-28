namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class EmployBlogBridgeSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.EmployBlogBridge>
    {
        public System.Guid EmployId { get; set; }

        public System.Guid BlogId { get; set; }

        public System.Func<Persistence.Entities.EmployBlogBridge, bool> Finder => e => e.EmployId == EmployId && e.BlogId == BlogId;

        public Persistence.Entities.EmployBlogBridge Export()
        {
            var entity = new Persistence.Entities.EmployBlogBridge
            {
                EmployId = EmployId,
                BlogId = BlogId
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.EmployBlogBridge entity)
        {
            EmployId = entity.EmployId;
            BlogId = entity.BlogId;
        }

        public void Mergue(Persistence.Entities.EmployBlogBridge entity)
        {
        }
    }
}

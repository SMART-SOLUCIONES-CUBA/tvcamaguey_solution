namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class EmployBlogBridge
    {
        public System.Guid EmployId { get; set; }

        public Employ Employ { get; set; }

        public System.Guid BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}

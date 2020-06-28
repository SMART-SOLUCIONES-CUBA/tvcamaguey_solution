namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class BlogDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Blog, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int AuthorCount { get; set; }

        public void Import(Cloud.Persistence.Entities.Blog entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Url = entity.Url;
            AuthorCount = entity.EmployBridges.Count;
        }
    }
}

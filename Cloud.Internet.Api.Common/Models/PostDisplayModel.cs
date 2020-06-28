namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class PostDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Post, string>
    {
        public PostDisplayModel()
        {
            Tags = new System.Collections.Generic.List<TagDisplayModel>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public System.DateTime PostedAt { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public EmployDisplayModel Author { get; set; }

        public CategoryDisplayModel Category { get; set; }

        public ImageDisplayModel Poster { get; set; }

        public System.Collections.Generic.List<TagDisplayModel> Tags { get; set; }

        public void Import(Persistence.Entities.Post entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            PostedAt = entity.PostedAt;
            Summary = entity.Summary;
            Content = entity.Content;

            Author = new EmployDisplayModel();
            Category = new CategoryDisplayModel();

            Author.Import(entity.Author);
            Category.Import(entity.Category);

            if (entity.Poster != null)
            {
                Poster = new ImageDisplayModel();

                Poster.Import(entity.Poster);
            }
        }
    }
}

using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class PostDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Post, string>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public System.DateTime PostedAt { get; set; }

        public EmployDisplayViewModel Author { get; set; }

        public string CategoryName { get; set; }

        public System.Guid PosterId { get; set; }

        public string PosterExtension { get; set; }

        public void Import(Persistence.Entities.Post entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Summary = entity.Summary;
            Content = entity.Content;
            PostedAt = entity.PostedAt;
            Author = new EmployDisplayViewModel();
            CategoryName = entity.Category.Name;
            PosterId = entity.PosterId ?? System.Guid.Empty;

            if (PosterId != System.Guid.Empty)
            {
                PosterExtension = entity.Poster.ContentType.ContentTypeToExtension();
            }

            Author.Import(entity.Author);
        }
    }
}

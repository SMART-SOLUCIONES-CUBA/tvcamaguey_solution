namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities
{
    public class Post : SAPRESSC.Core.Persistence.Entities.IEntity<string>
    {
        public Post()
        {
            TagBridges = new System.Collections.Generic.List<PostTagBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Summary { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Content { get; set; }

        public System.DateTime PostedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AuthorId")]
        public Cloud.Persistence.Entities.Employ Author { get; set; }

        public System.Guid AuthorId { get; set; }

        public Category Category { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PosterId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Poster { get; set; }

        public System.Guid? PosterId { get; set; }

        public System.Collections.Generic.List<PostTagBridge> TagBridges { get; set; }
    }
}

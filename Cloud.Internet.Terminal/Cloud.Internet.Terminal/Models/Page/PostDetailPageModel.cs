namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class PostDetailPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private Cloud.Persistence.Entities.Employ author;
        private System.Collections.Generic.List<Internet.Persistence.Entities.Tag> tags;

        public string Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public System.DateTime PostedAt { get; set; }

        public System.Guid AuthorId { get; set; }

        public Internet.Persistence.Entities.Category Category { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Poster { get; set; }

        #region Lazy load properties

        public Cloud.Persistence.Entities.Employ Author { get => author; set => SetProperty(ref author, value); }

        public System.Collections.Generic.List<Internet.Persistence.Entities.Tag> Tags { get => tags; set => SetProperty(ref tags, value); }

        #endregion
    }
}

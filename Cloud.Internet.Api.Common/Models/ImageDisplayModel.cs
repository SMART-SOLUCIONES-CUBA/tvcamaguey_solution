namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class ImageDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreateAt { get; set; }
        
        public string ContentType { get; set; }
        
        public long Length { get; set; }
        
        public string About { get; set; }
        
        public int Height { get; set; }
        
        public int Width { get; set; }

        public void Import(SAPRESSC.Middleware.Media.Persistence.Entities.Image entity)
        {
            Id = entity.Id;
            CreateAt = entity.CreateAt;
            ContentType = entity.ContentType;
            Length = entity.Length;
            Width = entity.Width;
            Height = entity.Height;
            About = entity.About;
        }
    }
}

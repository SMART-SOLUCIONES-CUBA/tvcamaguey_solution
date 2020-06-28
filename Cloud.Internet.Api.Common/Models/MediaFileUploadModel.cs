namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class MediaFileUploadModel
    {
        public System.Guid Id { get; set; }

        public byte[] ContentData { get; set; }

        public string ContentType { get; set; }
    }
}

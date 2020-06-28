using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using System.IO;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Managers
{
    public class ThumbnailFileManager : SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IThumbnailFileManagerFacade
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public ThumbnailFileManager(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
        }

        public void DeleteThumbnailFile(Thumbnail thumbnail)
        {
            File.Delete(GetThumbnailAbsoluteFilePath(thumbnail));
        }

        public string GetThumbnailAbsoluteFilePath(Thumbnail thumbnail)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine("media", "thumbnails"));

            if (!Directory.Exists(targetDirectryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetDirectryInfo.PhysicalPath);
            }

            var extension = thumbnail.ContentType.Split('/')[1].ToLowerInvariant();

            return Path.Combine(targetDirectryInfo.PhysicalPath, string.Format("{0}.{1}", thumbnail.Id, extension));
        }

        public byte[] GetThumbnailFileContent(Thumbnail thumbnail)
        {
            byte[] result;

            using (var file = File.OpenRead(GetThumbnailAbsoluteFilePath(thumbnail)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetThumbnailRelativeFilePath(Thumbnail thumbnail)
        {
            return null;
        }

        public void SaveThumbnailFileContent(System.Guid thumbnailId, byte[] content, string contentType)
        {
            File.WriteAllBytes(GetThumbnailAbsoluteFilePath(new Thumbnail { Id = thumbnailId, ContentType = contentType }), content);
        }
    }
}

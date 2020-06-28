using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System;
using System.IO;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Managers
{
    public class ImageFileManager : SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public ImageFileManager(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.env = env;
        }

        public void DeleteImageFile(Image image)
        {
            File.Delete(GetImageAbsoluteFilePath(image));
        }

        public string GetImageAbsoluteFilePath(Image image)
        {
            var targetDirectryInfo = env.WebRootFileProvider.GetFileInfo(Path.Combine("media", "images"));

            if (!Directory.Exists(targetDirectryInfo.PhysicalPath))
            {
                Directory.CreateDirectory(targetDirectryInfo.PhysicalPath);
            }

            return Path.Combine(targetDirectryInfo.PhysicalPath, string.Format("{0}.{1}", image.Id, image.ContentType.ContentTypeToExtension()));
        }

        public byte[] GetImageFileContent(Image image)
        {
            byte[] result;

            using (var file = File.OpenRead(GetImageAbsoluteFilePath(image)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        public string GetImageRelativeFilePath(Image image)
        {
            return null;
        }

        public void SaveImageFileContent(Guid imageId, byte[] content, string contentType)
        {
            File.WriteAllBytes(GetImageAbsoluteFilePath(new Image { Id = imageId, ContentType = contentType }), content);
        }
    }
}

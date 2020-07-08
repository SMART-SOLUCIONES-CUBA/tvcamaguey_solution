namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions
{
    public static class StringExtensions
    {
        public static string ToImageExtension(this string source)
        {
            switch (source)
            {
                case "image/jpeg":
                    return ".jpg";

                default:
                    return string.Format(".{0}", source.Split('/')[1].ToLowerInvariant());
            }
        }

        public static string ToImageContentType(this string source)
        {
            var discriminator = source.Substring(1).ToLowerInvariant();

            switch (discriminator)
            {
                case "jpg":
                    return "image/jpeg";

                default:
                    return string.Format("image/{0}", discriminator);
            }
        }

        public static string ToStandarizedHtmlPostContent(this string target)
        {
            return string.Format("<html><head><style type=\"text/css\">html,body,p,span,a {{ color: #212529 !important; font-family: sans-serif !important; line-height: 1.15 !important; }}</style></head><body>{0}</body></html>", target);
        }
    }
}
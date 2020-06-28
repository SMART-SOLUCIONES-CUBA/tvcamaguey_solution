using System.Reflection;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Providers
{
    public sealed class ImageSourceProvider
    {
        private static ImageSourceProvider instance;

        public static ImageSourceProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageSourceProvider();
                }

                return instance;
            }
        }

        public Xamarin.Forms.ImageSource RandomOfficeImageSource
        {
            get
            {
                var randomizer = new System.Random();

                return Xamarin.Forms.ImageSource.FromResource(string.Format("SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Assets.Images.Office-{0}.jpg", randomizer.Next(1, 3)), typeof(App).GetTypeInfo().Assembly);
            }
        }

        public Xamarin.Forms.ImageSource RandomRegionImageSource
        {
            get
            {
                var randomizer = new System.Random();

                return Xamarin.Forms.ImageSource.FromResource(string.Format("SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Assets.Images.Region-{0}.jpg", randomizer.Next(1, 10)), typeof(App).GetTypeInfo().Assembly);
            }
        }

        public Xamarin.Forms.ImageSource GetImage(System.Guid id, string extension)
        {
            var filePathProvider = Xamarin.Forms.DependencyService.Get<Facade.IFilePathProviderFacade>();

            var imageFilePath = filePathProvider.GetImageFilePath(id, extension);

            return Xamarin.Forms.ImageSource.FromFile(imageFilePath);
        }

        public Xamarin.Forms.ImageSource GetUserImage(System.Guid id, string extension, int width, int height)
        {
            var filePathProvider = Xamarin.Forms.DependencyService.Get<Facade.IFilePathProviderFacade>();

            var imageFilePath = filePathProvider.GetImageFilePath(id, extension);

            if (!System.IO.File.Exists(imageFilePath))
            {
                return GetDefaultUserProfile(width, height);
            }

            return Xamarin.Forms.ImageSource.FromFile(imageFilePath);
        }

        private Xamarin.Forms.ImageSource GetDefaultUserProfile(int width, int height)
        {
            var imageFilePath = Xamarin.Forms.DependencyService.Get<Facade.IFilePathProviderFacade>().GetDefaultUserProfileImageFilePath(width, height);

            if (System.IO.File.Exists(imageFilePath))
            {
                return Xamarin.Forms.ImageSource.FromFile(imageFilePath);
            }

            using (var surface = SkiaSharp.SKSurface.Create(new SkiaSharp.SKImageInfo(width, height, SkiaSharp.SKImageInfo.PlatformColorType, SkiaSharp.SKAlphaType.Premul)))
            {
                surface.Canvas.Clear(SkiaSharp.SKColors.LightGray);

                using (var paint = new SkiaSharp.SKPaint())
                {
                    paint.Typeface = Xamarin.Forms.DependencyService.Get<Helpers.Facade.ISkiaSharpHelperFacade>().GetSKTypefaceByFontFileName("IconmonstrIconic.ttf");
                    paint.TextSize = width / 2;
                    paint.IsAntialias = true;
                    paint.TextAlign = SkiaSharp.SKTextAlign.Center;
                    paint.Color = SkiaSharp.SKColors.Silver;
                    paint.Style = SkiaSharp.SKPaintStyle.Fill;

                    surface.Canvas.DrawText("\uE08f", width / 2, height / 2 + (paint.TextSize / 2), paint);
                }

                using (var skImage = surface.Snapshot())
                {
                    using (var output = System.IO.File.OpenWrite(imageFilePath))
                    {
                        skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75).SaveTo(output);
                    }
                }
            }

            return Xamarin.Forms.ImageSource.FromFile(imageFilePath);
        }
    }
}

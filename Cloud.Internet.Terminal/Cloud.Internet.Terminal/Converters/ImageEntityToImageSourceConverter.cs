using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class ImageEntityToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is SAPRESSC.Middleware.Media.Persistence.Entities.Image)) return null;

            return Providers.ImageSourceProvider.Instance.GetImage((value as SAPRESSC.Middleware.Media.Persistence.Entities.Image).Id, (value as SAPRESSC.Middleware.Media.Persistence.Entities.Image).ContentType.ContentTypeToExtension());
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.InvalidOperationException();
        }
    }
}

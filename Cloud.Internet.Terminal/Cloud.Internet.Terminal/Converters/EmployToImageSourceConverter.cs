using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class EmployToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var employ = value as Cloud.Persistence.Entities.Employ;

            if (employ.Identity.Image == null)
            {
                return employ.Identity.Sex == SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex.Male ? Providers.ImageSourceProvider.Instance.ProfileMaleDefault() : Providers.ImageSourceProvider.Instance.ProfileFemaleDefault();
            }

            return Providers.ImageSourceProvider.Instance.GetImage(employ.Identity.Image.Id, employ.Identity.Image.ContentType.ToImageExtension());
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

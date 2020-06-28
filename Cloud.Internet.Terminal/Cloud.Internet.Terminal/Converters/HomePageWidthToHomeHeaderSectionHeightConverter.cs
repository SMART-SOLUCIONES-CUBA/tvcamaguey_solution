namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class HomePageWidthToHomeHeaderSectionHeightConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (Xamarin.Forms.Application.Current.MainPage == null)
            {
                return width;
            }

            if (width > (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail.Height)
            {
                return width / 2 / 1.333333333333333;
            }

            return width / 1.333333333333333;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

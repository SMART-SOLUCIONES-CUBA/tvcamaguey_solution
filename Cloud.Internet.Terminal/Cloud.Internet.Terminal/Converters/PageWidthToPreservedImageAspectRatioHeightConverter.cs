namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PageWidthToPreservedImageAspectRatioHeightConverter : Xamarin.Forms.IValueConverter
    {
        public Pages.Facade.IImageContextBindingPageFacade ImageContextBindingPage { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (ImageContextBindingPage.BindingContextImage == null) return null;

            return width * ImageContextBindingPage.BindingContextImage.Height / ImageContextBindingPage.BindingContextImage.Width;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

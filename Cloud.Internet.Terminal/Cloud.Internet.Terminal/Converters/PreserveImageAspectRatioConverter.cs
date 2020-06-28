namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PreserveImageAspectRatioConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = System.Convert.ToDouble(value);

            if (!(parameter is Xamarin.Forms.BindableObject)) return null;

            var bindingContext = (parameter as Xamarin.Forms.BindableObject).BindingContext;

            if (bindingContext == null) return null;

            var postViewModel = bindingContext as Models.View.BasePostViewModel;

            return System.Math.Round(width * postViewModel.Post.Poster.Height / postViewModel.Post.Poster.Width, System.MidpointRounding.ToEven);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

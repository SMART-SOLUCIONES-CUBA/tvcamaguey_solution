namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PageWidthToNoFirstUserProfileFormGroupAlignSelfConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width < 0) return 0;

            if (width > (Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth * 2 + 20 * 2 + 20))
            {
                return Xamarin.Forms.FlexAlignSelf.Center;
            }
            else
            {
                return Xamarin.Forms.FlexAlignSelf.Auto;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.InvalidOperationException();
        }
    }
}

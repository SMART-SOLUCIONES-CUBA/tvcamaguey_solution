namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class UserImageIdToOverviewImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is System.Guid)) return null;

            return Providers.ImageSourceProvider.Instance.GetUserImage(((parameter as Xamarin.Forms.Page).BindingContext as Models.IProfileImageModeler).ProfileImageId, ((parameter as Xamarin.Forms.Page).BindingContext as Models.IProfileImageModeler).ProfileImageExtension, Helpers.ConstantProvisionHelper.Instance.OverviewProfileImageSize, Helpers.ConstantProvisionHelper.Instance.OverviewProfileImageSize);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.InvalidOperationException();
        }
    }
}

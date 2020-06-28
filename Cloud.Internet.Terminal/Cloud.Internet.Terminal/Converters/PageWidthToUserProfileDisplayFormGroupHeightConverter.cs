namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PageWidthToUserProfileDisplayFormGroupHeightConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.ContentPage Page { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width < 0) return 0;

            var model = Page.BindingContext as Models.Page.UserProfilePageModel;

            if (model == null || model.IsBusy) return 0;

            var profileFormGroup = (Enumerations.UserProfileFormGroup)System.Enum.Parse(typeof(Enumerations.UserProfileFormGroup), (string)parameter);

            string text = null;

            switch (profileFormGroup)
            {
                case Enumerations.UserProfileFormGroup.Name:
                    text = model.Name;

                    break;

                case Enumerations.UserProfileFormGroup.LastName:
                    text = model.LastName ?? "-";

                    break;

                case Enumerations.UserProfileFormGroup.Alias:
                    text = model.Alias ?? "-";

                    break;

                case Enumerations.UserProfileFormGroup.BornDate:
                    var dateTimeToStringConverter = Page.Resources["DateTimeToString"] as XFToolKit.Presentation.Converters.DateTimeToStringConverter;

                    text = (string)dateTimeToStringConverter.Convert(model.BornDate, targetType, "dddd, MMMM dd, YY", culture);

                    break;

                case Enumerations.UserProfileFormGroup.CivilStatus:
                    text = model.CivilStatus.ToString();

                    break;

                case Enumerations.UserProfileFormGroup.Sex:
                    text = model.Sex.ToString();

                    break;
            }

            var sizeCalculationHelper = Xamarin.Forms.DependencyService.Get<Helpers.Facade.ISizeCalculationHelperFacade>();

            var labelHeigth = sizeCalculationHelper.CalculateHeight("Label", Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth, Xamarin.Forms.Font.Default);

            var textHeigth = sizeCalculationHelper.CalculateHeight(text, Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth, Xamarin.Forms.Font.Default);

            return labelHeigth + textHeigth + (double)Xamarin.Forms.StackLayout.SpacingProperty.DefaultValue;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.InvalidOperationException();
        }
    }
}

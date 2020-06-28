namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PageWidthToUserProfileDisplayFormGroupSetHeightConverter : Xamarin.Forms.IValueConverter
    {
        public Xamarin.Forms.ContentPage Page { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;

            if (width < 0) return 0;

            var model = Page.BindingContext as Models.Page.UserProfilePageModel;

            if (model == null || model.IsBusy) return 0;

            var profileFormGroupSet = (Enumerations.UserProfileFormGroupSet)System.Enum.Parse(typeof(Enumerations.UserProfileFormGroupSet), (string)parameter);

            string largeText = null;
            string smallText = "-";

            switch (profileFormGroupSet)
            {
                case Enumerations.UserProfileFormGroupSet.Name_LastName:
                    largeText = model.LastName == null || model.Name.Length > model.LastName.Length ? model.Name : model.LastName;

                    if (model.LastName == null) break;

                    smallText = model.Name.Length < model.LastName.Length ? model.Name : model.LastName;

                    break;

                case Enumerations.UserProfileFormGroupSet.BornDate_Alias:
                    var dateTimeToStringConverter = Page.Resources["DateTimeToString"] as XFToolKit.Presentation.Converters.DateTimeToStringConverter;

                    var dateTimeString = (string)dateTimeToStringConverter.Convert(model.BornDate, targetType, "dddd, MMMM dd, YY", culture);

                    largeText = model.Alias == null || string.IsNullOrEmpty(model.Alias) ? dateTimeString : model.Alias;

                    if (model.Alias == null) break;

                    smallText = !string.IsNullOrEmpty(model.Alias) ? dateTimeString : model.Alias;

                    break;

                case Enumerations.UserProfileFormGroupSet.Sex_CivilStatus:
                    largeText = model.CivilStatus.ToString().Length > model.Sex.ToString().Length ? model.CivilStatus.ToString() : model.Sex.ToString();
                    smallText = model.CivilStatus.ToString().Length < model.Sex.ToString().Length ? model.CivilStatus.ToString() : model.Sex.ToString();

                    break;
            }

            var sizeCalculationHelper = Xamarin.Forms.DependencyService.Get<Helpers.Facade.ISizeCalculationHelperFacade>();

            var labelHeigth = sizeCalculationHelper.CalculateHeight("Label", Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth, Xamarin.Forms.Font.Default);

            var largeTextHeigth = sizeCalculationHelper.CalculateHeight(largeText, Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth, Xamarin.Forms.Font.Default);

            var largeFormGroupRequestHeigth = labelHeigth + largeTextHeigth + (double)Xamarin.Forms.StackLayout.SpacingProperty.DefaultValue + 20;

            if (width > (Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth * 2 + 20 * 2 + 20))
            {
                return largeFormGroupRequestHeigth;
            }
            else
            {
                var smallTextHeigth = sizeCalculationHelper.CalculateHeight(smallText, Helpers.ConstantProvisionHelper.Instance.FormGroupRequestWidth, Xamarin.Forms.Font.Default);

                var smallFormGroupRequestHeigth = labelHeigth + smallTextHeigth + (double)Xamarin.Forms.StackLayout.SpacingProperty.DefaultValue + 20;

                return largeFormGroupRequestHeigth + smallFormGroupRequestHeigth;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.InvalidOperationException();
        }
    }
}

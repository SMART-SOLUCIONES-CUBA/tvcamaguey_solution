namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class DateTimeToInvarianCultureRelativeDayStringConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var datetime = (System.DateTime)value;

            var dayDiff = datetime.Date - System.DateTime.Today;

            if (dayDiff.Days == 0 || (dayDiff.Days < 1 && dayDiff.Days > 0) || (dayDiff.Days > -1 && dayDiff.Days < 0))
            {
                return "Hoy";
            }

            if (dayDiff.Days < 2 && dayDiff.Days > 0)
            {
                return "Mañana";
            }

            if (dayDiff.Days > -2 && dayDiff.Days < 0)
            {
                return "Ayer";
            }

            return datetime.ToString("MMMM dd");
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

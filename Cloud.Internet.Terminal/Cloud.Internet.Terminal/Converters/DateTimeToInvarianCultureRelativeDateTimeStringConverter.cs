namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class DateTimeToInvarianCultureRelativeDateTimeStringConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var datetime = (System.DateTime)value;

            var dayDiff = datetime.Date - System.DateTime.Today;

            if (dayDiff.Days == 0 || (dayDiff.Days < 1 && dayDiff.Days > 0) || (dayDiff.Days > -1 && dayDiff.Days < 0))
            {
                return string.Format("Hoy, {0}", datetime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
            }

            if (dayDiff.Days < 2 && dayDiff.Days > 0)
            {
                return string.Format("Mañana, {0}", datetime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
            }

            if (dayDiff.Days > -2 && dayDiff.Days < 0)
            {
                return string.Format("Ayer, {0}", datetime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
            }

            return string.Format("{0}, {1}", datetime.ToString("MMMM dd"), datetime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

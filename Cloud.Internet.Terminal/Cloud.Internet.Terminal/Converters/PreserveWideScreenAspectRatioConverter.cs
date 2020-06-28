namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PreserveWideScreenAspectRatioConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = System.Convert.ToDouble(value);

            if (width <= 0) return width;

            var flowListView = parameter as DLToolkit.Forms.Controls.FlowListView;

            var flowListViewDesiredColumnCount = (int)System.Math.Floor(width / flowListView.FlowColumnMinWidth);

            return System.Math.Round(width / flowListViewDesiredColumnCount / 1.777777777777778, System.MidpointRounding.ToEven);
        }

        public object ConvertBack(object rawValue, System.Type targetType, object rawParameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

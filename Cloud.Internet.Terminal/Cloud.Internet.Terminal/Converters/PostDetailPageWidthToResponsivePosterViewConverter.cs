namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Converters
{
    public class PostDetailPageWidthToResponsivePosterViewConverter : Xamarin.Forms.IValueConverter
    {
        public Pages.PostDetailPage Page { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var pageWidth = (double)value;

            if (pageWidth == -1) return value;

            if (Page.BindingContext == null) return null;

            var poster = (Page.BindingContext as Models.Page.PostDetailPageModel).Poster;

            if (poster == null) return null;

            var pageHeight = (Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Detail.Height;

            if (poster.Width < poster.Height)
            {
                var heightRequest = System.Math.Min(pageWidth, pageHeight);

                return new Views.Custom.VerticalPosterView(poster, heightRequest);
            }
            else
            {
                var heightRequest = pageWidth * poster.Height / poster.Width;

                return new Views.Custom.HorizontalPosterView(poster, heightRequest);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

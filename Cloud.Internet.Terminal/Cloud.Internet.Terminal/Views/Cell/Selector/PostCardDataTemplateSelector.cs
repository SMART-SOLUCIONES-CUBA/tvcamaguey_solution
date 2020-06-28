namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Cell.Selector
{
    public class PostCardDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public Xamarin.Forms.DataTemplate TextOnlyDataTemplate { get; set; }

        public Xamarin.Forms.DataTemplate HasPosterDataTemplate { get; set; }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, Xamarin.Forms.BindableObject container)
        {
            var postViewModel = item as Models.View.BasePostViewModel;

            if (postViewModel is Models.View.TextOnlyPostViewModel)
            {
                return TextOnlyDataTemplate;
            }

            return HasPosterDataTemplate;
        }
    }
}

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Cell.Selector
{
    public class PostFlowDataTemplateSelector : DLToolkit.Forms.Controls.FlowTemplateSelector
    {
        public Xamarin.Forms.DataTemplate TextOnlyDataTemplate { get; set; }

        public Xamarin.Forms.DataTemplate HorizontalPosterDataTemplate { get; set; }

        public Xamarin.Forms.DataTemplate VerticalPosterDataTemplate { get; set; }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, int columnIndex, Xamarin.Forms.BindableObject container)
        {
            var postViewModel = item as Models.View.BasePostViewModel;

            if (postViewModel is Models.View.TextOnlyPostViewModel)
            {
                return TextOnlyDataTemplate;
            }

            if (postViewModel is Models.View.HorizontalPosterPostViewModel)
            {
                return HorizontalPosterDataTemplate;
            }

            return VerticalPosterDataTemplate;
        }
    }
}

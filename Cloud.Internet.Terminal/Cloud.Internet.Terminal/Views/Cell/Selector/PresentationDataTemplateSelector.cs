namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Cell.Selector
{
    public class PresentationDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public Xamarin.Forms.DataTemplate DefaultDataTemplate { get; set; }

        public Xamarin.Forms.DataTemplate NullDataTemplate { get; set; }

        protected override Xamarin.Forms.DataTemplate OnSelectTemplate(object item, Xamarin.Forms.BindableObject container)
        {
            return item is Models.View.EmptyPresentationViewModel ? NullDataTemplate : DefaultDataTemplate;
        }
    }
}

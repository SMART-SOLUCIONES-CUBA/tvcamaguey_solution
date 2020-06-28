namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.View
{
    public abstract class BasePresentationViewModel
    {
        public BasePresentationViewModel()
        {
        }

        public BasePresentationViewModel(Cloud.Persistence.Entities.Presentation presentation)
        {
            Presentation = presentation;
        }

        public Cloud.Persistence.Entities.Presentation Presentation { get; private set; }
    }
}
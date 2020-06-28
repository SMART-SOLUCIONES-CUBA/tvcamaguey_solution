namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class PresentationInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Presentation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.Presentation Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(Cloud.Persistence.Entities.Presentation entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.Presentation entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}

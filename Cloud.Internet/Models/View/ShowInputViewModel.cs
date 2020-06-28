namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class ShowInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Show, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.Show Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(Cloud.Persistence.Entities.Show entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.Show entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}

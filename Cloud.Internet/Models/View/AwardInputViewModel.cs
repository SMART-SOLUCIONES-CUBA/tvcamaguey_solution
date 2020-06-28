namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class AwardInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Award, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.Award Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(Cloud.Persistence.Entities.Award entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.Award entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}

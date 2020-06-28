namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class EmployInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Employ, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.Employ Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(Cloud.Persistence.Entities.Employ entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.Employ entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}

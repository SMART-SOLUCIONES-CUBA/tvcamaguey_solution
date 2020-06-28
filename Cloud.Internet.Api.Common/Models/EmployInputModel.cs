namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class EmployInputModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Employ, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.Employ Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Cloud.Persistence.Entities.Employ entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.Employ entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

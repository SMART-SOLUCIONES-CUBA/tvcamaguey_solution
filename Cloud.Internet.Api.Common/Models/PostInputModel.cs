namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class PostInputModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.Post, string>
    {
        public string Id { get; set; }

        public Persistence.Entities.Post Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Persistence.Entities.Post entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Persistence.Entities.Post entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
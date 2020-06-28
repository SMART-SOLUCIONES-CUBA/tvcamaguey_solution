namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class BlogInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.Blog, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.Blog Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(Cloud.Persistence.Entities.Blog entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.Blog entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}

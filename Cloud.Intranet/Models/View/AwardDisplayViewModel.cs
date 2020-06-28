namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class AwardDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Award, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public string Edition { get; set; }

        public bool IsInstitutional { get; set; }

        public string Description { get; set; }

        public int AwardedCount { get; set; }

        public void Import(Cloud.Persistence.Entities.Award entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Edition = entity.Edition;
            IsInstitutional = entity.IsInstitutional;
            Description = entity.Description;
            AwardedCount = entity.EmployBridges.Count;
        }
    }
}

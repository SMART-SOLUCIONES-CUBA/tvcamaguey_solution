namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class PresentationDetailPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.Generic.List<Cloud.Persistence.Entities.Employ> conductionEmploies;
        private System.Collections.Generic.List<Cloud.Persistence.Entities.Employ> directionEmploies;
        private System.Collections.Generic.List<Cloud.Persistence.Entities.Employ> productionEmploies;

        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public Cloud.Persistence.Entities.Show Show { get; set; }

        public System.Collections.Generic.List<Cloud.Persistence.Entities.Employ> ConductionEmploies { get => conductionEmploies; set => SetProperty(ref conductionEmploies, value); }

        public System.Collections.Generic.List<Cloud.Persistence.Entities.Employ> DirectionEmploies { get => directionEmploies; set => SetProperty(ref directionEmploies, value); }

        public System.Collections.Generic.List<Cloud.Persistence.Entities.Employ> ProductionEmploies { get => productionEmploies; set => SetProperty(ref productionEmploies, value); }

        public System.Guid ProductionTeamId { get; set; }

        public string About { get; set; }
    }
}

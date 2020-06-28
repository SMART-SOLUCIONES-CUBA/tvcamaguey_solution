namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Service.Options
{
    public class SyncronizationHandlingOptions
    {
        public System.TimeSpan IterationDesiredElapseTime { get; set; }

        public int IterationRequestCount { get; set; }

        public string IntranetCloudServer { get; set; }

        public string InternetCloudServer { get; set; }
    }
}

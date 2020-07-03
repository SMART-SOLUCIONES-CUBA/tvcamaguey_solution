namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Options
{
    public class MailSendOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSSL { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string SubjectTemplate { get; set; }

        public string BodyTemplate { get; set; }
    }
}

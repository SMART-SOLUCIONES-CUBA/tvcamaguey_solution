namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Options
{
    public class SmtpOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public MailKit.Security.SecureSocketOptions Security { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string SubjectTemplate { get; set; }

        public string BodyTemplate { get; set; }
    }
}

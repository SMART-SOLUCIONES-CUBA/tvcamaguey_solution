namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities
{
    public class TerminalUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public TerminalUser()
        {
            Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity();
        }

        public SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }

        public System.Guid IdentityId { get; set; }
    }
}

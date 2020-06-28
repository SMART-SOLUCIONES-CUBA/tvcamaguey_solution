namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Managers
{
    public class UserProfileManager : SAPRESSC.Core.Web.Security.Managers.Facade.IUserProfileManagerFacade
    {
        public SAPRESSC.Core.Web.Security.Models.View.IUserProfileViewModel GetUserProfile(string username)
        {
            var model = new Models.View.UserProfileViewModel();

            return model;
        }

        public void UpdateUserProfile(string username, SAPRESSC.Core.Web.Security.Models.View.IUserProfileViewModel profileModel)
        {
        }
    }
}

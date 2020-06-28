namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Extensions
{
    public static class ManagementDashboardSectionExtensions
    {
        public static Microsoft.AspNetCore.Html.HtmlString ToIconographyHtmlString(this SAPRESSC.Core.Web.Backend.Models.View.ManagementDashboardSectionViewModel self)
        {
            if (self.Controller == "users") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-user\"></span>");

            if (self.Controller == "groups") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-users\"></span>");

            if (self.Controller == "traces") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-shoe-prints\"></span>");

            if (self.Controller == "employees") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-id-card\"></span>");

            if (self.Controller == "blogs") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-blog\"></span>");

            if (self.Controller == "awards") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-award\"></span>");

            if (self.Controller == "presentations") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-calendar-alt\"></span>");

            if (self.Controller == "productionteams") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-people-carry\"></span>");

            if (self.Controller == "shows") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-theater-masks\"></span>");

            if (self.Controller == "institutioninfo") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-info-circle\"></span>");

            if (self.Controller == "posts") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-newspaper\"></span>");

            if (self.Controller == "categories") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-bookmark\"></span>");

            if (self.Controller == "tags") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-tags\"></span>");

            return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"fas fa-certificate\"></span>");
        }
    }
}

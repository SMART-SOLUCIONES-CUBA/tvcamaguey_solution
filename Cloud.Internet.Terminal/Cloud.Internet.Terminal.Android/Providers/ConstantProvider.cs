[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Providers.ConstantProvider))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Providers
{
    public class ConstantProvider : Terminal.Providers.Facade.IConstantProviderFacade
    {
        public int FluidContainerWidth => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.fluid_container_width);

        public int RegularProfileImageSize => FluidContainerWidth / 3;

        public int OverviewProfileImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.overview_profile_image_size);

        public int FormGroupRequestWidth => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.form_group_request_width);

        public int FormGroupRequestHeight => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.form_group_request_height);

        public int BillboardListViewRowHeight => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.billboard_list_view_row_height);

        public int BillboardListViewItemAtHourFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.billboard_list_view_item_at_hour_font_size);

        public int BillboardListViewItemAtOthersFontSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.billboard_list_view_item_at_others_font_size);

        public int PostListViewItemTextOnlyTemplateHeight => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.post_list_view_item_textonly_template_height);

        public int PostListViewItemHorizontalPosterTemplateHeight => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.post_list_view_item_horizontal_template_height);

        public int PostListViewItemVerticalPosterTemplateHeight => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.post_list_view_item_vertical_template_height);

        public int EmployOverviewContainerSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.employ_overview_container_size);

        public int EmployOverviewImageSize => Android.App.Application.Context.Resources.GetInteger(Resource.Integer.employ_overview_image_size);
    }
}
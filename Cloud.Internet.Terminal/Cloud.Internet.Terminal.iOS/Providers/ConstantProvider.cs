[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS.Providers.ConstantProvider))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS.Providers
{
    public class ConstantProvider : Terminal.Providers.Facade.IConstantProviderFacade
    {
        public int FluidContainerWidth => 320;

        public int RegularProfileImageSize => FluidContainerWidth / 3;

        public int OverviewProfileImageSize => 72;

        public int FormGroupRequestWidth => 296;

        public int FormGroupRequestHeight => 60;

        public int BillboardListViewRowHeight => 216;

        public int BillboardListViewItemAtHourFontSize => 72;

        public int BillboardListViewItemAtOthersFontSize => 28;

        public int PostListViewItemTextOnlyTemplateHeight => 144;

        public int PostListViewItemHorizontalPosterTemplateHeight => 240;

        public int PostListViewItemVerticalPosterTemplateHeight => 216;

        public int EmployOverviewContainerSize => 100;

        public int EmployOverviewImageSize => 72;
    }
}

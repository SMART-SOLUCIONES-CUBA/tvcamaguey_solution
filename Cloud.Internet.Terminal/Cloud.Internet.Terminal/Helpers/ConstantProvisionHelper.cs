namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Helpers
{
    public sealed class ConstantProvisionHelper : Providers.Facade.IConstantProviderFacade
    {
        private const string UNSELECTABLE_ROW_FLOWLISTVIEW_STYLEID = "DE9F48B7-AADC-4952-9D71-D7965BADF68D";

        private static ConstantProvisionHelper instance;

        public static ConstantProvisionHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConstantProvisionHelper();
                }

                return instance;
            }
        }

        public string UnselectableRowFlowListViewStyleId => UNSELECTABLE_ROW_FLOWLISTVIEW_STYLEID;

        public int FluidContainerWidth => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().FluidContainerWidth;

        public int RegularProfileImageSize => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().RegularProfileImageSize;

        public int OverviewProfileImageSize => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().OverviewProfileImageSize;

        public int FormGroupRequestWidth => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().FormGroupRequestWidth;

        public int FormGroupRequestHeight => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().FormGroupRequestHeight;

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex[] Sex_All => (SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex[])System.Enum.GetValues(typeof(SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex));

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus[] CivilStatus_All => (SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus[])System.Enum.GetValues(typeof(SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus));

        public int BillboardListViewRowHeight => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().BillboardListViewRowHeight;

        public int BillboardListViewItemAtHourFontSize => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().BillboardListViewItemAtHourFontSize;

        public int BillboardListViewItemAtOthersFontSize => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().BillboardListViewItemAtOthersFontSize;

        public int PostListViewItemTextOnlyTemplateHeight => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().PostListViewItemTextOnlyTemplateHeight;

        public int PostListViewItemHorizontalPosterTemplateHeight => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().PostListViewItemHorizontalPosterTemplateHeight;

        public int PostListViewItemVerticalPosterTemplateHeight => Xamarin.Forms.DependencyService.Get<Providers.Facade.IConstantProviderFacade>().PostListViewItemVerticalPosterTemplateHeight;
    }
}

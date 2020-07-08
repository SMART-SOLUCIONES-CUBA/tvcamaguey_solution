namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Providers.Facade
{
    public interface IConstantProviderFacade
    {
        int FluidContainerWidth { get; }

        int RegularProfileImageSize { get; }

        int OverviewProfileImageSize { get; }

        int FormGroupRequestWidth { get; }

        int FormGroupRequestHeight { get; }

        int BillboardListViewRowHeight { get; }

        int BillboardListViewItemAtHourFontSize { get; }

        int BillboardListViewItemAtOthersFontSize { get; }

        int PostListViewItemTextOnlyTemplateHeight { get; }

        int PostListViewItemHorizontalPosterTemplateHeight { get; }

        int PostListViewItemVerticalPosterTemplateHeight { get; }

        int EmployOverviewContainerSize { get; }

        int EmployOverviewImageSize { get; }
    }
}

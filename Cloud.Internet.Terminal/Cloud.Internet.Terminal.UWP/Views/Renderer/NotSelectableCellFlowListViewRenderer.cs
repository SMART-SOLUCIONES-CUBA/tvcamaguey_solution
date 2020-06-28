[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(DLToolkit.Forms.Controls.FlowListView), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Views.Renderer.NotSelectableCellFlowListViewRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Views.Renderer
{
    public class NotSelectableCellFlowListViewRenderer : Xamarin.Forms.Platform.UWP.ListViewRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.UWP.ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void UpdateNativeControl()
        {
            if (List != null && Element.ClassId == Terminal.Helpers.ConstantProvisionHelper.Instance.UnselectableRowFlowListViewStyleId)
            {
                List.IsItemClickEnabled = false;
                List.SelectionMode = Windows.UI.Xaml.Controls.ListViewSelectionMode.None;
            }

            base.UpdateNativeControl();
        }
    }
}

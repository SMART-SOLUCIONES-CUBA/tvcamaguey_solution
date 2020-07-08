using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class BillboardHistoryPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.DateTime startAt;
        private System.DateTime endAt;

        public BillboardHistoryPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.BillboardHistoryPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            ListView_Refreshing(this, System.EventArgs.Empty);

            IsEnabled = true;
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs args)
        {
            ListView_Refreshing(this, System.EventArgs.Empty);
        }

        private void ListView_Refreshing(object sender, System.EventArgs args)
        {
            (BindingContext as Models.Page.BillboardHistoryPageModel).IsBusy = true;

            var stopwatch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task.Run(() =>
            {
                stopwatch.Start();

                if (startAt == default)
                {
                    startAt = System.DateTime.Now;
                }

                var presentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    endAt = startAt;

                    startAt = startAt.AddDays(-6);

                    presentations = dbContext.Presentations.Include(e => e.Show).ThenInclude(e => e.Poster).Where(e => e.At.Date >= startAt && e.At.Date <= endAt).OrderBy(e => e.At).ToList();
                }

                return presentations;
            }).ContinueWith(async (System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>> task) =>
            {
                stopwatch.Stop();

                var reasonableElapseTime = System.TimeSpan.FromSeconds(1);

                if (stopwatch.Elapsed < reasonableElapseTime)
                {
                    await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var dayPresentationsStack = new System.Collections.Generic.Stack<Models.View.DayPresentationsViewModel>(task.Result.ToDayPresentationsViewModels(startAt.Date, endAt.Date));

                    while (dayPresentationsStack.Count > 0)
                    {
                        var dayPresentation = dayPresentationsStack.Pop();

                        (BindingContext as Models.Page.BillboardHistoryPageModel).DayPresentations.Insert(0, dayPresentation);

                        if (dayPresentation.Date < startAt)
                        {
                            startAt = dayPresentation.Date.Date;
                        }

                        if (dayPresentation.Date > this.endAt)
                        {
                            endAt = dayPresentation.Date.Date;
                        }
                    }

                    (BindingContext as Models.Page.BillboardHistoryPageModel).IsBusy = false;
                });
            });
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs args)
        {
            var target = (args.SelectedItem as Models.View.BasePresentationViewModel).Presentation;

            if (target == null)
            {
                (sender as Xamarin.Forms.ListView).SelectedItem = null;

                return;
            }

            this.NavigationPushPageAsync(new PresentationDetailPage(target));
        }
    }
}

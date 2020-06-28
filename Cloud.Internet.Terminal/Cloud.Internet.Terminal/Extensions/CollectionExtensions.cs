using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions
{
    public static class CollectionExtensions
    {
        //public static System.Collections.Generic.List<Models.View.DayPresentationsViewModel> ToDayPresentationsViewModels(this System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> target)
        //{
        //    var dayPresentations = new System.Collections.Generic.List<Models.View.DayPresentationsViewModel>();

        //    Models.View.DayPresentationsViewModel currentDayPresentationsViewModel = null;

        //    foreach (var presentation in target)
        //    {
        //        if (currentDayPresentationsViewModel == null || currentDayPresentationsViewModel.Date.Day != presentation.At.Day)
        //        {
        //            if (currentDayPresentationsViewModel != null)
        //            {
        //                dayPresentations.Add(currentDayPresentationsViewModel);
        //            }

        //            currentDayPresentationsViewModel = new Models.View.DayPresentationsViewModel { Date = presentation.At.Date };
        //        }

        //        currentDayPresentationsViewModel.Add(presentation);
        //    }

        //    if (currentDayPresentationsViewModel != null)
        //    {
        //        dayPresentations.Add(currentDayPresentationsViewModel);
        //    }

        //    return dayPresentations;
        //}

        public static System.Collections.Generic.List<Models.View.BasePostViewModel> ToViewModels(this System.Collections.Generic.List<Internet.Persistence.Entities.Post> target)
        {
            var result = new System.Collections.Generic.List<Models.View.BasePostViewModel>();

            foreach (var post in target)
            {
                if (post.Poster == null)
                {
                    result.Add(new Models.View.TextOnlyPostViewModel { Post = post });

                    continue;
                }

                if (post.Poster.Width > post.Poster.Height)
                {
                    result.Add(new Models.View.HorizontalPosterPostViewModel { Post = post });

                    continue;
                }

                result.Add(new Models.View.VerticalPosterPostViewModel { Post = post });
            }

            return result;
        }

        public static System.Collections.Generic.List<Models.View.DayPresentationsViewModel> ToDayPresentationsViewModels(this System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> target, System.DateTime startAt, System.DateTime endAt)
        {
            var dayPresentations = new System.Collections.Generic.List<Models.View.DayPresentationsViewModel>();

            var currentAt = startAt;

            while (currentAt <= endAt)
            {
                var currentDayPresentationsViewModel = new Models.View.DayPresentationsViewModel { Date = currentAt };

                var presentations = target.Where(e => e.At.Date == currentAt).Select(e => new Models.View.FullPresentationViewModel(e)).ToArray();

                if (presentations.Count() <= 0)
                {
                    currentDayPresentationsViewModel.Add(new Models.View.EmptyPresentationViewModel());
                }
                else
                {
                    currentDayPresentationsViewModel.AddRange(presentations);
                }

                dayPresentations.Add(currentDayPresentationsViewModel);

                currentAt = currentAt.AddDays(1);
            }

            return dayPresentations;
        }
    }
}

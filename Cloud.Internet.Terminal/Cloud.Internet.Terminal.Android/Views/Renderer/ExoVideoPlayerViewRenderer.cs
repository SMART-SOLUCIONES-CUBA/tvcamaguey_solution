using Com.Google.Android.Exoplayer2.UI;
using Com.Google.Android.Exoplayer2.Upstream;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SmartSolucionesCuba.XFToolKit.Presentation.Views.Rendered.VideoPlayerView), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Views.Renderer.ExoVideoPlayerViewRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Views.Renderer
{
    public class ExoVideoPlayerViewRenderer : ViewRenderer<XFToolKit.Presentation.Views.Rendered.VideoPlayerView, PlayerView>
    {
        public ExoVideoPlayerViewRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<XFToolKit.Presentation.Views.Rendered.VideoPlayerView> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    var playerView = new PlayerView(Context)
                    {
                        Player = Com.Google.Android.Exoplayer2.ExoPlayerFactory.NewSimpleInstance(Context, new Com.Google.Android.Exoplayer2.Trackselection.DefaultTrackSelector()),
                        ControllerAutoShow = false,
                        UseController = false,
                        ControllerHideOnTouch = false
                    };

                    playerView.ResizeMode = AspectRatioFrameLayout.ResizeModeFill;

                    (playerView.Player as Com.Google.Android.Exoplayer2.SimpleExoPlayer).Volume = 0;

                    SetNativeControl(playerView);
                }

                SetShowControls();

                SetRepeat();

                SetAutoPlay();

                SetSource();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == XFToolKit.Presentation.Views.Rendered.VideoPlayerView.ShowControlsProperty.PropertyName)
            {
                SetShowControls();
            }
            else if (args.PropertyName == XFToolKit.Presentation.Views.Rendered.VideoPlayerView.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == XFToolKit.Presentation.Views.Rendered.VideoPlayerView.AutoPlayProperty.PropertyName)
            {
                SetAutoPlay();
            }
            else if (args.PropertyName == XFToolKit.Presentation.Views.Rendered.VideoPlayerView.RepeatProperty.PropertyName)
            {
                SetRepeat();
            }
        }

        private void SetShowControls()
        {
            if (Element.ShowControls)
            {
                Control.ShowController();
            }
            else
            {
                Control.HideController();
            }
        }

        private void SetSource()
        {
            var hasSetSource = false;

            var defaultHandler = new Android.OS.Handler();

            var mediaSourceFactory = new Com.Google.Android.Exoplayer2.Source.ExtractorMediaSource.Factory(new DefaultDataSourceFactory(Context, "ExoPlayer"));

            if (Element.Source is XFToolKit.Presentation.Sources.Video.UriVideoSource)
            {
                var uri = (Element.Source as XFToolKit.Presentation.Sources.Video.UriVideoSource).Uri;

                if (!string.IsNullOrWhiteSpace(uri))
                {
                    (Control.Player as Com.Google.Android.Exoplayer2.SimpleExoPlayer).Prepare(mediaSourceFactory.CreateMediaSource(Android.Net.Uri.Parse(uri)));

                    hasSetSource = true;
                }
            }
            else if (Element.Source is XFToolKit.Presentation.Sources.Video.FileVideoSource)
            {
                var filename = (Element.Source as XFToolKit.Presentation.Sources.Video.FileVideoSource).File;

                if (!string.IsNullOrWhiteSpace(filename))
                {
                    (Control.Player as Com.Google.Android.Exoplayer2.SimpleExoPlayer).Prepare(mediaSourceFactory.CreateMediaSource(Android.Net.Uri.Parse(filename)));

                    hasSetSource = true;
                }
            }
            else if (Element.Source is XFToolKit.Presentation.Sources.Video.ResourceVideoSource)
            {
                var path = (Element.Source as XFToolKit.Presentation.Sources.Video.ResourceVideoSource).Path;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    var uri = "file:///android_asset/videos/" + path;

                    (Control.Player as Com.Google.Android.Exoplayer2.SimpleExoPlayer).Prepare(mediaSourceFactory.CreateMediaSource(Android.Net.Uri.Parse(uri)));

                    hasSetSource = true;
                }
            }

            if (hasSetSource && Element.AutoPlay)
            {
                Control.Player.PlayWhenReady = true;
            }
        }

        private void SetRepeat()
        {
            Control.Player.RepeatMode = Element.Repeat ? Com.Google.Android.Exoplayer2.Player.RepeatModeAll : Com.Google.Android.Exoplayer2.Player.RepeatModeOff;
        }

        private void SetAutoPlay()
        {
            Control.Player.PlayWhenReady = Element.AutoPlay;
        }
    }
}
using SmartSolucionesCuba.XFToolKit.Presentation.Sources.Video;
using SmartSolucionesCuba.XFToolKit.Presentation.Views.Rendered;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(VideoPlayerView), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Views.Renderer.NativeVideoPlayerViewRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Views.Renderer
{
    public class NativeVideoPlayerViewRenderer : ViewRenderer<VideoPlayerView, Windows.UI.Xaml.Controls.MediaElement>
    {
        private bool disposed;

        public NativeVideoPlayerViewRenderer()
        {
            disposed = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayerView> eventArgs)
        {
            base.OnElementChanged(eventArgs);

            if (eventArgs.NewElement != null)
            {
                if (Control == null)
                {
                    var mediaElement = new Windows.UI.Xaml.Controls.MediaElement
                    {
                        Volume = 0,
                        Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill
                    };

                    SetNativeControl(mediaElement);
                }

                SetShowControls();

                SetAutoPlay();

                SetRepeat();

                SetSource();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || disposed || Element == null || (Element.AutoPlay && (Element as IVideoPlayerController).Status == VideoStatus.Paused)) return;

            if (e.PropertyName == VideoPlayerView.ShowControlsProperty.PropertyName)
            {
                SetShowControls();
            }
            else if (e.PropertyName == VideoPlayerView.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (e.PropertyName == VideoPlayerView.AutoPlayProperty.PropertyName)
            {
                SetAutoPlay();
            }
            else if (e.PropertyName == VideoPlayerView.RepeatProperty.PropertyName)
            {
                SetRepeat();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            disposed = true;
        }

        private void SetShowControls()
        {
            if (Control == null || disposed) return;

            Control.AreTransportControlsEnabled = Element.ShowControls;
        }

        private void SetRepeat()
        {
            if (Control == null || disposed) return;

            Control.IsLooping = Element.Repeat;
        }

        private async void SetSource()
        {
            if (Control == null || disposed) return;

            var hasSetSource = false;

            try
            {
                if (Element.Source is UriVideoSource)
                {
                    var uri = (Element.Source as UriVideoSource).Uri;

                    if (!string.IsNullOrWhiteSpace(uri))
                    {
                        Control.Source = new Uri(uri);

                        hasSetSource = true;
                    }
                }
                else if (Element.Source is FileVideoSource)
                {
                    var file = (Element.Source as FileVideoSource).File;

                    if (!string.IsNullOrWhiteSpace(file))
                    {
                        var storageFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(file);

                        var stream = await storageFile.OpenReadAsync();

                        Control.SetSource(stream, storageFile.ContentType);

                        hasSetSource = true;
                    }
                }
                else if (Element.Source is ResourceVideoSource)
                {
                    var path = (Element.Source as ResourceVideoSource).Path;
                    var assembly = (Element.Source as ResourceVideoSource).Assembly;

                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        if (assembly == null)
                        {
                            Control.Source = new Uri(string.Format("ms-appx:///{0}", path));
                        }
                        else
                        {
                            var format = Path.GetExtension(path)?.Substring(1);

                            Control.SetSource(assembly.GetManifestResourceStream(path).AsRandomAccessStream(), format);
                        }

                        hasSetSource = true;
                    }
                }
            }
            catch
            {
            }

            if (!hasSetSource)
            {
                (Element as IVideoPlayerController).Status = VideoStatus.Paused;
            }
        }

        private void SetAutoPlay()
        {
            if (Control == null || disposed) return;

            Control.AutoPlay = Element.AutoPlay;
        }
    }
}

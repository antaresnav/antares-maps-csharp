using AntaresMaps.Map;
using AntaresMaps.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPSample.Scenarios
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Scenario2 : Page
    {
        /**
        * The amount by which to scroll the camera. Note that this amount is in raw pixels, not dp
        * (density-independent pixels).
        */
        private const int ScrollByPx = 100;

        private const int RotateByAngle = 20;

        public static readonly CameraPosition Budapest = CameraPosition.Builder().Target(new LatLng(47.497913, 19.040236))
                    .Zoom(14.5f)
                    .Bearing(0)
                    .Build();

        public static readonly CameraPosition Parliament = CameraPosition.Builder().Target(new LatLng(47.507076, 19.045663))
                    .Zoom(16.5f)
                    .Bearing(260)
                    .Build();

        private PolylineOptions _currPolylineOptions;

        private bool _isCanceled = false;

        public Scenario2()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            mMap.CameraMoveStarted += MMap_CameraMoveStarted;
            mMap.CameraMove += MMap_CameraMove;
            mMap.CameraIdle += MMap_CameraIdle;
            mMap.CameraMoveCanceled += MMap_CameraMoveCanceled;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            mMap.CameraMoveStarted -= MMap_CameraMoveStarted;
            mMap.CameraMove -= MMap_CameraMove;
            mMap.CameraIdle -= MMap_CameraIdle;
            mMap.CameraMoveCanceled -= MMap_CameraMoveCanceled;
        }

        private void MMap_CameraMoveCanceled(object sender, EventArgs e)
        {
            // When the camera stops moving, add its target to the current path, and draw it on the map.
            if (_currPolylineOptions != null)
            {
                AddCameraTargetToPath();
                mMap.AddPolyline(_currPolylineOptions);
            }
            _isCanceled = true;  // Set to clear the map when dragging starts again.
            _currPolylineOptions = null;
        }

        private void MMap_CameraIdle(object sender, EventArgs e)
        {
            if (_currPolylineOptions != null)
            {
                AddCameraTargetToPath();
                mMap.AddPolyline(_currPolylineOptions);
            }
            _currPolylineOptions = null;
            _isCanceled = false; // Set to *not* clear the map when dragging starts again.
        }

        private void MMap_CameraMove(object sender, EventArgs e)
        {
            // When the camera is moving, add its target to the current path we'll draw on the map.
            if (_currPolylineOptions != null)
            {
                AddCameraTargetToPath();
            }
        }

        private void MMap_CameraMoveStarted(object sender, CameraMoveStartedEventArgs e)
        {
            if (!_isCanceled)
            {
                mMap.Clear();
            }

            _currPolylineOptions = new PolylineOptions();
            _currPolylineOptions.Width = 5;
            switch (e.Reason)
            {
                case CameraMoveStartedEventArgs.ReasonGesture:
                    _currPolylineOptions.Color = ColorToUInt(Colors.Blue);
                    break;
                case CameraMoveStartedEventArgs.ReasonApiAnimation:
                    _currPolylineOptions.Color = ColorToUInt(Colors.Red);
                    break;
                case CameraMoveStartedEventArgs.ReasonDeveloperAnimation:
                    _currPolylineOptions.Color = ColorToUInt(Colors.Green);
                    break;
            }
            AddCameraTargetToPath();
        }

        private void AddCameraTargetToPath()
        {
            LatLng target = mMap.CameraPosition.Target;
            _currPolylineOptions.Add(target);
        }

        private uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        private void ChangeCamera(ICameraUpdate update)
        {
            ChangeCamera(update, null);
        }

        /**
        * Change the camera position by moving or animating the camera depending on the state of the
        * animate toggle button.
        */
        private void ChangeCamera(ICameraUpdate update, Action<bool> callback)
        {
            if (animateCB.IsChecked == true)
            {
                if (customDurationCB.IsChecked == true)
                {
                    int duration = (int)customDurationBar.Value;
                    mMap.AnimateCamera(update, duration, callback);
                }
                else
                {
                    mMap.AnimateCamera(update, callback);
                }
            }
            else
            {
                mMap.MoveCamera(update);
            }
        }

        private void ScrollUp_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.ScrollBy(0, -ScrollByPx));
        }

        private void ScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.ScrollBy(-ScrollByPx, 0));
        }

        private void ScrollDown_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.ScrollBy(0, ScrollByPx));
        }

        private void ScrollRight_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.ScrollBy(ScrollByPx, 0));
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.ZoomIn());
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.ZoomOut());
        }

        private void RotateLeft_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.NewCameraPosition(CameraPosition.Builder()
                .Target(mMap.CameraPosition.Target)
                .Zoom(mMap.CameraPosition.Zoom)
                .Bearing(mMap.CameraPosition.Bearing - RotateByAngle)
                .Build()));
        }

        private void RotateRight_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.NewCameraPosition(CameraPosition.Builder()
                .Target(mMap.CameraPosition.Target)
                .Zoom(mMap.CameraPosition.Zoom)
                .Bearing(mMap.CameraPosition.Bearing + RotateByAngle)
                .Build()));
        }

        private void BudapestButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.NewCameraPosition(Budapest));
        }

        private void ParliamentButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera(CameraUpdateFactory.NewCameraPosition(Parliament));
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mMap.StopAnimation();
        }

        private void AnimateCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            customDurationCB.IsEnabled = true;
            customDurationBar.IsEnabled = customDurationCB.IsChecked == true;
        }

        private void AnimateCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            customDurationCB.IsEnabled = false;
            customDurationBar.IsEnabled = false;
        }

        private void CustomDurationCB_Checked(object sender, RoutedEventArgs e)
        {
            customDurationBar.IsEnabled = true;
        }

        private void CustomDurationCB_Unchecked(object sender, RoutedEventArgs e)
        {
            customDurationBar.IsEnabled = false;
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.SetStyleUrl("file://Styles/demoMapStyle.json");
            mMap.MinZoomLevel = 4;
        }
    }
}

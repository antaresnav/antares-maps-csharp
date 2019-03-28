using AntaresMaps.Map;
using AntaresMaps.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Scenario6 : Page
    {
        private const int RotateByAngle = 20;

        private List<Marker> colorMarkers, rainbowMarkers;

        public Scenario6()
        {
            this.InitializeComponent();
            colorMarkers = new List<Marker>();
            rainbowMarkers = new List<Marker>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mMap.MarkerClick += MMap_MarkerClick;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            mMap.MarkerClick -= MMap_MarkerClick;
        }

        private void AddMarkersToMap()
        {
            colorMarkers.Clear();
            rainbowMarkers.Clear();

            var m0 = mMap.AddMarker(new MarkerOptions()
            {
                Icon = BitmapDescriptorFactory.FromFile(@"Images/MapMarker_Flag1_Right_Azure.png"),
                Position = new LatLng(47.499626, 19.044500),
                AnchorU = 0.3f,
                AnchorV = 0f
            });
            m0.Tag = "Azure flag";
            colorMarkers.Add(m0);

            // Image from stream
            var stream = File.OpenRead(@"Images/MapMarker_Flag1_Right_Black.png");
            var m1 = mMap.AddMarker(new MarkerOptions()
            {
                Icon = BitmapDescriptorFactory.FromStream(stream),
                Position = new LatLng(47.499626, 19.046000),
                AnchorU = 0.3f,
                AnchorV = 0f
            });
            m1.Tag = "Black flag";
            colorMarkers.Add(m1);

            // Image from bytes
            var bytes = File.ReadAllBytes(@"Images/MapMarker_Flag1_Right_Chartreuse.png");
            var m2 = mMap.AddMarker(new MarkerOptions()
            {
                Icon = BitmapDescriptorFactory.FromBytes(bytes),
                Position = new LatLng(47.499626, 19.047500),
                AnchorU = 0.3f,
                AnchorV = 0f
            });
            m2.Tag = "Chartreuse flag";
            colorMarkers.Add(m2);

            var m3 = mMap.AddMarker(new MarkerOptions()
            {
                Icon = BitmapDescriptorFactory.FromFile(@"Images/MapMarker_Flag1_Right_Blue.png"),
                Position = new LatLng(47.499626, 19.049000),
                AnchorU = 0.3f,
                AnchorV = 0f
            });
            m3.Tag = "Blue flag";
            colorMarkers.Add(m3);

            var m4 = mMap.AddMarker(new MarkerOptions()
            {
                Icon = BitmapDescriptorFactory.FromFile(@"Images/MapMarker_Flag1_Right_Green.png"),
                Position = new LatLng(47.499626, 19.050500),
                AnchorU = 0.3f,
                AnchorV = 0f
            });
            m4.Tag = "Green flag";
            colorMarkers.Add(m4);

            float rotation = (float)rotationBar.Value;
            bool flat = mFlatBox.IsChecked == true;
            int numMarkersInRainbow = 12;
            for (int i = 0; i < numMarkersInRainbow; i++)
            {
                Marker marker = mMap.AddMarker(new MarkerOptions()
                {
                    Position = new LatLng(
                                47.499 + 0.003 * Math.Sin(i * Math.PI / (numMarkersInRainbow - 1)),
                                19.0475 - 0.005 * Math.Cos(i * Math.PI / (numMarkersInRainbow - 1))),
                    Icon = BitmapDescriptorFactory.DefaultMarker(i * 360 / numMarkersInRainbow),
                    Rotation = rotation,
                    Flat = flat
                });
                rainbowMarkers.Add(marker);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            mMap.Clear();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            mMap.Clear();
            AddMarkersToMap();
        }

        private void RotateLeft_Click(object sender, RoutedEventArgs e)
        {
            mMap.MoveCamera(CameraUpdateFactory.BearingTo(mMap.CameraPosition.Bearing - RotateByAngle));
        }

        private void RotateRight_Click(object sender, RoutedEventArgs e)
        {
            mMap.MoveCamera(CameraUpdateFactory.BearingTo(mMap.CameraPosition.Bearing + RotateByAngle));
        }

        private void RotationBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            float rotation = (float)rotationBar.Value;
            foreach (Marker marker in rainbowMarkers)
            {
                marker.Rotation = rotation;
            }
        }

        private void MFlatBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Marker marker in rainbowMarkers)
            {
                marker.Flat = true;
            }
        }

        private void MFlatBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Marker marker in rainbowMarkers)
            {
                marker.Flat = false;
            }
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.SetStyleUrl("file://Styles/demoMapStyle.json");
            mMap.MinZoomLevel = 4;
            mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(47.500291, 19.047670), 16.0));
            AddMarkersToMap();
        }

        private void MMap_MarkerClick(object sender, AntaresMaps.MarkerClickEventArgs e)
        {
            if (rainbowMarkers.Contains(e.Marker))
            {
                float zIndex = e.Marker.ZIndex + 1.0f;
                e.Marker.ZIndex = zIndex;
                markerLabel.Text = "z-index set to " + zIndex;
            }

            foreach (var marker in colorMarkers)
            {
                if (e.Marker.Equals(marker))
                {
                    marker.Alpha = 0.5f;
                    markerLabel.Text = e.Marker.Tag + " clicked";
                }
                else
                {
                    marker.Alpha = 1f;
                }
            }
        }
    }
}

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
    public sealed partial class Scenario5 : Page
    {
        private Polyline mMutablePolyline;

        public Scenario5()
        {
            this.InitializeComponent();
        }

        private uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);
            value = value * 255;
            byte v = Convert.ToByte(value);
            byte p = Convert.ToByte(value * (1 - saturation));
            byte q = Convert.ToByte(value * (1 - f * saturation));
            byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));
            if (hi == 0) return Color.FromArgb(255, v, t, p);
            else if (hi == 1) return Color.FromArgb(255, q, v, p);
            else if (hi == 2) return Color.FromArgb(255, p, v, t);
            else if (hi == 3) return Color.FromArgb(255, p, q, v);
            else if (hi == 4) return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        private void HueBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            mMutablePolyline.Color = ColorToUInt(ColorFromHSV(hueBar.Value, 1, 1));
        }

        private void AlphaBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (mMutablePolyline != null)
            {
                mMutablePolyline.Alpha = (float)alphaBar.Value / 255F;
            }
        }

        private void WidthBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (mMutablePolyline != null)
            {
                mMutablePolyline.Width = (float)widthBar.Value;
            }
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.SetStyleUrl("file://Styles/demoMapStyle.json");
            mMap.MinZoomLevel = 4;
            var polylineOptions = new PolylineOptions()
            {
                Width = (float)widthBar.Value,
                Color = ColorToUInt(ColorFromHSV(hueBar.Value, 1, 1))
            }.Add(new LatLng(47.504441, 19.045179), new LatLng(47.500440, 19.039860), new LatLng(47.498874, 19.052728), new LatLng(47.495917, 19.057619));

            mMutablePolyline = mMap.AddPolyline(polylineOptions);

            // Move the map so that it is centered on the mutable polyline.
            mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(47.500440, 19.049179), 15));
        }
    }
}

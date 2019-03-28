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
    public sealed partial class Scenario8 : Page
    {
        public Scenario8()
        {
            this.InitializeComponent();
        }

        private void BuildingCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("building").Visible = true;
            mMap.GetLayer("building-top").Visible = true;
            mMap.Invalidate();
        }

        private void BuildingCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("building").Visible = false;
            mMap.GetLayer("building-top").Visible = false;
            mMap.Invalidate();
        }

        private void RailwayCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("railway").Visible = true;
            mMap.GetLayer("railway-hatching").Visible = true;
            mMap.GetLayer("railway-transit").Visible = true;
            mMap.GetLayer("railway-transit-hatching").Visible = true;
            mMap.GetLayer("railway-service").Visible = true;
            mMap.GetLayer("railway-service-hatching").Visible = true;
            mMap.Invalidate();
        }

        private void RailwayCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("railway").Visible = false;
            mMap.GetLayer("railway-hatching").Visible = false;
            mMap.GetLayer("railway-transit").Visible = false;
            mMap.GetLayer("railway-transit-hatching").Visible = false;
            mMap.GetLayer("railway-service").Visible = false;
            mMap.GetLayer("railway-service-hatching").Visible = false;
            mMap.Invalidate();
        }

        private void StreetCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("highway-minor").Visible = true;
            mMap.GetLayer("highway-minor-casing").Visible = true;
            mMap.Invalidate();
        }

        private void StreetCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("highway-minor").Visible = false;
            mMap.GetLayer("highway-minor-casing").Visible = false;
            mMap.Invalidate();
        }

        private void StreetNameCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("highway-name-minor").Visible = true;
            mMap.Invalidate();
        }

        private void StreetNameCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.GetLayer("highway-name-minor").Visible = false;
            mMap.Invalidate();
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.MinZoomLevel = 4;
            mMap?.SetStyleUrl("file://Styles/demoMapStyle.json");
        }
    }
}

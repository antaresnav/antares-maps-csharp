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
    public sealed partial class Scenario4 : Page
    {
        public Scenario4()
        {
            this.InitializeComponent();
        }

        private void ScrollCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.UiSettings.ScrollGesturesEnabled = true;
        }

        private void ScrollCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.UiSettings.ScrollGesturesEnabled = false;
        }

        private void CompassCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.UiSettings.SetCompassEnabled(true);
        }

        private void CompassCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.UiSettings.SetCompassEnabled(false);
        }

        private void ZoomCB_Checked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.UiSettings.ZoomGesturesEnabled = true;
        }

        private void ZoomCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mMap == null) return;
            mMap.UiSettings.ZoomGesturesEnabled = false;
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.SetStyleUrl("file://Styles/demoMapStyle.json");
            mMap.UiSettings.SetCompassFadeFacingNorth(false);
            mMap.MinZoomLevel = 4;
        }
    }
}

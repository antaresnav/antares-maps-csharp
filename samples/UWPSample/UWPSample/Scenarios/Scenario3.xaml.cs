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
    public sealed partial class Scenario3 : Page
    {
        public Scenario3()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            mMap.MapClick += MMap_MapClick;
            mMap.MapLongClick += MMap_MapLongClick;
            mMap.CameraIdle += MMap_CameraIdle;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            mMap.MapClick -= MMap_MapClick;
            mMap.MapLongClick -= MMap_MapLongClick;
            mMap.CameraIdle -= MMap_CameraIdle;
        }

        private void MMap_MapLongClick(object sender, AntaresMaps.MapClickEventArgs e)
        {
            tapLabel.Text = "long clicked, point=" + e.Point.ToString();
        }

        private void MMap_MapClick(object sender, AntaresMaps.MapClickEventArgs e)
        {
            tapLabel.Text = "clicked, point=" + e.Point.ToString();
        }

        private void MMap_CameraIdle(object sender, EventArgs e)
        {
            cameraLabel.Text = mMap.CameraPosition.ToString();
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.SetStyleUrl("file://Styles/demoMapStyle.json");
            mMap.MinZoomLevel = 4;
        }
    }
}

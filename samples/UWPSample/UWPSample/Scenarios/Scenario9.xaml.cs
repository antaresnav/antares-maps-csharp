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
    public sealed partial class Scenario9 : Page
    {
        public Scenario9()
        {
            this.InitializeComponent();
        }

        private void MMap1_MapReady(object sender, EventArgs e)
        {
            mMap1.MinZoomLevel = 4;
            mMap1.SetStyleUrl("file://Styles/demoMapStyle.json");
        }

        private void MMap2_MapReady(object sender, EventArgs e)
        {
            mMap2.MinZoomLevel = 4;
            mMap2.SetStyleUrl("https://www.dropbox.com/s/ljgd3z7unf6vrus/here_hybrid.json?raw=1");
        }
    }
}

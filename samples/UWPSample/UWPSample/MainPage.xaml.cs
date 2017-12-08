using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar.GetForCurrentView().HideAsync();
            }

            MapView.MaxZoomLevel = 18;
            MapView.MinZoomLevel = 4;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            ComboBoxItem value = comboBox?.SelectedItem as ComboBoxItem;
            switch (value?.Name)
            {
                case "OsmBright":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/osm_bright.json");
                    break;
                case "OsmBasic":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/osm_basic.json");
                    break;
                case "HereHybrid":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/here_hybrid.json");
                    break;
                case "OfflineMap":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/basic_default_style_offline.json");
                    break;
            }
        }
    }
}

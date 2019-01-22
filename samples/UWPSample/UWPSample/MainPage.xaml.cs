using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Popups;
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

        private async void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            ComboBoxItem value = comboBox?.SelectedItem as ComboBoxItem;
            switch (value?.Name)
            {
                case "Dsm":
                    MapView.SetStyleUrl("https://terkep.geox.hu/rutin/rutin_dsm10_style_dev_all.json");
                    break;
                case "OsmBright":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/osm_bright.json");
                    break;
                case "OsmBasic":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/osm_basic.json");
                    break;
                case "Openmaptiles":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/openmaptiles.json");
                    break;
                case "HereHybrid":
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/here_hybrid.json");
                    break;
                case "OfflineMap":
                 /*   using (var stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream("UWPSample.basic_default_style_offline.json"))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var result = await reader.ReadToEndAsync();
                            MapView.SetStyleJson(result);
                        }
                    }*/
                    MapView.SetStyleUrl("https://raw.githubusercontent.com/antaresnav/antares-maps-sharp/develop/styles/basic_default_style_offline.json");
                    break;
                case "Custom":
                    string text = await InputTextDialogAsync("Style URL");
                    MapView.SetStyleUrl(text, async (url,ex) =>
                    {
                        if(ex != null)
                        {
                            var dialog = new MessageDialog(ex.Message, "Error");
                            await dialog.ShowAsync();
                        }
                    });
                    break;
            }
        }

        private async Task<string> InputTextDialogAsync(string title)
        {
            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = title;
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Ok";
            dialog.SecondaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }

        private void ReloadButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MapView.ReloadStyleUrl();
        }
    }
}

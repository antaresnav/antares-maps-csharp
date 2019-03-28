using AntaresMaps.Map;
using AntaresMaps.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class Scenario7 : Page
    {
        public Scenario7()
        {
            this.InitializeComponent();
        }

        private async void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            ComboBoxItem value = comboBox?.SelectedItem as ComboBoxItem;
            switch (value?.Name)
            {
                case "Default":
                    mMap?.SetStyleUrl("file://Styles/demoMapStyle.json");
                    break;
                case "OsmBright":
                    mMap?.SetStyleUrl("https://www.dropbox.com/s/x6442cpylkyhn8s/osm_bright.json?raw=1", (s, ex) => {
                        if (ex == null)
                        {
                            ShowMessageDialogAsync("OSM bright style loaded");
                        }
                        else
                        {
                            ShowMessageDialogAsync("Error: " + ex.Message);
                        }
                    });
                    break;
                case "OsmBasic":
                    mMap?.SetStyleUrl("https://www.dropbox.com/s/dal559hedgi0eyd/osm_basic.json?raw=1", (s, ex) => {
                        if (ex == null)
                        {
                            ShowMessageDialogAsync("OSM basic style loaded");
                        }
                        else
                        {
                            ShowMessageDialogAsync("Error: " + ex.Message);
                        }
                    });
                    break;
                case "HereHybrid":
                    mMap?.SetStyleUrl("https://www.dropbox.com/s/ljgd3z7unf6vrus/here_hybrid.json?raw=1", (s, ex) => {
                        if (ex == null)
                        {
                            ShowMessageDialogAsync("HERE hybrid style loaded");
                        }
                        else
                        {
                            ShowMessageDialogAsync("Error: " + ex.Message);
                        }
                    });
                    break;
                case "OfflineDebrecen":
                    mMap?.SetStyleUrl("file://Styles/map-style.json", (s, ex) => {
                        if (ex == null)
                        {
                            mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(47.530815, 21.622319), 14.0));
                            ShowMessageDialogAsync("Offline style loaded");
                        }
                        else
                        {
                            ShowMessageDialogAsync("Error: " + ex.Message);
                        }
                    });
                    break;
                case "Custom":
                    string text = await InputTextDialogAsync("Style URL");
                    mMap?.SetStyleUrl(text, async (url, ex) =>
                    {
                        if (ex != null)
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

        private async void ShowMessageDialogAsync(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        private void MMap_MapReady(object sender, EventArgs e)
        {
            mMap.MinZoomLevel = 4;
            mMap?.SetStyleUrl("file://Styles/demoMapStyle.json");
        }
    }
}

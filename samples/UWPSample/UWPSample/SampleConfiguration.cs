using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSample.Scenarios;
using Windows.UI.Xaml.Controls;

namespace UWPSample
{
    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "AntaresMaps SDK";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title="Basic map", ClassType=typeof(Scenario1)},
            new Scenario() { Title="Camera", ClassType=typeof(Scenario2)},
            new Scenario() { Title="Events", ClassType=typeof(Scenario3)},
            new Scenario() { Title="UISettings", ClassType=typeof(Scenario4)},
            new Scenario() { Title="Polylines", ClassType=typeof(Scenario5)},
            new Scenario() { Title="Markers", ClassType=typeof(Scenario6)},
            new Scenario() { Title="Map styles", ClassType=typeof(Scenario7)},
            new Scenario() { Title="Map layers", ClassType=typeof(Scenario8)},
            new Scenario() { Title="Multiple Maps", ClassType=typeof(Scenario9)},
        };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}

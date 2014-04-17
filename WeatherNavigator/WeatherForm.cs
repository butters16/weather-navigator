using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherNavigator
{
    public partial class WeatherForm : Form
    {
        private readonly String urlString = "http://www.weather.com/";
        private readonly String zipString = "32701";

        public WeatherForm()
        {
            InitializeComponent();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(urlString);
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() == urlString)
            {
                ProcessFirstPage();
            }
            else if (e.Url.ToString().Contains(urlString))
            {
                ProcessSecondPage();
            }
            // Ignore all the other documents loads: ads, javascript, etc.
        }

        private void ProcessFirstPage()
        {
            //*[@id="typeaheadBox"]
            webBrowser.Document.GetElementById("typeaheadBox").SetAttribute("value", zipString);

            // //*[@id="headerSearchForm"]
            webBrowser.Document.GetElementById("headerSearchForm").InvokeMember("submit");
        }

        private void ProcessSecondPage()
        {
            Weather todayWeather = DocumentParser.ParseToday(webBrowser.Document);
            Weather tonightWeather = DocumentParser.ParseTonight(webBrowser.Document);

            StringBuilder builder = new StringBuilder();
            AppendWeather(todayWeather, "TODAY", builder);
            builder.Append(Environment.NewLine);
            AppendWeather(tonightWeather, "TONIGHT", builder);
            MessageBox.Show(builder.ToString(), "Weather for " + zipString);
        }

        private void AppendWeather(Weather weather, string day, StringBuilder builder)
        {
            builder.Append(day).Append(Environment.NewLine);
            builder.Append("=====").Append(Environment.NewLine);
            builder.Append("Temperature: ").Append(weather.temperature).Append(Environment.NewLine);
            builder.Append("Wind: ").Append(weather.wind).Append(Environment.NewLine);
            builder.Append("Humidity: ").Append(weather.humidity).Append(Environment.NewLine);
        }
    }
}

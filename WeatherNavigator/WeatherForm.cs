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
        public WeatherForm()
        {
            InitializeComponent();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate("http://www.weather.com");
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() == "http://www.weather.com/")
            {
                //*[@id="typeaheadBox"]
                webBrowser.Document.GetElementById("typeaheadBox").SetAttribute("Value", "32701");

                // //*[@id="headerSearchForm"]
                webBrowser.Document.GetElementById("headerSearchForm").InvokeMember("submit");
                //webBrowser.Navigate("http://www.weather.com/weather/today/32701");
            }
            else if (e.Url.ToString().Contains("http://www.weather.com/"))
            {
                var top = webBrowser.Document.GetElementById("wx-forecast-container");

                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[7]/div[1]/span[1] (ex: <span itemprop="temperature-fahrenheit">58</span>)
                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[8]/div[1] (ex: <div class="wx-temperature">75<span class="wx-degrees">°</span></div>)
                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[9]/div[1] (ex: <div class="wx-temperature">51<span class="wx-degrees">°</span></div>)
                var temp_today = top.Div(1).Div(2).Div(8).Div(1).InnerText;
                var temp_tonight = top.Div(1).Div(2).Div(9).Div(1).InnerText;

                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[1]/div/div/span (ex: <span class="wx-temp">Calm</span>)
                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[2]/div/div[2] (ex: <div class="wx-wind-label">E at 6 mph</div>)
                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[3]/div/div[2] (ex: <div class="wx-wind-label">NE at 7 mph</div>)
                var wind_today = top.Div(1).Div(2).Div(16).Div(2).Div(1).Div(2).InnerText;
                var wind_tonight = top.Div(1).Div(2).Div(16).Div(3).Div(1).Div(2).InnerText;

                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[4]/div (ex: <div class="wx-data">100%</div>)
                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[5]/div (ex: <div class="wx-data">59%</div>)
                //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[6]/div (ex: <div class="wx-data">88%</div>)
                var humid_today = top.Div(1).Div(2).Div(16).Div(5).Div(1).InnerText;
                var humid_tonight = top.Div(1).Div(2).Div(16).Div(6).Div(1).InnerText;

                StringBuilder builder = new StringBuilder();
                builder.Append("Today Temperature: ").Append(temp_today).Append(Environment.NewLine); 
                builder.Append("Today Wind: ").Append(wind_today).Append(Environment.NewLine);
                builder.Append("Today Humidity: ").Append(humid_today).Append(Environment.NewLine);
                builder.Append(Environment.NewLine);
                builder.Append("Tonight Temperature: ").Append(temp_tonight).Append(Environment.NewLine); 
                builder.Append("Tonight Wind: ").Append(wind_tonight).Append(Environment.NewLine);
                builder.Append("Tonight Humidity: ").Append(humid_tonight).Append(Environment.NewLine);
                MessageBox.Show(builder.ToString());
            }
        }
    }
}

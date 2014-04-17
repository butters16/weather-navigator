using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherNavigator
{
    class DocumentParser
    {
        public static Weather ParseToday(HtmlDocument document)
        {
            return Parse(document, 8, 2, 5);
        }

        public static Weather ParseTonight(HtmlDocument document)
        {
            return Parse(document, 9, 3, 6);
        }

        private static Weather Parse(HtmlDocument document, int temperatureIndex, int windIndex, int humidityIndex)
        {
            Weather weather = new Weather();

            var top = document.GetElementById("wx-forecast-container");

            //*[@id="wx-forecast-container"]/div[1]/div[2]/div[8]/div[1] (ex: <div class="wx-temperature">75<span class="wx-degrees">°</span></div>)
            weather.temperature = top.Div(1).Div(2).Div(temperatureIndex).Div(1).InnerText;

            //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[2]/div/div[2] (ex: <div class="wx-wind-label">E at 6 mph</div>)
            try
            {
                weather.wind = top.Div(1).Div(2).Div(16).Div(windIndex).Div(1).Div(2).InnerText;
            }
            catch (NullReferenceException)
            {
                // Do nothing.
            }

            //*[@id="wx-forecast-container"]/div[1]/div[2]/div[16]/div[5]/div (ex: <div class="wx-data">59%</div>)
            weather.humidity = top.Div(1).Div(2).Div(16).Div(humidityIndex).Div(1).InnerText;

            return weather;
        }
    }
}

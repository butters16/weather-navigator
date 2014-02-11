using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherNavigator
{
    /// <summary>
    /// Gives method chaining capabilities for HtmlElements for cleaner XPath-like code.
    /// </summary>
    public static class HtmlElementHelper
    {
        public static HtmlElement Div(this HtmlElement htmlElement, int oneBasedXPathIndex)
        {
            return htmlElement.General("div", oneBasedXPathIndex);
        }

        public static HtmlElement Span(this HtmlElement htmlElement, int oneBasedXPathIndex)
        {
            return htmlElement.General("span", oneBasedXPathIndex);
        }

        public static HtmlElement General(this HtmlElement htmlElement, string tagName, int oneBasedXPathIndex)
        {
            var elements = htmlElement.GetElementsByTagName(tagName);
            var zeroBasedIndex = oneBasedXPathIndex - 1;
            int index = 0;
            foreach (var element in elements)
            {
                HtmlElement blah = element as HtmlElement;
                if (blah.Parent == htmlElement)
                {
                    if (index == zeroBasedIndex)
                    {
                        return blah;
                    }
                    ++index;
                }
            }

            return null;
        }
    }
}

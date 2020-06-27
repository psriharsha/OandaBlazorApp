using OandaBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OandaBlazorApp.Misc
{
    public static class PriceExtension
    {
        public static string ShowAsPrice(this Double val, ViewType type)
        {
            StringBuilder componentValue = new StringBuilder();
            string stringValue = val.ToString();
            if (val == 0)
            {
                stringValue = "0.0000";
            }
            componentValue.Append("<sup>");
            componentValue.Append(stringValue.Substring(0, stringValue.Length - 3));
            componentValue.Append("</sup>");
            if (type == ViewType.TABLE)
            {
                componentValue.Append("<span class=\"font-weight-bold\">");
            }else if (type == ViewType.CARD)
            {
                componentValue.Append("<span class=\"stock-price\">");
            }
            componentValue.Append(stringValue.Substring(stringValue.Length - 3, 2));
            componentValue.Append("</span>");
            componentValue.Append("<sup>");
            componentValue.Append(stringValue.Substring(stringValue.Length - 1, 1));
            componentValue.Append("</sup>");
            Console.WriteLine(componentValue.ToString());
            return componentValue.ToString();
        }
    }
}

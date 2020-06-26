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
            componentValue.Append(stringValue.Substring(0, stringValue.Length - 3));
            if (type == ViewType.TABLE)
            {
                componentValue.Append("<span class='text-bold>");
            }else if (type == ViewType.CARD)
            {
                componentValue.Append("<span class='text-large>");
            }
            componentValue.Append(stringValue.Substring(stringValue.Length - 3, 2));
            componentValue.Append("</span>");
            componentValue.Append(stringValue.Substring(stringValue.Length - 1, 1));

            return componentValue.ToString();
        }
    }
}

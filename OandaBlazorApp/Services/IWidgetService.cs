using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Services
{
    public interface IWidgetService
    {
        event EventHandler<string> OnWindowClosed;
        ValueTask Open(string path, string name, int width, int height);
    }
}

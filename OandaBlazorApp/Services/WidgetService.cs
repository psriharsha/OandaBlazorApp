using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OandaBlazorApp.Services
{
    public class WidgetService : IWidgetService, IDisposable
    {
        private readonly IJSRuntime jsRuntime;
        private DotNetObjectReference<WidgetService> wServiceListener;
        public event EventHandler<string> OnWindowClosed;

        public WidgetService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public void Dispose()
        {
            wServiceListener?.Dispose();
        }

        public ValueTask Open(string url, string title, int height, int width)
        {
            wServiceListener = DotNetObjectReference.Create(this);
            return jsRuntime.InvokeVoidAsync("blazorWidget.open", url, title, $"height={height},width={width}", wServiceListener);
        }

        [JSInvokable("NotifyWindowClosed")]
        public void NotifyWindowClosed(string title)
        {
            OnWindowClosed?.Invoke(this, title);
        }
    }
}

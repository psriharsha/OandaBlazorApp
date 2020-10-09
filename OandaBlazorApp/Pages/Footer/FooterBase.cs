using Microsoft.AspNetCore.Components;
using OandaBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Footer
{
    public class FooterBase : ComponentBase
    {
        [Parameter]
        public ViewType viewType { get; set; }
        [Parameter]
        public EventCallback<ViewType> OnChangeViewType { get; set; }
        [Parameter]
        public string Message { get; set; }


        protected async Task ChangeViewType(ViewType type)
        {
            viewType = type;
            await OnChangeViewType.InvokeAsync(type);
        }
    }
}

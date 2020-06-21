using Microsoft.AspNetCore.Components;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        public String Token { get; set; }
        public List<String> Accounts { get; set; } = new List<String>();
        public String Text { get; set; }
        [Inject]
        public ILoginService loginService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }

        protected async Task CheckForValidAccounts()
        {
            Accounts = await loginService.GetAccounts(Token);
        }

        protected async Task SelectedAccount(String selectedAccountId)
        {
            await loginService.SetSelectedAccount(Token, selectedAccountId);
            navigationManager.NavigateTo("/", true);
        }
    }
}

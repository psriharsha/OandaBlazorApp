using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OandaBlazorApp.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState authState = null;
            var accountId = await localStorage.GetItemAsync<String>("api-account-id");
            var identity = (accountId == null) ? new ClaimsIdentity()
                : new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, StateHolder.ApiToken)
                }, "Local Storage Authentication");
            var user = new ClaimsPrincipal(identity);
            authState = new AuthenticationState(new ClaimsPrincipal(identity));
            return authState;
        }
    }
}

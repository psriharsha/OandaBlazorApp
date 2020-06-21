using OandaBlazorApp.Models;
using OandaBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace OandaBlazorApp.Pages.Login
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private Settings settings;

        public LoginService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }
        public async Task<List<String>> GetAccounts(String Token)
        {
            if (!String.IsNullOrEmpty(Token))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var result = await httpClient.GetFromJsonAsync<AccountsData>("");
                    if (result.accounts.Length > 0)
                        return result.accounts.Select(r => r.id).ToList();
                }
                catch (Exception ex)
                {
                }
            }

            return new List<String>();
        }

        public async Task SetSelectedAccount(string Token, string AccountID)
        {
            StateHolder.AccountId = AccountID;
            StateHolder.ApiToken = Token;
            await localStorage.SetItemAsync("api-token", Token);
            await localStorage.SetItemAsync("api-account-id", AccountID);
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Services
{
    public interface ILoginService
    {
        Task<List<String>> GetAccounts(String Token);
        Task SetSelectedAccount(String Token, String AccountID);
    }
}

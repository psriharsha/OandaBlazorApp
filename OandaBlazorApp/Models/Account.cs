using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OandaBlazorApp.Models
{
    public class Account
    {
        public String id { get; set; }
        public String[] tags { get; set; }
    }

    public class AccountsData
    {
        public Account[] accounts { get; set; }
    }

}

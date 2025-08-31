using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagerApp.DAL.Helpers
{
    public static class CurrentAccount
    {
        public static int AccountId { get; private set; }
        public static string AccountTitle { get; private set; }

        public static void SetCurrentAccount(int accountId, string accountTitle)
        {
            AccountId = accountId;
            AccountTitle = accountTitle;
        }
    }
}

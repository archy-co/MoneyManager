using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagerApp.DAL.Helpers
{
    public static class CurrentUser
    {
        
        public static int UserId { get; private set; }
        public static string Username { get; private set; }

        public static void SetCurrentUser(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }

        public static void ClearCurrentUser()
        {
            UserId = 0;
            Username = null;
        }
    }
}

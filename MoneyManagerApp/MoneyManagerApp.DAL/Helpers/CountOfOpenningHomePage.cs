using MoneyManagerApp.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagerApp.DAL.Helpers
{
    public static class CountOfOpenningHomePage
    {
        private static int count = 1;
        public static int Count { get { return count; }  set { count = value; } }

        public static void SetCountOfOpenningHomePage(int count)
        {
            Count = count;
        }


    }
}

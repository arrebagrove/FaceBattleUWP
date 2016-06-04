using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBattleUWP.API
{
    public static class URLs
    {
        private static string DOMAIN => "https://1percent.faith/facebattle";

        public static string LOGIN => $"{DOMAIN}/user/login";

        public static string REGISTER => $"{DOMAIN}/user/signup";

    }
}

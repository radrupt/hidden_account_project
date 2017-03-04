using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClient.Pandawork
{
    public class Cookie
    {
        private static Int32 userId;
        public static Int32 UserId
        {
            get { return Cookie.userId; }
            set { Cookie.userId = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TestWindow
{
    static class Log4net
    {
        private static log4net.ILog staticlog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static log4net.ILog log
        {
            get
            {
                return staticlog;
            }
            set
            {
                ;
            }
        }

    }
}


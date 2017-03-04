using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VideoClient.MapServiceReference;
using VideoClient.UserServiceReference;
using VideoCommon.com.pandawork.common.entity;

namespace VideoClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            //UserServiceClient client = new UserServiceClient();
            //Console.WriteLine(client.getAllUsers());
            //MapServiceClient ms = new MapServiceClient();            
            //Console.WriteLine("hankl" + ms.getAllMaps());
        }
    }
}

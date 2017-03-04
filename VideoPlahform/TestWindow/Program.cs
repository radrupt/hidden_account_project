using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestWindow.HieClient_HistoryStream_demo;

namespace TestWindow
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]//是一种线程模型,一种套间(或译为公寓)线程模式.用在程序的入口方法上,用于初始化   
        static void Main()
        {
            Application.EnableVisualStyles();//启用应用程序的可视样式。
            Application.SetCompatibleTextRenderingDefault(false);//设置一种绘制样式
            Application.Run(new HieCIU_RealStream_demo());//在当前线程上运行应用程序消息循环，并使窗口可见
        }
    }
}

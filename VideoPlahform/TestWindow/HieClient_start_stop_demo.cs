using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestWindow
{
    //启动服务
    //申请必要的内存，线程等资源。
    //返回 0 表示成功，否则表示错误码（参见 eHieClientError ） 
    //启动服务后，不再需要服务时请调用停止服务接口 HieClient_Stop 
    //接口类型：阻塞式 

    public partial class HieClient_start_stop_demo : Form
    {
        private CallDLL.interfaces.Client_Init client_init;

        public HieClient_start_stop_demo()
        {
            InitializeComponent();
            client_init = new CallDLL.impls.HieClient_Init_Impl();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!client_init.Client_Start())
            {
                this.label1.Text = "服务启动失败，请检查原因";
            }
            else
            {
                this.label1.Text = "服务启动成功！";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (!client_init.Client_Stop())
            {
                this.label1.Text = "未服务关闭失败，请检查原因";
            }
            else 
            {
                this.label1.Text = "关闭成功！";
            }
        }
    }
}

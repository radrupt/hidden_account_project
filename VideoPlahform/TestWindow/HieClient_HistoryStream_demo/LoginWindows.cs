using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestWindow.HieClient_HistoryStream_demo
{
    public partial class LoginWindows : Form
    {
        private HieCIU_HistoryStream_demo demo;
        private Reg.interfaces.CheckUserLogin checkUserLogin;

        public LoginWindows(HieCIU_HistoryStream_demo hieCIU_HistoryStream_demo)
        {
            demo = hieCIU_HistoryStream_demo;
            checkUserLogin = new Reg.impls.hie.CheckUserLoginImpl();
            this.demo = hieCIU_HistoryStream_demo;
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //isLegalLoginString:只是判断输入字符是否合法
            
            string errorStr = checkUserLogin.isLegalLoginString(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            if (errorStr == "")
            {
                try
                {
                    demo.device.Client_userLogin.Login(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                    demo.toolStripStatusLabel1.Text = "登录成功欢迎您 " + textBox3.Text;
                    
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(errorStr);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

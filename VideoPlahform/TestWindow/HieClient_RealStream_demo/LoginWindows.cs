using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestWindow.Device.impls;

namespace TestWindow.HieClient_RealStream_demo
{
    public partial class LoginWindows : Form
    {
        private HieCIU_RealStream_demo demo;
        private Reg.interfaces.CheckUserLogin checkUserLogin;
        public LoginWindows(HieCIU_RealStream_demo hieCIU_RealStream_demo)
        {
            demo = hieCIU_RealStream_demo;
            checkUserLogin = new Reg.impls.hie.CheckUserLoginImpl();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorStr = checkUserLogin.isLegalLoginString(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            if ( errorStr== "")
            {
                try
                {
                    //Client_userLogin：实现在HieClient_UserLogin_Impl.cs里
                    demo.device.Client_userLogin.Login(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                    demo.toolStripStatusLabel1.Text = "登录成功欢迎您 " + textBox3.Text;
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message+"--登录信息错误,登录失败");
                }
            }
            else
            {
                MessageBox.Show(errorStr+"---信息格式错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginWindows_Load(object sender, EventArgs e)
        {

        }
    }
}

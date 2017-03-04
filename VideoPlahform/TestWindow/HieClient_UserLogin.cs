using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using HUSER = System.IntPtr;
using TestWindow.Reg.Utilis;

namespace TestWindow
{
    public partial class HieClient_UserLogin : Form
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private CallDLL.interfaces.Client_Init client_init;
        private CallDLL.interfaces.Client_UserLogin client_userLogin;
        private CallDLL.interfaces.Client_UserLogout client_userLogout;
        private Reg.interfaces.CheckUserLogin checkUserLogin;

        public HieClient_UserLogin()
        {
            InitializeComponent();
            client_init = new CallDLL.impls.HieClient_Init_Impl();
            client_userLogin = new CallDLL.impls.HieClient_UserLogin_Impl();
            client_userLogout = new CallDLL.impls.HieClient_UserLogout_Impl();
            checkUserLogin = new Reg.impls.hie.CheckUserLoginImpl();
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (client_init.Client_Start())
            {
                button3.Enabled = true;
                button4.Enabled = true;
                label1.Text = "服务启动成功";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Logout(client_userLogin);
            label1.Text = "退出成功" + " 再见 " + textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client_init.Client_Stop())
            {
                this.Close();
            }
        }

        private void Login(string ip, string port, string userName, string password)
        {

            string cul = checkUserLogin.isLegalLoginString(ip, port, userName, password);
            if (cul.Equals(""))
            {
                try
                {
                    client_userLogin.Login(ip, port, userName, password);
                    label1.Text = "登录成功" + " 您好 " + userName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(cul);
            }
        }

        private void Logout(CallDLL.interfaces.Client_UserLogin userLogin)
        {
            client_userLogout.Logout(userLogin);
        }
    }
}

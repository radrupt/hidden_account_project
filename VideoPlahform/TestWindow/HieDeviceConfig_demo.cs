using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TestWindow
{
    public partial class HieDeviceConfig_demo : Form
    {

        private CallDLL.interfaces.Client_Init client_init;
        private CallDLL.interfaces.Client_UserLogin client_userLogin;
        private CallDLL.interfaces.Client_UserLogout client_userLogout;
        private CallDLL.interfaces.Client_DeviceConfig client_deviceConfig;
        private Reg.interfaces.CheckUserLogin checkUserLogin;

        public HieDeviceConfig_demo()
        {
            client_init = new CallDLL.impls.HieClient_Init_Impl();
            client_userLogin = new CallDLL.impls.HieClient_UserLogin_Impl();
            client_userLogout = new CallDLL.impls.HieClient_UserLogout_Impl();
            client_deviceConfig = new CallDLL.impls.HieClient_DeviceConfig_Impl();
            checkUserLogin = new Reg.impls.hie.CheckUserLoginImpl();
            
            
            InitializeComponent();

            button1.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;
        }

        private void HieDeviceConfig_demo_Load(object sender, EventArgs e)
        {
            try
            {
                if (client_init.init(CallDLL.interfaces.DecoderCardWorkMode.WorkMode_CIF))
                {
                    if (client_init.Client_Start())
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
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
                    button5.Enabled = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Logout(client_userLogin);
            label1.Text = "退出成功" + " 再见 " + textBox3.Text;
        }

        private void Logout(CallDLL.interfaces.Client_UserLogin userLogin)
        {
            client_userLogout.Logout(userLogin);
        }

        private void HieDeviceConfig_demo_FormClosing(object sender, FormClosingEventArgs e)
        {
            client_init.Client_Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBox3.Text;
                client_deviceConfig.DeviceConfig(client_userLogin, CallDLL.interfaces.eLanguage.SimplifiedChinese, userName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }
    }
}

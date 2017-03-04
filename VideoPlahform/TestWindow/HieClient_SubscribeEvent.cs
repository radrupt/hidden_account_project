using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestWindow.Reg.Utilis;
using HUSER = System.IntPtr;
namespace TestWindow
{
    public partial class HieClient_SubscribeEvent : Form
    {
        private delegate void SetTextCallback(string text);

        private CallDLL.interfaces.Client_Init client_init;
        private CallDLL.interfaces.Client_UserLogin client_login;
        private CallDLL.interfaces.Client_UserLogout client_logout;
        private CallDLL.interfaces.Client_SubscribeEvent client_subEvent;
        private CallDLL.interfaces.Client_UnSubscribeEvent client_unsubEvent;
        private CallDLL.interfaces.Client_DeleteUserForceCB client_deleteUserForce;

        private Reg.interfaces.CheckUserLogin checkUserLogin;

        public HieClient_SubscribeEvent()
        {
            

            InitializeComponent();
            client_subEvent = new CallDLL.impls.HieClient_SubscribeEvent_Impl();
            client_unsubEvent = new CallDLL.impls.HieClient_UnSubscribeEvent_Impl(); 
           
            client_subEvent.ValueChange += new CallDLL.interfaces.ValueChanged(HieClient_SubscribeEvent_vc);
            client_init = new CallDLL.impls.HieClient_Init_Impl();
            checkUserLogin = new Reg.impls.hie.CheckUserLoginImpl();
            client_deleteUserForce = new CallDLL.impls.HieClient_DeleteUserForceCB_Impl();
            
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            if (client_init.Client_Start())
            {
                label1.Text = "服务启动成功";
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                client_login = new CallDLL.impls.HieClient_UserLogin_Impl();
                client_logout = new CallDLL.impls.HieClient_UserLogout_Impl();
            }
        }

        void HieClient_SubscribeEvent_vc(string value)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback stcb = new SetTextCallback(HieClient_SubscribeEvent_vc);
                this.Invoke(stcb, new object[] { value });
            }
            else
            {
                this.richTextBox1.Text += value + "\n";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cul = checkUserLogin.isLegalLoginString(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);

            if (cul == "")
            {
                try
                {
                    client_deleteUserForce.DeleteUserForce();
                    client_login.Login(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                    label1.Text = "用户" + textBox3.Text + " 登录成功";
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
            client_subEvent.SubscribeAllEvent(client_login);
            this.richTextBox1.Text += "订阅全部事件\n";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            client_unsubEvent.UnSubscribeAllEvent(client_login);
            this.richTextBox1.Text += "退订全部事件";
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!client_deleteUserForce.IsUserForceDeleted)
                    client_logout.Logout(client_login);
                label1.Text = "用户" + textBox3.Text + " 退出成功";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HieClient_SubscribeEvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client_init.Client_Stop())
            {
                this.Close();
            }
            label1.Text = "服务关闭失败";
        }
    }
}

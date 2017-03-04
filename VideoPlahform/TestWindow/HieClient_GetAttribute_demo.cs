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
    public partial class HieClient_GetAttribute_demo : Form
    {
        private CallDLL.interfaces.Client_Init client_init;

        private CallDLL.interfaces.Client_SDKAttribute client_sdkAttribute;


        public HieClient_GetAttribute_demo()
        {
            InitializeComponent();
            client_init = new CallDLL.impls.HieClient_Init_Impl();
            client_sdkAttribute = new CallDLL.impls.HieClient_SDKAttribute_Impl();
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (client_init.Client_Start())
            {
                
                label7.Text = "服务启动成功\n";

                GetSDKAttr(CallDLL.interfaces.SDKAttr.ConnectTimeOut, label4);
                GetSDKAttr(CallDLL.interfaces.SDKAttr.CommandTimeOut, label5);
                GetSDKAttr(CallDLL.interfaces.SDKAttr.MediaTimeOut, label6);

                button3.Enabled = true;
            }
            else
            {
                label7.Text = "服务启动失败";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client_init.Client_Stop())
            {
                label7.Text = "服务关闭成功";
                button3.Enabled = false;
                this.Close();
            }
            else
            {
                label7.Text = "服务关闭失败";
            }
        }

        private void GetSDKAttr(CallDLL.interfaces.SDKAttr getAttrType, Label label)
        {
            label.Text = "" + client_sdkAttribute.GetSDKAttr(getAttrType);
        }

        private void SetSDKAttr(CallDLL.interfaces.SDKAttr setAttrType, Label label, TextBox textBox)
        {
            try
            {
                if (textBox.Text.Length > 0)
                {
                    uint dwAttrIn = Convert.ToUInt32(textBox.Text);
                    client_sdkAttribute.SetSDKAttr(setAttrType, dwAttrIn);
                    label.Text = dwAttrIn + "";
                    textBox.Text = "";
                }
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetSDKAttr(CallDLL.interfaces.SDKAttr.ConnectTimeOut, label4, textBox1);
            SetSDKAttr(CallDLL.interfaces.SDKAttr.CommandTimeOut, label5, textBox2);
            SetSDKAttr(CallDLL.interfaces.SDKAttr.MediaTimeOut, label6, textBox3);
        }
    }
}

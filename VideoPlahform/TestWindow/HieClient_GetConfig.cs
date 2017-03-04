using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HUSER = System.IntPtr;
using HieCiULib;
using System.Runtime.InteropServices;

namespace TestWindow
{
    public partial class HieClient_GetConfig : Form
    {
        private CallDLL.interfaces.Client_Init client_init;
        private CallDLL.interfaces.Client_UserLogin client_userLogin;
        private CallDLL.interfaces.Client_UserLogout client_userLogout;
        private Reg.interfaces.CheckUserLogin checkUserLogin;

        private Common.ConfigInformation gcConfig;
        
        public HieClient_GetConfig()
        {
            InitializeComponent();
            client_init = new CallDLL.impls.HieClient_Init_Impl();
            client_userLogin = new CallDLL.impls.HieClient_UserLogin_Impl();
            client_userLogout = new CallDLL.impls.HieClient_UserLogout_Impl();

            checkUserLogin = new Reg.impls.hie.CheckUserLoginImpl();
            button2.Enabled = false;
            button3.Enabled = false;
            if (client_init.Client_Start())
            {
                button2.Enabled = true;
                
                label1.Text = "服务启动成功";
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
                    button3.Enabled = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Logout(client_userLogin);
            label1.Text = "退出成功" + " 再见 " + textBox3.Text;
        }

        private void HieClient_GetConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            client_init.Client_Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IntPtr buffer = IntPtr.Zero;
            IntPtr buffer1 = IntPtr.Zero;
            try
            {

                Configure.HY_DVR_NET_CFG m_tkDvrNetCfg;
                Configure.HY_DVR_ETH_IF struEth = new Configure.HY_DVR_ETH_IF();
                gcConfig = new Common.ConfigInformation();
                gcConfig.dwMainCommand = Configure.HY_DVR_GET_NETCFG;
                gcConfig.dwAssistCommand = Configure.NETCFG_ALL;
                
                buffer = Marshal.AllocHGlobal(Marshal.SizeOf(gcConfig));
                Marshal.StructureToPtr(gcConfig, buffer, false);
               
                int r = Unit.HieClient_GetConfig(client_userLogin.UserInfo,buffer);

                m_tkDvrNetCfg = (Configure.HY_DVR_NET_CFG)Marshal.PtrToStructure(new IntPtr(buffer.ToInt32() + 8), typeof(Configure.HY_DVR_NET_CFG));

                richTextBox1.Text += "\ndwAssistCommand = " + gcConfig.dwAssistCommand;
                richTextBox1.Text += "\ndwConfigLen = " + gcConfig.dwConfigLen;
                richTextBox1.Text += "\ndwMainCommand =" + gcConfig.dwMainCommand;

                richTextBox1.Text += "\nstruEth";
                richTextBox1.Text += "\nbValid = " + m_tkDvrNetCfg.struEth.bValid;
                richTextBox1.Text += "\nszIPAddress = " + m_tkDvrNetCfg.struEth.szIPAddress;
                richTextBox1.Text += "\nszMacAddress = " + m_tkDvrNetCfg.struEth.szMacAddress;
                richTextBox1.Text += "\nszSubnetMask = " + m_tkDvrNetCfg.struEth.szSubnetMask;
                richTextBox1.Text += "\nszGateway = " + m_tkDvrNetCfg.struEth.szGateway;
                struEth = m_tkDvrNetCfg.struEth;

                struEth.szIPAddress = "192.168.1.21";
                m_tkDvrNetCfg.struEth = struEth;


                gcConfig.dwMainCommand = Configure.HY_DVR_SET_NETCFG;
                gcConfig.dwAssistCommand = Configure.NETCFG_ETH_IF;
                gcConfig.sConfig = StructToString(m_tkDvrNetCfg.struEth);
                //gcConfig.dwConfigLen = (uint)Marshal.SizeOf(typeof(Configure.HY_DVR_ETH_IF));
                buffer1 = Marshal.AllocHGlobal(Marshal.SizeOf(gcConfig));
                Marshal.StructureToPtr(gcConfig, buffer1, false);
                //DebugPrintInfo(150, buffer1, richTextBox1);
                r = Unit.HieClient_SetConfig(client_userLogin.UserInfo, buffer1);
                if (r == 0)
                {
                    richTextBox1.Text += "\nOK";
                }
                //richTextBox1.Text += "\nstruEth";
                //richTextBox1.Text += "\nbValid = " + m_tkDvrNetCfg.struEth.bValid;
                //richTextBox1.Text += "\nszIPAddress = " + m_tkDvrNetCfg.struEth.szIPAddress;
                //richTextBox1.Text += "\nszMacAddress = " + m_tkDvrNetCfg.struEth.szMacAddress;
                //richTextBox1.Text += "\nszSubnetMask = " + m_tkDvrNetCfg.struEth.szSubnetMask;
                //richTextBox1.Text += "\nszGateway = " + m_tkDvrNetCfg.struEth.szGateway;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
            }
            finally
            {
                try
                {
                    Marshal.FreeHGlobal(buffer);
                    Marshal.FreeHGlobal(buffer1);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            } 
        }

        public static String convert(byte b)
        {
            StringBuilder str = new StringBuilder(8);
            int[] bl = new int[8];

            for (int i = 0; i < bl.Length; i++)
            {
                bl[bl.Length - 1 - i] = ((b & (1 << i)) != 0) ? 1 : 0;
            }

            foreach (int num in bl) str.Append(num);

            return str.ToString();
        }

        public void DebugPrintInfo(int line, IntPtr ptr, RichTextBox ftb)
        {
            ftb.Text += "\n";
            for (int i = 0, j = 1; i < line*4; i += 4, j++)
            {
                ftb.Text += "(" + j + ")\t" + convert(Marshal.ReadByte(new IntPtr(ptr.ToInt32() + i))) +
                    " " + convert(Marshal.ReadByte(new IntPtr(ptr.ToInt32() + i + 1))) +
                    " " + convert(Marshal.ReadByte(new IntPtr(ptr.ToInt32() + i + 2))) +
                    " " + convert(Marshal.ReadByte(new IntPtr(ptr.ToInt32() + i + 3))) + "\n";

            }
        }

        public void DebugPrintInfo(int line, Common.ConfigInformation gcConfig, RichTextBox ftb)
        {
            IntPtr buffer = IntPtr.Zero;
            try
            {
                buffer = Marshal.AllocHGlobal(Marshal.SizeOf(gcConfig));
                Marshal.StructureToPtr(gcConfig, buffer, false);
                DebugPrintInfo(150, buffer, ftb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                try
                {
                    Marshal.FreeHGlobal(buffer);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// 结构体转string
        /// </summary>
        /// <param name="structObj">要转换的结构体</param>
        /// <returns>转换后的byte数组</returns>
        public static string StructToString(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return System.Text.Encoding.Default.GetString(bytes);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

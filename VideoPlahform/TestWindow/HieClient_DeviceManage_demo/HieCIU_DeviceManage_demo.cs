using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestWindow.tools;
namespace TestWindow.HieClient_DeviceManage_demo
{
    public partial class HieCIU_DeviceManage_demo : Form
    {
        /*如果是使用回调的话,那么这两个变量的生命周期必须和控件的生命周期一致,切记..
         但是没搞懂c#何时释放资源..*/
        private tools.DeviceProbe dpro = null;

        /*好吧,他的作用是让异步回调的事件里属于最新按钮按下的事件有效*/
        private int branchthreadID = 0;

        private DeviceProbe.AddRowInfo_CALLBACK addrowinfo_callback;

        public void addrowinfo(string[] rowinfo)
        {
            //这里无需上锁,原因在于branchthreadID永远是最新的
            if (rowinfo[0].Equals(branchthreadID.ToString())) {
                string[] info = {rowinfo[1],rowinfo[2],rowinfo[3],rowinfo[4],rowinfo[5],rowinfo[6],rowinfo[7],rowinfo[8],
                                    rowinfo[9],rowinfo[10],rowinfo[11],rowinfo[12],rowinfo[13]};
                devicemangegrid.Rows.Add(info);
            }            
        }
        
        public HieCIU_DeviceManage_demo()
        {            
                      
            InitializeComponent();
            
            devicemangegrid.ColumnCount = 13;
            string[] headtext ={"服务类型","MAC地址","IP地址","子网掩码","网关地址","管理主机IP",
                                   "管理主机端口","自动注册","注册间隔","探测端口","命令端口","媒体端口","通道数"};
            for (int i = 0; i < devicemangegrid.ColumnCount; ++i)
                devicemangegrid.Columns[i].HeaderText = headtext[i];    
        }
        
        private void probedevice_Click(object sender, EventArgs e)
        {
            branchthreadID++;
            if ( dpro != null )  //只要不为空,就意味着上一次的一定是有设备探测的
            {
                try
                {
                    dpro.stopdevicepro();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            devicemangegrid.Rows.Clear();
            addrowinfo_callback = new DeviceProbe.AddRowInfo_CALLBACK(addrowinfo);
            dpro = new tools.DeviceProbe(this, ref addrowinfo_callback, branchthreadID);
            try
            {
                dpro.Deviceprobe(8000, 9000);
            }
            catch (Exception ex) {
                MessageBox.Show( ex.Message );
            }
        }

        private void configuredevice_Click(object sender, EventArgs e)
        {
            string[] ss = new string[devicemangegrid.ColumnCount];

            for (int i = 0; i < ss.Length; ++i)
            {
                ss[i] = devicemangegrid.Rows[0].Cells[i].Value.ToString();
            }//这句代码需要等换界面了，再去实现它
            ss[6] = "3033";
            try
            {
                dpro.configuredevice(ss);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void configurealldevice_Click(object sender, EventArgs e)
        {
            string[] ss = new string[devicemangegrid.ColumnCount];
            for (int i = 0; i < ss.Length; ++i)
            {
                ss[i] = devicemangegrid.Rows[0].Cells[i].Value.ToString();
            }//这句代码需要等换界面了，再去实现它
            ss[6] = "3032";
            try
            {
                dpro.configurealldevice(ss,7000,9000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

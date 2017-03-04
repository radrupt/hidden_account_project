using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestWindow.Device.impls;
using TestWindow.DeviceBuilder;
using TestWindow.DeviceBuilder.impls;

namespace TestWindow.HieClient_HistoryStream_demo
{
    public partial class HieCIU_HistoryStream_demo : Form
    {
        public Hie_MGTC26_Device device;

        public HieCIU_HistoryStream_demo(Hie_MGTC26_Device device)
        {
            this.device = device;
            InitializeComponent();
        }

        private void HieCIU_HistoryStream_demo_Load(object sender, EventArgs e)
        {
            device.Client_init.init(CallDLL.interfaces.DecoderCardWorkMode.WorkMode_CIF);
            device.Client_init.Client_Start();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginWindows loginWindow = new LoginWindows(this);
            loginWindow.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (device.Client_userLogin.UserInfo != new Constants.CConstants().INVALID_HANDLE_VALUE)
            {
                device.Client_userLogout.Logout(device.Client_userLogin);
                device.Client_userLogin.UserInfo = new Constants.CConstants().INVALID_HANDLE_VALUE;
                this.toolStripStatusLabel1.Text = "Admin已经退出";
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (device.Client_userLogin.UserInfo != new Constants.CConstants().INVALID_HANDLE_VALUE)
            {
                device.Client_userLogout.Logout(device.Client_userLogin);
                device.Client_userLogin.UserInfo = new Constants.CConstants().INVALID_HANDLE_VALUE;
            }
            device.Client_init.Client_Stop();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_historyStream.HistoryStreamCreate(device.Client_userLogin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                device.Client_streamControl.StreamOpen(device.Client_historyStream.HHistoryStream);
                device.Client_streamControl.StreamPlay(device.Client_historyStream.HHistoryStream, panel1.Handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_streamControl.StreamPause(device.Client_historyStream.HHistoryStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_streamControl.StreamResume(device.Client_historyStream.HHistoryStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_streamControl.StreamStop(device.Client_historyStream.HHistoryStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_historyStream.HistoryStreamDestroy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

    }
}

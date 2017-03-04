using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestWindow.DeviceBuilder;
using TestWindow.DeviceBuilder.impls;
using TestWindow.Device;
using TestWindow.Device.impls;
using HieCiULib;
using System.Runtime.InteropServices;
namespace TestWindow
{
    //partial:分部类型定义允许将类，结构或接口的定义拆分到不同的文件里
    //HieCIU_RealStream_demo之所以分两部分，是从功能的角度上来划分的，这部分实现的是数据的初始各种东西，另一部分则是界面的初始化
    public partial class HieCIU_RealStream_demo : Form
    {
        public const int USER = 0x500;

        public const int MYMESSAGE = USER + 1;  //自定义播放结束消息

        public Hie_MGTC26_Device device;


        private System.Timers.Timer myTimer;

        private tools.PlayRecoder playrec;

        private Hashtable button_ptzcontrolMode  = new Hashtable();

        protected override void DefWndProc(ref Message m)//重写消息接收函数，处理自定义消息
        {
            switch (m.Msg)
            {
                //接收自定义消息MYMESSAGE，并显示其参数  
                case MYMESSAGE:
                    toolStripStatusLabel1.Text = "播放结束";
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
        public HieCIU_RealStream_demo()
        {
            DeviceDirector director = new DeviceDirector();//它的意义在于当所有的设备都实现了二次封装，
            //即可统一的通过DeviceDirector来运行,所以目前设备向导(DeviceDirector)所调用的函数得和AbstractDeviceBuilder
            //里的函数一致
            AbstractDeviceBuilder builder = new Hie_MGTC26_Builder();
            director.Construct(builder);
            device = (Hie_MGTC26_Device)builder.getDevice();
            
            InitializeComponent();//初始化界面
            //----------------用到mouse事件
            button_ptzcontrolMode.Add("buttonUp", Common.ePTZControlCmdCode.ePTZControlCodeUp);
            button_ptzcontrolMode.Add("buttonDown", Common.ePTZControlCmdCode.ePTZControlCodeDown);
            button_ptzcontrolMode.Add("buttonLeft", Common.ePTZControlCmdCode.ePTZControlCodeLeft);
            button_ptzcontrolMode.Add("buttonRight", Common.ePTZControlCmdCode.ePTZControlCodeRight);
            button_ptzcontrolMode.Add("buttonZoomIn", Common.ePTZControlCmdCode.ePTZControlCodeZoomIn);
            button_ptzcontrolMode.Add("buttonZoomOut", Common.ePTZControlCmdCode.ePTZControlCodeZoomOut);
            button_ptzcontrolMode.Add("buttonApertureUp", Common.ePTZControlCmdCode.ePTZControlCodeApertureUp);
            button_ptzcontrolMode.Add("buttonApertureDown", Common.ePTZControlCmdCode.ePTZControlCodeApertureDown);
            button_ptzcontrolMode.Add("buttonFocusFar", Common.ePTZControlCmdCode.ePTZControlCodeFocusFar);
            button_ptzcontrolMode.Add("buttonFocusNear", Common.ePTZControlCmdCode.ePTZControlCodeFocusNear);
            //下面四个没实现，这四个也只需做界面部分，逻辑部分无需添加
            button_ptzcontrolMode.Add("buttonAutoUpLeft", Common.ePTZControlCmdCode.ePTZControlCodeUpLeft);//云台上仰和左转    
            button_ptzcontrolMode.Add("buttonAutoUpRight", Common.ePTZControlCmdCode.ePTZControlCodeUpRight);//云台上仰和右转 
            button_ptzcontrolMode.Add("buttonAutoDownLeft", Common.ePTZControlCmdCode.ePTZControlCodeDownLeft);//云台下俯和左转
            button_ptzcontrolMode.Add("buttonAutoDownRight", Common.ePTZControlCmdCode.ePTZControlCodeDownRight);//云台下俯和右转
            //----------------用到click事件
            button_ptzcontrolMode.Add("加热器", Common.ePTZControlCmdCode.ePTZControlCodeHeater);
            button_ptzcontrolMode.Add("摄像机", Common.ePTZControlCmdCode.ePTZControlCodeCameraPower);
            button_ptzcontrolMode.Add("灯光", Common.ePTZControlCmdCode.ePTZControlCodeLightPower);
            button_ptzcontrolMode.Add("雨刷", Common.ePTZControlCmdCode.ePTZControlCodeWiper);
            button_ptzcontrolMode.Add("风扇", Common.ePTZControlCmdCode.ePTZControlCodeFans);
            //下面三个暂时没实现，由于仅仅是对界面添加三个按钮，设置对应的名字为下面三个，再添加到相应的button_open(close)_click事件
            button_ptzcontrolMode.Add("buttonAutoZoom", Common.ePTZControlCmdCode.ePTZControlCodeFans);//开自动焦距（自动倍率）
            button_ptzcontrolMode.Add("buttonAutoFocus", Common.ePTZControlCmdCode.ePTZControlCodeFans);//开自动调焦
            button_ptzcontrolMode.Add("buttonAutoAperture", Common.ePTZControlCmdCode.ePTZControlCodeFans);//开自动光圈
            //预置点：就是记录监控设备，0--128个
            button_ptzcontrolMode.Add("buttonPresetSet", Common.ePTZControlCmdCode.ePTZControlCodePresetSet);//设置预置点
            button_ptzcontrolMode.Add("buttonPresetClear", Common.ePTZControlCmdCode.ePTZControlCodePresetClear);//清除预置点
            button_ptzcontrolMode.Add("buttonPresetCall", Common.ePTZControlCmdCode.ePTZControlCodePresetCall);//转到预置点
            button_ptzcontrolMode.Add("buttonCuriserSetStart", Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStart);//启动巡航记忆
            button_ptzcontrolMode.Add("buttonCuriserSetStop", Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStop);//关闭巡航记忆
            button_ptzcontrolMode.Add("buttonCuriserAddPreset", Common.ePTZControlCmdCode.ePTZControlCodeCuriserAddPreset);//将预置点加入巡航序列
            button_ptzcontrolMode.Add("buttonCuriserRunStart", Common.ePTZControlCmdCode.ePTZControlCodeCuriserRunStart);//开始巡航
            //这边ePTZControlCodeCuriserRunStop没用所以使用ePTZControlCodeTraceRunStop替换了
            button_ptzcontrolMode.Add("buttonCuriserRunStop", Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop);//停止巡航
            button_ptzcontrolMode.Add("buttonTraceSetStart", Common.ePTZControlCmdCode.ePTZControlCodeTraceSetStart);//启动轨迹记忆
            button_ptzcontrolMode.Add("buttonTraceSetStop", Common.ePTZControlCmdCode.ePTZControlCodeTraceSetStop);//关闭轨迹记忆
            button_ptzcontrolMode.Add("buttonTraceRunStart", Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStart);//开始轨迹
            button_ptzcontrolMode.Add("buttonTraceRunStop", Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop);//停止轨迹
            button_ptzcontrolMode.Add("buttonSystemReset", Common.ePTZControlCmdCode.ePTZControlCodeSystemReset);//系统复位

        }

        private void HieCIU_RealStream_demo_Load(object sender, EventArgs e)
        {
            device.Client_init.init(CallDLL.interfaces.DecoderCardWorkMode.WorkMode_CIF);//这个地方是说明景的这台机器用的是CIF视频编码？？？
            device.Client_init.Client_Start();//那Client_Start的返回值没有用啊
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (device.Client_userLogin.UserInfo != new Constants.CConstants().INVALID_HANDLE_VALUE)
            {
                device.Client_userLogout.Logout(device.Client_userLogin);
                device.Client_userLogin.UserInfo = new Constants.CConstants().INVALID_HANDLE_VALUE;
            }
        }

        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HieClient_RealStream_demo.LoginWindows loginWindow = new HieClient_RealStream_demo.LoginWindows(this);
            loginWindow.Show();
        }

        private void ConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_realStream.RealStreamConnect(device.Client_userLogin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_streamControl.StreamOpen(device.Client_realStream.HRealStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                device.Client_realStream.RealStreamDisconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void HieCIU_RealStream_demo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device.Client_userLogin.UserInfo != new Constants.CConstants().INVALID_HANDLE_VALUE)
            {
                device.Client_userLogout.Logout(device.Client_userLogin);
                device.Client_userLogin.UserInfo = new Constants.CConstants().INVALID_HANDLE_VALUE;
            }
            device.Client_init.Client_Stop();
        }

        private void button1_Click(object sender, EventArgs e)//播放
        {
            try
            {
                IntPtr test_UserInfo = device.Client_userLogin.UserInfo;
                timer1.Enabled = true;
                IntPtr test_HRealStream = device.Client_realStream.HRealStream;
                device.Client_streamControl.StreamPlay(device.Client_realStream.HRealStream, panel1.Handle);
                
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
                
                device.Client_streamControl.StreamPause(device.Client_realStream.HRealStream);
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
             
                device.Client_streamControl.StreamResume(device.Client_realStream.HRealStream);
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
                timer1.Enabled = false;
                device.Client_streamControl.StreamStop(device.Client_realStream.HRealStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)//截图
        {
            try
            {
                device.Client_streamControl.FilePath = @"D:";
                IntPtr ww = device.Client_realStream.HRealStream;
                device.Client_streamControl.StreamSnapShot(device.Client_realStream.HRealStream, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")+"hello.bmp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }
       
        //ePTZCtrlCmdCode:  控制码类型
        //dwCmdCode1:   PTZ 命令参数 1: 开始,0：停止
        //dwCmdCode2:   
        //dwCmdCode3:
        //dwCmdCode4:
        private void PtzControl(Common.ePTZControlCmdCode ePTZCtrlCmdCode, uint dwCmdCode1,
                                         uint dwCmdCode2 = 5, uint dwCmdCode3 = 0, uint dwCmdCode4 = 0)
        {//控制台调用
            try
            {
                //待完善,多通道选择......
                uint dwChnnl = 0;                                                 //通道编号: 1
                TrackBar tbr = this.trackbarRotationSpeed;                        //获取当前旋转速度
                dwCmdCode2 = (uint)(tbr.Value != 0 ? tbr.Value : 5);

                device.Client_ptzControl.PtzControl(device.Client_userLogin.UserInfo, dwChnnl,
                       ePTZCtrlCmdCode, dwCmdCode1, dwCmdCode2, dwCmdCode3, dwCmdCode4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + " " + ex.Message);
            }
        }
        //设备操作特性分三类
        //1.得按住鼠标左键才能发生的设备运行
        //2.设备的关闭开始,是通过第一个参数0,1控制的
        //3.设备的关闭打开,是通过控制码来管理的，和第一个参数无关
        //当然还有一个特殊的也是唯一个一个四个参数都有效的：将预置点加入巡航序列

        //第一类：持续性触发事件
        private void button_ptz_MouseDown(object sender, MouseEventArgs e)//鼠标在button上并点击
        {
            Button btn = (Button)sender;
            if (e.Button == MouseButtons.Left)//用户点击左键
            {
                Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
                ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
                PtzControl(ePTZCtrlCmdCode,1);
            }
        }
        private void button_ptz_MouseUp(object sender, MouseEventArgs e)//在鼠标指针在控件上并释放鼠标键时发生。
        {
            Button btn = (Button)sender;
            if (e.Button == MouseButtons.Left)//用户点击左键
            {
                Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
                ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
                PtzControl( ePTZCtrlCmdCode, 0 );
            }
        }
        //第二类：0，1控制事件
        private void button_Open_Click(object sender, EventArgs e)//打开选中辅助设备,或一些辅助功能如自动变焦
        {
            Button btn = (Button)sender;
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
            if (btn.Name.IndexOf("AuxiliaryEquipment") > -1)//判断是否是辅助设备
            {
                ComboBox cbox = this.comboBoxAuxiliaryEquipment;
                if (cbox.Text == "全部")
                {
                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeHeater, 1);
                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCameraPower, 1);
                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeLightPower, 1);
                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeWiper, 1);
                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeFans, 1);
                }
                else
                {
                    ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[cbox.Text];
                    PtzControl(ePTZCtrlCmdCode, 1);//1表示打开对应的设备
                }
            }
            else
            {
                ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
                PtzControl(ePTZCtrlCmdCode, 1);//1表示打开对应的设备
            }
        }

        private void button_Close_Click(object sender, EventArgs e)//关闭选中辅助设备,或一些辅助功能如自动变焦
        {
            Button btn = (Button)sender;
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
            if (btn.Name.IndexOf("AuxiliaryEquipment") > -1)//判断是否是辅助设备
            {
                ComboBox cbox = this.comboBoxAuxiliaryEquipment;
                if (cbox.Text == "全部")
                {
                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeAllOff, 0);//关闭（或断开）所有开关 
                }
                else
                {
                    ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[cbox.Text];
                    PtzControl(ePTZCtrlCmdCode, 0);//0表示关闭对应的设备
                }
            }
            else
            {
                ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
                PtzControl( ePTZCtrlCmdCode, 0 );//0表示关闭对应的设备
            }
        }
        //之所以将巡航喻辅助设备放在不同的方法里，原因是他们的参数不一致，对，
        //看将哪些功能放在一个函数里就是看这些功能所传的有效参数是否一致
        //第三类，控制码控制事件，第四类事件也包含在内
        private void button_Preset_Curiser_Click(object sender, EventArgs e)//对预置点进行操作
        {
            Button btn = (Button)sender;
            ComboBox cbox=null;
            //判断是那个事件类型的按钮触发了本次事件，有个特殊buttonCuriserAddPreset,两个字符串都有，所以优先Curiser
            if (btn.Name.IndexOf("Curiser") > -1)
                cbox = this.comboBoxCuriser;
            else if (btn.Name.IndexOf("Preset") > -1)
                cbox = this.comboBoxPreset;
            uint number = System.UInt32.Parse(cbox.Text);
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
            ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
            if (btn.Name == this.buttonCuriserAddPreset.Name)//对预置点做特殊处理,因为它的四个参数都有效
            {
                uint presetnumberPreset = System.UInt32.Parse(this.comboBoxPreset.Text);
                //Param2              Param3              Param4                 Param4  
                //巡航路线号 [ >=0 ]  预置点序号 [ >=0 ]  停顿时间（秒）[ >=0 ]  巡航速度 [ 1 - 10 ] 

                PtzControl(ePTZCtrlCmdCode, number-1, presetnumberPreset, 1,5);
            }
            else
                 PtzControl( ePTZCtrlCmdCode , number-1 );
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = device.Client_streamControl.StreamGetPlayedTime(device.Client_realStream.HRealStream).ToString();
            label4.Text = device.Client_streamControl.StreamGetAbsoluteTime(device.Client_realStream.HRealStream).ToString();
            label6.Text = device.Client_streamControl.StreamGetPlayedFrameNum(device.Client_realStream.HRealStream).ToString();
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            Log4net.log.Debug("设备管理");
            HieClient_DeviceManage_demo.HieCIU_DeviceManage_demo devicemange = new HieClient_DeviceManage_demo.HieCIU_DeviceManage_demo();
            devicemange.Show();            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HieClient_HistoryStream_demo.HieCIU_HistoryStream_demo qq =
                new TestWindow.HieClient_HistoryStream_demo.HieCIU_HistoryStream_demo(device);
            qq.Show();
        }
        private void remotecontrol(uint channel, CallDLL.interfaces.RecordType rt)
        {
            try
            {
                device.Client_remoteControl.RecordControl(device.Client_userLogin, channel, rt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            remotecontrol(0, CallDLL.interfaces.RecordType.ByManualRecord);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            remotecontrol(0, CallDLL.interfaces.RecordType.StopRecord);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                CallDLL.interfaces.TimeInfo begintime = new CallDLL.interfaces.TimeInfo();
                CallDLL.interfaces.TimeInfo endtime = new CallDLL.interfaces.TimeInfo();
                begintime.year = 2013;
                begintime.month = 12;
                begintime.day = 30;
                begintime.hour = 10;
                begintime.minute = 10;
                begintime.second = 10;
                endtime.year = 2013;
                endtime.month = 12;
                endtime.day = 30;
                endtime.hour = 17;
                endtime.minute = 10;
                endtime.second = 10;
                IntPtr hStreamHandle = device.Client_query.HistoryStreamQueryConnect(
                    device.Client_userLogin, 0, 1, CallDLL.interfaces.HistoryStreamType.GeneralRecord, begintime, endtime);
                string[] aa = device.Client_query.HistoryStreamQueryNext(hStreamHandle);
                if (aa[0].Equals("-1"))
                    MessageBox.Show(aa[1]);
                else
                {
                    string cc = null;
                    cc += aa[0].ToString();
                    cc += aa[1].ToString();
                    cc += aa[2].ToString();
                    cc += aa[3].ToString();
                    cc += aa[4].ToString();
                    MessageBox.Show(cc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)//判断设备是否在线
        {
            string ip = "192.168.1.10";
            Boolean vv = tools.DeviceProbe.DeviceIsOnline(ip, 5050, 100);
            MessageBox.Show(ip+(vv==true?"：在线":"：离线"));
        }

        private void StartRecording(object sender, EventArgs e)
        {
            device.Client_streamControl.StartRecord(device.Client_realStream.HRealStream);
        }

        private void StopRecording_Click(object sender, EventArgs e)
        {
            device.Client_streamControl.StopRecord();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        private void button12_Click(object sender, EventArgs e)
        {         
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    playrec = new tools.PlayRecoder(openfile.FileName, 3, panel1.Handle, GetForegroundWindow());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (playrec == null) return;
            try
            {
                playrec.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (playrec == null) return;
            try
            {
                playrec.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (playrec == null) return;
            try
            {
                playrec.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }              
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (playrec == null) return;
            try
            {
                playrec.Pause();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          
        }

        private void button17_Click(object sender, EventArgs e)
        {
            playrec.SnapBMP();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            playrec.LocalZoom();
        }        
    }
}

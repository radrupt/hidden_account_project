using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VideoCommon.com.pandawork.common.dto;
using log4net;
using System.Windows;
using VideoClient.View.Video;
using System.Windows.Controls;

using HieCiULib;
using ViewDevice = VideoClient.View.Device;

using VideoClient.Service.DeviceBuilder;
using VideoClient.Service.DeviceBuilder.impls;
using VideoClient.Service.Device;
using VideoClient.Service.Device.impls;
using System.Windows.Interop;
using System.Windows.Forms.Integration;

namespace VideoClient.ViewModel
{
    class DeviceViewModel : ViewModelBase
    {
        public static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region 属性
        uint rotationSpeed = 1;                          //云台旋转速度,初始为1
        List<DeviceDTO> allDeviceInfo;              //保存的设备信息
        List<Hie_MGTC26_Device> allConnectDevice;   //保存已经连接好的设备
        Hie_MGTC26_Device operatorDevice;           //当前操作的设备

        public List<DeviceDTO> AllDeviceInfo
        {
            get { return allDeviceInfo; }
            set { allDeviceInfo = value; }
        }

        public uint RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }

        public List<Hie_MGTC26_Device> AllConnectDevice
        {
            get { return allConnectDevice; }
            set { ; }
        }

        public Hie_MGTC26_Device OperatorDevice
        {
            get { return OperatorDevice; }
            set { ; }
        }

        #endregion

        #region 构造方法
        public DeviceViewModel()
        {
            //命令与事件对应起来
            StreamPlayCommand = new RelayCommand<UserControl>((e) => StreamPlayCommandHandler(e));
            StreamPauseCommand = new RelayCommand(() => StreamPauseCommandHandler());
            StreamResumeCommand = new RelayCommand(() => StreamResumeCommandHandler());
            StreamStopCommand = new RelayCommand(() => StreamStopCommandHandler());
            OpenAuxiEquipCommand = new RelayCommand<object>((e) => OpenAuxiEquipCommandHandler(e));
            CloseAuxiEquipCommand = new RelayCommand<object>((e) => CloseAuxiEquipCommandHandler(e));
            PresetSetCommand = new RelayCommand<object>((e) => PresetSetCommandHandler(e));
            PresetCallCommand = new RelayCommand<object>((e) => PresetCallCommandHandler(e));
            PresetClearCommand = new RelayCommand<object>((e) => PresetClearCommandHandler(e));
            CuriserAddPresetCommand = new RelayCommand<object>((e) => CuriserAddPresetCommandHandler(e));
            CuriserAddPresetOKCommand = new RelayCommand<object>((e) => CuriserAddPresetOKCommandHandler(e));
            CuriserRunStartCommand = new RelayCommand<object>((e) => CuriserRunStartCommandHandler(e));
            CuriserRunStopCommand = new RelayCommand<object>((e) => CuriserRunStopCommandHandler(e));


            

            //由于无法和服务器通信
            //手动设置一个DeviceDTO
            //本该使用IDeviceService.getAllDevices();获取设备信息

            //暂时使用的是给一个数据
            DeviceDTO devicedto1 = new DeviceDTO();
            devicedto1.Ip = "192.168.1.10";
            devicedto1.LoginName = "Admin";
            devicedto1.CmdPort = "5050";
            devicedto1.Pwd = "888888";

            DeviceDTO devicedto2 = new DeviceDTO();
            devicedto2.Ip = "192.168.1.10";
            devicedto2.LoginName = "Admin";
            devicedto2.CmdPort = "5050";
            devicedto2.Pwd = "888888";

            allDeviceInfo = new List<DeviceDTO>();
            allConnectDevice = new List<Hie_MGTC26_Device>();
            allDeviceInfo.Add(devicedto1);
            allDeviceInfo.Add(devicedto2);

            OpenDevice(devicedto1);


        }
        #endregion

        #region 命令
        public RelayCommand<UserControl> StreamPlayCommand { get; private set; }  //实时流播放
        public RelayCommand StreamPauseCommand { get; private set; }  //实时流暂停
        public RelayCommand StreamResumeCommand { get; private set; } //实时流恢复
        public RelayCommand StreamStopCommand { get; private set; }   //实时流停止
        public RelayCommand<Object> OpenAuxiEquipCommand { get; private set; } //打开辅助设备
        public RelayCommand<Object> CloseAuxiEquipCommand { get; private set; } //打开辅助设备
        public RelayCommand<Object> PresetSetCommand { get; private set; }  //设置预置点
        public RelayCommand<Object> PresetCallCommand { get; private set; }  //转到预置点 
        public RelayCommand<Object> PresetClearCommand { get; private set; }  //清除预置点 
        public RelayCommand<Object> CuriserAddPresetCommand { get; private set; }  //将预置点加入巡航序列
        public RelayCommand<Object> CuriserRunStartCommand { get; private set; }  //开始巡航 
        public RelayCommand<Object> CuriserRunStopCommand { get; private set; }  //停止巡航
        public RelayCommand<Object> CuriserAddPresetOKCommand { get; private set; }  //停止巡航
        
        #endregion

        #region 事件
        //点击设备的打开按钮或双击设备树上的设备对应的事件
        //这里obj是绑定的数据
        private void OpenDevice(Object obj)
        {
            DeviceDTO deviceDTO = (DeviceDTO)obj;
            Hie_MGTC26_Device device = CreateDevice();
            try
            {
                DeviceInit(device);
                Login(device, deviceDTO);
                ////由于没有做打开设备的命令，所以先给operatorDevice赋值
                operatorDevice = device;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }

        }
        //播放
        //这里的obj是播放的panel
        //调用播放的初始条件: 双击,点击播放按钮
        private void StreamPlayCommandHandler(UserControl userControl)
        {
            PlayerView playerView = (userControl as PlayerView);
            System.Windows.Forms.Panel panel = playerView.video;
            try
            {
                //实时流的连接开启
                RealStreamConnectOpen(operatorDevice);
                //播放
                RealStreamPlay(operatorDevice, panel);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void StreamPauseCommandHandler()
        {
            try
            {
                RealStreamPause(operatorDevice);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void StreamResumeCommandHandler()
        {
            try
            {
                RealStreamResume(operatorDevice);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void StreamStopCommandHandler()
        {
            try
            {
                RealStreamStop(operatorDevice);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //这两个主要是无法在xaml里绑定
        public void PtzUpControlCommandHandler(object sender, RoutedEventArgs e)
        {
            button_ptz_MouseUp(sender);
        }
        public void PtzDownControlCommandHandler(object sender, RoutedEventArgs e)
        {
            button_ptz_MouseDown(sender);
        }

        private void OpenAuxiEquipCommandHandler(object sender)
        {
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;

            ComboBox cbox = (ComboBox)sender;
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
        private void CloseAuxiEquipCommandHandler(object sender)
        {
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;

            ComboBox cbox = (ComboBox)sender;
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

        private void PresetSetCommandHandler(Object sender)
        {
            PtzControl( Common.ePTZControlCmdCode.ePTZControlCodePresetSet,System.UInt32.Parse(((ComboBox)sender).Text));
        }

        private void PresetCallCommandHandler(Object sender)
        {
            PtzControl(Common.ePTZControlCmdCode.ePTZControlCodePresetCall, System.UInt32.Parse(((ComboBox)sender).Text));
        }

        private void PresetClearCommandHandler(Object sender)
        {
            PtzControl(Common.ePTZControlCmdCode.ePTZControlCodePresetClear, System.UInt32.Parse(((ComboBox)sender).Text));
        }

        private void CuriserAddPresetCommandHandler(Object sender)
        {
            uint curiserIndex = System.UInt32.Parse(((ComboBox)sender).Text);
            ViewDevice.CuriserAddPreset curiserAddPresetWindow = new ViewDevice.CuriserAddPreset(curiserIndex);
            curiserAddPresetWindow.Show();
        }

        private void CuriserAddPresetOKCommandHandler(Object sender)
        {
            ViewDevice.CuriserAddPreset curiserAddPresetWindow = (ViewDevice.CuriserAddPreset)sender;
            uint curiserIndex = curiserAddPresetWindow.CuriserIndex;
            List<ViewDevice.CuriserPreset> allPreset = (List<ViewDevice.CuriserPreset>)(curiserAddPresetWindow.curiserPreset.ItemsSource);
            curiserAddPresetWindow.Close();
            PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStart, curiserIndex);

            for (uint i = 1; i <= 3; i++) {

                    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCuriserAddPreset,
                               curiserIndex,
                               i,
                               1,
                               5);

            }
            PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStop, curiserIndex);
            
            //foreach (ViewDevice.CuriserPreset curiserPreset in allPreset)
            //    PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCuriserAddPreset,
            //               curiserIndex,
            //               System.UInt32.Parse(curiserPreset.presetIndex),
            //               System.UInt32.Parse(curiserPreset.remainTime),
            //               System.UInt32.Parse(curiserPreset.speed));
        }

        private void CuriserRunStartCommandHandler(Object sender)
        {
            PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCuriserRunStart, System.UInt32.Parse(((ComboBox)sender).Text));
        }

        private void CuriserRunStopCommandHandler(Object sender)
        {
            PtzControl(Common.ePTZControlCmdCode.ePTZControlCodeCuriserRunStop, System.UInt32.Parse(((ComboBox)sender).Text));
        }

        #endregion

        #region 事件用到的方法
        //ePTZCtrlCmdCode:  控制码类型
        //dwCmdCode1:   PTZ 命令参数 1: 开始,0：停止
        //dwCmdCode2:   
        //dwCmdCode3:
        //dwCmdCode4:
        private void PtzControl(Common.ePTZControlCmdCode ePTZCtrlCmdCode, uint dwCmdCode1,
                                         uint dwCmdCode2 = 5, uint dwCmdCode3 = 0, uint dwCmdCode4 = 0)
        {//PTZ控制台调用
            try
            {
                //待完善,多通道选择......
                uint dwChnnl = 0;                                                 //通道编号: 1
                dwCmdCode2 = rotationSpeed;

                operatorDevice.Client_ptzControl.PtzControl(operatorDevice.Client_userLogin.UserInfo, dwChnnl,
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
        private void button_ptz_MouseDown(Object obj)//鼠标在button上并点击
        {
            Button btn = (Button)obj;
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
            ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
            PtzControl(ePTZCtrlCmdCode, 1);
        }
        private void button_ptz_MouseUp(Object obj)//在鼠标指针在控件上并释放鼠标键时发生。
        {
            Button btn = (Button)obj;
            Common.ePTZControlCmdCode ePTZCtrlCmdCode = Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop;
            ePTZCtrlCmdCode = (Common.ePTZControlCmdCode)button_ptzcontrolMode[btn.Name];
            PtzControl(ePTZCtrlCmdCode, 0);
        }
        //使用设备向导创建一个设备
        private Hie_MGTC26_Device CreateDevice()
        {
            DeviceDirector director = new DeviceDirector();
            AbstractDeviceBuilder builder = new Hie_MGTC26_Builder();
            director.Construct(builder);
            allConnectDevice.Add((Hie_MGTC26_Device)builder.getDevice());
            return (Hie_MGTC26_Device)builder.getDevice();
        }
        //设备SDK初始化
        private void DeviceInit(Hie_MGTC26_Device device)
        {
            if (device == null) throw new Exception("device==null,无设备");
            device.Client_init.init(Service.CallDLL.interfaces.DecoderCardWorkMode.WorkMode_CIF);//这个地方是说明景的这台机器用的是CIF视频编码？？？
            device.Client_init.Client_Start();//那Client_Start的返回值没有用啊
        }
        //用户登录设备
        private void Login(Hie_MGTC26_Device device, DeviceDTO deviceDTO)
        {
            if (device == null) throw new Exception("device==null,无设备");
            if (deviceDTO == null) throw new Exception("deviceDTO==null,无设备信息");
            try
            {
                device.Client_userLogin.Login(deviceDTO.Ip, deviceDTO.CmdPort, deviceDTO.LoginName, deviceDTO.Pwd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "--登录信息错误,登录失败");
            }
        }
        //实时流的连接,开启
        private void RealStreamConnectOpen(Hie_MGTC26_Device device)
        {
            if (device == null) throw new Exception("device==null,无设备");
            try
            {
                device.Client_realStream.RealStreamConnect(device.Client_userLogin);
                device.Client_streamControl.StreamOpen(device.Client_realStream.HRealStream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Source + " " + ex.Message);
            }

        }
        //实时流的播放
        private void RealStreamPlay(Hie_MGTC26_Device device, System.Windows.Forms.Panel video)
        {
            if (device == null) throw new Exception("device==null,无设备");
            if (video == null) throw new Exception("video==null,无显示Panel");
            try
            {
                device.Client_streamControl.StreamPlay(device.Client_realStream.HRealStream, video.Handle);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Source + " " + ex.Message);
            }

        }
        //实时流的暂停
        private void RealStreamPause(Hie_MGTC26_Device device)
        {
            if (device == null) throw new Exception("device==null,无设备");
            try
            {
                device.Client_streamControl.StreamPause(device.Client_realStream.HRealStream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Source + " " + ex.Message);
            }

        }
        //实时流的恢复
        private void RealStreamResume(Hie_MGTC26_Device device)
        {
            if (device == null) throw new Exception("device==null,无设备");
            try
            {
                device.Client_streamControl.StreamResume(device.Client_realStream.HRealStream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Source + " " + ex.Message);
            }

        }
        //实时流的停止
        private void RealStreamStop(Hie_MGTC26_Device device)
        {
            if (device == null) throw new Exception("device==null,无设备");
            try
            {
                device.Client_streamControl.StreamStop(device.Client_realStream.HRealStream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Source + " " + ex.Message);
            }
        }
        #endregion

        //虽然这里这样做很不好，但是省事啊
        #region 前台元素名字对应的云台操作码的哈希表
        private Hashtable button_ptzcontrolMode = new Hashtable { 
            //----------------用到mouse事件
            {"buttonUp", Common.ePTZControlCmdCode.ePTZControlCodeUp },
            {"buttonDown", Common.ePTZControlCmdCode.ePTZControlCodeDown},
            {"buttonLeft", Common.ePTZControlCmdCode.ePTZControlCodeLeft},
            {"buttonRight", Common.ePTZControlCmdCode.ePTZControlCodeRight},
            {"buttonZoomIn", Common.ePTZControlCmdCode.ePTZControlCodeZoomIn},
            {"buttonZoomOut", Common.ePTZControlCmdCode.ePTZControlCodeZoomOut},
            {"buttonApertureUp", Common.ePTZControlCmdCode.ePTZControlCodeApertureUp},
            {"buttonApertureDown", Common.ePTZControlCmdCode.ePTZControlCodeApertureDown},
            {"buttonFocusFar", Common.ePTZControlCmdCode.ePTZControlCodeFocusFar},
            {"buttonFocusNear", Common.ePTZControlCmdCode.ePTZControlCodeFocusNear},
            //----------------用到click事件
            {"加热器", Common.ePTZControlCmdCode.ePTZControlCodeHeater},
            {"摄像机", Common.ePTZControlCmdCode.ePTZControlCodeCameraPower},
            {"灯光", Common.ePTZControlCmdCode.ePTZControlCodeLightPower},
            {"雨刷", Common.ePTZControlCmdCode.ePTZControlCodeWiper},
            {"风扇", Common.ePTZControlCmdCode.ePTZControlCodeFans},
            {"buttonAutoZoom", Common.ePTZControlCmdCode.ePTZControlCodeFans},//开自动焦距（自动倍率）
            {"buttonAutoFocus", Common.ePTZControlCmdCode.ePTZControlCodeFans},//开自动调焦
            {"buttonAutoAperture", Common.ePTZControlCmdCode.ePTZControlCodeFans},//开自动光圈};
            {"buttonPresetSet", Common.ePTZControlCmdCode.ePTZControlCodePresetSet},//设置预置点
            {"buttonPresetClear", Common.ePTZControlCmdCode.ePTZControlCodePresetClear},//清除预置点
            {"buttonPresetCall", Common.ePTZControlCmdCode.ePTZControlCodePresetCall},//转到预置点
            {"buttonCuriserSetStart", Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStart},//启动巡航记忆
            {"buttonCuriserSetStop", Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStop},//关闭巡航记忆
            {"buttonCuriserAddPreset", Common.ePTZControlCmdCode.ePTZControlCodeCuriserAddPreset},//将预置点加入巡航序列
            {"buttonCuriserRunStart", Common.ePTZControlCmdCode.ePTZControlCodeCuriserRunStart}//开始巡航
        };
        #endregion
    }
}

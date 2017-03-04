
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HieCiULib;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
namespace TestWindow.tools
{
    /// <summary>
    /// Version:1.0
    /// Date:2013/12/18
    /// Author:wangd933
    /// </summary>
    class DeviceProbe{
        /* @brief 
         * 给前台的表格添加信息的委托回调函数(由于本类或者说执行该类的
         * 线程没有操纵主线程UI的权限,所以建立这个委托,希望有可以操纵
         * 主线程UI的家伙做这件事)
         * @param rowinfo
         * 设备信息表格信息
         * @return
         * void
         */
        public delegate void AddRowInfo_CALLBACK(string[] rowinfo);

        /*设备配置信息哈希表,key是设备mac地址*/
        private Hashtable deviceconfig ;
        private Hashtable devicetype;

        private Form devicemanage;
        private DeviceProbe.AddRowInfo_CALLBACK addrowinfo_callback;
        private int branchthreadID;
        //锁住判断hash表里的键和向hash表里添加键的这两个过程
        private System.Object lockThis;

        /*如果是使用回调的话,那么这个变量的生命周期必须和该类的生命周期一致,切记..
         但是没搞懂c#何时释放资源..*/
        private Common.CB_DeviceProbe cbDeviceProbe;

        public DeviceProbe(Form devicemanage,
            ref DeviceProbe.AddRowInfo_CALLBACK addrowinfo_callback, int branchthreadID)
        {
            deviceconfig = new Hashtable();

            devicetype = new Hashtable();
            //devicetypeinit

            lockThis = new System.Object();

            this.devicemanage = devicemanage;//获取当前窗口控件
            
            this.addrowinfo_callback = addrowinfo_callback;

            this.branchthreadID = branchthreadID;//获得分配给支线程的ID号

        }
        /* @brief 
         * 配置指定设备函数
         * @param cConfig
         * 该设备的新的设备配置信息(该信息必须按表格的显示顺序排列)
         * @return
         * void
         */
        public void configuredevice(string[] cConfig)
        {
            if (deviceconfig != null) {
                Common.DeviceProbeConfig commoncConfig =
                    string_DeviceProbeConfig_to_Common_DeviceConfig(cConfig,false);

                int devicesetconfig = -1;
                devicesetconfig = HieCIU.HieCIU_DeviceProbeSetDeviceConfig(
                    commoncConfig.m_bytDevMac, commoncConfig.m_bytDevDetectPort, ref commoncConfig);
                if (devicesetconfig != 0)
                    throw new Exception(" 设备探测信息配置失败. " + Constants.ErrorConstants.getErrorString(devicesetconfig));
            }
        }
        /* @brief 
         * 配置所有设备函数
         * @param cConfig
         * 该设备的新的设备配置信息(该信息必须按表格的显示顺序排列)
         * @return
         * void
         */
        public void configurealldevice(string[] cConfig, ushort startport, ushort endport)
        {
            if (deviceconfig != null)
            {
                Common.DeviceProbeConfig commoncConfig =
                    string_DeviceProbeConfig_to_Common_DeviceConfig(cConfig,true);

                int devicesetallconfig = -1;
                Common.DeviceProbeParameter cParameter = new Common.DeviceProbeParameter();
                cParameter.m_wBeginPort = startport;
                cParameter.m_wEndPort = endport;
                devicesetallconfig = HieCIU.HieCIU_DeviceProbeSetAllDeviceConfig(ref cParameter, ref commoncConfig);
                if (devicesetallconfig != 0)
                    throw new Exception(" 设备探测信息配置失败. " + Constants.ErrorConstants.getErrorString(devicesetallconfig));
            }
        }

        /* @brief 
         * 设备探测函数
         * @param ref  addrowinfo_callback
         * 设备信息表格更新回调函数
         * @param startport
         * 探测其实端口
         * @param endport
         * 探测结束端口
         * @return
         * void
         */
        public void Deviceprobe(ushort startport, ushort endport)
        {
            int deviceprobestart = -1;//设备探测是否成功
            Common.DeviceProbeParameter cParameter = new Common.DeviceProbeParameter();
            cParameter.m_wBeginPort = startport;
            cParameter.m_wEndPort = endport;
            cbDeviceProbe = new Common.CB_DeviceProbe(callback_DeviceProbe);
            deviceprobestart = HieCIU.HieCIU_DeviceProbeStart(ref cParameter, cbDeviceProbe);
            if (deviceprobestart != 0)
                throw new Exception("设备探测启动失败." + Constants.ErrorConstants.getErrorString(deviceprobestart));
            
        }
        /* @brief 
         * 停止设备探测
         * @param 
         * void
         * @return
         * void
         */
        public void stopdevicepro() {

            int deviceprobestop = -1;
            deviceprobestop = HieCIU.HieCIU_DeviceProbeStop();
            if ( deviceprobestop != 0 )
                throw new Exception("设备探测停止失败. " + Constants.ErrorConstants.getErrorString(deviceprobestop));

        }

        /* @brief 
         * 设备探测回调函数,广播信号找到了探测端口范围内的设备,则调用该回调函数
         * @param ref  Common.DeviceProbeConfig
         * 封装的DeviceConfigInfo设备配置信息
         * @return
         * int
         */
        private void callback_DeviceProbe(ref Common.DeviceProbeConfig cConfig)
        {                       
             //按唯一mac地址存储设备信息
             string m_devmac = String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
                    cConfig.m_bytDevMac[0],cConfig.m_bytDevMac[1],cConfig.m_bytDevMac[2],
                    cConfig.m_bytDevMac[3],cConfig.m_bytDevMac[4],cConfig.m_bytDevMac[5]);

             lock (lockThis)
             {
                 if (!deviceconfig.ContainsKey(m_devmac))
                 {
                     deviceconfig.Add(m_devmac, cConfig);
                     //获得转换后的需要在前台显示,并可被用户修改的数据
                     string[] newinfo = Common_DeviceProbeConfig_to_string_DeviceConfig(cConfig);
                     //第一个字符串是分支线程ID
                     string[] deviceconfigstring = new string[1 + newinfo.Length];
                     deviceconfigstring[0] = branchthreadID.ToString();
                     for (int i = 1; i < deviceconfigstring.Length; ++i)
                         deviceconfigstring[i] = newinfo[i - 1];

                     if (devicemanage.InvokeRequired)
                         devicemanage.BeginInvoke(addrowinfo_callback, new object[] { deviceconfigstring });                 
                 }
             }
             return ;
        }

        /* @brief 
         * 封装的DeviceConfigInfo(Common.DeviceProbeConfig)转为适合前台的设备配置信息
         * @param   cConfig
         * 封装的DeviceConfigInfo设备配置信息
         * @return
         * 返回适合前台的设备配置信息(DeviceConfigInfo)
         */
        private string[] Common_DeviceProbeConfig_to_string_DeviceConfig(Common.DeviceProbeConfig cConfig)
        {        
            string[] info =  {
                    //服务类型
                    //Char.ConvertFromUtf32(cConfig.m_bytDevType), 
                    tools.DeviceTypecs.GetDeviceType(cConfig.m_bytDevType),
                    //MAC地址
                    String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
                        cConfig.m_bytDevMac[0],cConfig.m_bytDevMac[1],cConfig.m_bytDevMac[2],
                        cConfig.m_bytDevMac[3],cConfig.m_bytDevMac[4],cConfig.m_bytDevMac[5]),
                    //IP地址
                    System.Text.Encoding.ASCII.GetString ( cConfig.m_bytDevIP ),
                    //子网掩码
                    System.Text.Encoding.ASCII.GetString ( cConfig.m_bytDevMask ),
                    //网关地址
                    System.Text.Encoding.ASCII.GetString ( cConfig.m_bytDevGateway ),
                    //管理主机IP
                    System.Text.Encoding.ASCII.GetString ( cConfig.m_bytDevManHost ),
                    //管理主机端口
                    cConfig.m_wManHostPort.ToString(),      
                    //自动注册
                    (cConfig.m_bytDevAutoReg == 1)?"启用":"关闭",
                    //注册间隔
                    cConfig.m_bytDevRegInterval.ToString(),
                    //探测端口
                    cConfig.m_bytDevDetectPort.ToString(),
                    //命令端口
                    cConfig.m_wCmdPort.ToString(),
                    //媒体端口
                    cConfig.m_wMediaPort.ToString(),
                    //通道数
                    cConfig.m_bytDevChan.ToString()                   
                    };
             return info;
        }
        /* @brief 
        * 前台的设备配置信息转为适合封装的DeviceConfigInfo(Common.DeviceProbeConfig)
        * @param   cConfig
        * 前台的设备配置信息
        * @return
        * 返回封装的DeviceConfigInfo(Common.DeviceProbeConfig)
        */
        private Common.DeviceProbeConfig string_DeviceProbeConfig_to_Common_DeviceConfig(string[] cConfigstring,Boolean AllDevice)
        {
            Common.DeviceProbeConfig cConfig = (Common.DeviceProbeConfig)deviceconfig[cConfigstring[1]];
            //表格里下标7：是否自动注册
            cConfig.m_bytDevAutoReg = (byte)((cConfigstring[7].Equals("启用")) ? 1 : 0);
            //表格里下标8：设备注册间隔时间
            cConfig.m_bytDevRegInterval = Convert.ToByte(cConfigstring[8]);
            //表格里下标2：设备当前IP地址
            if (!AllDevice )
                cConfig.m_bytDevIP = System.Text.Encoding.ASCII.GetBytes ( cConfigstring[2] );
            //表格里下标3：设备IP地址掩码
            cConfig.m_bytDevMask = System.Text.Encoding.ASCII.GetBytes( cConfigstring[3] );
            //表格里下标4：设备网关地址
            cConfig.m_bytDevGateway = System.Text.Encoding.ASCII.GetBytes( cConfigstring[4] );
            //表格里下标5：设备管理主机地址
            cConfig.m_bytDevManHost = System.Text.Encoding.ASCII.GetBytes( cConfigstring[5] );
            //表格里下标6：设备管理主机端口
            cConfig.m_wManHostPort = Convert.ToUInt16(cConfigstring[6]);
            //表格里下标9：设备探测端口
            cConfig.m_bytDevDetectPort = Convert.ToUInt16(cConfigstring[9]);
            //表格里下标10：设备命令端口
            cConfig.m_wCmdPort = Convert.ToUInt16(cConfigstring[10]);
            //表格里下标11：设备媒体端口
            cConfig.m_wMediaPort = Convert.ToUInt16(cConfigstring[11]);
            //有效数据位,表明设备探测配置信息可修改
            cConfig.m_dwValidMask = 0x1FEC;

            return cConfig;
        }
        /*探测设备是否在线
         */
        public static Boolean DeviceIsOnline(string cAddress, uint dwCommandPort, uint dwTimeOut)
        {
            int deviceisonline = -1;
            deviceisonline = HieCIU.HieCIU_DeviceIsOnline(System.Text.Encoding.ASCII.GetBytes(cAddress), dwCommandPort, dwTimeOut);
            if( deviceisonline  == 1 )
                return false;
            return true;
        }
    }
}

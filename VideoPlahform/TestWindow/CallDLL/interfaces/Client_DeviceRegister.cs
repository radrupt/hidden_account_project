using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 设备注册事件的委托
    /// </summary>
    /// <param name="value"></param>
    public delegate void DeviceRegistered(Hashtable info);

    /// <summary>
    /// 对设备注册管理相关的接口
    /// 
    /// Version:1.0
    /// Date:2012/06/01
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_DeviceRegister
    {
        /// <summary>
        /// 注册设备事件
        /// </summary>
        event DeviceRegistered DeviceRegister;

        /// <summary>
        /// 当设备被注册以后触发此事件
        /// </summary>
        /// <param name="e"></param>
        void OnDeviceRegistered(Hashtable e);

        /// <summary>
        /// 启动监听程序，监听设备的注册请求 
        /// </summary>
        /// <param name="ipAddress">PC 机的 IP 地址，如果为 NULL，SDK 将自动获取 PC 机的 IP 地址，
        /// 如果 PC 机有多个 IP 地址，可以指定一个 IP 地址进行监听。 </param>
        /// <param name="port">本地监听端口号，由用户设置</param>
        void DeviceRegisterListenStart(string ipAddress,int port);

        /// <summary>
        /// 停止监听程序
        /// </summary>
        void DeviceRegisterListenStop();
    }
}

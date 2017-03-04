using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 通道数据接收完成事件的代理
    /// </summary>
    /// <param name="value">相关信息</param>
    public delegate void TransDataReceived(string value);

    /// <summary>
    /// 主类型(串口)枚举信息
    /// </summary>
    public enum SerialType
    {
        TTY232 = 0, //RS232串口
        TTY485,     //RS485串口
        TTY422      //RS422串口
    }

    /// <summary>
    /// 透明通道用于在客户端和设备端之间收发信息，信息格式由接收方和发送方自行约定。
    /// 透明通道不再使用时，需调用断开操作，以释放资源。
    /// 
    /// Version:1.0
    /// Date:2012/06/20
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_TransparentChannel
    {
        /// <summary>
        /// 通道数据接收完成事件
        /// </summary>
        event TransDataReceived TransDataReceive;

        /// <summary>
        /// 通道数据接收完成触发函数
        /// </summary>
        /// <param name="e">相关信息</param>
        void OnTransDataReceived(string e);

        /// <summary>
        /// 透明通道连接操作
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="majorType">主类型</param>
        /// <param name="minorType">辅助类型</param>
        /// <param name="isCallBack">是否设置回调</param>
        /// <returns>透明通道句柄</returns>
        IntPtr TransparentChannelConnect(Client_UserLogin userLogin, SerialType majorType, uint minorType, bool isCallBack);

        /// <summary>
        /// 透明通道断开操作
        /// 透明通道不再使用时，需调用断开操作，以释放资源。
        /// </summary>
        /// <param name="tranHandle">透明通道句柄</param>
        void TransparentChannelDisconnect(IntPtr tranHandle);

        /// <summary>
        /// 透明通道数据发送
        /// </summary>
        /// <param name="tranHandle">透明通道句柄，通过连接透明通道获取</param>
        /// <param name="bufContent">透明通道数据,字符串形式</param>
        void TransparentChannelWrite(IntPtr tranHandle,string bufContent);

    }
}

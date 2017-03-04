using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 流媒体枚举类型
    /// </summary>
    public enum StreamMediaType
    {
        MainVideoAndSound = 0,  //主码流音视频  
        MainVideo,  //主码流视频 
        MainSound,  //主码流音频 
        AssistVideo, //子码流视频 
    }

    /// <summary>
    /// 流媒体传输枚举类型
    /// </summary>
    public enum StreamTransferMode
    {
        GeneralTCP = 0,  //普通的TCP 
        GeneralRTP  //普通的RTP（暂不支持）
    }

    /// <summary>
    /// 提供处理实时流连接和断开连接操作
    /// 
    /// Version:1.0
    /// Date:2012/06/04
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_RealStream
    {
        /// <summary>
        /// 实时流句柄，初始化为IntPtr(-1)
        /// </summary>
        IntPtr HRealStream
        {
            get;
            set;
        }

        /// <summary>
        /// 建立默认的实时流连接
        /// </summary>
        /// <param name="userLogin">登录用户以后的用户对象</param>
        void RealStreamConnect(Client_UserLogin userLogin);
        
        /// <summary>
        /// 建立可以设定详细参数的实时流连接
        /// </summary>
        /// <param name="userLogin">登录用户以后的用户对象</param>
        /// <param name="Channel">通道（default = 0 表示通道1）</param>
        /// <param name="smt">流媒体枚举类型 </param>
        /// <param name="stm">流媒体传输枚举类型</param>
        void RealStreamConnect(Client_UserLogin userLogin, uint Channel, StreamMediaType smt,StreamTransferMode stm);

        /// <summary>
        /// 断开实时流连接
        /// </summary>
        void RealStreamDisconnect();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 音频对讲委托
    /// </summary>
    /// <param name="voiceHandle">获得的音频句柄</param>
    /// <param name="info">获得音频信息</param>
    public delegate void Nettalked(IntPtr voiceHandle, string info);

    /// <summary>
    /// 语音编解码模式 枚举
    /// </summary>
    public enum VoiceMode
    {
        eVoiceModeBegin = 0,  //起始值 
        eVoiceG726,  //G726编解码模式 
        eVoiceG711U,  //G711U编解码模式 
        eVoiceModeEnd  //结束值 
    }

    /// <summary>
    /// 语音流相关的操作
    /// 
    /// Version:1.0
    /// Date:2012/06/19
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_VoiceStream
    {
        /// <summary>
        /// 音频对讲事件
        /// </summary>
        event Nettalked Nettalk;

        /// <summary>
        /// 音频对讲事件被触发函数
        /// </summary>
        /// <param name="voiceHandle">音频句柄</param>
        /// <param name="e">相关信息</param>
        void OnNettalk(IntPtr voiceHandle,string e);

        /// <summary>
        /// 连接语音流 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channel">传输信道</param>
        /// <param name="vm">音频模式</param>
        /// <param name="stm">流传输模式</param>
        /// <param name="isCallBack">是否回调</param>
        /// <returns>已经连接的音频流句柄</returns>
        IntPtr VoiceStreamConnect(Client_UserLogin userLogin,uint channel,VoiceMode vm,StreamTransferMode stm,bool isCallBack);

        /// <summary>
        /// 断开语音流 
        /// </summary>
        /// <param name="voiceHandle">音频流句柄</param>
        void VoiceStreamDisconnect(IntPtr voiceHandle);
    }
}

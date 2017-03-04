using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HUSER = System.IntPtr;
namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 值改变事件代理
    /// </summary>
    /// <param name="value">改变的值</param>
    public delegate void ValueChanged(string value);

    /// <summary>
    /// 订阅事件接口
    /// 
    /// Version:1.0
    /// Date:2012/06/01
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_SubscribeEvent
    {
        /// <summary>
        /// 用户句柄
        /// </summary>
        HUSER HUser
        {
            get;
        }

        /// <summary>
        /// 值改变事件
        /// </summary>
        event ValueChanged ValueChange;

        /// <summary>
        /// 当值改变的时候触发
        /// </summary>
        /// <param name="e">改变的值</param>
        void OnValueChanged(string e);

        /// <summary>
        /// 订阅所有的事件
        /// </summary>
        /// <param name="client_login">用户登录信息</param>
        void SubscribeAllEvent(Client_UserLogin client_login);

        /// <summary>
        /// 根据事件类型订阅事件
        /// </summary>
        /// <param name="client_login">用户登录信息</param>
        /// <param name="eventType">事件类型</param>
        void SubscribeEventByType(Client_UserLogin client_login,uint eventType);
    }
}

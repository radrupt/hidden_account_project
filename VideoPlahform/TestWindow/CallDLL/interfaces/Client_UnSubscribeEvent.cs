using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 取消订阅事件对应的接口
    /// 
    /// Version:1.0
    /// Date:2012/06/04
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_UnSubscribeEvent
    {
        /// <summary>
        /// 取消订阅所有事件
        /// </summary>
        /// <param name="client_login">用户登录信息</param>
        /// <returns>所有事件类型</returns>
        void UnSubscribeAllEvent(Client_UserLogin client_login);
        
        /// <summary>
        /// 取消订阅指定类型的事件
        /// </summary>
        /// <param name="client_login">用户登录信息</param>
        /// <param name="eventType">事件类型</param>
        /// <returns>事件类型</returns>
        void UnSubscribeEventByType(Client_UserLogin client_login, uint eventType);
    }
}

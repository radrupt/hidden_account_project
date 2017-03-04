using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 语言枚举类型
    /// </summary>
    public enum eLanguage
    {
        SimplifiedChinese,
        English,
        TraditionalChinese
    }

    /// <summary>
    /// 用于处理设置设备参数
    ///  
    /// Version:1.0
    /// Date:2012/06/01
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_DeviceConfig
    {
        /// <summary>
        /// 用于处理设置设备参数
        /// </summary>
        /// <param name="userLogin">登录用户句柄</param>
        /// <param name="els">显示的语言枚举类型</param>
        /// <param name="userName">用户名</param>
        void DeviceConfig(Client_UserLogin userLogin, eLanguage els, string userName);
    }
}

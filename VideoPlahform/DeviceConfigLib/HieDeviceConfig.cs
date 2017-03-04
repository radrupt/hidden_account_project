using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HieCiULib
{
    /// <summary>
    /// 用来设置设备配置的信息DLL接口类
    /// 
    /// Version:1.0
    /// Date:2012/06/01
    /// Author:baobaoyeye
    /// </summary>
    /// <remarks>此类最好结合订阅用户事件一起使用，尤其是当设置网络相关的信息时候</remarks>
    public class HieDeviceConfig
    {
        /// <summary>
        /// 配置显示语言枚举类型
        /// </summary>
        public enum eLanguageSelect
        {
	        eLanguageBegin = 0,
	        eSimplifiedChinese,
	        eEnglish,
	        eTraditionalChinese,
	        eLanguageEnd,
        }

        /// <summary>
        /// 启动配置服务
        /// </summary>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>启动服务后,不再需要服务时请调用停止服务接口 接口类型: 阻塞式</remarks>
        [DllImport("HieDeviceConfig.dll", EntryPoint = "HieCFG_Start", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HieCFG_Start();

        /// <summary>
        /// 停止配置服务
        /// </summary>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieDeviceConfig.dll", EntryPoint = "HieCFG_Stop", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HieCFG_Stop();

        /// <summary>
        /// 配置接口
        /// </summary>
        /// <param name="nLoginHandle">用户句柄，用户登陆后得到登陆句柄</param>
        /// <param name="language">语言选择（简体中文、繁体中文、英文，目前不支持繁体中文）当英文、繁体中文资源加载失败时，使用简体中文资源</param>
        /// <param name="userName">用户名</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieDeviceConfig.dll", EntryPoint = "HieCFG_Configutation", CallingConvention=CallingConvention.Cdecl)]
        public static extern int HieCFG_Configutation(int nLoginHandle,eLanguageSelect language,string userName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HUSER = System.IntPtr;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 用户登录操作接口
    /// 
    /// Version:1.0
    /// Date:2012/06/04
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_UserLogin
    {
        /// <summary>
        /// 用户登录句柄
        /// </summary>
        HUSER UserInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 处理用户登录，此方法前需要确定参数合法性
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <remarks>此操作为阻塞操作 处理此操作失败会产生异常</remarks>
        void Login(string ip, string port, string userName, string password);
    }
}

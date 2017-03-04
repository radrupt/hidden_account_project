using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 用户登出操作接口
    /// 
    /// Version:1.0
    /// Date:2012/06/04
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_UserLogout
    {
        /// <summary>
        /// 处理用户退出，处理过程阻塞
        /// </summary>
        /// <param name="userLogin">用户登录对象</param>
        /// <remarks>此操作失败会产生异常</remarks>
        void Logout(Client_UserLogin userLogin);
    }
}

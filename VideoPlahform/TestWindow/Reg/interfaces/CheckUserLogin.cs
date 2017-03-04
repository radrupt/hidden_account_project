using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.Reg.interfaces
{
    interface CheckUserLogin
    {
        /// <summary>
        /// 测试用户登录参数是否正确
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果登录正确返回空串，否则返回错误原因</returns>
        string isLegalLoginString(string ip, string port, string userName, string password);
    }
}

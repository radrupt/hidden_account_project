using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HUSER = System.IntPtr;
using HieCiULib;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_UserLogin_Impl : interfaces.Client_UserLogin
    {
        private HUSER userInfo = new Constants.CConstants().INVALID_HANDLE_VALUE;

        void interfaces.Client_UserLogin.Login(string ip, string port, string userName, string password)
        {
            int loginCode = -1;

            HUSER hUserlogin = new Constants.CConstants().INVALID_HANDLE_VALUE;
            Common.UserLoginPara tLoginPara = new Common.UserLoginPara();
            tLoginPara.dwCommandPort = Convert.ToUInt32(port);
            tLoginPara.sServerIP = ip;
            tLoginPara.sUName = userName;
            tLoginPara.sUPass = password;
            //Unit是链接HieClientUnit.dll的文件
            //hUserlogin：用户句柄，大概是当一个用户登录成功后，用户客户端和设备端之间就可以通过这个句柄进行各种操作了
            //tLoginPara：用户登录信息
            loginCode = Unit.HieClient_UserLogin(ref hUserlogin, ref tLoginPara);
            if (loginCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(loginCode));
            }

            userInfo = hUserlogin;
        }

        HUSER interfaces.Client_UserLogin.UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
            }
        }
    }
}

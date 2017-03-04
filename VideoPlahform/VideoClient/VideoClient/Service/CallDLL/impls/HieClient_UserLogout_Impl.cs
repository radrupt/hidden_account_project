using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HieCiULib;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_UserLogout_Impl:interfaces.Client_UserLogout
    {
        void interfaces.Client_UserLogout.Logout(interfaces.Client_UserLogin userLogin)
        {
            int nLogoutCode = -1;
            IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
            try
            {
                if (userLogin.UserInfo != INVALID_HANDLE_VALUE)
                {
                    nLogoutCode = Unit.HieClient_UserLogout(userLogin.UserInfo);

                    if (nLogoutCode != 0)
                    {
                        throw new Exception(Constants.ErrorConstants.getErrorString(nLogoutCode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                userLogin.UserInfo = INVALID_HANDLE_VALUE;
            }
        }
    }
}

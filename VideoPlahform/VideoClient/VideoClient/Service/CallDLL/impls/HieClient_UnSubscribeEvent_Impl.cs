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
    public class HieClient_UnSubscribeEvent_Impl:interfaces.Client_UnSubscribeEvent
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        private static int MAX_NUM = 15;

        void interfaces.Client_UnSubscribeEvent.UnSubscribeAllEvent(interfaces.Client_UserLogin client_login)
        {
            HUSER userInfo = client_login.UserInfo;
            uint dwAllEvtType = 0;
            if (userInfo != INVALID_HANDLE_VALUE)
            {
                int nUnSubscriptEvtCode = -1;
                for (int i = 0; i < MAX_NUM; i++)
                {
                    dwAllEvtType |= ((uint)1 << i);
                }
                nUnSubscriptEvtCode = Unit.HieClient_UnSubscribeEvent(userInfo, dwAllEvtType);
                if (nUnSubscriptEvtCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nUnSubscriptEvtCode));
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorInvalidUser));
            }
            
        }

        void interfaces.Client_UnSubscribeEvent.UnSubscribeEventByType(interfaces.Client_UserLogin client_login, uint eventType)
        {
            HUSER userInfo = client_login.UserInfo;
            if (userInfo != INVALID_HANDLE_VALUE && eventType > 0)
            {
                int nUnSubscriptEvtCode = -1;
                nUnSubscriptEvtCode = Unit.HieClient_UnSubscribeEvent(userInfo, eventType);
                if (nUnSubscriptEvtCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nUnSubscriptEvtCode));
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorInvalidUser));
            }
        }
    }
}

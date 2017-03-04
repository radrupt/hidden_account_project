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
    public class HieClient_SubscribeEvent_Impl:interfaces.Client_SubscribeEvent
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new Constants.CConstants().INVALID_HANDLE_VALUE;

        private static string[] aUserEvent = {
              "报警事件",
              "心跳丢失,网络断开",
              "网络重连成功",
              "远程用户断开",
              "远程流媒体断开",
              "硬盘组管理事件",
              "历史流事件通知",
              "实时流启动连接ID通知",
              "实时流停止连接ID通知",
              "语音流启动连接ID通知",
              "语音流停止连接ID通知",
              "历史流销毁事件通知",
              "历史流启动事件通知",
              "历史流停止事件通知",
              "历史流创建事件通知"
        };

        private HUSER hUser = INVALID_HANDLE_VALUE;

        public event interfaces.ValueChanged ValueChange;

        public void OnValueChanged(string e)
        {
            if (ValueChange != null)
            {
                ValueChange(e);
            }
        }

        public HUSER HUser
        {
            get { return hUser; }
        }

        private string cB_STR = "";

        public string CB_STR
        {
            get { return cB_STR; }
            set
            {
                if (cB_STR != value)
                {
                    OnValueChanged(value);
                }
                cB_STR = value;
            }
        }

        private static int MAX_NUM = 15;

        private Common.CB_UserEvent callback_userEvent;

         void interfaces.Client_SubscribeEvent.SubscribeAllEvent(interfaces.Client_UserLogin client_login)
        {
            HUSER userInfo = client_login.UserInfo;
            if (userInfo != INVALID_HANDLE_VALUE)
            {
                uint dwAllEvtType = 0;
                int nSubscriptEvtCode = -1;
                for (int i = 0; i < MAX_NUM; i++)
                {
                    dwAllEvtType |= ((uint)1 << i);
                }
                callback_userEvent = new Common.CB_UserEvent(callBack_userEventOp);
                nSubscriptEvtCode = Unit.HieClient_SubscribeEvent(userInfo, dwAllEvtType, callback_userEvent, 0);

                if (nSubscriptEvtCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nSubscriptEvtCode));
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(
                    (int)Constants.ErrorConstants.ClientError.ErrorInvalidUser));
            }
        }

         void interfaces.Client_SubscribeEvent.SubscribeEventByType(interfaces.Client_UserLogin client_login, uint eventType)
        {
            HUSER userInfo = client_login.UserInfo;
            if (userInfo != INVALID_HANDLE_VALUE && eventType > 0)
            {
                int nSubscriptEvtCode = -1;
                callback_userEvent = new Common.CB_UserEvent(callBack_userEventOp);
                nSubscriptEvtCode = Unit.HieClient_SubscribeEvent(userInfo, eventType, callback_userEvent, 0);
                if (nSubscriptEvtCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nSubscriptEvtCode));
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(
                    (int)Constants.ErrorConstants.ClientError.ErrorInvalidUser));
            }
        }

        /// <summary>
        /// 用于将设备回调的结果保存到CB_STR中，这里封装的还不够好，需要查询API进行下一步的操作
        /// </summary>
        /// <param name="hUser"></param>
        /// <param name="dwEventType"></param>
        /// <param name="dwParam1"></param>
        /// <param name="dwParam2"></param>
        /// <param name="dwParam3"></param>
        /// <param name="dwUserData"></param>
        /// <returns></returns>
        private int callBack_userEventOp(HUSER hUser, uint dwEventType, uint dwParam1, uint dwParam2, uint dwParam3, uint dwUserData)
        {
            this.hUser = hUser;
            string retstr = "";
            retstr = dwEventType + "";
            // 此时的 i 就是当前的事件 index
            switch (dwEventType)
            {
                 /** 报警事件   */
                case Common.USEREVENT_ALARM_NOTICE:
                    retstr += " 报警事件" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 心跳丢失,网络断开  */
                case Common.USEREVENT_HEARTBEAT_LOST:
                    retstr += " 心跳丢失,网络断开";
                    break;
                /** 网络重连成功  */
                case Common.USEREVENT_NET_RECOVER:
                    retstr += " 网络重连成功";
                    break;
                /** 远程用户断开  */
                case Common.USEREVENT_USER_DISCONNECT:
                    retstr += " 远程用户断开" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 远程流媒体断开 */
                case Common.USEREVENT_STREAM_DISCONNECT:
                    retstr += " 远程流媒体断开" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 硬盘组管理事件 */
                case Common.USEREVENT_DISKGROUP_MANAGE:
                    retstr += " 硬盘组管理事件" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 历史流事件通知 */
                case Common.USEREVENT_HISTORYSTREAM_NOTICE:
                    retstr += " 历史流事件通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 实时流启动连接ID通知  */
                case Common.USEREVENT_REALSTREAM_STARTLINK:
                    retstr += " 实时流启动连接ID通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 实时流停止连接ID通知  */
                case Common.USEREVENT_REALSTREAM_STOPLINK:
                    retstr += " 实时流停止连接ID通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 语音流启动连接ID通知  */
                case Common.USEREVENT_VOICESTREAM_STARTLINK:
                    retstr += " 语音流启动连接ID通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 语音流停止连接ID通知  */
                case Common.USEREVENT_VOICESTREAM_STOPLINK:
                    retstr += " 语音流停止连接ID通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 历史流销毁事件通知  */
                case Common.USEREVENT_HISTORYSTREAM_DESTROYLINK:
                    retstr += " 历史流销毁事件通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 历史流启动事件通知  */
                case Common.USEREVENT_HISTORYSTREAM_STARTLINK:
                    retstr += " 历史流启动事件通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 历史流停止事件通知  */
                case Common.USEREVENT_HISTORYSTREAM_STOPLINK:
                    retstr += " 历史流停止事件通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                /** 历史流创建事件通知  */
                case Common.USEREVENT_HISTORYSTREAM_CREATELINK:
                    retstr += " 历史流创建事件通知" + " " + dwParam1 + " " + dwParam2 + " " + dwParam3;
                    break;
                default:
                    break;
            }
            retstr += " " + dwUserData;
            CB_STR = retstr;
            return 0;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.CallDLL.interfaces;
using HieCiULib;
using System.Collections;
using System.Runtime.InteropServices;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_TransparentChannel_Impl : Client_TransparentChannel
    {
        private Hashtable enumST = new Hashtable();
        private Common.CB_TransparentChannel callBack_TransparentChannelEvent;
        private string cB_Str = "";

        public string CB_Str
        {
            get { return cB_Str; }
            set 
            {
                if (cB_Str != value)
                {
                    OnTransDataReceived(value);
                }
                cB_Str = value;
            }
        }

        public HieClient_TransparentChannel_Impl()
        {
            enumST.Add(SerialType.TTY232,Common.eSerialType.eTTY232);
            enumST.Add(SerialType.TTY422,Common.eSerialType.eTTY422);
            enumST.Add(SerialType.TTY485,Common.eSerialType.eTTY485);
        }

        public event TransDataReceived TransDataReceive;

        public void OnTransDataReceived(string e)
        {
            if (TransDataReceive != null)
            {
                TransDataReceive(e);
            }
        }

        public IntPtr TransparentChannelConnect(Client_UserLogin userLogin, SerialType majorType, uint minorType, bool isCallBack)
        {
            IntPtr tranHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            Common.TransparentChannelPara tcp = new Common.TransparentChannelPara();
            if(enumST.ContainsKey(majorType))
            {
                tcp.dwMajorType = (uint)enumST[majorType];
                tcp.dwMinorType = minorType;
                int nTransConnCode = -1;                   //建立透明通道连接返回码
                nTransConnCode = HieCIU.HieCIU_TransparentChannelConnect(ref tranHandle, userLogin.UserInfo, ref tcp);
                if (nTransConnCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nTransConnCode));
                }
                if (isCallBack)//是否需要处理回调
                {
                    try 
	                {	        
		                TransparentChannelCB(tranHandle);
	                }
	                catch (Exception ex)
	                {
		                throw ex;
	                }
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            return tranHandle;
        }

        private void TransparentChannelCB(IntPtr tranHandle)
        {
            callBack_TransparentChannelEvent = new Common.CB_TransparentChannel(callBack_TransparentChannelEventOp);
            int nTransCBCode = -1;
            nTransCBCode = HieCIU.HieCIU_TransparentChannelCB(tranHandle, callBack_TransparentChannelEvent, 0);
            if (nTransCBCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nTransCBCode));
            }
        }

        private int callBack_TransparentChannelEventOp(IntPtr tranHandle, ref Common.Buffer buffer, uint userData)
        {
            try
            {
                cB_Str = Marshal.PtrToStringUni(buffer.pBuffer, (int)buffer.dwBufLen);
            }
            catch (Exception ex)
            {
                cB_Str = "";
                throw ex;
            }
            return 0;
        }

        public void TransparentChannelDisconnect(IntPtr tranHandle)
        {
            int nTransDisConnCode = -1;
            nTransDisConnCode = HieCIU.HieCIU_TransparentChannelDisconnect(tranHandle);
            if (nTransDisConnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nTransDisConnCode));
            }
        }

        public void TransparentChannelWrite(IntPtr tranHandle, string bufContent)
        {
            Common.Buffer buffer = new Common.Buffer();
            IntPtr buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(Marshal.SizeOf(bufContent));
                Marshal.StructureToPtr(bufContent, buf, false);
                buffer.pBuffer = buf;
                buffer.dwBufLen = (uint)Marshal.SizeOf(bufContent);
            }
            catch (Exception ex)
            {
                throw ex ;
            }
            finally
            {
                try
                {
                    Marshal.FreeHGlobal(buf);
                }
                catch (Exception ex)
                {
                    throw ex ;
                }
            }
            
            int nTransWriteCode = -1;
            nTransWriteCode = HieCIU.HieCIU_TransparentChannelWrite(tranHandle,ref buffer);
            if (nTransWriteCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nTransWriteCode));
            }
        }
    }
}

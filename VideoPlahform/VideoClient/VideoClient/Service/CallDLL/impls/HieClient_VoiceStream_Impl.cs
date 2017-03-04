using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.CallDLL.interfaces;
using System.Collections;
using HieCiULib;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_VoiceStream_Impl : Client_VoiceStream
    {
        private Hashtable enumVM = new Hashtable();
        private Hashtable enumSTM = new Hashtable();
        private string cB_Str = "";
        private IntPtr cB_voiceHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        private HieCIUCommon.CB_Nettalk callback_NetTalkEvent;
        public HieClient_VoiceStream_Impl()
        {
            enumVM.Add(VoiceMode.eVoiceG711U,Common.eVoiceMode.eVoiceG711U);
            enumVM.Add(VoiceMode.eVoiceG726, Common.eVoiceMode.eVoiceG726);
            enumVM.Add(VoiceMode.eVoiceModeBegin, Common.eVoiceMode.eVoiceModeBegin);
            enumVM.Add(VoiceMode.eVoiceModeEnd, Common.eVoiceMode.eVoiceModeEnd);

            enumSTM.Add(StreamTransferMode.GeneralRTP,Common.eStreamTransferMode.eGeneralRTP);
            enumSTM.Add(StreamTransferMode.GeneralTCP, Common.eStreamTransferMode.eGeneralTCP);
        }

        public string CB_Str
        {
            get { return cB_Str;}
            set 
            {
                if (cB_Str != value)
                {
                    OnNettalk(cB_voiceHandle,value);
                }
                cB_Str = value;
            }
        }

        public IntPtr CB_voiceHandle
        {
            get{ return cB_voiceHandle;}
            set
            {
                if(cB_voiceHandle != value)
                {
                    OnNettalk(value,cB_Str);
                }
                cB_voiceHandle = value;
            }
        }

        public event Nettalked Nettalk;

        public void OnNettalk(IntPtr voiceHandle,string e)
        {
            if(Nettalk != null)
            {
                Nettalk(cB_voiceHandle,e);
            }
        }

        public IntPtr VoiceStreamConnect(Client_UserLogin userLogin, uint channel, VoiceMode vm, StreamTransferMode stm, bool isCallBack)
        {
            IntPtr voiceHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            if (enumVM.ContainsKey(vm))
            {
                Common.VoiceStreamPara vsp = new Common.VoiceStreamPara();
                vsp.dwChannel = channel;
                vsp.eMode = (Common.eVoiceMode)enumVM[vm];
                vsp.eTransferMode = (Common.eStreamTransferMode)enumSTM[stm];
                int nVoiceStrmConnCode = -1;                    //建立语音对讲流连接返回码
                nVoiceStrmConnCode = HieCIU.HieCIU_VoiceStreamConnect(ref voiceHandle, userLogin.UserInfo, ref vsp);
                if (nVoiceStrmConnCode != 0)
                {
                    voiceHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
                    throw new Exception(Constants.ErrorConstants.getErrorString(nVoiceStrmConnCode));
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(
                    (int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            if (isCallBack)
            {
                try 
	            {
                    VoiceStreaming(voiceHandle);
	            }
	            catch (Exception ex)
	            {
		            throw ex;
	            }
            }
            return voiceHandle;
        }

        private void VoiceStreaming(IntPtr voiceHandle)
        {
            callback_NetTalkEvent = new HieCIUCommon.CB_Nettalk(callback_netTalkEventOp);
            int nSetNettalkCBCode = -1;                    //设置设置语音流回调返回码
            nSetNettalkCBCode = HieCIU.HieCIU_VoiceStreamCB(voiceHandle, callback_NetTalkEvent, 0);
            if (nSetNettalkCBCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetNettalkCBCode));
            }
        }

        private int callback_netTalkEventOp(IntPtr voiceHandle, 
            ref HieCIUCommon.AudioCodecParam acp, 
            Common.eVoiceStreamSource vss, 
            string buffer, 
            uint bufLen, 
            uint userData)
        {
            cB_voiceHandle = voiceHandle;
            StringBuilder sb = new StringBuilder();
            sb.Append("channel:" + acp.nChannel + " ");
            sb.Append("codecId:" + acp.CodecID + " ");
            sb.Append("bitsPerSample:" + acp.nBitsPerSample + " ");
            sb.Append("samplePerSecond:" + acp.nSamplePerSecond + " ");
            sb.Append("voiceStreamSource:" + Enum.GetName(typeof(Common.eVoiceStreamSource),vss) + " ");
            sb.Append("buffer:" + buffer + " ");
            sb.Append("userData:" + userData.ToString());
            cB_Str = sb.ToString();
            return 0;
        }

        public void VoiceStreamDisconnect(IntPtr voiceHandle)
        {
            int nVoiceStrmDisConnCode = -1;                    //断开语音对讲流连接返回码
            nVoiceStrmDisConnCode = HieCIU.HieCIU_VoiceStreamDisconnect(voiceHandle);
            if (nVoiceStrmDisConnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nVoiceStrmDisConnCode));
            }
            voiceHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        }
    }
}

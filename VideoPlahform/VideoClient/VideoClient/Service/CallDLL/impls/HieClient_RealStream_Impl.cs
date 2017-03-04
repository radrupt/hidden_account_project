using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VideoClient.Service.CallDLL.interfaces;
using HieCiULib;
namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_RealStream_Impl:interfaces.Client_RealStream
    {
        private Hashtable enumSMT = new Hashtable();
        private Hashtable enumSTM = new Hashtable();
        private IntPtr hRealStream = new Constants.CConstants().INVALID_HANDLE_VALUE;

        public HieClient_RealStream_Impl()
        {
            enumSMT.Add(StreamMediaType.AssistVideo,Common.eStreamMediaType.eAssistVideo);
            enumSMT.Add(StreamMediaType.MainSound, Common.eStreamMediaType.eMainSound);
            enumSMT.Add(StreamMediaType.MainVideo, Common.eStreamMediaType.eMainVideo);
            enumSMT.Add(StreamMediaType.MainVideoAndSound, Common.eStreamMediaType.eMainVideoAndSound);

            enumSTM.Add(StreamTransferMode.GeneralRTP, Common.eStreamTransferMode.eGeneralRTP);
            enumSTM.Add(StreamTransferMode.GeneralTCP, Common.eStreamTransferMode.eGeneralTCP);
        }

        public IntPtr HRealStream
        {
            get
            {
                return hRealStream;
            }
            set
            {
                hRealStream = value;
            }
        }

        public void RealStreamConnect(interfaces.Client_UserLogin userLogin)
        {
            try
            {
                RealStreamConnect(userLogin,0,StreamMediaType.MainVideoAndSound,StreamTransferMode.GeneralTCP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RealStreamConnect(
            interfaces.Client_UserLogin userLogin, 
            uint Channel, 
            interfaces.StreamMediaType smt, 
            interfaces.StreamTransferMode stm)
        {
            if (enumSMT.ContainsKey(smt) && enumSTM.ContainsKey(stm))
	        {
		        HieCiULib.Common.RealStreamPara rsp = new HieCiULib.Common.RealStreamPara();
                rsp.dwChannel = Channel;
                rsp.eMediaType = (Common.eStreamMediaType)enumSMT[smt];
                rsp.eTransferMode = (Common.eStreamTransferMode)enumSTM[stm];
                int nRSConnCode = -1;
                nRSConnCode = HieCiULib.HieCIU.HieCIU_RealStreamConnect(ref hRealStream, userLogin.UserInfo, ref rsp);
                if (nRSConnCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nRSConnCode));
                }
	        }
            else{
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }


        public void RealStreamDisconnect()
        {
            int nRSDisConnCode = -1;
            if (hRealStream != new Constants.CConstants().INVALID_HANDLE_VALUE)
            {
                nRSDisConnCode = HieCiULib.HieCIU.HieCIU_RealStreamDisconnect(hRealStream);
                if (nRSDisConnCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nRSDisConnCode));
                }
                hRealStream = new Constants.CConstants().INVALID_HANDLE_VALUE;
            }
        }
    }
}

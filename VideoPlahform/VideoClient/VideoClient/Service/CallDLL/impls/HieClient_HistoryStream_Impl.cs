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
    public class HieClient_HistoryStream_Impl:Client_HistoryStream
    {

        private IntPtr hHistoryStream =  new Constants.CConstants().INVALID_HANDLE_VALUE;
        private Hashtable enumHST = new Hashtable();
        private Hashtable enumSTM = new Hashtable();
        private int playSpeed = 16;
        private IntPtr[] hsyncList;  //这个应该是存储目标历史流
        private uint nsyncNum = 128; // 关联的历史流(目标历史流的)句柄数目: 128
        private long indwwrittedSize;  //是通知下载的字节数
        private Boolean downloadsuccess ;
        public HieClient_HistoryStream_Impl()
        {
            enumHST.Add(HistoryStreamType.HistoryStreamTypeBegin, Common.eHistoryStreamType.eHistoryStreamTypeBegin);
            enumHST.Add(HistoryStreamType.AlarmRecord, Common.eHistoryStreamType.eAlarmRecord);
            enumHST.Add(HistoryStreamType.AllStreamMedia, Common.eHistoryStreamType.eAllStreamMedia);
            enumHST.Add(HistoryStreamType.GeneralRecord, Common.eHistoryStreamType.eGeneralRecord);
            enumHST.Add(HistoryStreamType.ManualRecord, Common.eHistoryStreamType.eManualRecord);
            enumHST.Add(HistoryStreamType.MotionRecord, Common.eHistoryStreamType.eMotionRecord);
            enumHST.Add(HistoryStreamType.HistoryStreamTypeEnd, Common.eHistoryStreamType.eHistoryStreamTypeEnd);

            enumSTM.Add(StreamTransferMode.GeneralTCP, Common.eStreamTransferMode.eGeneralTCP);
            //暂不支持
            //enumSTM.Add(StreamTransferMode.GeneralRTP, Common.eStreamTransferMode.eGeneralRTP);
            hsyncList = new IntPtr[128];
            downloadsuccess = false;
        }

        public IntPtr HHistoryStream
        {
            get
            {
                return hHistoryStream;
            }
            set
            {
                hHistoryStream = value;
            }
        }

        public int PlaySpeed
        {
            get
            {
                return playSpeed;
            }
            set
            {
                playSpeed = value;
            }
        }

        public IntPtr[] hSyncList{
            get{ return hsyncList; }
            set { ;}
        }

        public uint nSyncNum{
            get{ return nsyncNum; }
            set { ;}
        }

        public long indwWrittedSize {
            get 
            {
                return indwwrittedSize;
            }
            set { ;}
        }

        public Boolean DownloadSuccess
        {
            get { return downloadsuccess; }
            set { ;}
        }

        public void HistoryStreamCreate(interfaces.Client_UserLogin userLogin)
        {
            try
            {
                TimeInfo beginTime = new TimeInfo();
                beginTime.year = 2013;
                beginTime.month = 12;
                beginTime.day = 28;
                TimeInfo endTime = new TimeInfo();
                endTime.year = 2013;
                endTime.month = 12;
                endTime.day = 28;

                HistoryStreamCreate(userLogin,1, 0, 0, beginTime, endTime, HistoryStreamType.AllStreamMedia, StreamTransferMode.GeneralTCP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void HistoryStreamCreate(
            interfaces.Client_UserLogin userLogin,
            uint diskGroup, 
            uint channel, 
            uint enableEndTime, 
            TimeInfo beginTime, 
            TimeInfo endTime,
            interfaces.HistoryStreamType hst,
            interfaces.StreamTransferMode stm)
        {
            if (enumHST.ContainsKey(hst) && enumSTM.ContainsKey(stm))
            {
                HieCiULib.Common.HistoryStreamPara hsp = new Common.HistoryStreamPara();
                hsp.dwDiskGroup = diskGroup;
                hsp.dwChannel = channel;
                hsp.dwEnableEndTime = enableEndTime;
                hsp.cBeginTime = HieClient_CommonUtilies.TimeInfoCopy(beginTime);
                hsp.cEndTime = HieClient_CommonUtilies.TimeInfoCopy(endTime);
                hsp.eStreamType = (Common.eHistoryStreamType)enumHST[hst];
                hsp.eTransferMode = (Common.eStreamTransferMode)enumSTM[stm];
                int nCrtHisStrmCode = -1;                   //创建历史流的返回码
                nCrtHisStrmCode = HieCiULib.HieCIU.HieCIU_HistoryStreamCreate(ref hHistoryStream, 
                                                                              userLogin.UserInfo, ref hsp);
                if (nCrtHisStrmCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nCrtHisStrmCode));
                }   
            }
            else
            {
                throw new Exception("不支持该录像类型或传输协议");
            }
        }

        //历史流多种类型有问题，暂时先放着！！
        //public void HistoryStreamMultiTypeCreate(
        //    interfaces.Client_UserLogin userLogin,
        //    uint diskGroup,
        //    uint channel,
        //    uint enableEndTime,
        //    TimeInfo beginTime,
        //    TimeInfo endTime,
        //    uint dwStreamType,
        //    interfaces.StreamTransferMode stm)
        //{
        //    if (enumHST.ContainsKey(hst) && enumSTM.ContainsKey(stm))
        //    {
        //        HieCiULib.Common.HistoryStreamMultiTypePara hsp = new Common.HistoryStreamMultiTypePara();
        //        hsp.dwDiskGroup = diskGroup;
        //        hsp.dwChannel = channel;
        //        hsp.dwEnableEndTime = enableEndTime;
        //        hsp.cBeginTime = HieClient_CommonUtilies.TimeInfoCopy(beginTime);
        //        hsp.cEndTime = HieClient_CommonUtilies.TimeInfoCopy(endTime);
        //        hsp.dwStreamType = (Common.eHistoryStreamType)enumHST[hst];
        //        hsp.eTransferMode = (Common.eStreamTransferMode)enumSTM[stm];
        //        int nCrtHisStrmCode = -1;                   //创建历史流的返回码
        //        nCrtHisStrmCode = HieCiULib.HieCIU.HieCIU_HistoryStreamCreate(ref hHistoryStream, userLogin.UserInfo, ref hsp);
        //        if (nCrtHisStrmCode != 0)
        //        {
        //            throw new Exception(Constants.ErrorConstants.getErrorString(nCrtHisStrmCode));
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception(
        //            Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
        //    }
        //}

        public void HistoryStreamDestroy()
        {
            int nDstryHisStrmCode = -1;                   //销毁历史流的返回码
            if (hHistoryStream != new Constants.CConstants().INVALID_HANDLE_VALUE)
            {
                nDstryHisStrmCode = HieCiULib.HieCIU.HieCIU_HistoryStreamDestroy(hHistoryStream);
                if (nDstryHisStrmCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nDstryHisStrmCode));
                }
                hHistoryStream = new Constants.CConstants().INVALID_HANDLE_VALUE;
            }
        }

        public void HistoryStreamFast()
        {
            if (playSpeed == 256) return;
            playSpeed *= 2;
            int nStrmFastCode = -1;                   //历史流快放返回码
            nStrmFastCode = HieCiULib.HieCIU.HieCIU_HistoryStreamFast(hHistoryStream);
            if (nStrmFastCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStrmFastCode));
            }
        }

        public void HistoryStreamSlow()
        {
            if (playSpeed == 1) return;
            playSpeed /= 2;
            int nStrmSlowCode = -1;                   //历史流慢放返回码
            nStrmSlowCode = HieCiULib.HieCIU.HieCIU_HistoryStreamFast(hHistoryStream);
            if (nStrmSlowCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStrmSlowCode));
            }
        }

        public void HistoryStreamOneByOne()
        {
            int nStrmOBOCode = -1;                   //历史流单帧进播放返回码
            nStrmOBOCode = HieCiULib.HieCIU.HieCIU_HistoryStreamOneByOne(hHistoryStream);
            if (nStrmOBOCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStrmOBOCode));
            }
        }

        public void HistoryStreamSync( IntPtr hstreamDest ) {
            int nHisStrmSyncCode = -1;
            nHisStrmSyncCode = HieCIU.HieCIU_HistoryStreamSync( hHistoryStream, hstreamDest );
            if (nHisStrmSyncCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nHisStrmSyncCode));
            }
        }
        public void HistoryStreamUnsync( IntPtr hstreamDest)
        {
            int nHisStrmUnSyncCode = -1;
            nHisStrmUnSyncCode = HieCIU.HieCIU_HistoryStreamUnsync(hHistoryStream, hstreamDest);
            if (nHisStrmUnSyncCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nHisStrmUnSyncCode));
            }
        }
        //只能当该类作为基准历史流的时候可以使用
        //一个基准历史流相关的N多个目的历史流
        //使用该方法前得先create
        //该方法一般是用于查看多个录像，按同一时间播放
        public void HistoryStreamGetSyncList()
        {
            int nGetSyncListCode = -1;
            nGetSyncListCode = HieCIU.HieCIU_HistoryStreamGetSyncList(hHistoryStream, ref  hsyncList, ref nsyncNum);
            if (nGetSyncListCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nGetSyncListCode));
            }
        }

        public void HistoryStreamDownload(
            Client_UserLogin userLogin,
            uint diskGroup,
            uint channel,
            uint enableEndTime,
            TimeInfo beginTime,
            TimeInfo endTime,
            HistoryStreamType hst,
            StreamTransferMode stm)
        {
            if (enumHST.ContainsKey(hst) && enumSTM.ContainsKey(stm))
            {
                HieCiULib.Common.HistoryStreamPara hsp = new Common.HistoryStreamPara();
                hsp.dwDiskGroup = diskGroup;
                hsp.dwChannel = channel;
                hsp.dwEnableEndTime = enableEndTime;
                hsp.cBeginTime = HieClient_CommonUtilies.TimeInfoCopy(beginTime);
                hsp.cEndTime = HieClient_CommonUtilies.TimeInfoCopy(endTime);
                hsp.eStreamType = (Common.eHistoryStreamType)enumHST[hst];
                hsp.eTransferMode = (Common.eStreamTransferMode)enumSTM[stm];
                int nCrtHisStrmDownloadCode = -1;                   //创建历史流下载的返回码
                HieCIUCommon.CB_SpecifyFileName cb_specfilename =
                    new HieCIUCommon.CB_SpecifyFileName(CALLBACK_SpecifyFileName);
                nCrtHisStrmDownloadCode = HieCiULib.HieCIU.HieCIU_HistoryStreamDownload(ref hHistoryStream,
                                                           userLogin.UserInfo, ref hsp, cb_specfilename,0);
                if (nCrtHisStrmDownloadCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nCrtHisStrmDownloadCode));
                }
            }
            else
            {
                throw new Exception("不支持该录像类型或传输协议");
            }
        }
        public void HistoryStreamDownloadDestory()
        {
            int nDstryHisStrmDownloadCode = -1;
            //此时句柄是历史流下载句柄
            nDstryHisStrmDownloadCode = HieCIU.HieCIU_HistoryStreamDownloadDestory(hHistoryStream);
            if (nDstryHisStrmDownloadCode != 0) {
                throw new Exception(Constants.ErrorConstants.getErrorString(nDstryHisStrmDownloadCode));
            }
        }
        //调用它前必须创建历史流
        public void HistoryStreamPosition(Common.eOperationType eOperation, ref Common.TimeInfo tHisStrmTime)
        {
            int nHisStrmPositionCode = -1;
            nHisStrmPositionCode = HieCIU.HieCIU_HistoryStreamPosition(
                                    hHistoryStream, eOperation,ref tHisStrmTime);
            if (nHisStrmPositionCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nHisStrmPositionCode));
            }
        
        }
        public int CALLBACK_SpecifyFileName(IntPtr hStream, int inbComplete,
        ref string pFileName, ref Common.AbsoluteTime pAbsoluteTime, long indwWrittedSize) {

            //inbComplete 的值小于0 表示当前是通知下载的字节数
            if (inbComplete < 0) {
                indwwrittedSize = indwWrittedSize;
                return 0;
            }
            if (inbComplete > 0) {
                downloadsuccess = true;
                return 0;
            }
            pFileName = (pAbsoluteTime.wYear + 1900).ToString() + (pAbsoluteTime.wMonth + 1).ToString()
                + pAbsoluteTime.wDay.ToString() + pAbsoluteTime.wHour.ToString()
                + pAbsoluteTime.wMinute.ToString() + pAbsoluteTime.wSecond.ToString()
                + pAbsoluteTime.wMillisecond.ToString();
            return 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWindow.CallDLL.interfaces;
using System.Collections;
using HieCiULib;

namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_Query_Impl:Client_Query
    {
        private Hashtable enumLMAT = new Hashtable();
        private Hashtable enumLMIT = new Hashtable();
        private Hashtable enumHST = new Hashtable();
        private Hashtable enumDGT = new Hashtable();
        public HieClient_Query_Impl()
        {
            #region enumLMAT.add

            enumLMAT.Add(LogMajorType.LogAlarm, Common.eHistoryLogMajorType.eHistoryLogAlarm);
            enumLMAT.Add(LogMajorType.LogException, Common.eHistoryLogMajorType.eHistoryLogException);
            enumLMAT.Add(LogMajorType.LogMajorAll, Common.eHistoryLogMajorType.eHistoryLogMajorAll);
            enumLMAT.Add(LogMajorType.LogMajorTypeBegin, Common.eHistoryLogMajorType.eHistoryLogMajorTypeBegin);
            enumLMAT.Add(LogMajorType.LogMajorTypeEnd, Common.eHistoryLogMajorType.eHistoryLogMajorTypeEnd);
            enumLMAT.Add(LogMajorType.LogRecord, Common.eHistoryLogMajorType.eHistoryLogRecord);
            enumLMAT.Add(LogMajorType.LogStorage, Common.eHistoryLogMajorType.eHistoryLogStorage);
            enumLMAT.Add(LogMajorType.LogSysOperation, Common.eHistoryLogMajorType.eHistoryLogSysOperation);
            enumLMAT.Add(LogMajorType.LogSysSetting, Common.eHistoryLogMajorType.eHistoryLogSysSetting);
            enumLMAT.Add(LogMajorType.LogUserManage, Common.eHistoryLogMajorType.eHistoryLogUserManage);
            #endregion

            #region enumLMIT.Add

            enumLMIT.Add(LogMinorType.LogAlarmAll, Common.eHistoryLogMinorType.eHistoryLogAlarmAll);
            enumLMIT.Add(LogMinorType.LogAlarmInputAlarm, Common.eHistoryLogMinorType.eHistoryLogAlarmInputAlarm);
            enumLMIT.Add(LogMinorType.LogAlarmMotionStart, Common.eHistoryLogMinorType.eHistoryLogAlarmMotionStart);
            enumLMIT.Add(LogMinorType.LogAlarmMotionStop, Common.eHistoryLogMinorType.eHistoryLogAlarmMotionStop);
            enumLMIT.Add(LogMinorType.LogExceptionAll, Common.eHistoryLogMinorType.eHistoryLogExceptionAll);
            enumLMIT.Add(LogMinorType.LogExceptionHardDiskError, Common.eHistoryLogMinorType.eHistoryLogExceptionHardDiskError);
            enumLMIT.Add(LogMinorType.LogExceptionHardDiskFull, Common.eHistoryLogMinorType.eHistoryLogExceptionHardDiskFull);
            enumLMIT.Add(LogMinorType.LogExceptionIllegalAccess, Common.eHistoryLogMinorType.eHistoryLogExceptionIllegalAccess);
            enumLMIT.Add(LogMinorType.LogExceptionIpConflict, Common.eHistoryLogMinorType.eHistoryLogExceptionIpConflict);
            enumLMIT.Add(LogMinorType.LogExceptionNetDisconnect, Common.eHistoryLogMinorType.eHistoryLogExceptionNetDisconnect);
            enumLMIT.Add(LogMinorType.LogExceptionSignalLost, Common.eHistoryLogMinorType.eHistoryLogExceptionSignalLost);
            enumLMIT.Add(LogMinorType.LogExceptionSignalRecover, Common.eHistoryLogMinorType.eHistoryLogExceptionSignalRecover);
            enumLMIT.Add(LogMinorType.LogRecordAll, Common.eHistoryLogMinorType.eHistoryLogRecordAll);
            enumLMIT.Add(LogMinorType.LogRecordStartAuto, Common.eHistoryLogMinorType.eHistoryLogRecordStartAuto);
            enumLMIT.Add(LogMinorType.LogRecordStartManual, Common.eHistoryLogMinorType.eHistoryLogRecordStartManual);
            enumLMIT.Add(LogMinorType.LogRecordStop, Common.eHistoryLogMinorType.eHistoryLogRecordStop);
            enumLMIT.Add(LogMinorType.LogStorageAddPartition, Common.eHistoryLogMinorType.eHistoryLogStorageAddPartition);
            enumLMIT.Add(LogMinorType.LogStorageAll, Common.eHistoryLogMinorType.eHistoryLogStorageAll);
            enumLMIT.Add(LogMinorType.LogStorageDiskGroupOperation, Common.eHistoryLogMinorType.eHistoryLogStorageDiskGroupOperation);
            enumLMIT.Add(LogMinorType.LogStorageFormatPartition, Common.eHistoryLogMinorType.eHistoryLogStorageFormatPartition);
            enumLMIT.Add(LogMinorType.LogStorageRemovePartition, Common.eHistoryLogMinorType.eHistoryLogStorageRemovePartition);
            enumLMIT.Add(LogMinorType.LogStorageUnmountDisk, Common.eHistoryLogMinorType.eHistoryLogStorageUnmountDisk);
            enumLMIT.Add(LogMinorType.LogSysOpAll, Common.eHistoryLogMinorType.eHistoryLogSysOpAll);
            enumLMIT.Add(LogMinorType.LogSysOpClearAlarm, Common.eHistoryLogMinorType.eHistoryLogSysOpClearAlarm);
            enumLMIT.Add(LogMinorType.LogSysOpIllShutdown, Common.eHistoryLogMinorType.eHistoryLogSysOpIllShutdown);
            enumLMIT.Add(LogMinorType.LogSysOpPowerOff, Common.eHistoryLogMinorType.eHistoryLogSysOpPowerOff);
            enumLMIT.Add(LogMinorType.LogSysOpPowerOn, Common.eHistoryLogMinorType.eHistoryLogSysOpPowerOn);
            enumLMIT.Add(LogMinorType.LogSysOpPTZControl, Common.eHistoryLogMinorType.eHistoryLogSysOpPTZControl);
            enumLMIT.Add(LogMinorType.LogSysOpRemoteReset, Common.eHistoryLogMinorType.eHistoryLogSysOpRemoteReset);
            enumLMIT.Add(LogMinorType.LogSysOpUpdate, Common.eHistoryLogMinorType.eHistoryLogSysOpUpdate);
            enumLMIT.Add(LogMinorType.LogSysSetAlarmInSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetAlarmInSetting);
            enumLMIT.Add(LogMinorType.LogSysSetAlarmOutSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetAlarmOutSetting);
            enumLMIT.Add(LogMinorType.LogSysSetAll, Common.eHistoryLogMinorType.eHistoryLogSysSetAll);
            enumLMIT.Add(LogMinorType.LogSysSetCodecSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetCodecSetting);
            enumLMIT.Add(LogMinorType.LogSysSetCommonSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetCommonSetting);
            enumLMIT.Add(LogMinorType.LogSysSetCustomizeFunction, Common.eHistoryLogMinorType.eHistoryLogSysSetCustomizeFunction);
            enumLMIT.Add(LogMinorType.LogSysSetDefaultSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetDefaultSetting);
            enumLMIT.Add(LogMinorType.LogSysSetDisplayDeviceSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetDisplayDeviceSetting);
            enumLMIT.Add(LogMinorType.LogSysSetDisplayModeSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetDisplayModeSetting);
            enumLMIT.Add(LogMinorType.LogSysSetExceptionSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetExceptionSetting);
            enumLMIT.Add(LogMinorType.LogSysSetExportPTZProtocol, Common.eHistoryLogMinorType.eHistoryLogSysSetExportPTZProtocol);
            enumLMIT.Add(LogMinorType.LogSysSetExportSettings, Common.eHistoryLogMinorType.eHistoryLogSysSetExportSettings);
            enumLMIT.Add(LogMinorType.LogSysSetImportPTZProtocol, Common.eHistoryLogMinorType.eHistoryLogSysSetImportPTZProtocol);
            enumLMIT.Add(LogMinorType.LogSysSetImportSettings, Common.eHistoryLogMinorType.eHistoryLogSysSetImportSettings);
            enumLMIT.Add(LogMinorType.LogSysSetMaintainSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetMaintainSetting);
            enumLMIT.Add(LogMinorType.LogSysSetMotionSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetMotionSetting);
            enumLMIT.Add(LogMinorType.LogSysSetNetSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetNetSetting);
            enumLMIT.Add(LogMinorType.LogSysSetPictureSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetPictureSetting);
            enumLMIT.Add(LogMinorType.LogSysSetPTZSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetPTZSetting);
            enumLMIT.Add(LogMinorType.LogSysSetServerSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetServerSetting);
            enumLMIT.Add(LogMinorType.LogSysSetTimeSetting, Common.eHistoryLogMinorType.eHistoryLogSysSetTimeSetting);
            enumLMIT.Add(LogMinorType.LogUserAdd, Common.eHistoryLogMinorType.eHistoryLogUserAdd);
            enumLMIT.Add(LogMinorType.LogUserAll, Common.eHistoryLogMinorType.eHistoryLogUserAll);
            enumLMIT.Add(LogMinorType.LogUserDelete, Common.eHistoryLogMinorType.eHistoryLogUserDelete);
            enumLMIT.Add(LogMinorType.LogUserEdit, Common.eHistoryLogMinorType.eHistoryLogUserEdit);
            enumLMIT.Add(LogMinorType.LogUserLogin, Common.eHistoryLogMinorType.eHistoryLogUserLogin);
            enumLMIT.Add(LogMinorType.LogUserLogout, Common.eHistoryLogMinorType.eHistoryLogUserLogout);
            #endregion

            #region enumHST.Add
            enumHST.Add(HistoryStreamType.AlarmRecord,Common.eHistoryStreamType.eAlarmRecord);
            enumHST.Add(HistoryStreamType.AllStreamMedia, Common.eHistoryStreamType.eAllStreamMedia);
            enumHST.Add(HistoryStreamType.GeneralRecord, Common.eHistoryStreamType.eGeneralRecord);
            enumHST.Add(HistoryStreamType.HistoryStreamTypeBegin, Common.eHistoryStreamType.eHistoryStreamTypeBegin);
            enumHST.Add(HistoryStreamType.HistoryStreamTypeEnd, Common.eHistoryStreamType.eHistoryStreamTypeEnd);
            enumHST.Add(HistoryStreamType.ManualRecord, Common.eHistoryStreamType.eManualRecord);
            enumHST.Add(HistoryStreamType.MotionRecord, Common.eHistoryStreamType.eMotionRecord);
            #endregion

            #region enumDGT.Add
            enumDGT.Add(DiskGroupType.DiskGroupAlarm,Common.eDiskGroupType.eDiskGroupAlarm);
            enumDGT.Add(DiskGroupType.DiskGroupBackup, Common.eDiskGroupType.eDiskGroupBackup);
            enumDGT.Add(DiskGroupType.DiskGroupNormal, Common.eDiskGroupType.eDiskGroupNormal);
            enumDGT.Add(DiskGroupType.DiskGroupRedundance, Common.eDiskGroupType.eDiskGroupRedundance);
            enumDGT.Add(DiskGroupType.DiskGroupTypeBegin, Common.eDiskGroupType.eDiskGroupTypeBegin);
            enumDGT.Add(DiskGroupType.DiskGroupTypeEnd, Common.eDiskGroupType.eDiskGroupTypeEnd);
            #endregion
        }

        public IntPtr HistoryStreamQueryConnect(
            Client_UserLogin userLogin, 
            uint channel, 
            uint diskGroup, 
            HistoryStreamType hst, 
            TimeInfo beginTime, 
            TimeInfo endTime)
        {
            IntPtr hStreamHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            if (enumHST.ContainsKey(hst))
            {
                int nHisStreamQueryConnCode = -1;
                Common.HistoryStreamQueryFactor hsqf = new Common.HistoryStreamQueryFactor();
                hsqf.cBeginTime = HieClient_CommonUtilies.TimeInfoToString(beginTime);
                hsqf.cEndTime = HieClient_CommonUtilies.TimeInfoToString(endTime);
                hsqf.dwChannel = channel;
                hsqf.dwDiskGroup = diskGroup;
                hsqf.eStreamType = (Common.eHistoryStreamType)enumHST[hst];
                nHisStreamQueryConnCode = HieCIU.HieCIU_HistoryStreamQueryConnect(ref hStreamHandle, userLogin.UserInfo, ref hsqf);
                //这里ref参数返回的信息中有部分被忽略
                if (nHisStreamQueryConnCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nHisStreamQueryConnCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            return hStreamHandle;
        }

        public string[] HistoryStreamQueryNext(IntPtr streamHandle)
        {
            string[] queryinfo = new string[5];
            int nHisStrmQueryCode = -1;
            Common.HistoryStreamQueryResult hsqr = new Common.HistoryStreamQueryResult();
            nHisStrmQueryCode = HieCIU.HieCIU_HistoryStreamQueryNext(streamHandle, ref hsqr);
            if ( nHisStrmQueryCode ==(int) Common.eQueryStatus.eQueryFinished ){
                queryinfo[0] = "-1";
                queryinfo[1] = "查询结束";
            }
            else if( nHisStrmQueryCode ==(int) Common.eQueryStatus.eQueryBusy ){
                queryinfo[0] = "-1";
                queryinfo[1] = "查询忙";
            }
            else if (nHisStrmQueryCode == (int)Common.eQueryStatus.eQueryFailed)
            {
                queryinfo[0] = "-1";
                queryinfo[1] = "查询失败";
            }
            else
            {
                queryinfo[0] = hsqr.dwChannel.ToString();
                queryinfo[1] = hsqr.eStreamType.ToString();
                if (enumHST.ContainsValue(hsqr.eStreamType))
                {
                    foreach (HistoryStreamType sht in enumHST)
                    {
                        if (enumHST[sht].Equals(hsqr.eStreamType))
                        {
                            queryinfo[2] =  enumHST[sht].ToString();
                            break;
                        }
                    }
                }
                queryinfo[3] = hsqr.cBeginTime;
                queryinfo[4] = hsqr.cEndTime;

            }
            return queryinfo;
        }

        public IntPtr HistoryStreamMultiTypeQueryConnect(
            Client_UserLogin userLogin, 
            uint channel, 
            uint diskGroup, 
            uint streamType, 
            TimeInfo beginTime, 
            TimeInfo endTime)
        {
            IntPtr hStreamHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            int nHisStreamMultQueryConnCode = -1;
            
            Common.HistoryStreamMultiTypeQueryFactor hsmtqf = new Common.HistoryStreamMultiTypeQueryFactor();
            hsmtqf.cBeginTime = HieClient_CommonUtilies.TimeInfoToString(beginTime);
            hsmtqf.cEndTime = HieClient_CommonUtilies.TimeInfoToString(endTime);
            hsmtqf.dwChannel = channel;
            hsmtqf.dwDiskGroup = diskGroup;
            hsmtqf.dwStreamType = streamType;
            nHisStreamMultQueryConnCode = HieCIU.HieCIU_HistoryStreamMultiTypeQueryConnect(
                ref hStreamHandle,
                userLogin.UserInfo,
                ref hsmtqf);
            //这里ref参数返回的信息中有部分被忽略
            if (nHisStreamMultQueryConnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nHisStreamMultQueryConnCode));
            }
            return hStreamHandle;
        }

        public Hashtable HistoryStreamMultiTypeQueryNext(IntPtr streamHandle)
        {
            int nHisStrmMulQueryCode = -1;
            Common.HistoryStreamMultiTypeQueryResult hsmtqr = new Common.HistoryStreamMultiTypeQueryResult();
            nHisStrmMulQueryCode = HieCIU.HieCIU_HistoryStreamMultiTypeQueryNext(streamHandle, ref hsmtqr);
            if (nHisStrmMulQueryCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nHisStrmMulQueryCode));
            }
            Hashtable res = new Hashtable();
            res.Add("channel", hsmtqr.dwChannel);
            res.Add("streamType", hsmtqr.dwStreamType);
            res.Add("streamSize", hsmtqr.dwStreamSize);
            res.Add("beginTimeStr", hsmtqr.cBeginTime);
            res.Add("endTimeStr", hsmtqr.cEndTime);
            return res;
        }

        public void HistoryStreamQueryDisconnect(IntPtr streamHandle)
        {
            int nHisStreamQuerDisconnCode = -1;
            nHisStreamQuerDisconnCode = HieCIU.HieCIU_HistoryStreamQueryDisconnect(streamHandle);
            if (nHisStreamQuerDisconnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nHisStreamQuerDisconnCode));
            }
            streamHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        }

        public IntPtr LogQueryConnect(
            Client_UserLogin userLogin, 
            uint QueryMode, 
            LogMajorType MajorType, 
            LogMinorType MinorType, 
            uint Channel, 
            TimeInfo StartTime, 
            TimeInfo StopTime)
        {
            IntPtr logHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            if (enumLMAT.ContainsKey(MajorType) && enumLMIT.ContainsKey(MinorType))
            {
                int nLogQueryConnCode = -1;
                Common.LogQueryFactor lqf = new Common.LogQueryFactor();
                lqf.m_dwChannel = Channel;
                lqf.m_dwQueryMode = QueryMode;
                lqf.m_eMajorType = (Common.eHistoryLogMajorType)enumLMAT[MajorType];
                lqf.m_eMinorType = (Common.eHistoryLogMinorType)enumLMIT[MinorType];
                lqf.m_sStartTime = HieClient_CommonUtilies.TimeInfoToString(StartTime);
                lqf.m_sStopTime = HieClient_CommonUtilies.TimeInfoToString(StopTime);
                nLogQueryConnCode = HieCIU.HieCIU_HistoryLogQueryConnect(ref logHandle, userLogin.UserInfo, ref lqf);
                if (nLogQueryConnCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nLogQueryConnCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            return logHandle;
        }

        public void LogQueryDisconnect(IntPtr logHandle)
        {
            int nLogQueryDisconnCode = -1;
            nLogQueryDisconnCode = HieCIU.HieCIU_HistoryLogQueryDisconnect(logHandle);
            if (nLogQueryDisconnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nLogQueryDisconnCode));
            }
            logHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        }

        public Hashtable LogQueryNext(IntPtr logHandle)
        {
            Hashtable res = new Hashtable();
            int nLogQueryCode = -1;
            Common.LogQueryResult lqr = new Common.LogQueryResult();
            nLogQueryCode = HieCIU.HieCIU_HistoryLogQueryNext(logHandle, ref lqr);
            if (nLogQueryCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nLogQueryCode));
            }
            res.Add("detailInfo",lqr.m_dwDetailInfo);
            if (enumLMAT.ContainsValue(lqr.m_eMajorType))
            {
                foreach (LogMajorType lmt in enumLMAT)
                {
                    if (enumLMAT[lmt].Equals(lqr.m_eMajorType))
                    {
                        res.Add("majorType", enumLMAT[lmt].ToString());
                        break;
                    }
                }
            }
            if (enumLMIT.ContainsValue(lqr.m_eMinorType))
            {
                foreach (LogMinorType lmt in enumLMIT)
                {
                    if (enumLMIT[lmt].Equals(lqr.m_eMinorType))
                    {
                        res.Add("minorType", enumLMIT[lmt].ToString());
                        break;
                    }
                }
            }
            res.Add("logTime",lqr.m_sLogTime);
            res.Add("userIp",lqr.m_sUserIP);
            res.Add("userName",lqr.m_sUserName);
            return res;
        }

        public IntPtr FileQueryConnect(
            Client_UserLogin userLogin, 
            uint channel, 
            uint fileType, 
            TimeInfo beginTime, 
            TimeInfo endTime)
        {
            IntPtr fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            Common.FileQueryFactor fqf = new Common.FileQueryFactor();
            fqf.dwChannel = channel;
            fqf.dwFileType = fileType;
            fqf.cBeginTime = HieClient_CommonUtilies.TimeInfoToString(beginTime);
            fqf.cEndTime = HieClient_CommonUtilies.TimeInfoToString(endTime);
            int nFileQueryConnCode = -1;
            nFileQueryConnCode = HieCIU.HieCIU_FileQueryConnect(ref fileHandle, userLogin.UserInfo, ref fqf);
            if (nFileQueryConnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nFileQueryConnCode));
            }
            return fileHandle;
        }

        public void FileQueryDisconnect(IntPtr fileQueryHandle)
        {
            int nFileQueryDisconn = -1;
            nFileQueryDisconn = HieCIU.HieCIU_FileQueryDisconnect(fileQueryHandle);
            if (nFileQueryDisconn != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nFileQueryDisconn));
            }
            fileQueryHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        }

        public Hashtable FileQueryNext(IntPtr fileQueryHandle)
        {
            int nFileQueryNextCode = -1;
            Common.FileQueryResult fqr = new Common.FileQueryResult();
            nFileQueryNextCode = HieCIU.HieCIU_FileQueryNext(fileQueryHandle, ref fqr);
            if (nFileQueryNextCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nFileQueryNextCode));
            }
            Hashtable res = new Hashtable();
            res.Add("createTime",fqr.cCreateTime);
            res.Add("fileName",fqr.cFileName);
            res.Add("channel",fqr.dwChannel);
            res.Add("dataSize",fqr.dwDataSize);
            res.Add("fileType",fqr.dwFileType);
            res.Add("lock",fqr.dwLock);
            
            return res;
        }

        public uint GetDataSize(
            Client_UserLogin userLogin, 
            uint channelBits, 
            DiskGroupType dgt, 
            HistoryStreamType hst, 
            TimeInfo startTime, 
            TimeInfo endTime, 
            uint userData)
        {
            int getDataSizeCode = -1;
            uint dataSize = 0;
            if (enumDGT.ContainsKey(dgt) && enumHST.ContainsKey(hst))
            {
                getDataSizeCode = HieCIU.HieCIU_GetDataSize(
                    userLogin.UserInfo,
                    channelBits,
                    (Common.eDiskGroupType)enumDGT[dgt],
                    (Common.eHistoryStreamType)enumHST[hst],
                    HieClient_CommonUtilies.TimeInfoToString(startTime),
                    HieClient_CommonUtilies.TimeInfoToString(endTime),
                    userData,
                    ref dataSize);
                if (getDataSizeCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(getDataSizeCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            return dataSize;
        }

        public uint GetMultiTypeDataSize(Client_UserLogin userLogin, uint channelBits, DiskGroupType dgt, uint streamType, TimeInfo startTime, TimeInfo endTime, uint userData)
        {
            int getMulDataSizeCode = -1;
            uint mulDataSize = 0;
            if (enumDGT.ContainsKey(dgt))
            {
                getMulDataSizeCode = HieCIU.HieCIU_GetMultiTypeDataSize(
                    userLogin.UserInfo,
                    channelBits,
                    (Common.eDiskGroupType)enumDGT[dgt],
                    streamType,
                    HieClient_CommonUtilies.TimeInfoToString(startTime),
                    HieClient_CommonUtilies.TimeInfoToString(endTime),
                    userData,
                    ref mulDataSize);
                if (getMulDataSizeCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(getMulDataSizeCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            return mulDataSize;
        }

        public uint[] DataExistCheck(Client_UserLogin userLogin, uint channel, uint majorType, uint minorType, TimeInfo month)
        {
            int nDataExistCheckCode = -1;
            uint[] res = new uint[32];
            uint dwResult = 0;
            nDataExistCheckCode = HieCIU.HieCIU_DataExistCheck(
                userLogin.UserInfo, 
                channel, 
                majorType, 
                minorType, 
                HieClient_CommonUtilies.TimeInfoToString(month), 
                ref dwResult);
            if (nDataExistCheckCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nDataExistCheckCode));
            }
            //判断指定日期（i+1），是否存在指定数据，res中为1表示存在，0表示不存在
            for (int i = 0; i < 32; i++)
            {
                res[i] = (dwResult >> i) & 1;
            }
            return res;
        }
    }
}

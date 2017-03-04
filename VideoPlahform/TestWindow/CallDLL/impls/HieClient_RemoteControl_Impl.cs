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
    public class HieClient_RemoteControl_Impl:Client_RemoteControl
    {
        private uint chanNum = 0;
        private uint stateBits;

        private Hashtable enumRDC = new Hashtable();
        private Hashtable enumIF = new Hashtable();
        private Hashtable enumFLOC = new Hashtable();
        private Hashtable enumDGO = new Hashtable();

        public HieClient_RemoteControl_Impl()
        {
            enumRDC.Add(RemoteDeviceControl.RemoteDeviceControlBegin,Common.eRemoteDeviceControl.eRemoteDeviceControlBegin);
            enumRDC.Add(RemoteDeviceControl.DeviceHalt,Common.eRemoteDeviceControl.eDeviceHalt);
            enumRDC.Add(RemoteDeviceControl.DeviceReboot,Common.eRemoteDeviceControl.eDeviceReboot);
            enumRDC.Add(RemoteDeviceControl.DeviceSendTestEmail,Common.eRemoteDeviceControl.eDeviceSendTestEmail);
            enumRDC.Add(RemoteDeviceControl.DeviceSetDefault,Common.eRemoteDeviceControl.eDeviceSetDefault);
            enumRDC.Add(RemoteDeviceControl.DeviceStandby,Common.eRemoteDeviceControl.eDeviceStandby);
            enumRDC.Add(RemoteDeviceControl.RemoteDeviceControlEnd,Common.eRemoteDeviceControl.eRemoteDeviceControlEnd);

            enumIF.Add(ImageFormat.BmpFormat, Common.eImageFormat.eBmpFormat);
            enumIF.Add(ImageFormat.ImageFormatBegin, Common.eImageFormat.eImageFormatBegin);
            enumIF.Add(ImageFormat.ImageFormatEnd, Common.eImageFormat.eImageFormatEnd);
            enumIF.Add(ImageFormat.JpgFormat, Common.eImageFormat.eJpgFormat);

            enumFLOC.Add(FileLockOperationCode.LockFile, Common.eFileLockOperationCode.eLockFile);
            enumFLOC.Add(FileLockOperationCode.UnlockFile, Common.eFileLockOperationCode.eUnlockFile);

            enumDGO.Add(DiskGroupOperation.CreateNewPartition,Common.eDiskGroupOperation.eCreateNewPartition);
            enumDGO.Add(DiskGroupOperation.DeletePartition,Common.eDiskGroupOperation.eDeletePartition);
            enumDGO.Add(DiskGroupOperation.DiskGroupAddPartition,Common.eDiskGroupOperation.eDiskGroupAddPartition);
            enumDGO.Add(DiskGroupOperation.DiskGroupBindChannel,Common.eDiskGroupOperation.eDiskGroupBindChannel);
            enumDGO.Add(DiskGroupOperation.DiskGroupDelPartition, Common.eDiskGroupOperation.eDiskGroupDelPartition);
            enumDGO.Add(DiskGroupOperation.DiskGroupKeepTime, Common.eDiskGroupOperation.eDiskGroupKeepTime);
            enumDGO.Add(DiskGroupOperation.DiskGroupOperationBegin, Common.eDiskGroupOperation.eDiskGroupOperationBegin);
            enumDGO.Add(DiskGroupOperation.DiskGroupOperationEnd, Common.eDiskGroupOperation.eDiskGroupOperationEnd);
            enumDGO.Add(DiskGroupOperation.FormatPartition, Common.eDiskGroupOperation.eFormatPartition);
            enumDGO.Add(DiskGroupOperation.UnMountDisk, Common.eDiskGroupOperation.eUnMountDisk);
        }

        public uint ChanNum
        {
            get
            {
                return chanNum;
            }
            set
            {
                chanNum = value;
            }
        }

        public uint StateBits
        {
            get
            {
                return stateBits;    
            }
            set
            {
                stateBits = value;
            }
        }

        public void DeviceControl(Client_UserLogin userLogin, RemoteDeviceControl rdc)
        {
            if (enumRDC.ContainsKey(rdc))
            {
                int nDevCtrlCode = -1;                   //远程设备控制返回码
                nDevCtrlCode = HieCIU.HieCIU_DeviceControl(
                    userLogin.UserInfo,
                    (Common.eRemoteDeviceControl)enumRDC[rdc]);
                if (nDevCtrlCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nDevCtrlCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void ForceIFrame(Client_UserLogin userLogin, uint Channel)
        {
            int nForceIFrameCode = -1;                   //远程设备强制I帧返回码
            nForceIFrameCode = HieCIU.HieCIU_ForceIFrame(userLogin.UserInfo,Channel);
            if (nForceIFrameCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nForceIFrameCode));
            }
        }

        public void ImageCapture(Client_UserLogin userLogin, 
            uint Channel, 
            ImageFormat imgF, 
            uint FileSize, 
            string FileName, 
            string CreateTime)
        {
            if (enumIF.ContainsKey(imgF))
            {
                int nImgCaptureCode = -1;                   //远程抓图返回码

                Common.ImageFileInfo ifi = new Common.ImageFileInfo();
                ifi.m_dwFileSize = FileSize;
                ifi.m_sCreateTime = CreateTime;
                ifi.m_sFileName = FileName;
                nImgCaptureCode = HieCIU.HieCIU_ImageCapture(userLogin.UserInfo,
                    Channel,
                    (Common.eImageFormat)enumIF[imgF],
                    ref ifi);
                if (nImgCaptureCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nImgCaptureCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void FileLock(Client_UserLogin userLogin, string FileName, FileLockOperationCode floc)
        {
            if (enumFLOC.ContainsKey(floc))
            {
                int nFileLockCode = -1;
                nFileLockCode = HieCIU.HieCIU_FileLock(
                    userLogin.UserInfo, 
                    FileName, 
                    (Common.eFileLockOperationCode)enumFLOC[floc]);
                if (nFileLockCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nFileLockCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void RecordControl(Client_UserLogin userLogin, uint Channel, RecordType rt)
        {
            int nRmtRcrdCtrlCode = -1;                   //远程录像控制回码
            nRmtRcrdCtrlCode = HieCIU.HieCIU_RecordControl(userLogin.UserInfo, Channel, (uint)rt);
            if (nRmtRcrdCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nRmtRcrdCtrlCode));
            }
        }

        public void AlarmClear(Client_UserLogin userLogin, uint Channel, uint AlarmType)
        {
            int nAlarmClearCode  = -1;                   //远程清除报警返回码
            nAlarmClearCode = HieCIU.HieCIU_AlarmClear(userLogin.UserInfo, Channel, AlarmType);
            if (nAlarmClearCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nAlarmClearCode));
            }
        }

        public void AlarmoutControl(Client_UserLogin userLogin, uint Channel, uint Switch)
        {
            int nAlarmCtrlCode = -1;                   //远程报警输出控制返回码
            nAlarmCtrlCode = HieCIU.HieCIU_AlarmControl(userLogin.UserInfo,Channel,Switch);
            if (nAlarmCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nAlarmCtrlCode));
            }
        }

        public void AlarmoutStateGet(Client_UserLogin userLogin)
        {
            int nGetAlarmStateCode = -1;                   //获取远程报警输出状态返回码
            nGetAlarmStateCode = HieCIU.HieCIU_AlarminStateGet(
                userLogin.UserInfo, 
                ref chanNum, 
                ref stateBits);
            if (nGetAlarmStateCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nGetAlarmStateCode));
            }
        }

        public void DiskGroupManage(Client_UserLogin userLogin, DiskGroupOperation dgo, uint arg1, uint arg2, uint arg3)
        {
            if (enumDGO.ContainsKey(dgo))
            {
                int nDskGrpCtrlCode = -1;
                nDskGrpCtrlCode = HieCIU.HieCIU_DiskGroupManage(
                    userLogin.UserInfo,
                    (Common.eDiskGroupOperation)enumDGO[dgo],
                    arg1,
                    arg2,
                    arg3);
                if (nDskGrpCtrlCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nDskGrpCtrlCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }
    }
}

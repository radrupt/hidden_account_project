using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.CallDLL.interfaces;
using System.Collections;
using HieCiULib;
using System.Threading;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_FileUpload_Impl:Client_FileUpload
    {
        private Hashtable enumTFT = new Hashtable();

        private Common.CB_FileUpload callBack_FileUploadEvent;

        private bool isfinished = false;

        public bool Isfinished
        {
            get { return isfinished; }
            set 
            {
                if (isfinished != value)
                {
                    OnFileUpload(isfinished,uploadedSize);
                }
                isfinished = value;
            }
        }

        private uint uploadedSize = 0;

        public uint UploadedSize
        {
            get { return uploadedSize; }
            set 
            {
                if (uploadedSize != value)
                {
                    OnFileUpload(isfinished, uploadedSize);   
                }
                uploadedSize = value;
            }
        }

        public event FileUploaded FileUpload;

        public void OnFileUpload(bool isFinished, uint uploadedSize)
        {
            if (FileUpload != null)
            {
                FileUpload(isfinished, uploadedSize);
            }
        }

        public HieClient_FileUpload_Impl()
        {
            enumTFT.Add(TransferFileType.DeviceConfig, Common.eTransferFileType.eDeviceConfig);
            enumTFT.Add(TransferFileType.FileSystemUpdate, Common.eTransferFileType.eFileSystemUpdate);
            enumTFT.Add(TransferFileType.PtzProtocol, Common.eTransferFileType.ePtzProtocol);
            enumTFT.Add(TransferFileType.TransferFileTypeBegin, Common.eTransferFileType.eTransferFileTypeBegin);
            enumTFT.Add(TransferFileType.TransferFileTypeEnd, Common.eTransferFileType.eTransferFileTypeEnd);
        }


        public IntPtr FileUploadConnect(Client_UserLogin userLogin, TransferFileType tft, string RemoteFilePath, string LocalFilePath)
        {
            IntPtr fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            if (enumTFT.ContainsKey(tft) && RemoteFilePath.Length < 256 && LocalFilePath.Length < 256)
            {
                int nUploadConnCode = -1;                   //建立文件上传连接返回码
                Common.FileUploadPara fup = new Common.FileUploadPara();
                fup.eFileType = (Common.eTransferFileType)enumTFT[tft];
                fup.strLocalFilePath = LocalFilePath;
                fup.strRemoteFilePath = RemoteFilePath;
                nUploadConnCode = HieCIU.HieCIU_FileUploadConnect(ref fileHandle,userLogin.UserInfo,ref fup);
                if(nUploadConnCode != 0)
                {
                    fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
                    throw new Exception(Constants.ErrorConstants.getErrorString(nUploadConnCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter)
                    );
            }
            return fileHandle;
        }

        public void FileUploadDisconnect(IntPtr fileHandle)
        {
            int nUploadDisConnCode = -1;                   //断开文件上传连接返回码
            nUploadDisConnCode = HieCIU.HieCIU_FileUploadDisconnect(fileHandle);
            if (nUploadDisConnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nUploadDisConnCode));
            }
            fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        }

        public void FileUploadStart(IntPtr fileHandle)
        {
            int nStartFileTransCode = -1;                   //启动文件传输返回码
            nStartFileTransCode = HieCIU.HieCIU_FileUploadControl(fileHandle, Common.eFileTransferControl.eFileTransferStart);
            if (nStartFileTransCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStartFileTransCode));
            }
            FileUploading(fileHandle);
        }

        private void FileUploading(IntPtr fileHandle)
        {
            callBack_FileUploadEvent = new Common.CB_FileUpload(callBack_FileUploadEventOp);
            int nSetUploadCBCode = -1;                   //设置文件上传回调返回码
            nSetUploadCBCode = HieCIU.HieCIU_FileUploadCB(fileHandle, callBack_FileUploadEvent, 0);
            if (nSetUploadCBCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetUploadCBCode));
            }
            do
            {
                Thread.Sleep(5000);
            } while (!isfinished);
            isfinished = false;
            uploadedSize = 0;
        }

        public void FileUploadStop(IntPtr fileHandle)
        {
            int nStopFileTransCode = -1;                   //启动文件传输返回码
            nStopFileTransCode = HieCIU.HieCIU_FileUploadControl(fileHandle, Common.eFileTransferControl.eFileTransferStop);
            if (nStopFileTransCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStopFileTransCode));
            }
        }

        private int callBack_FileUploadEventOp(IntPtr fileHandle,Common.FileUploadState fus,uint userData)
        {
            switch (fus.dwStatus)
            {
                case 5:
                    isfinished = true;
                    uploadedSize = fus.dwUploadSize;
                    break;
                case 1:
                    isfinished = true;
                    uploadedSize = fus.dwUploadSize;
                    break;
                case 0:
                    isfinished = false;
                    uploadedSize = fus.dwUploadSize;
                    break;
                default:
                    break;
            }
            return 0;
        }
    }
}

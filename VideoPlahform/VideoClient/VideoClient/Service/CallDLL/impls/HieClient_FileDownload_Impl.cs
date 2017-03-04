using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.CallDLL.interfaces;
using HieCiULib;
using System.Collections;
using System.Threading;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_FileDownload_Impl : Client_FileDownload
    {
        public event FileDownloaded FileDownload;
        private Hashtable enumTFT = new Hashtable();
        private Common.CB_FileDownload callBack_FileDownloadEvent;

        private bool isfinished = false;

        public bool Isfinished
        {
            get { return isfinished; }
            set
            {
                if (isfinished != value)
                {
                    OnFileDownload(isfinished, downloadedSize);
                }
                isfinished = value;
            }
        }

        private uint downloadedSize = 0;

        public uint DownloadedSize
        {
            get { return downloadedSize; }
            set
            {
                if (downloadedSize != value)
                {
                    OnFileDownload(isfinished, downloadedSize);
                }
                downloadedSize = value;
            }
        }

        public void OnFileDownload(bool isFinished, uint downloadSize)
        {
            if (FileDownload != null)
            {
                FileDownload(isfinished, downloadedSize);
            }
        }

        public HieClient_FileDownload_Impl()
        {
            enumTFT.Add(TransferFileType.DeviceConfig, Common.eTransferFileType.eDeviceConfig);
            enumTFT.Add(TransferFileType.FileSystemUpdate, Common.eTransferFileType.eFileSystemUpdate);
            enumTFT.Add(TransferFileType.PtzProtocol, Common.eTransferFileType.ePtzProtocol);
            enumTFT.Add(TransferFileType.TransferFileTypeBegin, Common.eTransferFileType.eTransferFileTypeBegin);
            enumTFT.Add(TransferFileType.TransferFileTypeEnd, Common.eTransferFileType.eTransferFileTypeEnd);
        }

        public IntPtr FileDownloadConnect(Client_UserLogin userLogin, TransferFileType tft, string RemoteFilePath, string LocalFilePath)
        {
            IntPtr fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
            if (enumTFT.ContainsKey(tft) && RemoteFilePath.Length < 256 && LocalFilePath.Length < 256)
            {
                int nDownloadConnCode = -1;                   //建立文件上传连接返回码
                Common.FileDownloadPara fdp = new Common.FileDownloadPara();
                fdp.eFileType = (Common.eTransferFileType)enumTFT[tft];
                fdp.strLocalFilePath = LocalFilePath;
                fdp.strRemoteFilePath = RemoteFilePath;
                nDownloadConnCode = HieCIU.HieCIU_FileDownloadConnect(ref fileHandle, userLogin.UserInfo, ref fdp);
                if (nDownloadConnCode != 0)
                {
                    fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
                    throw new Exception(Constants.ErrorConstants.getErrorString(nDownloadConnCode));
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

        public void FileDownloadStart(IntPtr fileHandle)
        {
            int nStartFileTransCode = -1;                   //启动文件传输返回码
            nStartFileTransCode = HieCIU.HieCIU_FileDownloadControl(fileHandle, Common.eFileTransferControl.eFileTransferStart);
            if (nStartFileTransCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStartFileTransCode));
            }
            FileDownloading(fileHandle);
        }

        private void FileDownloading(IntPtr fileHandle)
        {
            callBack_FileDownloadEvent = new Common.CB_FileDownload(callBack_FileDownloadEventOp);
            int nSetDownloadCBCode = -1;                   //设置文件上传回调返回码
            nSetDownloadCBCode = HieCIU.HieCIU_FileDownloadCB(fileHandle, callBack_FileDownloadEvent, 0);
            if (nSetDownloadCBCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetDownloadCBCode));
            }
            do
            {
                Thread.Sleep(5000);
            } while (!isfinished);
            isfinished = false;
            downloadedSize = 0;
        }

        public void FileDownloadStop(IntPtr fileHandle)
        {
            int nStopFileTransCode = -1;                   //启动文件传输返回码
            nStopFileTransCode = HieCIU.HieCIU_FileDownloadControl(fileHandle, Common.eFileTransferControl.eFileTransferStop);
            if (nStopFileTransCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStopFileTransCode));
            }
        }

        public void FileDownloadDisconnect(IntPtr fileHandle)
        {
            int nDownloadDisConnCode = -1;                   //断开文件下载连接返回码
            nDownloadDisConnCode = HieCIU.HieCIU_FileDownloadDisconnect(fileHandle);
            if (nDownloadDisConnCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nDownloadDisConnCode));
            }

            fileHandle = new Constants.CConstants().INVALID_HANDLE_VALUE;
        }

        /// <summary>
        /// 文件下载数据回调
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="buffer">下载文件数据，包括数据缓冲区与长度buffer 的长度表示下载状态: </param>
        /// <param name="userData"></param>
        /// <returns></returns>
        private int callBack_FileDownloadEventOp(IntPtr fileHandle, Common.Buffer buffer, uint userData)
        {
            if (buffer.dwBufLen == 0)
            {
                isfinished = true;

            }
            else if (buffer.dwBufLen == UInt32.MaxValue - 1)
            {
                isfinished = true;
            }
            else
            {
                downloadedSize += buffer.dwBufLen; 
            }
            return 0;
        }
    }
}

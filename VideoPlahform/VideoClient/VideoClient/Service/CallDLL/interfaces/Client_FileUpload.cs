using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 文件上传事件的委托
    /// </summary>
    /// <param name="isFinished">文件是否传输完成</param>
    /// <param name="uploadedSize">已经上传的文件大小</param>
    public delegate void FileUploaded(bool isFinished, uint uploadedSize);

    /// <summary>
    /// 文件传输类型
    /// </summary>
    public enum TransferFileType
    {
        TransferFileTypeBegin = 0,  //起始值 
        FileSystemUpdate,  //升级包文件 
        PtzProtocol,  //云台协议文件 
        DeviceConfig,  //设备配置文件 
        TransferFileTypeEnd,  //结束值 
    }

    /// <summary>
    /// 用于处理文件上传的一些操作
    /// 
    /// Version:1.0
    /// Date:2012/06/19
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_FileUpload
    {
        /// <summary>
        /// 文件上传事件
        /// </summary>
        event FileUploaded FileUpload;

        /// <summary>
        /// 当文件上传时触发函数
        /// </summary>
        /// <param name="isFinished">文件是否已经上传完成</param>
        /// <param name="uploadedSize">已经上传的文件大小</param>
        void OnFileUpload(bool isFinished, uint uploadedSize);

        /// <summary>
        /// 文件上传连接操作
        /// 文件上传连接建立后，文件上传过程随即开始。
        /// 文件上传完成或终止时，需调用此接口释放资源。
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="tft">传输文件的类型</param>
        /// <param name="RemoteFilePath">远程文件的路径</param>
        /// <param name="LocalFilePath">本地文件路径</param>
        /// <returns>文件句柄</returns>
        IntPtr FileUploadConnect(Client_UserLogin userLogin, TransferFileType tft, string RemoteFilePath,string LocalFilePath);

        /// <summary>
        /// 文件上传控制，启动文件传输
        /// </summary>
        /// <param name="fileHandle">文件句柄</param>
        void FileUploadStart(IntPtr fileHandle);

        /// <summary>
        /// 文件上传控制，停止文件传输
        /// </summary>
        /// <param name="fileHandle">文件句柄</param>
        void FileUploadStop(IntPtr fileHandle);

        /// <summary>
        /// 文件上传断开操作
        /// 文件上传完成或终止时，需调用此接口释放资源。
        /// </summary>
        /// <param name="fileHandle">文件句柄</param>
        void FileUploadDisconnect(IntPtr fileHandle);
    }
}

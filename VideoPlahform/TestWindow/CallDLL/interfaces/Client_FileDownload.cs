using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 文件下载事件的代理
    /// </summary>
    /// <param name="isFinished">文件是否已经下载完成</param>
    /// <param name="downloadSize">已经下载的文件的尺寸</param>
    public delegate void FileDownloaded(bool isFinished,uint downloadSize);

    /// <summary>
    /// 文件下载相关操作，设备端是以流方式存储，无文件概念，文件操作主要是指升级包、云台协议、设备配置文件、图片等。 
    ///
    /// Version:1.0
    /// Date:2012/06/20
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_FileDownload
    {
        /// <summary>
        /// 文件下载事件
        /// </summary>
        event FileDownloaded FileDownload;

        /// <summary>
        /// 文件下载时触发函数
        /// </summary>
        /// <param name="isFinished">文件是否已经下载完成</param>
        /// <param name="downloadSize">已经下载文件的尺寸</param>
        void OnFileDownload(bool isFinished, uint downloadSize);
        
        /// <summary>
        /// 文件下载连接操作
        /// 文件下载连接建立后，文件下载过程随即开始。
        /// 文件下载完成或终止时，需调用此接口释放资源。
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="tft">传输文件的类型</param>
        /// <param name="RemoteFilePath">远程文件的路径</param>
        /// <param name="LocalFilePath">本地文件路径</param>
        /// <returns>文件句柄</returns>
        IntPtr FileDownloadConnect(Client_UserLogin userLogin, TransferFileType tft, string RemoteFilePath, string LocalFilePath);

        /// <summary>
        /// 文件下载控制，启动文件传输
        /// </summary>
        /// <param name="fileHandle">文件句柄</param>
        void FileDownloadStart(IntPtr fileHandle);

        /// <summary>
        /// 文件下载控制，停止文件传输
        /// </summary>
        /// <param name="fileHandle">文件句柄</param>
        void FileDownloadStop(IntPtr fileHandle);

        /// <summary>
        /// 文件下载断开操作
        /// 文件下载完成或终止时，需调用此接口释放资源。
        /// </summary>
        /// <param name="fileHandle">文件句柄</param>
        void FileDownloadDisconnect(IntPtr fileHandle);
    }
}

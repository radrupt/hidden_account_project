using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 远程设备控制枚举类型
    /// </summary>
    public enum RemoteDeviceControl
    {
        RemoteDeviceControlBegin = 0,  //起始值 
        DeviceReboot,  //重启动 
        DeviceHalt,  //关机 
        DeviceStandby,  //待机 
        DeviceSetDefault,  //恢复系统默认 
        DeviceSendTestEmail,  //发送测试邮件 
        RemoteDeviceControlEnd  //结束值 
    }

    /// <summary>
    /// 抓图文件格式
    /// </summary>
    public enum ImageFormat
    {
        ImageFormatBegin = 0,  //起始值 
        BmpFormat,  //位图格式 
        JpgFormat,  //jpg格式 
        ImageFormatEnd,  //结束值 
    }

    /// <summary>
    /// 文件锁操作
    /// </summary>
    public enum FileLockOperationCode
    {
        UnlockFile = 0,  //解锁 
        LockFile,  //锁定
    }

    /// <summary>
    /// 硬盘组操作类型
    /// </summary>
    public enum DiskGroupOperation
    {
        DiskGroupOperationBegin = 0,  //起始值 
        UnMountDisk,  //卸载分区 
        CreateNewPartition,  //建立新分区 
        DeletePartition,  //删除分区 
        FormatPartition,  //格式化分区 
        DiskGroupKeepTime,  //分组保持数据时间 
        DiskGroupBindChannel,  //分组绑定通道 
        DiskGroupAddPartition,  //添加分区至分组 
        DiskGroupDelPartition,  //从分组删除分区 
        DiskGroupOperationEnd,  //结束值 
    }

    /// <summary>
    /// 录像类型
    /// </summary>
    public enum RecordType
    {
        ByManualRecord = 0, //手动录像模 
        ByAutoRecord,   //自动录像模式
        StopRecord      //停止录像
    }

    /// <summary>
    /// 用于处理远程设备控制的一些操作
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_RemoteControl
    {
        /// <summary>
        /// 有效报警输出通道个数,默认值为0
        /// </summary>
        uint ChanNum
        {
            get;
            set;
        }

        /// <summary>
        /// 位 0 - 31 分别表示 0 - 31 通道的报警输出状态
        /// </summary>
        uint StateBits
        {
            get;
            set;
        }

        /// <summary>
        /// 远程设备控制
        /// 远程控制设备关机、重启动、待机、恢复系统默认。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="rdc">远程设备控制枚举类型</param>
        void DeviceControl(Client_UserLogin userLogin, RemoteDeviceControl rdc);

        /// <summary>
        /// 远程设备强制 I 帧
        /// 强制 I 帧是指控制设备端通道立即产生一下关键帧，主要用于在网络丢失数据或无图像时，可用于迅速恢复图像。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="Channel">通道号 </param>
        void ForceIFrame(Client_UserLogin userLogin,uint Channel);

        /// <summary>
        /// 远程抓图
        /// 所捕获的图片文件保存在设备端。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="Channel">通道号 </param>
        /// <param name="imgF">抓图文件格式</param>
        /// <param name="FileSize">文件大小 </param>
        /// <param name="FileName">文件名</param>
        /// <param name="CreateTime">文件创建时间</param>
        void ImageCapture(Client_UserLogin userLogin, uint Channel, ImageFormat imgF, uint FileSize, string FileName, string CreateTime);

        /// <summary>
        /// 远程锁定文件
        /// 被锁定的文件在轮转过程中将不能被删除。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="FileName">文件名</param>
        /// <param name="floc">操作码</param>
        void FileLock(Client_UserLogin userLogin, string FileName, FileLockOperationCode floc);

        /// <summary>
        /// 远程录像控制
        /// 设置某个通道的录像模式。
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="Channel">通道号 </param>
        /// <param name="rt">录像类型</param>
        void RecordControl(Client_UserLogin userLogin, uint Channel, RecordType rt);

        /// <summary>
        /// 远程清除报警
        /// 若报警状态还在持续，远程消警后将还是会有报警事件。
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="Channel">通道号 </param>
        /// <param name="AlarmType">报警类型，可填任意数值，设备端无区分</param>
        void AlarmClear(Client_UserLogin userLogin, uint Channel, uint AlarmType);

        /// <summary>
        /// 远程报警输出控制 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="Channel">通道号 </param>
        /// <param name="Switch">开关状态，0 - 关闭，1 - 打开</param>
        void AlarmoutControl(Client_UserLogin userLogin, uint Channel, uint Switch);

        /// <summary>
        /// 获取远程报警输出状态 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        void AlarmoutStateGet(Client_UserLogin userLogin);

        /// <summary>
        /// 远程盘组管理
        /// 远程对硬盘及硬盘分组进行管理的远程命令接口。
        /// 此函数只是进行了粗粒度的封装，需要进一步封装才能达到简化的效果
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="dgo">硬盘组操作类型</param>
        /// <param name="arg1">操作参数1</param>
        /// <param name="arg2">操作参数2</param>
        /// <param name="arg3">操作参数3</param>
        void DiskGroupManage(Client_UserLogin userLogin, DiskGroupOperation dgo, uint arg1, uint arg2, uint arg3);
    }
}

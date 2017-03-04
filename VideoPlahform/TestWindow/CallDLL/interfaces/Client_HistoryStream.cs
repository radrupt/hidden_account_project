using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HieCiULib;
namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 历史流类型 枚举类型
    /// </summary>
    public enum HistoryStreamType
    {
        HistoryStreamTypeBegin,  //起始值 
        AllStreamMedia,  //所有流媒体  
        GeneralRecord,  //普通录像  
        ManualRecord,  //手动录像  
        MotionRecord,  //移动录像  
        AlarmRecord,  //报警录像  
        HistoryStreamTypeEnd,  //结束值
    }

    /// <summary>
    /// 时间信息 结构体
    /// </summary>
    public struct TimeInfo
    {
        public ushort year;		/*!< 年	*/
        public ushort month;		/*!< 月	*/
        public ushort day;			/*!< 日	*/
        public ushort hour;		/*!< 时	*/
        public ushort minute;		/*!< 分	*/
        public ushort second;		/*!< 秒	*/
    }

    /// <summary>
    /// 提供处理历史流创建和销毁的操作
    /// 
    /// Version:1.0
    /// Date:2012/06/11
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_HistoryStream
    {
        /// <summary>
        /// 历史流句柄，初始化为IntPtr(-1)
        /// 可作为历史流句柄
        /// 可作为历史流同步句柄
        /// 可作为历史流下载句柄
        /// </summary>
        IntPtr HHistoryStream
        {
            get;
            set;
        }

        /// <summary>
        /// 当前播放速度
        /// </summary>
        int PlaySpeed
        {
            get;
            set;
        }
        /// <summary>
        /// 存储关联目标历史流句柄的数组
        /// </summary>
         IntPtr[] hSyncList
        {
            get;
            set;
        }
        /// <summary>
        /// 存储关联目标历史流句柄的个数
        /// </summary>
         uint nSyncNum
        {
            get;
            set;
        }
        /// <summary>
        /// 历史流文件下载大小
        /// </summary>
         long indwWrittedSize
        {
            get;
            set;
        }
        /// <summary>
        /// 历史流文件下载是否成功
        /// </summary>
         Boolean DownloadSuccess
        {
            get;
            set;
        }


        /// <summary>
        /// 这个是测试用的！！！
        /// </summary>
        /// <param name="userLogin">用户句柄，通过用户登录获取的句柄</param>
        void HistoryStreamCreate(Client_UserLogin userLogin);

        /// <summary>
        /// 创建历史通道（仅支持单一录像类型和所有录像类型） 
        ///  用完它一定要记住使用HistoryStreamDestroy销毁该历史流通道
        /// </summary>
        /// <param name="userLogin">用户句柄，通过用户登录获取的句柄</param>
        /// <param name="diskGroup">盘组,对应Common.eDiskGroupType</param>
        /// <param name="channel">流媒体通道号</param>
        /// <param name="enableEndTime">结束时间是否有效（ 0 表示无效， 其他 表示有效）</param>
        /// <param name="beginTime">历史流起始时间 </param>
        /// <param name="endTime">历史流结束时间</param>
        /// <param name="hst">历史流类型</param>
        /// <param name="stm">传输模式</param>
        void HistoryStreamCreate(
            Client_UserLogin userLogin, 
            uint diskGroup, 
            uint channel, 
            uint enableEndTime, 
            TimeInfo beginTime, 
            TimeInfo endTime, 
            HistoryStreamType hst, 
            StreamTransferMode stm);

        /// <summary>
        /// 创建历史通道（支持多种录像类型的组合） 
        /// </summary>
        /// <param name="times">快放的倍数[2,4,8,16]</param>
        //void HistoryStreamMultiTypeCreate(
        //    interfaces.Client_UserLogin userLogin,
        //    uint diskGroup,
        //    uint channel,
        //    uint enableEndTime,
        //    TimeInfo beginTime,
        //    TimeInfo endTime,
        //    uint dwStreamType,
        //    interfaces.StreamTransferMode stm);

        /// <summary>
        /// 销毁历史通道
        /// </summary>
        void HistoryStreamDestroy();

        /// <summary>
        /// 得先HistoryStreamCreate
        /// 慢放，1 2 4 8 16
        /// 实际使用16,32,64,128，256
        /// </summary>
        /// <param name="times">快放的倍数[2,4,8,16]</param>
        void HistoryStreamFast();

        /// <summary>
        /// 得先HistoryStreamCreate
        /// 慢放，1 1/2 1/4 1/8 1/16
        /// 实际使用16，8,4,2,1
        /// </summary>
        /// <param name="times">慢放的倍数[2,4,8,16]</param>
        void HistoryStreamSlow();

        /// <summary>
        /// 得先HistoryStreamCreate
        /// 单帧进，要恢复正常播放可调用 StreamPlay 函数 
        /// </summary>
        void HistoryStreamOneByOne();

        /// <summary>
        ///  当前类为基准历史流
        ///  使用之后一定要HistoryStreamUnsync销毁
        /// 多路历史流同步回放
        ///将目的历史通道与基准历史通道进行同步操作。
        /// </summary>
        void HistoryStreamSync( IntPtr hstreamDest );

        /// <summary>
        /// 当前类为基准历史流
        /// 解除多路历史流同步回放
        ///将目的历史通道从基准历史流中移除操作，移除成功后在基准流上所做的操作将不影响被剔除同步的目的历史流。
        /// </summary>
        void HistoryStreamUnsync( IntPtr hstreamDest);


        /// <summary>
        ///  当前类为基准历史流
        /// 获取某个历史流上所关联的同步回放流通道列表
        /// </summary>
        void HistoryStreamGetSyncList();

        /// <summary>
        /// 历史流下载（仅支持单一录像类型和所有录像类型） 
        /// </summary>
        void HistoryStreamDownload(
            Client_UserLogin userLogin,
            uint diskGroup,
            uint channel,
            uint enableEndTime,
            TimeInfo beginTime,
            TimeInfo endTime,
            HistoryStreamType hst,
            StreamTransferMode stm
            );

        /// <summary>
        /// 历史流下载（支持多种录像类型的组合）  
        /// </summary>
        //void HistoryStreamMultiTypeDownload();

        /// <summary>
        /// 停止历史流下载   
        /// </summary>
        void HistoryStreamDownloadDestory();

        /// <summary>
        /// 获取或设置历史流通道时间位置
        ///并不能使用此接口来显示播放位置，此位置只表示数据已经到达了本地，并不表示正在播放的位置
        /// </summary>
        void HistoryStreamPosition(Common.eOperationType eOperation, ref Common.TimeInfo tHisStrmTime);

    }
}

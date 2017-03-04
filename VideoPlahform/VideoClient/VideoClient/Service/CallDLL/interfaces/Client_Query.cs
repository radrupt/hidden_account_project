using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 日志查询主类型
    /// </summary>
    public enum LogMajorType
    {
        LogMajorTypeBegin,   // 起始值 
        LogSysOperation,   // 系统管理日志 
        LogSysSetting,   // 系统配置日志 
        LogRecord,   // 录像日志 
        LogUserManage,   // 用户管理日志 
        LogAlarm,   // 设备报警日志 
        LogException,   // 设备异常日志 
        LogStorage,   // 存储管理日志 
        LogMajorAll,   // 忽略主类型 
        LogMajorTypeEnd,   // 结束值 
    }

    /// <summary>
    /// 盘组类型
    /// </summary>
    public enum DiskGroupType
    {
        DiskGroupTypeBegin = 0,//  起始值 
        DiskGroupNormal,//  普通盘组 
        DiskGroupAlarm,//  报警盘组 
        DiskGroupRedundance,//  冗余盘组 
        DiskGroupBackup,//  备份盘组 
        DiskGroupTypeEnd,//  结束值 
    }

    /// <summary>
    /// 日志查询子类型 
    /// </summary>
    public enum LogMinorType
    {
        LogSysOpAll = 0,      // 系统操作所有子项 
        LogSysOpUpdate,   // 系统升级 
        LogSysOpPTZControl,   // 云台控制 
        LogSysOpIllShutdown,   // 非法关机 
        LogSysOpPowerOff,   // 正常关机 
        LogSysOpPowerOn,   // 开机 
        LogSysOpRemoteReset,   // 远程重启 
        LogSysOpClearAlarm,   // 消警 
        LogSysSetAll,   // 系统设置所有子项 
        LogSysSetTimeSetting,   // 录像时间表设置 
        LogSysSetCommonSetting,   // 普通设置 
        LogSysSetCodecSetting,   // 编码设置 
        LogSysSetNetSetting,   // 网络设置 
        LogSysSetServerSetting,   // 服务器设置 
        LogSysSetMotionSetting,   // 移动侦测设置 
        LogSysSetPTZSetting,   // 云台解码器设置 
        LogSysSetDefaultSetting,   // 恢复默认设置 
        LogSysSetDisplayDeviceSetting,   // 设置显示设备参数 
        LogSysSetDisplayModeSetting,   // 显示模式设置 
        LogSysSetPictureSetting,   // 图像颜色设置 
        LogSysSetMaintainSetting,   // 自动维护设置 
        LogSysSetAlarmOutSetting,   // 报警输出设置 
        LogSysSetAlarmInSetting,   // 报警输入设置 
        LogSysSetCustomizeFunction,   // 功能定制设置 
        LogSysSetImportPTZProtocol,   // 导入云台协议 
        LogSysSetExportPTZProtocol,   // 导出云台协议 
        LogSysSetImportSettings,   // 导入参数配置 
        LogSysSetExportSettings,   // 导出参数配置 
        LogSysSetExceptionSetting,   // 异常参数配置 
        LogRecordAll,   // 录像控制所有子项 
        LogRecordStartManual,   // 启动手动录像 
        LogRecordStartAuto,   // 启动自动录像 
        LogRecordStop,   // 停止结束 
        LogUserAll,   // 用户管理所有子项 
        LogUserAdd,   //添加用户 
        LogUserDelete,   // 删除用户 
        LogUserEdit,   // 修改用户资料 
        LogUserLogin,   //用户登录 
        LogUserLogout,   // 用户登出 
        LogAlarmAll,   // 报警所有子项 
        LogAlarmMotionStart,   // 移动侦测开始 
        LogAlarmMotionStop,   // 移动侦测结束 
        LogAlarmInputAlarm,   // 输入报警开始 
        LogExceptionAll,   // 异常所有子项 
        LogExceptionIpConflict,   // IP冲突 
        LogExceptionHardDiskFull,   // 硬盘满 
        LogExceptionHardDiskError,   // 硬盘出错 
        LogExceptionIllegalAccess,   // 非法访问 
        LogExceptionSignalLost,   // 信号丢失 
        LogExceptionSignalRecover,   // 信号恢复 
        LogExceptionNetDisconnect,   // 网线断 
        LogStorageAll,   // 存储管理所有子项 
        LogStorageFormatPartition,   // 分区格式化 
        LogStorageAddPartition,   // 添加分区 
        LogStorageRemovePartition,   // 删除分区　 
        LogStorageUnmountDisk,   // 卸载硬盘 
        LogStorageDiskGroupOperation,   // 磁盘分组管理 
    }

    /// <summary>
    /// 处理查询的连接，断开，操作
    /// 
    /// Version:1.0
    /// Date:2012/06/13
    /// Author:baobaoyeye
    ///
    /// Version:1.1
    /// Date:2012/06/27
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_Query
    {

#region 历史流查询

        /// <summary>
        /// 历史流查询连接操作（仅支持单一录像类型和所有录像类型）
        /// 历史流查询主要用于获取设备端录像数据在时间上的分布状况，查询结果以时间片段的形式给出，
        /// 用以建立历史录像数据时间轴，清晰的描述某通道某时间段的录像类型信息。
        /// 查询完成或中途终止时，需调用断开操作，以释放资源。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channel">通道号</param>
        /// <param name="diskGroup">盘组</param>
        /// <param name="hst">历史流类型</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>历史流查询句柄</returns>
        IntPtr HistoryStreamQueryConnect(
            Client_UserLogin userLogin,
            uint channel,
            uint diskGroup,
            HistoryStreamType hst,
            TimeInfo beginTime,
            TimeInfo endTime);

        /// <summary>
        /// 历史流查询下一条（仅支持单一录像类型和所有录像类型）
        /// 结果存放在 pStreamQueryResult 中，以时间片段和录像类型的形式给出。 
        /// </summary>
        /// <param name="streamHandle">历史流查询句柄</param>
        /// <returns>查询结果 一组key-value</returns>
        string[] HistoryStreamQueryNext(IntPtr streamHandle);

        /// <summary>
        /// 历史流查询连接操作（支持多种录像类型的组合）
        /// 历史流查询主要用于获取设备端录像数据在时间上的分布状况，查询结果以时间片段的形式给出，
        /// 用以建立历史录像数据时间轴，清晰的描述某通道某时间段的录像类型信息。
        /// 查询完成或中途终止时，需调用断开操作，以释放资源。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channel">通道号</param>
        /// <param name="diskGroup">盘组</param>
        /// <param name="streamType">流类型</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>历史流查询句柄</returns>
        IntPtr HistoryStreamMultiTypeQueryConnect(
            Client_UserLogin userLogin,
            uint channel,
            uint diskGroup,
            uint streamType,
            TimeInfo beginTime,
            TimeInfo endTime);

        /// <summary>
        /// 历史流查询下一条（支持多种录像类型的组合）
        /// 结果以时间片段和录像类型的形式给出。
        /// </summary>
        /// <param name="streamHandle">历史流查询句柄</param>
        /// <returns>查询结果 一组key-value</returns>
        Hashtable HistoryStreamMultiTypeQueryNext(IntPtr streamHandle);

        /// <summary>
        /// 历史流查询断开操作
        /// 查询完成或中途终止时，需调用断开操作，以释放资源。 
        /// </summary>
        /// <param name="streamHandle">历史流查询句柄</param>
        void HistoryStreamQueryDisconnect(IntPtr streamHandle);
#endregion

#region 日志查询

        /// <summary>
        /// 历史日志查询连接操作
        /// 日志查询主要用于获取设备端记录的系统本地和远程操作和运行信息。
        /// 查询完成或中途终止时，需调用断开操作，以释放资源。
        /// </summary>
        /// <param name="userLogin">用户句柄，通过用户登录获取的句柄 </param>
        /// <param name="QueryMode">查询方式:     默认</param>
        /// <param name="MajorType">主类型：      异常日志</param>
        /// <param name="MinorType">次类型：      异常所有子项</param>
        /// <param name="Channel">通道:         0</param>
        /// <param name="StartTime">起始日期时间: 2011/01/01-00:00:00</param>
        /// <param name="StopTime">终止日期时间: 2011/01/05-00:00:00</param>
        /// <returns>查询log句柄</returns>
        IntPtr LogQueryConnect(
            Client_UserLogin userLogin, 
            uint QueryMode, 
            LogMajorType MajorType, 
            LogMinorType MinorType, 
            uint Channel, 
            TimeInfo StartTime, 
            TimeInfo StopTime);

        /// <summary>
        /// 历史日志查询断开操作
        /// 查询完成或中途终止时，需调用断开操作，以释放资源。 
        /// </summary>
        /// <param name="logHandle">查询log句柄</param>
        void LogQueryDisconnect(IntPtr logHandle);

        /// <summary>
        /// 历史日志查询
        /// </summary>
        /// <param name="logHandle">查询log句柄</param>
        /// <returns>查询结果集key-value形式</returns>
        Hashtable LogQueryNext(IntPtr logHandle);
#endregion

#region 文件查询

        /// <summary>
        /// 文件查询连接操作
        /// 文件查询用于获取指定条件的文件详细信息。
        /// 查询完成或终止后，需调用断开操作，以释放资源。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channel">通道号</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>文件查询句柄</returns>
        IntPtr FileQueryConnect(
            Client_UserLogin userLogin,
            uint channel,
            uint fileType, 
            TimeInfo beginTime,
            TimeInfo endTime);

        /// <summary>
        /// 文件查询断开操作
        /// 查询完成或终止后，需调用断开操作，以释放资源。 
        /// </summary>
        /// <param name="fileQueryHandle">文件查询句柄</param>
        void FileQueryDisconnect(IntPtr fileQueryHandle);

        /// <summary>
        /// 文件查询下一条 
        /// </summary>
        /// <param name="fileQueryHandle">文件查询句柄</param>
        /// <returns>查询结果集的key-value值</returns>
        Hashtable FileQueryNext(IntPtr fileQueryHandle);
#endregion


#region 数据查询
        
        /// <summary>
        /// 获取数据大小（仅支持单一录像类型和所有录像类型）
        /// 由于此功能使用异步实现，所以此处的输出参数 dwDataSize 所返回的数据并不是有效的数据大小，但是此参数不可设置为 NULL。
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channelBits">数据管道掩码信息</param>
        /// <param name="dgt">盘组类型</param>
        /// <param name="hst">历史流类型</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <param name="userData">用户数据信息</param>
        /// <returns>查询数据大小</returns>
        uint GetDataSize(
            Client_UserLogin userLogin,
            uint channelBits,
            DiskGroupType dgt,
            HistoryStreamType hst,
            TimeInfo startTime,
            TimeInfo endTime,
            uint userData);

        /// <summary>
        /// 获取数据大小（支持多种录像类型的组合） 
        /// 由于此功能使用异步实现，所以此处的输出参数 dwDataSize 所返回的数据并不是有效的数据大小，但是此参数不可设置为 NULL。
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channelBits">通道掩码信息</param>
        /// <param name="dgt">磁盘分组信息</param>
        /// <param name="streamType">录像类型</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <param name="userData">用户数据</param>
        /// <returns>数据大小</returns>
        uint GetMultiTypeDataSize(
            Client_UserLogin userLogin,
            uint channelBits,
            DiskGroupType dgt,
            uint streamType,
            TimeInfo startTime,
            TimeInfo endTime,
            uint userData);

        /// <summary>
        /// 判断某天是否存在指定数据
        /// 给定一个月份，在 dwResult 中按位返回该月中每一天是否有指定类型的数据。 
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <param name="channel">数据通道信息</param>
        /// <param name="majorType">主类型信息</param>
        /// <param name="minorType">辅助类型信息</param>
        /// <param name="month">月份信息</param>
        /// <returns>查询结果</returns>
        uint[] DataExistCheck(
            Client_UserLogin userLogin,
            uint channel,
            uint majorType,
            uint minorType,
            TimeInfo month
            );
#endregion
    }
}

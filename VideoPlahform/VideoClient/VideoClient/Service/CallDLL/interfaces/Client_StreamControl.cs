using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 解码模式
    /// </summary>
    public enum DecodeMode
    {
        SoftDecode = 0,     //软件解码 
        HardDecode,         //硬解码卡解码 
        MatrixDecode        //矩阵解码卡解码
    }

    /// <summary>
    /// 解码显示模式
    /// </summary>
    public enum DecodeShowMode{
        NormalMode = 0,    //普通模式 
        OverlayMode        //覆盖模式，一台客户端同一时间只能启动一个
    }

    /// <summary>
    /// 叠加字符和图像回调的方式 
    /// </summary>
    public enum DrawFuncMode
    {
        DrawFuncModeBegin = 0,
        AutoSizeByShowWnd,     //画图坐标随显示的变化而变化 
        FixedPosition          //画图坐标及大小为相对于显示窗口的绝对坐标和尺寸
    }

    /// <summary>
    /// 图像显示质量高低模式
    /// </summary>
    public enum ShowQualityMode
    {
        ShowQualityModeBegin = 0,   
        LowQuality,  //低质量 
        HighQuality  //高质量
    }

    /// <summary>
    /// 任务控制枚举类型
    /// </summary>
    public enum TaskControl
    {
        TaskStart = 0,  //任务启动 
        TaskStop,  //任务停止 
        TaskSetSpeed,  //历史流快放时，设置快放速度
    }

    /// <summary>
    /// 媒体功能枚举类型 
    /// </summary>
    public enum MediaFunctionType
    {
        MediaFunctionTypeBegin = 0,  //起始值 
        MediaFunctionRealStream,  //实时流 
        MediaFunctionVoiceStream,  //语音流 
        MediaFunctionHistoryStream,  //历史流 
        MediaFunctionTransparent,  //透明通道  
        MediaFunctionFileUpload,  //文件上传 
        MediaFunctionFileDownload,  //文件下载 
        MediaFunctionTypeEnd,  //结束值 
    }

    /// <summary>
    /// 处理流媒体播放的控制
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_StreamControl
    {
        bool IsStreamPlaying
        {
            get;
            set;
        }

        /// <summary>
        /// 截图路径名
        /// </summary>
        string FilePath
        {
            get;
            set;
        }

        /// <summary>
        /// 打开默认流解码器,在流解码回放之前调用
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamOpen(IntPtr hStream);

        /// <summary>
        /// 打开之定义流解码器,在流解码回放之前调用
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="dm">解码模式枚举</param>
        /// <param name="dsm">解码显示模式枚举</param>
        /// <param name="ColorRef">若是 OVERLAY 模式显示图像，则需要指定一种关键色，
        /// 在打开之前需要把显示窗口的背景色设置为此颜色 </param>
        /// <param name="dfm">叠加字符和图片模式</param>
        /// <param name="userData">叠加字符和图片回调函数回调时的用户数据</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamOpen(IntPtr hStream, 
            DecodeMode dm, 
            DecodeShowMode dsm, 
            uint ColorRef,
            DrawFuncMode dfm,
            uint userData);

        /*
        void StreamOpen(Client_RealStream realStream,
            DecodeMode dm,
            DecodeShowMode dsm,
            uint ColorRef,
            DrawFuncMode dfm,
            ////
            uint userData);
        */

        /// <summary>
        /// 开始流解码回放 
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="ComponentHandler">图像显示组件句柄</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamPlay(IntPtr hStream, IntPtr ComponentHandler);

        /// <summary>
        /// 开始录像
        /// </summary>
        /// 返回1,失败.返回0,成功
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        int StartRecord(IntPtr hStream);

        /// <summary>
        /// 停止录像
        /// </summary>
        void StopRecord();
        /// <summary>
        /// 暂停播放流媒体
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamPause(IntPtr hStream);

        /// <summary>
        /// 恢复暂停播放流媒体
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamResume(IntPtr hStream);

        /// <summary>
        /// 停止播放流媒体（下次播放前先Open）
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamStop(IntPtr hStream);

        /// <summary>
        /// 设置声音音量 
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="inVolumn">音量，范围( 0-100 ) </param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamSetVolume(IntPtr hStream, int inVolumn);

        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>注意此函数必须在界面线程里调用，否则将听不到声音 </remarks>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamPlaySound(IntPtr hStream);

        /// <summary>
        /// 停止播放声音
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamStopSound(IntPtr hStream);

        /// <summary>
        /// 获取已经播放的时间（此时间来自于播放器计算的时间）
        /// </summary>
        /// <param name="hStream">流句柄,通过连接实时流或历史流获取</param>
        /// <returns>流媒体已经播放的时间</returns>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        uint StreamGetPlayedTime(IntPtr hStream);

        /// <summary>
        /// 获取正在播放的时间
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <returns>当成功返回返回时则表示获取正在播放的时间正常，此时间是自 1970 以来的秒数（精度：秒）</returns>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        uint StreamGetAbsoluteTime(IntPtr hStream);

        /// <summary>
        /// 获取已经播放的帧数（此时间来自于播放器计算的帧数）
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <returns>用于保存已经播放的帧数 </returns>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        uint StreamGetPlayedFrameNum(IntPtr hStream);

        /// <summary>
        /// 设置亮度
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="inBright">亮度值 1-100</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamChangeBright(IntPtr hStream, int inBright);

        /// <summary>
        /// 设置图像显示质量（由于目前显卡性能一般均较强劲，一般可不调用）
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取 </param>
        /// <param name="sqm">图像显示质量高低模式枚举类型</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamSetPicQuality(IntPtr hStream,ShowQualityMode sqm);

        /// <summary>
        /// 设置对比度 
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="inContrast">对比度值，范围 1-100 </param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamChangeContrast(IntPtr hStream, int inContrast);

        /// <summary>
        /// 抓图
        /// </summary>
        /// <param name="hStream">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="inFileName">抓图保存文件名(不包括路径名)</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void StreamSnapShot(IntPtr hStream, string inFileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hStream"></param>
        /// <param name="tc"></param>
        void StreamMediaControl(IntPtr hStream,TaskControl tc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hStream"></param>
        /// <param name="linkId"></param>
        void SetStreamMediaLinkID(IntPtr hStream,uint linkId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="mft"></param>
        /// <returns></returns>
        List<string> QueryStreamMediaLinkID(Client_UserLogin userLogin,MediaFunctionType mft);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Hie_MP4Player
{
    public class HIE_MP4Player
    {
        public const int USER = 0x500;

        public const int MYMESSAGE = USER + 1;//自定义播放结束消息

        public const int MP4Play_MAX_SUPPORTS = 64; //最大支持通道数

        public const int MP4Play_NOERROR = 0;  //
        public const int MP4Play_PARA_OVER = 1;  //
        public const int MP4Play_ORDER_ERROR = 2;  //
        public const int MP4Play_NOT_SUPPORT = 3;  //
        public const int MP4Play_DEC_VIDEO_ERROR = 4;  //
        public const int MP4Play_DEC_AUDIO_ERROR = 5;  //
        public const int MP4Play_ALLOC_MEMORY_ERROR = 6;  //
        public const int MP4Play_OPEN_FILE_ERROR = 7;  //
        public const int MP4Play_SUPPORT_OPEN_ONLY = 8;  //
        public const int MP4Play_CREATE_DDRAW_ERROR = 9;  //
        public const int MP4Play_CREATE_OFFSCREEN_ERROR = 10;  //
        public const int MP4Play_BUF_OVER = 11;  //
        public const int MP4Play_CREATE_SOUND_ERROR = 12;  //
        public const int MP4Play_SET_VOLUME_ERROR = 13;  //
        public const int MP4Play_SUPPORT_FILE_ONLY = 14;  //
        public const int MP4Play_SUPPORT_STREAM_ONLY = 15;  //
        public const int MP4Play_SYS_NOT_SUPPORT = 16;  //
        public const int MP4Play_FILEHEADER_UNKNOWN = 17;  //
        public const int MP4Play_VERSION_INCORRECT = 18;  //
        public const int MP4Play_INIT_DECODER_ERROR = 19;  //
        public const int MP4Play_CHECK_FILE_ERROR = 20;  //
        public const int MP4Play_SUPPORT_FILESTREAM_ONLY = 21;  //
        public const int MP4Play_BLT_ERROR = 22;  //
        public const int MP4Play_UPDATE_ERROR = 23;  //

        public const int MAX_DISPLAY_WND = 4;  // 最大显示窗口数

        //Display type
        public const int  DISPLAY_NORMAL = 1;  // 正常分辨率数据送显卡显示
        public const int DISPLAY_QUARTER = 2;  // 1/4分辨率数据送显卡显示 

        public const int SUPPORT_DDRAW = 1;  //  支持DIRECTDRAW
        public const int SUPPORT_BLT = 2;  //  显卡支持BLT操作
        public const int SUPPORT_BLTFOURCC = 4;  //  显卡BLT支持颜色转换
        public const int SUPPORT_BLTSHRINKX = 8;  //  显卡BLT支持X轴缩小
        public const int SUPPORT_BLTSHRINKY = 16;  //  显卡BLT支持Y轴缩小  
        public const int SUPPORT_BLTSTRETCHX = 32;  //  显卡BLT支持X轴放大
        public const int SUPPORT_BLTSTRETCHY = 64;  //  显卡BLT支持Y轴放大
        public const int SUPPORT_SSE = 128;  //  CPU支持SSE指令
        public const int SUPPORT_MMX = 256;  //  CPU支持MMX

        public const int MAX_DIS_FRAMES = 50;  // 最大缓存数据帧数 
        public const int MIN_DIS_FRAMES = 6;  // 最小缓存数据帧数 

        public const int BY_FRAMENUM = 1;  // 按帧序号查找
        public const int BY_FRAMETIME = 2;  // 按帧时戳查找 

        public const int SOURCE_BUF_MAX = 2048 * 1000 * 4;  //最大缓冲区
        public const int SOURCE_BUF_MIN = 1024 * 50;  // 最小缓冲区

        public const int STREAME_REALTIME = 0;  // 实时流
        public const int STREAME_FILE = 1;  // 文件流

        public const int T_AUDIO16 = 101;  // 采样率8000,单声道,每个采样点16位的音频帧
        public const int T_UYVY = 1;  // UYVY的视频帧
        public const int T_YV12 = 3;  // YV12的视频帧
        public const int T_RGB32 = 7;  // 32位RGB的视频帧

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct FRAME_POS
        {
	        public long nFrameNum;          
	        public uint nFrameTime;          
	        public ulong nFilePos;            
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct FRAME_INFO
        {
	        public long nWidth;
	        public long nHeight;
	        public long nStamp;
	        public  long nType;
	        public long nFrameRate;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct FRAME_TYPE
        {
            public IntPtr pDataBuf; //数据帧首地址 ,得marshal转换为char*
	        public long nSize;		//数据帧的大小
	        public long nFrameNum; //数据帧的个数
	        public bool bIsAudio;	//是否音频帧
	        public long nReserved; //保留
        }

        /// <summary>
        /// 锁定一个没有被锁定的回放通道 
        /// </summary>
        /// <param name="pdwPort">pdwPort 被锁定的通道号 </param>
        /// <returns>TRUE表示成功锁定一个通道,在没有调用 MP4Play_UnlockPort(LONG nPort) 
        /// 对锁定的通道解锁前将不会再被锁定。FALSE表示当前所有的通道均被锁定 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_LockNewPort", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_LockNewPort(ref int pdwPort);

        /// <summary>
        /// 对一个锁定的回放通道进行解锁
        /// </summary>
        /// <param name="nPort">通道号,MP4Play_LockNewPort(LONG* pdwPort) 调用成功时得到的通道号</param>
        /// <returns>TRUE表示对指定的回放通道成功解锁。FALSE表示通道错误或指定的回放通道未被锁定  </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_UnlockPort", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_UnlockPort(int nPort);

        /// <summary>
        /// 打开播放文件
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="sFileName">文件名，文件不能超过4G或小于4K  </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_OpenFile", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_OpenFile(int nPort, string sFileName);

        /// <summary>
        /// 关闭播放文件
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_CloseFile", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_CloseFile(int nPort);

        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        public delegate void pFileRefDone(int nPort, uint nUser);
 

        /// <summary>
        /// 设置回调函数指针，文件索引建立后回调
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="cb_filerefdone">回调函数指针，如果为NULL就不建索引</param>
        /// <param name="nUser">用户数据 </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetFileRefCallBack",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetFileRefCallBack(int nPort,
            pFileRefDone cb_filerefdone,uint nUser);

        /// <summary>
        /// 设置文件结束时发送的消息
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="hWnd">播放窗口句柄</param>
        /// <param name="nMsg">播放结束时用户接收到的消息 </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetFileEndMsg", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetFileEndMsg(int nPort, IntPtr hWnd, uint nMsg);

        #region vedio control

        /// <summary>
        /// 开始播放 
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="hWnd">播放窗口句柄 </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_Play", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_Play(int nPort, IntPtr hWnd);

        /// <summary>
        /// 暂停/恢复播放  
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="nPause">为TRUE暂停，否则恢复 </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_Pause", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean  MP4Play_Pause(int nPort, Boolean nPause);

        /// <summary>
        /// 停止播放 
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_Stop", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_Stop(int nPort);

        /// <summary>
        /// 快速播放  
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>每次调用将使当前播放速度加快一倍，最多调用4次；要恢复正常播放调用 MP4Play_Play(),从当前位置开始正常播放。</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_Fast", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean  MP4Play_Fast(int nPort);

        /// <summary>
        /// 慢速播放  
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>每次调用将使当前播放速度慢一倍；最多调用4次；要恢复正常播放调用 MP4Play_Play()。 </remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_Slow", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_Slow(int nPort);

        /// <summary>
        /// 单帧播放   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_OneByOne", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_OneByOne(int nPort);

        /// <summary>
        /// 单帧退回放   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_OneByOneBack", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_OneByOneBack(int nPort);

        #endregion

        #region audio control

        /// <summary>
        /// 设置音量    
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="nVolume">nVolume 音量大小，范围0– 0xffff  </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetVolume", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetVolume(int nPort,ushort nVolume);

        /// <summary>
        /// 关闭声音    
        /// </summary>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_StopSound", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_StopSound();  
        
        /// <summary>
        /// 打开声音     
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_PlaySound", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_PlaySound(int nPort); 

         /// <summary>
        /// 获得音量     
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_GetVolume", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort MP4Play_GetVolume(int nPort);

        #endregion 

        #region Display Control

        /*图像质量*/

        /// <summary>
        /// 设置源缓冲区阀值和剩余数据小于等于阀值时的回调函数指针  
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="bHighQuality">画面质量，TRUE高质量，FALSE低质量 </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>当设置成高质量时画面效果好，但CPU 利用率高。在支持多路播放时，可以设为低质量，
        /// 以降低CPU 利用率；当某路放大 播放时将该路设置成高质量，以达到好的画面效果</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetPicQuality", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetPicQuality(int nPort, Boolean bHighQuality); 

        /// <summary>
        /// 获得当前通道的图像质量   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="bHighQuality">TRUE表示高质量，FALSE表示低质量</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_GetPictureQuality", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_GetPictureQuality(int nPort,ref Boolean bHighQuality);

        /*OVERLAY模式*/
        //.....未写

        /*局部放大*/
        ///<summary>
        ///对图像感兴趣部分进行放大
        /// </summary>
        ///<param name="nPort">通道号</param>
        ///<param name="bLocalZoom">为TRUE进行局部放大，为FALSE不进行局部放大 </param>
        ///<param name="Left">需要局部放大区域的X坐标</param>
        ///<param name="Top">需要局部放大区域的Y坐标</param>
        ///<param name="Width">需要局部放大的宽度(像素)</param>
        ///<param name="Height">需要局部放大的高度度(像素)</param>
        ///<returns>TRUE表示成功,FALSE表示失败</returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_LocalZoom", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_LocalZoom  (  int      nPort,  
                              Boolean  bLocalZoom,  
                              int      Left,  
                              int      Top,  
                              int      Width,  
                              int      Height  
                             ) ;

        public struct RECT {
            int left;
            int Top;
            int Width;
            int Height;
        }
        ///<summary>
        ///设置或增加显示区域。可以做局部放大显示
        /// </summary>
        ///<param name="nPort">通道号</param>
        ///<param name="nRegionNum">显示区域序号，0~(MAX_DISPLAY_WND-1)。如果nRegionNum为0，表示对主要显示窗口( MP4Play_Play(LONG nPort, HWND hWnd) 中设置的窗口)进行设置，将忽略hDestWnd和bEnable 的设置  </param>
        ///<param name="pSrcRect">设置在要显示的原始图像上的区域，如：如果原始图像是352*288，那么pSrcRect可设置的范围只能在(0，0，352，288)之中。 如果pSrcRect=NULL,将显示整个图像 </param>
        ///<param name="hDestWnd">设置显示窗口。如果该区域的窗口已经设置过(打开过)，那么该参数被忽略</param>
        ///<param name="bEnable">打开(设置)或关闭显示区域</param>
        ///<returns>TRUE表示成功,FALSE表示失败</returns>
        ///<remarks>如果机器的显卡只能支持DibDraw的显示模式， 对于非主窗口( MP4Play_Play(LONG nPort, HWND hWnd) 中设置的窗口)之外的显示区域都将无法显示视频图像。</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetDisplayRegion", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetDisplayRegion(int nPort,
                                                                  uint nRegionNum,
                                                                  RECT pSrcRect,
                                                                  IntPtr hDestWnd,
                                                                  Boolean bEnable
                                                                 );



        #endregion

        #region capture image

        /// <summary>
        /// 抓图回调函数 
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pBuf">图像数据缓冲区(带边，Y 16像素,UV各8像素)</param>
        /// <param name="nSize">图像数据大小</param>
        /// <param name="nWidth">画面宽，单位像素</param>
        /// <param name="nHeight">画面高</param>
        /// <param name="nStamp"> 时戳信息，单位毫秒</param>
        /// <param name="nType">位图格式(保留，暂未用)，RGB24 </param> 
        /// 宏定义       宏定义值            含义 
        /// T_AUDIO16     101            采样率8000,单声道,每个采样点16位的音频帧 
        /// T_UYVY        1              UYVY的视频帧  
        /// T_YV12        3              YV12的视频帧
        /// T_RGB32       7              32位RGB的视频帧 
        /// <param name="nReserved">保留</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        public delegate void DisplayCBFun(int nPort,
             IntPtr pBuf,int nSize,int nWidth,int nHeight,int nStamp,int nType,int nReserved);

        /// <summary>
        /// 设置抓图回调函数 
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="cb_display">回调函数指针，可以为NULL</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>设置抓图回调函数；注意要尽快返回，如果要停止回调，可以把回调函数指针DisplayCBFun设为NULL.
        /// 一旦设置回调函数，则一直有效， 直到程序退出。该函数可以在任何时候调用</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetDisplayCallBack", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetDisplayCallBack(int nPort, DisplayCBFun cb_display);

        /// <summary>
        /// 直接抓图
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pBuf">数据保存的缓冲区</param>
        /// <param name="nSize">函数调用作为输入参数时表示pBuf缓冲区的长度，函数返回作为输出参数时表示实际位图文件的大小</param>
        /// <param name="nWidth">位图宽度(保留，暂未用)，可直接访问BITMAPINFOHEADER获取宽度</param>
        /// <param name="nHeight">位图高度(保留，暂未用)，可直接访问BITMAPINFOHEADER获取高度</param>
        /// <param name="nType">位图格式(保留，暂未用)，RGB24 </param>
        /// <param name="sFileName">要保存的文件名,最好以BMP作为文件扩展名</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>缓冲区的长度必须大于等于sizeof(BITMAPFILEHEADER) + 
        /// sizeof(BITMAPINFOHEADER) + nWidth * nHeight * 3个字节，否则将发生内存越界错误。
        /// 此抓图函数与回调方式抓图在效果上没有区别，但在使用方式有区别.本函数将截取当前帧的图像直到返回为止 </remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SnapBMP", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SnapBMP(int nPort, Byte[] pBuf, ref int nSize, ref int nWidth,
            ref int nHeight,ref int nType );

        /// <summary>
        /// 将抓图得到的图像数据保存成BMP文件
        /// </summary>
        /// <param name="pBuf">数据保存的缓冲区</param>
        /// <param name="nSize">图像大小</param>
        /// <param name="nWidth">位图宽度(保留，暂未用)，可直接访问BITMAPINFOHEADER获取宽度</param>
        /// <param name="nHeight">位图高度(保留，暂未用)，可直接访问BITMAPINFOHEADER获取高度</param>
        /// <param name="nType">位图格式(保留，暂未用)，RGB24 </param>
        /// <param name="sFileName">要保存的文件名,最好以BMP作为文件扩展名</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>转换函数占用的cpu资源，如果不需要保存图片，则不要调用</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_ConvertToBmpFile", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_ConvertToBmpFile(IntPtr pBuf, int nSize, int nWidth, 
            int nHeight, int nType, string sFileName);

        #endregion
        
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        public delegate void SourceBufCallBack(int nPort, uint nBufSize, uint dwUser, IntPtr pResvered); 

        /// <summary>
        /// 设置源缓冲区阀值和剩余数据小于等于阀值时的回调函数指针  
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="nThreShold">阀值，应该小于MP4Play_OpenStream设置的缓冲大小</param>
        /// <param name="cb_sourcebuf">回调函数指针</param>
        /// /// <param name="dwUser">用户数据</param>
        /// /// <param name="pReserved">保留数据</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetSourceBufCallBack", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetSourceBufCallBack(int nPort, uint nThreShold,
            SourceBufCallBack cb_sourcebuf,uint dwUser,IntPtr pReserved);
        
        /// <summary>
        /// 设置流播放的模式。   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="nMode">nMode:STREAME_REALTIME</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>必须在播放之前设置</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_SetStreamOpenMode",CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_SetStreamOpenMode(int nPort, uint nMode);

        /// <summary>
        /// 获取流播放的模式。   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_GetStreamOpenMode", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_GetStreamOpenMode(int nPort);

        /// <summary>
        /// 得到当前版本播放器能播放的文件的文件头长度  
        /// </summary>
        /// <returns>此版本播放器对应的文件头的长度 </returns>
        /// <remarks>必须在播放之前设置</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_GetFileHeadLength")]
        public static extern uint MP4Play_GetFileHeadLength();

         /// <summary>
        /// 打开流接口(类似打开文件)   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pFileHeadBuf">用户从卡上得到的文件头数据 </param>
        /// /// <param name="nSize">nBufPoolSize</param>
        /// /// <param name="nBufPoolSize">设置播放器中存放数据流的缓冲区大小。范围是 SOURCE_BUF_MIN ~ SOURCE_BUF_MAX  </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>必须在播放之前设置</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_OpenStream", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_OpenStream(int nPort, Byte[] pFileHeadBuf, uint nSize, uint nBufPoolSize);
    
        /// <summary>
        /// 结束流   
        /// </summary>
        /// <param name="nPort">通道号</param>
       /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_CloseStream", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_CloseStream(int nPort);

        /// <summary>
        /// 输入从卡上得到的流数据   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// /// <param name="pBuf">缓冲区地址</param>
        /// /// <param name="nSize">缓冲区大小</param>
        /// <returns>成功为TRUE,表示已经输入数据。FALSE 表示失败，数据没有输入 </returns>
        /// <remarks>打开流之后才能输入数据</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_InputData", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_InputData(int nPort, byte[] pBuf, uint nSize);

        /// <summary>
        /// 获得码流中原始图像的大小   
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// /// <param name="pBuf">原始图像的宽。在PAL制CIF格式下是352</param>
        /// /// <param name="nSize">原始图像的高。在PAL制CIF格式下是288 </param>
        /// <returns>TRUE表示成功，FALSE表示失败 </returns>
        /// <remarks>根据获取的图像大小来设置显示窗口的区域，可以不用显卡做缩放工作，对于那些不支持硬件缩放的显卡来说非常有用</remarks>
        [DllImport("HIE_MP4Play.dll", EntryPoint = "MP4Play_GetPictureSize", CallingConvention = CallingConvention.StdCall)]
        public static extern Boolean MP4Play_GetPictureSize(int nPort,ref int pWidth,ref int pHeight);

    }
}

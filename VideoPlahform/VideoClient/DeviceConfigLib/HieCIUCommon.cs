using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIE_RESULT=System.Int32;
using System.Runtime.InteropServices;
//对应的是HieCIUCommon.h
namespace HieCiULib
{
    public class HieCIUCommon
    {
        public const int HIEC_BASE_ERROR			=10000;

        public const int HIEC_ERR_OK				=0;
        public const int HIEC_ERR_PARAMETER		=(HIEC_BASE_ERROR+1);			// 参数出错
        public const int HIEC_ERR_ORDER			=(HIEC_BASE_ERROR+2);			// 调用顺序出错
        public const int HIEC_ERR_NEEDMEMORY		=(HIEC_BASE_ERROR+3);			// 需要更大的缓冲区或内存

        public const int HIEC_ERR_UNSUPPORTED	=(HIEC_BASE_ERROR+100);		// 不支持的操作或接口
        public const int HIEC_ERR_NORESOURCE		=(HIEC_BASE_ERROR+101);		// 无更多的资源
        public const int HIEC_ERR_INVALID_PROC	=(HIEC_BASE_ERROR+200);		// 无效的SDK接口，可能是解码库的版本不正确
        public const int HIEC_ERR_CALLSDK		=(HIEC_BASE_ERROR+201);		// 调用软解码SDK或矩阵解码SDK失败
        public const int HIEC_ERR_FILE_USING		=(HIEC_BASE_ERROR+202);		// 文件正在被使用
        public const int HIEC_ERR_WRITE_FILE	=	(HIEC_BASE_ERROR+203);		// 写文件失败
        public const int HIEC_ERR_INVALID_CODEC=	(HIEC_BASE_ERROR+204);		// 无效的编码格式
        public const int HIEC_ERR_AUDIO_OUT		=(HIEC_BASE_ERROR+205);		// 音频输出设备错误
        public const int HIEC_ERR_INVALID_VOICE=	(HIEC_BASE_ERROR+206);		// 无效的对讲连接


        public const int SYSINFO_CAPS_MATRIX_DEC_WORK_MODE		=	(0x01 << 0);		// 表示矩阵解码的工作模式字段有效
        public const int SYSINFO_CAPS_OTHER_PARAMETER			=		(0x01 << 1);		// 可以依次扩展

        /*!
        *  \enum tagSystemInfo
        *  \brief 启动本库需要在HieCIU_StartUp之前设置的参数
        *         如果不设置，系统将以默认的方式进行参数的初始化
        */
        //StructLayoutAttribute:控制类或结构中字段的物理布局
        //LayoutKind.Sequential:按成员出现的顺序依次布局
        [StructLayoutAttribute(LayoutKind.Sequential)]
        
        public struct SYSTEM_INFO
        {
	        public uint   dwSysCaps;									/*!< 表示当前哪些字段为有效参数 */

	        public tmsdk.DecoderCardWorkMode_t	dwMatrixDecodeWorkMode;	/*!< 矩阵卡解码工作模式, 默认是解码CIF */
	                                            						/*!< 用于以后扩展其它的参数*/
            //MarshalAsAttribute:指示如何在托管代码和非托管代码之间封送数据。
            //UnmanagedType:指定如何将参数或字段封送到非托管代码,有它则必须指定SizeConst
            //ArraySubType:区分字符串类型
            //U4:4字节无符号整数
            //[]里的定义只对下一个变量，结构，类有效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst=16)]
            
            public uint[] dwReservedValue;

	        public IntPtr pdwReservedPtr;								/*!< 保留指针，如果连接扩展参数也够用可用此指针*/
        }

        /*!
         *  \enum eStreamMode 
         *  \brief 媒体流类型定义(主要包含实时流和历史流)
         */
        public enum StreamMode
        {
	        eStreamModeBegin,
	        eStreamReal,					/*!< 实时流 */
	        eStreamHistory,					/*!< 历史流 */
	        eStreamEnd
        }

        /*!
         *  \struct tagDecodedFrame 
         *  \brief  解码回调所用帧信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct DecodedFrameHeader 
        {
	        public uint nFrameType;		/*!< 帧类型 */
	        public uint nTimeStamp;		/*!< 帧时间戳 */
            //平台相关的字符字符串
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string pFrameBuffer; /*!< 解码后的帧内容 */
	        public uint nFrameSize;		/*!< 解码后的帧内容长度，以字节为单位 */
        }

        /*!
        *  \fn (void* CB_Decode)(IN PT_DECODE_FRAME_HEADER pDecodedFrame, 
								        IN inUserData);
         *  \brief  解码回调函数，以最大速度进行解码及早回调给用户
         *  \param  pDecodedFrame  解码帧结构体
         *  \return HIEC_ERR_OK, 成功
         *  \return 其它值详见错误码定义
         */
        //delegate声明,任何与该委托的返回类型和参数都相同的函数都可以通过该委托类似于函数指针的方式调用
        public delegate void CB_Decode(ref DecodedFrameHeader pDecodedFrame, uint inUserData);

        //StructLayout特性允许我们控制Structure语句块的元素在内存中的排列方式,以及当这些元素被传递给外部DLL时，运行库排列这些元素的方式
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] //顺序内存布局,ANSI编码方式

        public struct tm
        {
            public int tm_sec;
            public int tm_min;
            public int tm_hour;
            public int tm_mday;
            public int tm_mon;
            public int tm_year;
            public int tm_wday;
            public int tm_yday;
            public int tm_isdst;
        }

        /*!
         *  \struct tagTimeSlice 
         *  \brief  时间段信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct TimeSlice
        {
	        public tm tmStart;						/*!< 某一个时间段开始时间(绝对时间)*/
	        public tm tmEnd;						/*!< 某一个时间段结束时间(绝对时间)*/
        }


        /*!
         *  \enum eSingalStandard
         *  \brief 信号制式 
         */
        public enum eSingalStandard
        {
	        eSingalStandardBegin,
	        eSingalPAL,						/*!< PAL制 */
	        eSingalNTSC,					/*!< NTSC制 */
	        eSingalSECAM,					/*!< SECAM制 */
	        eSingalStandardEnd
        }

        //用于调试
        /*!
         *  \enum eControlDeviceFlag 
         *  \brief  设备控制标识定义
         */
        public enum eControlDeviceFlag
        {
            CDF_GET_IO_IN_STATE,				/*!< 获取IO输入状态*/
            CDF_SET_IO_IN_STATE,				/*!< 设置IO输入状态*/
            CDF_GET_IO_OUT_STATE,				/*!< 获取IO输出状态*/
            CDF_SET_IO_OUT_STATE,				/*!< 设置IO输出状态*/

            CDF_GET_RECORD_STATE,				/*!< 获取录像状态*/
            CDF_MANAAL_RECORD_START,			/*!< 远程启动手动录像*/
            CDF_MANAAL_RECORD_STOP,				/*!< 远程停止手动录像*/

            CDF_CLS_ALARM_ACTION,				/*!< 远程清除报警*/
            CDF_REBOOT,							/*!< 远程重启*/
            CDF_FORCE_IFRAME,					/*!< 远程强制I帧*/
        }			

        /*!
         *  \enum eNettalkCodec 
         *  \brief  支持的对讲音频编码器类型
         */
        public enum eNettalkCodec
        {
	        eNettalkCodecBeginValue,
	        eNettalkCodecG726		= 0x01,		/*!< G726 */
	        eNettalkCodecG729		= 0x02,		/*!< G729 */
	        eNettalkCodecADPCMima	= 0x04,		/*!< ADPCMima */
	        eNettalkCodecPCMA		= 0x08,		/*!< PCM-A */
	        eNettalkCodecPCMU		= 0x10,		/*!< PCM-U */
	        eNettalkCodecEndValue
        }

        /*!
         *  \enum eNettalkCodec 
         *  \brief  音频采样率(每秒钟会采集多个样本)
         */
        public enum eSamplePerSecond
        {
	        eSamplePerSecondBeginValue = 0,
	        eSamplePerSecond_8000  = 8000,		/*!< 每秒8000次 */
	        eSamplePerSecond_11025 = 11025,		/*!< 每秒11025次 */
	        eSamplePerSecond_22050 = 22050,		/*!< 每秒22050次 */
	        eSamplePerSecond_44100 = 44100,		/*!< 每秒44100次 */
	        SamplePerSecondEndValue
        }

        /*!
         *  \struct tagAudioCodecParam 
         *  \brief  音频编码参数
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct AudioCodecParam 
        {
	        public eNettalkCodec CodecID;
	        public uint  nChannel;				/*!< 声道(设备对讲一般是单声道:1) */
	        public uint  nBitsPerSample;		/*!< 每个采样多少位表示(设备对讲一般是16位表示) */
	        public uint  nSamplePerSecond;		/*!< 采样频率(设备对讲一般是8000次) */
        }


        /*!
        *  \fn void (*CB_Nettalk)(IN unsigned int inUserID,  
						        IN eNettalkCodec inCodec,
						        IN PT_AUDIO_CODEC_PARAM inCodecParam,
						        IN eVoiceStreamSource inDataSource,
						        IN char* inBuffer,
						        IN unsigned int inBufLen, 
						        IN unsigned int inUserData);
         *  \brief  语音对讲回调,可将本地对讲的音频和远程音频都回调给应用层
         *  \param  inUserID  用户登陆ID
         *  \param  inCodecParam  编码参数
         *  \param  inDataSource  数据来此哪里,本地还远程
         *  \param  inBuffer  编码后的数据
         *  \param  inBufLen  编码后的数据的长度
         *  \param  inUserData  用户数据
         *  \return HIE_ERR_OK, 成功
         *  \return 其它值详见错误码定义
         */
        public delegate int CB_Nettalk(System.IntPtr hStream, ref AudioCodecParam inCodecParam, 
            Common.eVoiceStreamSource inDataSource, string inBuffer, uint inBufLen, uint inUserData);

        /*!
         *  \enum eShowQualityMode 
         *  \brief  图像显示质量高低模式
         */
        public enum eShowQualityMode
        {
	        eShowQualityModeBegin,
	        eLowQuality,					/*!< 低质量 */
	        eHighQuality,					/*!< 高质量 */
	        eShowQualityModeEnd
        }


        /*!
         *  \enum eDrawFuncMode 
         *  \brief  叠加字符和图像回调的方式
         */
        public enum eDrawFuncMode
        {
	        eDrawFuncModeBegin,
	        eAutoSizeByShowWnd,				/*!<  画图坐标随显示的变化而变化 */
	        eFixedPosition,					/*!<  画图坐标及大小为相对于显示窗口的绝对坐标和尺寸 */
	        eDrawFuncModeEnd
        }

        /*!
        *  @brief
        *	叠加字幕和图像回调,只支持软解码
        * @param hStream
        *	流媒体句柄,通过连接流获取,表示流媒体数据的来源
        * @param inDrawDC  
        *	绘图表面的ＤＣ
        * @param dwUserData
        *	用户数据,设置流媒体数据回调时传入的用户数据
        * @return
        *	返回0
        * @note
        *	由于此回调函数在流媒体数据接收线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        */
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        public delegate void CB_DrawFun(System.IntPtr hStream, System.IntPtr inDrawDC, uint dwUserData);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HDC__ {
    
            /// int
            public int unused;
        }

        /*!< 文件名最大长度 */
        public const int HIECIU_MAX_FILENAME_LEN				=256;
        /*!
        *  @brief
        *	用于获取视频流下载时文件名的回调
        * @param hStream
        *	流媒体句柄,通过连接流获取,表示流媒体数据的来源
        * @param inbComplete
        *   下载是否完成，TRUE表示完成，FALSE表示未完成, 0xFFFFFFFF表示当前是通知下载的字节数
        * @param pFileName  
        *	文件名缓冲区，本Buffer的长度为HIECIU_MAX_FILENAME_LEN,请注意文件名不要超过本长度
        * @param pAbsoluteTime
        *   开始下载视频的绝对时间
        * @param indwWrittedSize
        *   已经下载了多少字节
        * @return
        *	返回TRUE(1)表示生成成功，SDK将会使用这个pFileName文件名；
        *   返回FALSE(0)表示生成失败，SDK将会使用自身生成的文件名(一般情况，用户最好不要让其生成失败)
        * @note
        *	由于此回调函数在流媒体数据接收线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        */
        //UnmanagedFunctionPointerAttribute:控制作为非托管函数指针传入或传出非托管代码的委托签名的封送行为
        //CallingConvention:指定调用在非托管代码中实现的方法所需的调用约定。
        //StdCall:被调用方清理堆栈。 这是使用平台 invoke 调用非托管函数的默认约定。
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        //注意原头文件里是BOOL,是一个三值逻辑,<0 error; >0 true; ==0 false 
        public delegate int CB_SpecifyFileName(IntPtr hStream,  int inbComplete,
        ref string pFileName, ref Common.AbsoluteTime pAbsoluteTime, long indwWrittedSize);
        /*!
         *  \struct tagLocalConfig 
         *  \brief  本地配置即PC机配置情况
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct LocalConfig 
        {
	        public uint  nMatrixDecChannelNum;			/*!< 矩阵解码卡的解码通道数*/
	        public uint  nMatrixDisplayChannelNum;		/*!< 矩阵解码卡的显示通道数*/
        }


        /*!
         *  \enum eDecodeModeSetting 
         *  \brief 解码模式设置
         */
        public enum eDecodeModeSetting
        {
	        eDecodeModeSettingAll				=0,		/*!< 软解码模式与矩阵解码模式	*/
	        eDecodeModeSettingSoft,						/*!< 软解码模式					*/
	        eDecodeModeSettingMatrix					/*!< 矩阵解码模式				*/
        }

    }
}

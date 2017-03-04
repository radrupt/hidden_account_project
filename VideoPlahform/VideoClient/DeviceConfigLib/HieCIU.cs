using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/************* 定义常用的别名 beg ************/
/* 此处定义会再所有涉及到sdk操作函数中均使用 ****/
/*  用户句柄,通过登录操作获取 */
using HUSER = System.IntPtr;
/*  流媒体句柄,通过流媒体连接操作获取     */
using HSTREAM = System.IntPtr;
/*  历史通道句柄,通过历史通道创建获取     */
using HHISTORY = System.IntPtr;
/*  历史流查询句柄,通过历史流查询创建获取    */
using HSTREAM_QUERY = System.IntPtr;
/*  文件查询句柄,通过文件查询创建获取     */
using HFILE_QUERY = System.IntPtr;
/*  历史日志查询句柄,通过历史日志查询创建获取   */
using HLOG_QUERY = System.IntPtr;
/*  透明通道句柄,通过透明通道创建获取     */
using HTRANSPARENT = System.IntPtr;
/*  文件传输句柄,通过文件传输创建获取     */
using HFILE_TRANSFER = System.IntPtr;
/*  窗口句柄,用于显示解码图像 */
using HWINDOW = System.IntPtr;
using System.Runtime.InteropServices;
/************* 定义常用的别名 end  **************/

namespace HieCiULib//应该是专门链接明景的DLL
{
    public class HieCIU
    {
        
        /*!
        * @brief
        *	设置解码模式(软解码|矩阵解码)
        * @param nDecodeMode
        *	解码模式
        * @return
        *	返回0表示成功, 否则表示失败，将返回错误码
        * @note
        *	此接口是为了解决多进程时多次初始化系统SDK的问题，当一个进程已经设置了矩阵解码时，另一个进程就不应再设置矩阵解码，否则将导致
        *	矩阵卡初始化影响到第一个进程。此接口需在接口HieCIU_Initialize和HieCIU_StartUp之前调用，当不调用此接口时默认设置解码模式
        *	为eDecodeModeAll。
        * @note
        *	接口类型: 阻塞式
        */
        //EntryPoint：指示要调用的 DLL 入口点的名称或序号。，那明景的入口点的名称或序号怎么设定的
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetDecodeMode", CallingConvention = CallingConvention.StdCall)]
        public static extern void HieCIU_SetDecodeMode(HieCIUCommon.eDecodeModeSetting nDecodeMode);


        /*!
        * @brief
        *	设置在启动服务之前必须设置的参数,如果不调用将以默认的方式运行
        *   具体可以设置哪些参数，具体见T_SYSTEM_INFO的定义
        * @return
        *	返回0表示成功, 否则表示失败，将返回错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_Initialize", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_Initialize(ref HieCIUCommon.SYSTEM_INFO pSystemInfo);

        /*!
        * @brief
        *	启动服务(申请必要的内存，线程等资源)
        * @return
        *	返回0表示成功, 否则表示失败，将返回错误码
        * @note
        *	启动服务后,不再需要服务时可调用停止服务接口HieCIU_CleanUp
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StartUp", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StartUp();

        /*!
        * @brief
        *	停止服务(释放服务所使用的内存，线程等资源)
        * @return
        *	返回0表示成功, 否则表示失败，将返回错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_CleanUp", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_CleanUp();


        /*!
        * @brief
        *	用户登录,对设备的所有操作均需要先登录
        * @param hUser
        *	用户句柄地址,登录后接口填充该句柄值
        * @param cUserLoginPara
        *	用户登录参数信息,具体cUserLoginPara的类型可参见其结构体UserLoginPara定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_UserLogin", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_UserLogin(ref HUSER hUser, ref Common.UserLoginPara cUserLoginPara);

        /*!
        * @brief
        *	用户注销,成功注销后此句柄值将会无效
        * @param hUser
        *	用户句柄值,通过用户登录获取的句柄
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_UserLog", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_Userout( HUSER hUser);

        /*!
        * @brief
        *	订阅服务器事件
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwEventTypes
        *	远程事件类型,通过或运算一次可以订阅多种事件，共用相同回调和用户数据
        * @param dwUserData
        *	事件回调时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SubscribeEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SubscribeEvent( HUSER hUser,  uint dwEventTypes,  Common.CB_UserEvent cbEvent,  uint dwUserData);

        /*!
        * @brief
        *	退订服务器事件
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwEventTypes
        *	远程事件类型,通过或运算一次可以退订多种事件
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_UnSubscribeEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_UnSubscribeEvent( HUSER hUser,  uint dwEventTypes);

        /*!
        * @brief
        *	设置配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置信息,具体cConfig的类型可参见其结构体ConfigInformation定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetConfig", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetConfig( HUSER hUser,  IntPtr cConfig);

        /*!
        * @brief
        *	获取配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置信息,具体cConfig的类型可参见其结构体ConfigInformation定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetConfig", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetConfig( HUSER hUser, IntPtr cConfig);

        /*!
        * @brief
        *	设置配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置信息,具体cConfig的类型可参见其结构体ConfigInformationV2定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetConfigV2", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetConfigV2( HUSER hUser, Common.ConfigInformationV2 cConfig);

        /*!
        * @brief
        *	获取配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置信息,具体cConfig的类型可参见其结构体ConfigInformationV2定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetConfigV2", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetConfigV2( HUSER hUser,  Common.ConfigInformationV2 cConfig);

        /*!
        * @brief
        *	连接实时流
        * @param hStream
        *	流媒体句柄地址,连接成功后接口将填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	实时流连接信息,cStreamPara具体信息请参见RealStreamPara定义
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_RealStreamConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_RealStreamConnect(ref HSTREAM hStream,  HUSER hUser,  ref Common.RealStreamPara cStreamPara);

        /*!
        * @brief
        *	断开实时流,并会关闭其对对应的播放通道
        * @param hStream
        *	流媒体句柄,通过连接实时流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_RealStreamDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_RealStreamDisconnect( HSTREAM hStream);

        /*!
        * @brief
        *	创建历史流通道（支持多种录像类型的组合）
        * @param hHistory
        *	历史流通道句柄,创建后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	历史通道参数信息,具体cStreamPara的类型可参见其结构体HistoryStreamPara定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamMultiTypeCreate", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamMultiTypeCreate(ref HSTREAM hHistoryStream,  
                                                                     HUSER hUser, 
                                                                     ref Common.HistoryStreamMultiTypePara cStreamPara);

        /*!
        * @brief
        *	创建历史流通道（仅支持单一录像类型和所有录像类型）
        * @param hHistory
        *	历史流通道句柄,创建后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	历史通道参数信息,具体cStreamPara的类型可参见其结构体HistoryStreamPara定义说明
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamCreate", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamCreate(ref HSTREAM hHistoryStream,  HUSER hUser, ref Common.HistoryStreamPara cStreamPara);

        /*!
        * @brief
        *	销毁历史流通道,并会关闭其对对应的播放通道
        * @param hHistory
        *	历史通道句柄,通过创建历史通道获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamDestroy", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamDestroy( HSTREAM hHistoryStream);

        /*!
        * @brief
        *	获取或设置历史流通道时间位置
        *   并不能使用此接口来显示播放位置，此位置只表示数据已经到达了本地，并不表示正在播放的位置
        *   若要获取位置可以使用HieCIU_StreamGetAbsoluteTime或者HieCIU_StreamGetPlayedTime获取。
        * @param hHistoryStream
        *	历史流通道句柄,通过创建历史通道获取
        * @param eOperation
        *	历史流通道操作类型,用eOperationSet设置,用eOperationGet获取。
        *   历史流下载的进度也由此接口获取。
        * @param cTime
        *	历史通道时间信息,获取时此参数为出参,设置时此参数为入参
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamPosition", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamPosition( HSTREAM hHistoryStream, 
											         Common.eOperationType eOperation, 
											         ref Common.TimeInfo cTime);
        //#if 0  此处为测试用
        ///*!
        //* @brief
        //*	历史流通道控制
        //* @param hHistory
        //*	历史流通道句柄,通过创建历史流通道获取
        //* @param eControl
        //*	历史流通道控制命令,主要是开始eTaskStart和停止eTaskStop。
        //* @return
        //*	返回0表示成功, 否则表示错误码
        //* @note
        //*	接口类型: 阻塞式
        //*/
        //public static extern int HieCIU_HistoryStreamControl( HSTREAM hHistoryStream,  eTaskControl eControl);
        //#endif
        /*!
        * @brief
        *	为流打开一个解码器，在HieCIU_StreamPlay之前调用
        * @param hStream
        *	实时流句柄或历史流句柄,通过连接实时流HieCIU_RealStreamConnect或创建历史流HieCIU_HistoryStreamCreate获取
        * @param inDecodeMode  
        *   解码模式,软解，硬盘或矩阵解码
        * @param inShowMode
        *   显示图像模式,OVERLAY模式或普通模式
        * @param inColorKey
        *   若是OVERLAY模式显示图像，则需要指定一种关键色，在打开之前需要把显示窗口的背景色设置为此颜色
        * @param inDrawMode  
        *	叠加字符和图片模式,非软解码置NULL(硬解和矩阵解码暂不支持叠加操作)
        * @param pcbDrawFun 
        *	叠加字符和图片回调函数指针,非软解码置NULL(硬解和矩阵解码暂不支持叠加操作)
        * @param dwUserData
        *   叠加字符和图片回调函数回调时的用户数据,非软解码置NULL(硬解和矩阵解码暂不支持叠加操作)
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamOpen", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamOpen( HSTREAM hStream,
										         Common.eDecodeMode   inDecodeMode,
										         Common.eDecodeShowMode inShowMode,
										         uint inColorKey,
										         HieCIUCommon.eDrawFuncMode inDrawMode,
										         HieCIUCommon.CB_DrawFun pcbDrawFun, 
										         uint dwUserData);

        /*!
        * @brief
        *	开始流解码，回放
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inWnd
        *   图像显示窗口
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamPlay", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamPlay( HSTREAM hStream,
										         HWINDOW inWnd);

        /*!
        * @brief
        *	暂停流的播放
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inbPause
        *   非零为暂停，否则为恢复播放
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamPause", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamPause( HSTREAM hHistoryStream, 
										             [MarshalAsAttribute(UnmanagedType.Bool)] bool inbPause);

        /*!
        * @brief
        *	停止流解码，回放
        * @param hStream
        *	流句柄,通过连接实时流或创建历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamStop", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamStop( HSTREAM hStream);
        //#if 0  此处为测试用
        ///*!
        //* @brief
        //*	开始解码
        //* @param hStream
        //*	流句柄,通过连接实时流或历史流获取
        //* @return
        //*	返回0表示成功, 否则表示错误码
        //* @note
        //*	接口类型: 阻塞式
        //*/
        //public static extern int HieCIU_StreamStartDecode( HSTREAM hStream);

        ///*!
        //* @brief
        //*	停止流解码(自然无法再进行显示图像,也无法将解码数据回调给用户)
        //* @param hStream
        //*	流句柄,通过连接实时流或历史流获取
        //* @return
        //*	返回0表示成功, 否则表示错误码
        //* @note
        //*	接口类型: 阻塞式
        //*/
        //public static extern int HieCIU_StreamStopDecode( HSTREAM hStream);
        
        /*!
        * @brief
        *	将接收到的流直接保存成文件
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_StreamCaptureFile( HSTREAM hStream,  string inFileName);
        //#endif
        /*!
        * @brief
        *	播放声音
        *   注意此函数必须在界面线程里调用，否则将听不到声音
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamPlaySound", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamPlaySound( HSTREAM hStream);

        /*!
        * @brief
        *	设置声音音量
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inVolume
        *   音量，范围(0x0~0xFFFF)
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamPlaySound", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamSetVolume( HSTREAM hStream,  ushort inVolumn);

        /*!
        * @brief
        *	停止播放声音
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamStopSound", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamStopSound( HSTREAM hStream);

        /*!
        * @brief
        *	获取已经播放的时间(此时间来自于播放器计算的时间)
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamGetPlayedTime", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamGetPlayedTime( HSTREAM hStream,  ref uint PlayedTime);

        /*!
        * @brief
        *	获取正在播放的时间
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param PlayTime
        *   当成功返回返回时则表示获取正在播放的时间正常，此时间是自1970以来的秒数(精度：秒)
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamGetAbsoluteTime", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamGetAbsoluteTime( HSTREAM hStream,  ref uint PlayTime);

        /*!
        * @brief
        *	获取已经播放的帧数(此时间来自于播放器计算的帧数)
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param PlayedFrameNum
        *   用于保存已经播放的帧数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamGetPlayedFrameNum", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamGetPlayedFrameNum( HSTREAM hStream,  ref uint PlayedFrameNum);

        /*!
        * @brief
        *	丢弃Ｂ帧(暂未实现)
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inFrameNum
        *   要丢的Ｂ帧数目
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamThrowBFrame", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamThrowBFrame( HSTREAM hStream,  uint inFrameNum);

        /*!
        * @brief
        *	抓图
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @inFileName
        *   抓图保存文件全名
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamSnapShot", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamSnapShot( HSTREAM hStream,  string inFileName);

        /*!
        * @brief
        *	设置亮度
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inBright
        *   亮度值,范围0~255
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamChangeBright", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamChangeBright( HSTREAM hStream,  uint inBright);


        /*!
        * @brief
        *	设置图像显示质量(由于目前显卡性能一般均较强劲，一般可不调用)
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inQuality
        *   高或低(eHighQuality或eLowQuality)
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamSetPicQuality", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamSetPicQuality( HSTREAM hStream,  HieCIUCommon.eShowQualityMode inQuality );

        /*!
        * @brief
        *	设置对比度
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inContrast
        *   对比度值,范围0~255
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamChangeContrast", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamChangeContrast( HSTREAM hStream,  uint inContrast);

        /*!
        * @brief
        *   只支持矩阵解码卡方式。
        *	设置解码通道的视频外部输出（矩阵输出）。把视频图像从hStream所使用的解
        *   码通道的第inPort路，送到第inDisplayChannel个显示通道的第inDisplayRegion个区
        *   域。此函数中inPort 目前为0（一路解码通道支持一路外部视频矩阵输出）。如果inOpen 
        *   为0，则inDisplayChannel、inDisplayRegion 无意义。
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @param inPort
        *   解码通道的输出端口
        * @param inOpen
        *   TRUE 开；FALSE 关；
        * @param inDisplayChannel
        *   要输出的显示通道索引
        * @param inDisplayRegion
        *   要输出的显示区域
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamSetDecVideo", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamSetDecVideo( HSTREAM hStream, 
												             int inPort,
												             [MarshalAsAttribute(UnmanagedType.Bool)] bool inOpen,
												             int inDisplayChannel,
												             int inDisplayRegion);

        /*!
        * @brief
        *	快放,每调用一次播放速度快一倍，至16倍止
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamFast", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamFast( HSTREAM hStream);

        /*!
        * @brief
        *	慢放,每调用一次播放速度慢一倍，至1/16倍止
        * @param hStream
        *	流句柄,通过连接实时流或历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamSlow", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamSlow( HSTREAM hStream);

        /*!
        * @brief
        *	单帧进,要恢复正常播放可调用HieCIU_StreamPlay函数
        * @param hStream
        *	流句柄,通过连接历史流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamOneByOne", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamOneByOne( HSTREAM inHistoryStream);

        /*!
        * @brief
        *	单帧退(暂未实现)
        * @param inHistoryStream
        *	流句柄,通过连接历史流获取.
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamOneByOneBack", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamOneByOneBack( HSTREAM inHistoryStream);


        /*!
        * @brief
        *	多路历史流同步回放
        * @param inHistoryStream
        *	流句柄,通过连接历史流获取.
        * @param inDestStream
        *	流句柄,通过连接历史流获取.表示此路流数据与inHistoryStream同步回放，
        *   基准为inHistoryStream,当操作基准流时会在此路流上作相同的操作
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamSync", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamSync( HSTREAM inHistoryStream, 
													         HSTREAM inDestStream);

        /*!
        * @brief
        *	剔除已经多路流同步回放中的一个流通道，再在基准流上所做的操作将不影响被剔除同步的流
        * @param inHistoryStream
        *	流句柄,通过连接历史流获取.
        * @param inDestStream
        *	流句柄,通过连接历史流获取.
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamUnsync", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamUnsync( HSTREAM inHistoryStream, 
													         HSTREAM inDestStreamID);

        /*!
        * @brief
        *	获取某个历史流上所关联的同步回放流通道列表
        * @param inHistoryStream
        *	流句柄,通过连接历史流获取.此流句柄是同步的基准
        * @param StreamList
        *	用户给定历史流上所关联的多路同步回放流句柄缓冲区,一般可给定一个最大句构数据的缓冲区
        * @param StreamNum
        *   关联的历史流句柄数目
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamGetSyncList", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamGetSyncList( HSTREAM inHistoryStream, 
													             ref HSTREAM[] StreamList,
													             ref uint StreamNum );



        /*!
        * @brief
        *	设置矩阵显示区域
        * @param inDisplayChannel
        *	矩阵卡显示通道号
        * @param inRegionCount
        *   区域个数
        * @param pParam
        *   区域参数数组首地址,具体pParam定请参见REGION_PARAM
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_MatrixSetDisplayRegion", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_MatrixSetDisplayRegion( int inDisplayChannel,			
													         int inRegionCount,
													         ref tmsdk.REGION_PARAM pParam);

        /*!
        * @brief
        *	设置矩阵卡显示通道的制式
        * @param inDisplayChannel
        *	矩阵卡显示通道号
        * @param VideoStandard
        *	制式,目前有	StandardNTSC,StandardPAL,StandardSECAM三种
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_MatrixSetDisplaySignalStandard", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_MatrixSetDisplaySignalStandard( int inDisplayChannel,		// 矩阵卡显示通道号
															         tmsdk.VideoStandard_t VideoStandard);

        /*!
        * @brief
        *	设置矩阵显示位置
        * @param inDisplayChannel
        *	矩阵卡显示通道号
        * @param inRegionCount
        *   要调整位置的区域号
        * @param inLeft
        *   调整后的位置(X坐标)
        * @param inTop
        *   调整后的位置(Y坐标)
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_MatrixSetDisplayPosition", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_MatrixSetDisplayPosition( int inDisplayChannel,
													             int inRegionCount,
													             int inLeft,
													             int inTop);

        /*!
        * @brief
        *	清除矩阵显示通道的某区域
        * @param inDisplayChannel
        *	矩阵卡显示通道号
        * @param inRegionCount
        *   要调整位置的区域号
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_MatrixClearRegion", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_MatrixClearRegion( int inDisplayChannel,
												             int inRegionCount);


        /*!
        * @brief
        *	填充某矩阵显示通道的某区域
        * @param inDisplayChannel
        *	矩阵卡显示通道号
        * @param inRegionCount
        *   要填充的区域号
        * @param inFileName
        *   待填充的图像文件名。inFileName所指向图象必须和HieCIU_MatrixSetDisplayRegion中
        *   设置的图像大小相同，否则图像无法正常显示。
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_MatrixFillRegion", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_MatrixFillRegion( int inDisplayChannel,
												             int inRegionCount,
												             string inFileName);



        /*!
        * @brief
        *	连接语音流
        * @param hStream
        *	语音流句柄地址,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	语音流连接信息，cStreamPara具体信息请参见VoiceStreamPara定义
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_VoiceStreamConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_VoiceStreamConnect(ref HSTREAM hStream, 
										             HUSER hUser, 
										             ref Common.VoiceStreamPara cStreamPara);

        /*!
        * @brief
        *	断开语音流
        * @param hStream
        *	语音流句柄,通过连接语音流HieCIU_VoiceStreamConnect获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_VoiceStreamDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_VoiceStreamDisconnect( HSTREAM hStream);

        /*!
        * @brief
        *	设置语音流的音量(暂未实现)
        * @param hStream
        *	语音流句柄,通过连接语音流获取
        * @param inVolume
        *	音量
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetVoiceStreamVolume", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetVoiceStreamVolume( HSTREAM hStream,  ushort inVolume);

        /*!
        * @brief
        *	设置语音流回调接口
        * @param hStream
        *	语音流句柄,通过连接语音流获取
        * @param cbStreamVoice
        *	语音流回调接口,若值为空(NULL),表示不再需要回调
        * @param dwUserData
        *	语音流回调时回传给调用者
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	由于此回调函数在语音流数据接收与捕获线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_VoiceStreamCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_VoiceStreamCB( HSTREAM hStream, 
									         HieCIUCommon.CB_Nettalk cbStreamVoice, 
									         uint dwUserData);


        /*!
        * @brief
        *	设置流媒体回调接口
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param cbStreamMedia
        *	流媒体回调接口,若值为空(NULL),表示不再需要回调,cbStreamMedia具体信息请参见CB_StreamMedia定义
        * @param dwUserData
        *	流媒体回调时回传给调用者
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	应用层可使用此接口设置流媒体数据回调,
        *	由于此回调函数在流媒体数据接收线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamMediaCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamMediaCB( HSTREAM hStream, 
									         Common.CB_StreamMedia cbStreamMedia, 
									         uint dwUserData);

        /*!
        * @brief
        *	设置原始流媒体回调接口
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param cbStreamMedia
        *	流媒体回调接口,若值为空(NULL),表示不再需要回调,cbStreamMedia具体信息请参见CB_StreamMediaRaw定义
        * @param dwUserData
        *	流媒体回调时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	应用层可使用此接口设置流媒体数据回调,
        *	由于此回调函数在流媒体数据接收线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamMediaRawCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamMediaRawCB( HSTREAM hStream,  Common.CB_StreamMediaRaw cbStreamMedia,  uint dwUserData);

        /*!
        * @brief
        *	流媒体控制接口,控制流媒体的启动与停止
        * @param hStream
        *	流媒体句柄,通过连接流获取,实时流句柄，历史流句柄，语音流句柄均可使用。
        * @param eMediaControl
        *	流媒体控制命令,开始eTaskStart和停止eTaskStop
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StreamMediaControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StreamMediaControl( HSTREAM hStream, 
										             Common.eTaskControl eMediaControl);



        /*!
        * @brief
        *	历史流查询连接操作（仅支持单一录像类型和所有录像类型）
        *   主要用于建立历史录像数据时间轴,如可以清晰的描述某通道某时间段的录像类型信息
        * @param hStreamQuery
        *	历史流查询句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamQueryFactor
        *	历史流查询条件
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamQueryConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamQueryConnect(ref HSTREAM_QUERY hStreamQuery, 
												         HUSER hUser, 
												         ref Common.HistoryStreamQueryFactor cStreamQueryFactor);

        /*!
        * @brief
        *	历史流查询连接操作（支持多种录像类型的组合）
        *   主要用于建立历史录像数据时间轴,如可以清晰的描述某通道某时间段的录像类型信息
        * @param hStreamQuery
        *	历史流查询句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamQueryFactor
        *	历史流查询条件
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamMultiTypeQueryConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamMultiTypeQueryConnect(ref HSTREAM_QUERY hStreamQuery, 
														             HUSER hUser, 
														             ref Common.HistoryStreamMultiTypeQueryFactor cStreamQueryFactor);

        /*!
        * @brief
        *	历史流查询断开操作
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamQueryDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamQueryDisconnect( HSTREAM_QUERY hStreamQuery);

        /*!
        * @brief
        *	历史流查询下一条（仅支持单一录像类型和所有录像类型）
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @param pStreamQueryResult
        *	历史流查询的一条记录
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamQueryNext", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamQueryNext( HSTREAM_QUERY hStreamQuery, 
											             ref Common.HistoryStreamQueryResult pStreamQueryResult);

        /*!
        * @brief
        *	历史流查询下一条（支持多种录像类型的组合）
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @param pStreamQueryResult
        *	历史流查询的一条记录
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamMultiTypeQueryNext", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamMultiTypeQueryNext( HSTREAM_QUERY hStreamQuery, 
													             ref Common.HistoryStreamMultiTypeQueryResult pStreamQueryResult);

        /*!
        * @brief
        *	历史日志查询连接操作
        * @param hLogQuery
        *	历史日志查询句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cLogQueryFactor
        *	历史日志查询条件
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryLogQueryConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryLogQueryConnect(ref HLOG_QUERY hLogQuery, 
											             HUSER hUser, 
											             ref Common.LogQueryFactor cLogQueryFactor);

        /*!
        * @brief
        *	历史日志查询断开操作
        * @param hLogQuery
        *	历史日志查询句柄,通过连接历史日志查询获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryLogQueryConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryLogQueryDisconnect( HLOG_QUERY hLogQuery);

        /*!
        * @brief
        *	历史日志查询下一条
        * @param hLogQuery
        *	历史日志查询句柄,通过连接历史日志查询获取
        * @param pLogQueryResult
        *	历史日志查询的一条记录
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryLogQueryNext", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryLogQueryNext( HLOG_QUERY hLogQuery, 
										             ref Common.LogQueryResult pLogQueryResult);

        /*!
        * @brief
        *	透明通道连接操作
        * @param hTransparent
        *	透明通道句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cTransparentPara
        *	透明通道连接参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_TransparentChannelConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_TransparentChannelConnect(ref HTRANSPARENT hTransparent, 
												         HUSER hUser, 
												         ref Common.TransparentChannelPara cTransparentPara);

        /*!
        * @brief
        *	透明通道断开操作
        * @param hTransparent
        *	透明通道句柄,通过连接透明通道获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_TransparentChannelDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_TransparentChannelDisconnect( HTRANSPARENT hTransparent);

        /*!
        * @brief
        *	透明通道数据发送
        * @param hTransparent
        *	透明通道句柄,通过连接透明通道获取
        * @param cBuffer
        *	透明通道数据缓冲区
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_TransparentChannelWrite", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_TransparentChannelWrite( HTRANSPARENT hTransparent, 
											             ref Common.Buffer cBuffer);

        /*!
        * @brief
        *	设置透明通道数据回调接口
        * @param hTransparent
        *	透明通道句柄,通过连接透明通道获取
        * @param cbTransparent
        *	透明通道数据回调接口,若值为空(NULL),表示不再需要回调
        * @param dwUserData
        *	透明通道数据回调时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	由于此回调函数在透明通道数据接收线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        * @note
        *	接口类型: 阻塞式
        */

        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_TransparentChannelCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_TransparentChannelCB( HTRANSPARENT hTransparent, 
										             Common.CB_TransparentChannel cbTransparent, 
										             uint dwUserData);



        //#if 0  此处为测试用
        //// 已经使用单独的接口进行调用，此接口寿终正寝
        ///*!
        //    *  \fn HIECIU_API int HieCIU_ControlDevice( unsigned int inUserID,
        //                                                         eControlDeviceFlag inCDFlag, 
        //                                                        void* pParam);
        //    *  \brief  控制设备
        //    *  \param  inUserID  用户ID
        //    *  \param  inCDFlag  控制设备类型标识,详见eControlDeviceFlag定义
        //    *  \param  pParam    控制设备所对应的参数指针,pParm将根据inCDFlag的不同有可能为输入或输出或输入输出参数
        //    *  \return HIEC_ERR_OK , 成功
        //    *  \return 其它值详见错误码定义
        //    */
        //public static extern int HieCIU_ControlDevice( unsigned int inUserID,
        //                                             eControlDeviceFlag inCDFlag, 
        //                                             void* pParam);

        //#endif



        /*!
        * \brief
        *  远程控制PTZ
        * \param hUser
        *  用户登录句柄
        * \param dwChannel
        *   通道编号(从0开始)
        * \param eCommandCode
        *   PTZ命令字
        * \param dwParam1
        *   PTZ命令参数1
        * \param dwParam2
        *   PTZ命令参数2
        * \param dwParam3
        *   PTZ命令参数3
        * \param dwParam4
        *   PTZ命令参数4
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_PtzControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_PtzControl( uint UserID, 
										         uint inChannel, 
										         Common.ePTZControlCmdCode inCommand, 
										         int inParam1, 
										         int inParam2, 
										         int inParam3, 
										         int inParam4);

        /*!
            *  \fn public static extern int HieCIU_VerifyVersion( char* inDeviceIP);
            *  \brief  验证版本(暂未实现)
            *  \param  inDeviceIP  设备IP地址
            *  \return HIEC_ERR_OK , 成功
            *  \return 其它值详见错误码定义
            */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_VerifyVersion", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_VerifyVersion(IntPtr inDeviceIP);




        /*!
        * @brief
        *	文件查询连接操作
        * @param hFileQuery
        *	文件查询句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param pFileQueryFactor
        *	文件查询条件
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileQueryConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileQueryConnect(ref HFILE_QUERY hFileQuery,  HUSER hUser, ref Common.FileQueryFactor pFileQueryFactor);

        /*!
        * @brief
        *	文件查询断开操作
        * @param hFileQuery
        *	文件查询句柄,通过连接文件查询获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileQueryDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileQueryDisconnect( HFILE_QUERY hFileQuery);

        /*!
        * @brief
        *	文件查询下一条
        * @param hFileQuery
        *	文件查询句柄 ,通过连接文件查询获取
        * @param pFileQueryResult
        *	文件查询的一条记录
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileQueryNext", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileQueryNext( HFILE_QUERY hFileQuery,ref  Common.FileQueryResult pFileQueryResult);


        /*!
        * @brief
        *	文件上传连接操作
        * @param hFileTransfer
        *	文件上传句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param pFileUploadPara
        *	文件上传参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileUploadConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileUploadConnect(ref HFILE_TRANSFER hFileTransfer,  HUSER hUser, ref Common.FileUploadPara pFileUploadPara);

        /*!
        * @brief
        *	文件上传控制
        * @param hFileTransfer
        *	文件上传句柄,连接后接口填充该句柄值
        * @param eControlCode
        *	启动或停止文件传输
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileUploadControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileUploadControl( HFILE_TRANSFER hFileTransfer,  Common.eFileTransferControl eControlCode);

        /*!
        * @brief
        *	文件上传断开操作
        * @param hFileTransfer
        *	文件上传句柄,通过连接文件上传获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileUploadDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileUploadDisconnect( HFILE_TRANSFER hFileTransfer);

        /*!
        * @brief
        *	设置文件上传回调接口
        * @param hFileTransfer
        *	文件上传句柄,通过连接文件上传获取
        * @param cbFileUpload
        *	文件上传状态回调
        * @param dwUserData
        *	文件上传回调时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileUploadCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileUploadCB( HFILE_TRANSFER hFileTransfer,  Common.CB_FileUpload cbFileUpload,  uint dwUserData);

        /*!
        * @brief
        *	文件下载连接操作
        * @param hFileTransfer
        *	文件下载句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param pFileDownloadPara
        *	文件下载参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileDownloadConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileDownloadConnect(ref HFILE_TRANSFER hFileTransfer,  HUSER hUser,  ref Common.FileDownloadPara pFileDownloadPara);

        /*!
        * @brief
        *	文件下载控制
        * @param hFileTransfer
        *	文件下载句柄,连接后接口填充该句柄值
        * @param eControlCode
        *	启动或停止文件传输
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileDownloadControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileDownloadControl( HFILE_TRANSFER hFileTransfer,  Common.eFileTransferControl eControlCode);

        /*!
        * @brief
        *	文件下载断开操作
        * @param hFileTransfer
        *	文件下载句柄,通过连接文件下载获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileDownloadDisconnect", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileDownloadDisconnect( HFILE_TRANSFER hFileTransfer);

        /*!
        * @brief
        *	设置文件下载回调接口
        * @param hFileTransfer
        *	文件下载句柄,通过连接文件下载获取
        * @param cbFileDownload
        *	文件下载数据回调
        * @param dwUserData
        *	文件下载回调时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileDownloadCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileDownloadCB( HFILE_TRANSFER hFileTransfer,  Common.CB_FileDownload cbFileDownload,  uint dwUserData);


        /*!
        * @brief
        *	远程设备控制
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param eOperationCode
        *	设备操作码
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceControl( HUSER hUser, Common.eRemoteDeviceControl eOperationCode);

        /*!
        * @brief
        *	远程设备重启动
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_ForceIFrame", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_ForceIFrame( HUSER hUser, uint dwChannel);

        /*!
        * @brief
        *	远程设备重启动
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param dwAlarmType
        *	报警类型
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_AlarmClear", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_AlarmClear( HUSER hUser, uint dwChannel,  uint dwAlarmType);

        /*!
        * @brief
        *	远程抓图
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param eFormat
        *	图片格式
        * @param pImageFileInfo
        *	图片信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_ImageCapture", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_ImageCapture( HUSER hUser,  uint dwChannel,  Common.eImageFormat eFormat,  ref Common.ImageFileInfo pImageFileInfo);

        /*!
        * @brief
        *	远程锁定图片
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param szFileName
        *	文件名
        * @param eOperationCode
        *	操作码
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_FileLock", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_FileLock( HUSER hUser,  string szFileName,  Common.eFileLockOperationCode eOperationCode);

        /*!
        * @brief
        *	远程录像控制
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param dwRecordType
        *	录像类型
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_RecordControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_RecordControl( HUSER hUser,  uint dwChannel,  uint dwRecordType);

        /*!
        * @brief
        *	远程录像控制
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param dwSwitch
        *	开关状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_AlarmControl", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_AlarmControl( HUSER hUser,  uint dwChannel,  uint dwSwitch);

        /*!
        * @brief
        *	获取远程报警输出状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChanNum
        *	有效报警输出通道个数
        * @param dwStateBits
        *	位0-31分别表示0－31通道的报警输出状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_AlarmStateGet", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_AlarmStateGet( HUSER hUser,  ref uint dwChanNum,  ref uint dwStateBits);

        /*!
        * @brief
        *	获取远程报警输入状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChanNum
        *	有效报警输入通道个数
        * @param dwStateBits
        *	位0-31分别表示0－31通道的报警输入状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_AlarminStateGet", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_AlarminStateGet( HUSER hUser,  ref uint dwChanNum,  ref uint dwStateBits);

        /*!
        * \brief
        *  判断某天是否存在指定数据
        * \param hUser
        *  用户登录句柄
        * \param dwChannel
        *   通道编号(从0开始)
        * \param dwMajorType
        *   主类型，　0-录像文件   1-图片文件
        * \param dwMinorType
        *   次类型
        * \param szYearMonth
        *   年月时间串
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DataExistCheck", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DataExistCheck( HUSER hUser,  uint dwChannel,  uint dwMajorType,  uint dwMinorType,  string szYearMonth,  ref uint dwResult);

        /*!
        * \brief
        *	启动监听程序，监听设备的注册请求
        * \param sListenIP
        *  PC 机的IP 地址，如果为NULL，SDK 将自动获取PC 机的IP 地址，如果
        *   PC 机有多个IP 地址，可以指定一个IP 地址进行监听。
        * \param dwListenPort
        *   本地监听端口号，由用户设置;
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceRegisterListenStart", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceRegisterListenStart( string sListenIP,  uint dwListenPort);

        /*!
        * \brief
        *	停止监听程序
        * \return
        *   TRUE表示成功， FALSE表示失败
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceRegisterListenStop", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceRegisterListenStop();


        /*!
        * \brief
        *    设置PU设备的注册回调函数
        * \param cbf
        *    回调函数指针
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceRegisterCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceRegisterCB( Common.CB_DeviceRegister cbDeviceRegister);


        /*!
        * @brief
        *	历史流下载（仅支持单一录像类型和所有录像类型）
        * @param hHistoryStream
        *   待创建的历史流句柄地址，若成功则此句柄值将会被填充
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *   要下载的历史流信息，如时间段，通道等
        * @param pFnSpecifyFileName
        *   在下载过程中会让用户有机会参与文件名的创建工作，以便用户管理这些下载的文件
        * @param dwMaxFileSize
        *	下载的文件的最大长度
        *   需要了解的是下载的文件大小并不一定小于或等于指定的大小，当文件大小到达指定大小时
        *   会一直等到有一个关键帧到来才会被关闭，意味着I帧之前的其它帧也会被写到此文件中。
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamDownload", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamDownload(ref HSTREAM hHistoryStream, 
											         HUSER hUser, 
											         ref Common.HistoryStreamPara cStreamPara, 
											         HieCIUCommon.CB_SpecifyFileName pFnSpecifyFileName,
											         uint  dwMaxFileSize);

        /*!
        * @brief
        *	历史流下载（支持多种录像类型的组合）
        * @param hHistoryStream
        *   待创建的历史流句柄地址，若成功则此句柄值将会被填充
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *   要下载的历史流信息，如时间段，通道等
        * @param pFnSpecifyFileName
        *   在下载过程中会让用户有机会参与文件名的创建工作，以便用户管理这些下载的文件
        * @param dwMaxFileSize
        *	下载的文件的最大长度
        *   需要了解的是下载的文件大小并不一定小于或等于指定的大小，当文件大小到达指定大小时
        *   会一直等到有一个关键帧到来才会被关闭，意味着I帧之前的其它帧也会被写到此文件中。
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamMultiTypeDownload", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamMultiTypeDownload(ref HSTREAM hHistoryStream, 
											         HUSER hUser, 
											         ref Common.HistoryStreamMultiTypePara cStreamPara, 
											         HieCIUCommon.CB_SpecifyFileName pFnSpecifyFileName,
											         uint  dwMaxFileSize);
        /*!
        * @brief
        *	停止历史流下载
        * @param hHistoryStream
        *   使用HieCIU_HistoryStreamDownload创建的历史流句柄
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_HistoryStreamDownloadDestory", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_HistoryStreamDownloadDestory( HSTREAM hHistoryStream);

        /*!
        * @brief
        *	远程盘组管理
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param eOperationCode
        *	操作码
        * @param dwParam1-3
        *	参数列表
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DiskGroupManage", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DiskGroupManage( HUSER hUser,  Common.eDiskGroupOperation eOperationCode,  uint dwParam1,  uint dwParam2,  uint dwParam3);

        /*!
        * @brief
        *	获取数据大小（仅支持单一录像类型和所有录像类型）
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannelBits
        *	0－31位分别表示0－31通道的选中状态，1表示选中
        * @param eStreamType
        *	录像类型
        * @param eDskGroupType
        *	磁盘分组类型　
        * @param szStartTime
        *	开始时间
        * @param szEndTime
        *	结束时间
        * @param dwUserData
        *	用户数据
        * @param dwDataSize
        *	数据大小
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetDataSize", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetDataSize( HUSER hUser,  uint dwChannelBits,  Common.eDiskGroupType eDskGroupType,  Common.eHistoryStreamType eStreamType,  string szStartTime,  string szEndTime,  uint dwUserData,  ref uint dwDataSize);


        /*!
        * @brief
        *	获取数据大小（支持多种录像类型的组合）
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannelBits
        *	0－31位分别表示0－31通道的选中状态，1表示选中
        * @param eStreamType
        *	录像类型
        * @param eDskGroupType
        *	磁盘分组类型　
        * @param szStartTime
        *	开始时间
        * @param szEndTime
        *	结束时间
        * @param dwUserData
        *	用户数据
        * @param dwDataSize
        *	数据大小
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetMultiTypeDataSize", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetMultiTypeDataSize( HUSER hUser,  uint dwChannelBits,  Common.eDiskGroupType eDskGroupType,  uint eStreamType,  string szStartTime, string szEndTime,  uint dwUserData,  ref uint dwDataSize);

        /*!
        * @brief
        *	设置强制删除已登录用户回调接口
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cbStreamMedia
        *	流媒体回调接口,若值为空(NULL),表示不再需要回调
        * @param dwUserData
        *	发送命令时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	应用层可使用此接口设置强制删除已登录用户回调,
        *	由此接口通知用户已被设备端强制删除,
        *	由于此回调函数在需要回复设备,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作.
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeleteUserForceCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeleteUserForceCB( Common.CB_DeleteUserForce cbDeleteUserForce,  uint dwUserData);

        /*!
        * \brief
        *	字符串转点阵
        * \param pString
        *	源字符串
        * \param cFont
        *   字体信息参数
        * \param buffer
        *   点阵数据缓冲区
        * \param dwBuflen
        *   点阵数据缓冲区长度,转换后将被填写为实际使用的长度
        * \param dwWidth
        *   实际点阵宽度
        * \param dwHeight
        *   实际点阵高度
        * \return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_StringToLattice", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_StringToLattice( IntPtr pString,  ref Common.FontPara cFont,  IntPtr buffer,  ref uint dwBuflen,  ref uint dwWidth,  ref uint dwHeight);

        /*!
        * @brief
        *	设置流媒体连接ID
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param dwLinkID
        *	流媒体连接ID
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        *	设置的媒体连接ID会保存到服务器上,在服务器重启之前一直存在,客户可以查询设备上的连接ID
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetStreamMediaLinkID", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetStreamMediaLinkID( HSTREAM hStream,  uint dwLinkID);

        /*!
        * @brief
        *	查询流媒体连接ID
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param eType
        *	流媒体类型
        * @param cQueryResult
        *	查询连接ID结果
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        *	查询设置在服务器上的媒体连接ID
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_QueryStreamMediaLinkID", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_QueryStreamMediaLinkID( HUSER hUser,  Common.eMediaFunctionType eType,  ref Common.MediaLinkIDQueryResult cQueryResult);

        /*!
        * @brief
        *	获取客户端SDK属性(用于控制客户端SDK接口行为)
        * @param eType
        *	客户端SDK属性类型
        * @param dwAttribute
        *	客户端SDK属性值
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetAttribute", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetAttribute( Common.eClientSDKAttributeType eType,  ref uint dwAttribute);

        /*!
        * @brief
        *	设置客户端SDK属性(用于控制客户端SDK接口行为)
        * @param eType
        *	客户端SDK属性类型
        * @param dwAttribute
        *	客户端SDK属性值
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetAttribute", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetAttribute( Common.eClientSDKAttributeType eType,  uint dwAttribute);


        /*
        * @brief
        *	分屏模式控制接口
        * @param dwUserID
        *	用户ID
        * @param dwScreenDev
        *	0代表主显, 1、2、3、4分别代表spot1、spot2、spot3、spot4
        * @param dwSplitNum
        *	分屏数，支持1-16分屏
        * @param pChanList
        *	通道列表（pChanList表示一个DWORD的数组，包含16个DWORD成员。例如：dwSplitNum为4时，总共有4个分屏，
        *	如果pChanList[0]为2，表示第一个分屏预览通道2的视频信号。）
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetScreenSplitMode", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetScreenSplitMode( HUSER hUser,  uint dwScreenDev,  uint dwSplitNum,  ref uint pChanList);



        /*!
        * \brief
        *	使用HTTP协议读取服务端的配置信息
        * \param pURL
        *	服务端URL路径,可用格式:
        *	纯域名:			http://xxx.xxx.com
        *	纯IP地址:		http://xxx.xxx.xxx.xxx
        *	域名与端口:		http://xxx.xxx.com:port
        *	IP地址与端口	http://xxx.xxx.xxx.xxx:port
        * \param cConfig
        *   来自HTTP的设备配置
        * \return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetDeviceConfigFromHttp", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetDeviceConfigFromHttp(string  pURL,  ref Common.DeviceConfigFromHttp cConfig);

        /*!
        * \brief
        *	启动探测设备,在指定的探测信息范围内探测设备
        * \param cParameter
        *	探测配置参数
        * \param cbDeviceProbe
        *	探测回调接口
        * \return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceProbeStart", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceProbeStart( ref Common.DeviceProbeParameter cParameter,  Common.CB_DeviceProbe cbDeviceProbe);

        /*!
        * \brief
        *	停止探测设备
        * \return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceProbeStop", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceProbeStop();

        /*!
        * \brief
        *	通过探测方式设置设备配置
        * \param pMac
        *	设备MAC地址, 同DeviceProbeConfig结构中MAC地址信息, 长度为 MAC_BINARY_ADDRESS_LEN
        * \param wProbePort
        *	设备探测端口
        * \param cConfig
        *	设备配置信息
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceProbeSetDeviceConfig", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceProbeSetDeviceConfig(  byte[] pMac,  ushort wProbePort, ref Common.DeviceProbeConfig cConfig);

        /*!
        * \brief
        *	通过探测方式设置指定探测端口范围内的所有设备配置
        * \param cParameter
        *	探测配置参数
        * \param cConfig
        *	设备配置信息
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceProbeSetAllDeviceConfig", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceProbeSetAllDeviceConfig( ref Common.DeviceProbeParameter cParameter, ref Common.DeviceProbeConfig cConfig);

        /*!
        * @brief
        *	远程恢复色度默认参数
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param dwTimeSection
        *	时间段标号（对于色度配置的高级模式，色度的设置分为4个时间段，1表示默认时间段(是指除了自定义时段以外的时间)，
        *	2、4、8分别表示3个自定义时间段。
        *	可以通过或操作对多个自定义时间段进行设置，例如(1|4)表示对默认时间段和第2个自定义时间段进行设置。）
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SetChromaDefault", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SetChromaDefault( HUSER hUser, uint dwChannel, uint dwTimeSection);

        /*
        * @brief
        *	获取面板状态
        * @param dwUserID
        *	用户ID
        * @param cStatus
        *	面板状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_PanelGetStatus", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_PanelGetStatus( HUSER hUser,  ref Common.PanelStatusInfo cStatus);

        /*
        * @brief
        *	设置面板状态
        * @param dwUserID
        *	用户ID
        * @param cStatus
        *	面板状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_PanelSetStatus", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_PanelSetStatus( HUSER hUser,  ref Common.PanelStatusInfo cStatus);

        /*
        * @brief
        *	面板控制
        * @param dwUserID
        *	用户ID
        * @param cControl
        *	面板控制参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_PanelSetStatus", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_PanelControl( HUSER hUser,  ref Common.PanelControlParameter cControl);


        /*
        * @brief
        *	设置或增加显示区域。可以做局部放大显示
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param nRegionNum
        *	显示区域序号，0~(MAX_DISPLAY_WND-1)。如果nRegionNum为0，表示对
        *	主要显示窗口(MP4Play_ Play中设置的窗口)进行设置，将忽略hDestWnd和bEnable 的设置
        * @param pSrcRect
        *	设置在要显示的原始图像上的区域，如：如果原始图像是352*288，那么
        *	pSrcRect 可设置的范围只能在（0，0，352，288）之中。如果pSrcRect=NULL,将显示整个图像
        * @param hDestWnd
        *	设置显示窗口。如果该区域的窗口已经设置过（打开过），那么该参数被
        *	忽略
        * @param bEnable
        *	打开（设置）或关闭显示区域
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_SoftSetDisplayRegion", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_SoftSetDisplayRegion( HSTREAM hStream,  uint nRegionNum,  ref Common.RECT pSrcRect,  IntPtr hDestWnd,  [MarshalAsAttribute(UnmanagedType.Bool)] bool bEnable);


        /*
        * @brief
        *	默认文件头获取
        * @param pBuffer
        *	存放默认文件头的缓冲区
        * @param pBufferLen
        *	默认文件头长度
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetFileHeader", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_GetFileHeader( IntPtr pBuffer, ref uint pBufferLen);

        /*!
        * @brief
        *	流转发连接
        * @param hStream
        *	流媒体句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cStreamPara
        *	连接参数信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        *	用于将从视频源设备获取的流转发到其它解码或存储设备.
        */
        //public static extern int HieCIU_StreamTransferConnect( HSTREAM *hStream,  HUSER hUser,  DWORD dwChannel,  const LPStreamTransferPara cStreamPara);

        /*!
        * @brief
        *	媒体流转发
        * @param hStream
        *	流媒体句柄,通过流转发连接获取
        * @param cFrame
        *	帧信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_StreamTransferSend( HSTREAM hStream,  const LPStreamTransferFrame cFrame);

        /*!
        * @brief
        *	流转发断开
        * @param hStream
        *	流媒体句柄,通过流转发连接获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_StreamTransferDisconnect( HSTREAM hStream);

        /*!
        * @brief
        *	动态主动流媒体连接
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cStreamPara
        *	连接参数信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        *	动态的媒体连接不会保存,设备重启后该流媒体不会自动连接
        */
        //public static extern int HieCIU_DecoderDynamicConnect( HUSER hUser,  DWORD dwChannel,  const LPDecoderDynamicInformation cStreamPara);

        /*!
        * @brief
        *	动态主动流媒体断开
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderDynamicDisconnect( HUSER hUser,  DWORD dwChannel);

        /*!
        * @brief
        *	设置解码配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderSetConfig( HUSER hUser,  const LPDecoderDeviceConfig cConfig);

        /*!
        * @brief
        *	获取解码配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetConfig( HUSER hUser,  LPDecoderDeviceConfig cConfig);

        /*!
        * @brief
        *	设置解码通道配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderSetChannelConfig( HUSER hUser,  DWORD dwChannel,  const LPDecoderChannelConfig cConfig);

        /*!
        * @brief
        *	获取解码通道配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetChannelConfig( HUSER hUser,  DWORD dwChannel,  LPDecoderChannelConfig cConfig);

        /*!
        * @brief
        *	设置解码通道视频制式
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderSetVideoStandard( HUSER hUser,  DWORD dwChannel,  const LPDecoderVideoStandardConfig cConfig);

        /*!
        * @brief
        *	获取解码通道视频制式
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetVideoStandard( HUSER hUser,  DWORD dwChannel,  LPDecoderVideoStandardConfig cConfig);

        /*!
        * @brief
        *	设置解码通道轮转配置
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderSetChannelLoopConfig( HUSER hUser,  DWORD dwChannel,  const LPDecoderChannelLoopConfig cConfig);

        /*!
        * @brief
        *	获取解码通道轮转配置
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cConfig
        *	配置
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetChannelLoopConfig( HUSER hUser,  DWORD dwChannel,  LPDecoderChannelLoopConfig cConfig);

        /*!
        * @brief
        *	设置解码通道轮询状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param eStatus
        *	状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelSetLoopEnable( HUSER hUser,  DWORD dwChannel,  eDecoderChannelStatus eStatus);

        /*!
        * @brief
        *	获取解码通道轮询状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param eStatus
        *	状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelGetLoopEnable( HUSER hUser,  DWORD dwChannel,  eDecoderChannelStatus *eStatus);

        /*!
        * @brief
        *	获取解码通道轮询状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStatus
        *	状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetLoopEnable( HUSER hUser,  LPDecoderLoopStatus cStatus);

        /*!
        * @brief
        *	获取解码通道信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cInformation
        *	状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetChannelInformation( HUSER hUser,  DWORD dwChannel,  LPDecoderChannelInformation cInformation);

        /*!
        * @brief
        *	获取解码器通道状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStatus
        *	状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelGetStatus( HUSER hUser,  LPDecoderChannelStatus cStatus);

        /*!
        * @brief
        *	设置解码器通道播放状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	解码通道号
        * @param cControl
        *	控制参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelSetPlayStatus( HUSER hUser,  DWORD dwChannel,  LPDecoderSetPlayStatusParameter cControl);

        /*!
        * @brief
        *	获取解码器通道播放状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cControl
        *	控制参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelGetPlayStatus( HUSER hUser,  LPDecoderGetPlayStatusParameter cControl);

        /*!
        * @brief
        *	上传LOGO图片
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param cParameter
        *	LOGO图片参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_UploadLogo( HUSER hUser,  DWORD dwChannel,  LPUploadLogoParameter cParameter);

        /*!
        * @brief
        *	切换解码通道LOGO图片
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @param eOperationCode
        *	操作码　
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelLogoSwitch( HUSER hUser,  DWORD dwChannel,  eDeviceChannelControl eOperationCode);

        /*!
        * @brief
        *	控制显示通道显示状态
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwDisplay
        *	显示通道号
        * @param cStatus
        *	状态
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderChannelDisplayControl( HUSER hUser,  DWORD dwDisplay,  LPDecoderDisplayStatus cStatus);

        /*!
        * @brief
        *	设置显示配置
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwDisplay
        *	显示设备号
        * @param cConfig
        *	参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderSetDisplayConfig( HUSER hUser,  DWORD dwDisplay,  const LPDecoderDisplayConfig cConfig);

        /*!
        * @brief
        *	获取显示配置
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwDisplay
        *	显示设备号
        * @param cConfig
        *	参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_DecoderGetDisplayConfig( HUSER hUser,  DWORD dwDisplay,  LPDecoderDisplayConfig cConfig);

        /*!
        * @brief
        *	转发历史流查询连接操作（支持多种录像类型的组合）
        * @param hStreamQuery
        *	历史流查询句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamQueryFactor
        *	历史流查询条件
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_TransmitHistoryStreamQueryConnect( HSTREAM_QUERY *hStreamQuery,  HUSER hUser,  LPTransmitHistoryStreamQueryFactor cStreamQueryFactor);

        /*!
        * @brief
        *	转发历史流查询断开操作
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_TransmitHistoryStreamQueryDisconnect( HSTREAM_QUERY hStreamQuery);

        /*!
        * @brief
        *	转发历史流查询下一条（仅支持单一录像类型和所有录像类型）
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @param pStreamQueryResult
        *	历史流查询的一条记录
        * @return
        *	返回0表示成功, 2为查询忙,3为查询已经完成,其它值为错误码
        * @note
        *	接口类型: 阻塞式
        */
        //public static extern int HieCIU_TransmitHistoryStreamQueryNext( HSTREAM_QUERY hStreamQuery,  LPHistoryStreamMultiTypeQueryResult pStreamQueryResult);

        /*!
        * @brief
        *	检测设备是否在线
        * @param cAddress
        *	设备地址
        * @param dwCommandPort
        *	设备信令端口
        * @param dwTime
        *	检测超时时间(毫秒),若小于100则将使用默认时间
        * @return
        *	返回0表示成功(在线), 1表示失败(不在线)
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_DeviceIsOnline", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieCIU_DeviceIsOnline( byte[] cAddress,  uint dwCommandPort,  uint dwTime);

        /*!
        * @brief
        *	识别指定的设备使用网络库的新旧版本
        *	阻塞操作
        * @param sServerIP
        *	设备的IP地址
        * @param dwCommandPort
        *	端口号
        * @param dwTime
        *	超时时间(毫秒),若为0则将使用默认时间10秒
        * @param nType
        *	网络库的版本类型
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieCIU.dll", EntryPoint = "HieCIU_GetSDKVersion", CallingConvention = CallingConvention.StdCall)]
        public static extern int  HieCIU_GetSDKVersion( IntPtr sServerIP,  uint dwCommandPort,  uint dwTime, ref Common.eNetworkSDKVersion nType);

    }
}

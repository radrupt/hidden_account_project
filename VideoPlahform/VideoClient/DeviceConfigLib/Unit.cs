using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
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
/************* 定义常用的别名 end  **************/
/*
 * 
 * test svn branchs1
 * 
 **/
namespace HieCiULib
{
    public class Unit
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>启动服务后,不再需要服务时请调用停止服务接口   接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_Start")]
        public static extern int HieClient_Start();

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_Stop")]
        public static extern int HieClient_Stop();

        /// <summary>
        /// 用户登录,对设备的所有操作均需要先登录
        /// </summary>
        /// <param name="hUser">用户句柄,登录后接口填充该句柄值</param>
        /// <param name="cUserLoginPara">用户登录参数信息</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_UserLogin", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_UserLogin(ref HUSER hUser, ref Common.UserLoginPara cUserLoginPara);

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="hUser">用户句柄,通过用户登录获取的句柄</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_UserLogout", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_UserLogout(HUSER hUser);

        /// <summary>
        /// 订阅服务器事件
        /// </summary>
        /// <param name="hUser">用户句柄,通过用户登录获取的句柄</param>
        /// <param name="dwEventTypes">远程事件类型,通过或运算一次可以订阅多种事件，共用相同回调和用户数据</param>
        /// <param name="cbEvent">回调函数</param>
        /// <param name="dwUserData">事件回调时回传给应用层</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_SubscribeEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_SubscribeEvent(HUSER hUser, uint dwEventTypes, Common.CB_UserEvent cbEvent, uint dwUserData);

        /// <summary>
        /// 退订服务器事件
        /// </summary>
        /// <param name="hUser">用户句柄,通过用户登录获取的句柄</param>
        /// <param name="dwEventTypes">远程事件类型,通过或运算一次可以退订多种事件</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_UnSubscribeEvent", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_UnSubscribeEvent(HUSER hUser, uint dwEventTypes);

        /*!
        * @brief
        *	设置配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_SetConfig", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int HieClient_SetConfig(HUSER hUser, IntPtr ii);

        /*!
        * @brief
        *	获取配置信息
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cConfig
        *	配置信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetConfig", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int HieClient_GetConfig(HUSER hUser, IntPtr ii);

        /// <summary>
        /// 获取客户端SDK属性(用于控制客户端SDK接口行为)
        /// </summary>
        /// <param name="eType">客户端SDK属性类型</param>
        /// <param name="dwAttribute">客户端SDK属性值</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetAttribute", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_GetAttribute(Common.eClientSDKAttributeType eType, ref uint dwAttribute);

        /// <summary>
        /// 设置客户端SDK属性(用于控制客户端SDK接口行为)
        /// </summary>
        /// <param name="eType">客户端SDK属性类型</param>
        /// <param name="dwAttribute">客户端SDK属性值</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>接口类型: 阻塞式</remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_SetAttribute", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_SetAttribute(Common.eClientSDKAttributeType eType, uint dwAttribute);

        /*!
        * @brief
        *	远程设备控制
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param eOperationCode
        *	操作码　
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceControl(HUSER hUser, Common.eRemoteDeviceControl eOperationCode);

        /*!
        * @brief
        *	远程设备强制I帧
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannel
        *	通道号
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_ForceIFrame", CharSet = CharSet.Ansi)]
        public static extern int HieClient_ForceIFrame(HUSER hUser, uint dwChannel);

        /*!
        * @brief
        *	远程清除报警
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_AlarmClear", CharSet = CharSet.Ansi)]
        public static extern int HieClient_AlarmClear(HUSER hUser, uint dwChannel, uint dwAlarmType);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_ImageCapture", CharSet = CharSet.Ansi)]
        public static extern int HieClient_ImageCapture(HUSER hUser, uint dwChannel, Common.eImageFormat eFormat, ref Common.ImageFileInfo pImageFileInfo);

        /*!
        * @brief
        *	远程锁定文件
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileLock", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileLock(HUSER hUser, string szFileName, Common.eFileLockOperationCode eOperationCode);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_RecordControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_RecordControl(HUSER hUser, uint dwChannel, uint dwRecordType);

        /*!
        * @brief
        *	远程报警输出控制
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_AlarmoutControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_AlarmoutControl(HUSER hUser, uint dwChannel, uint dwSwitch);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_AlarmoutStateGet", CharSet = CharSet.Ansi)]
        public static extern int HieClient_AlarmoutStateGet(HUSER hUser, ref uint dwChanNum, ref uint dwStateBits);

        /*!
        * @brief
        *	连接实时流
        * @param hStream
        *	流媒体句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	实时流连接信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_RealStreamConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_RealStreamConnect(ref HSTREAM hStream, HUSER hUser, ref Common.RealStreamPara cStreamPara);

        /*!
        * @brief
        *	断开实时流
        * @param hStream
        *	流媒体句柄,通过连接实时流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_RealStreamDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_RealStreamDisconnect(HSTREAM hStream);

        /*!
        * @brief
        *	创建历史通道（仅支持单一录像类型和所有录像类型）
        * @param hHistory
        *	历史通道句柄,创建后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	历史通道参数信息
        * @param dwUserData
        *	历史流用户数据，该数据会在历史流事件通知中传递给用户；
        *	用户可以根据该用户数据判断是哪一个历史流及该历史流是用于下载或是回放的
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamCreate", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamCreate(ref HSTREAM hHistory, HUSER hUser, ref Common.HistoryStreamPara cStreamPara, uint dwUserData);

        /*!
        * @brief
        *	创建历史通道（支持多种录像类型的组合）
        * @param hHistory
        *	历史通道句柄,创建后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	历史通道参数信息
        * @param dwUserData
        *	历史流用户数据，该数据会在历史流事件通知中传递给用户；
        *	用户可以根据该用户数据判断是哪一个历史流及该历史流是用于下载或是回放的
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamMultiTypeCreate", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamMultiTypeCreate(ref HSTREAM hHistory, HUSER hUser, ref Common.HistoryStreamMultiTypePara cStreamPara, uint dwUserData);

        /*!
        * @brief
        *	销毁历史通道
        * @param hHistory
        *	历史通道句柄,通过创建历史通道获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamDestroy", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamDestroy(HSTREAM hHistory);

        /*!
        * @brief
        *	历史通道时间位置
        * @param hHistory
        *	历史通道句柄,通过创建历史通道获取
        * @param eOperation
        *	历史通道操作类型
        * @param cTime
        *	历史通道时间信息,获取时此参数为出参,设置时此参数为入参
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamPosition", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamPosition(HSTREAM hHistory, Common.eOperationType eOperation, ref Common.TimeInfo cTime);

        /*!
        * @brief
        *	将目的历史通道与基准历史通道进行同步操作
        * @param hHistory
        *	基准历史通道句柄,连接的流将以该历史通道时间为基准
        * @param hDestHistory
        *	目的历史通道句柄，该历史通道将于基准历史通道同步
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	历史流支持多通道同时间点同时操作
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamSync", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamSync(HSTREAM hHistory, HSTREAM hDestHistory);

        /*!
        * @brief
        *	将目的历史通道从基准历史流中移除操作
        * @param hHistory
        *   基准历史通道句柄
        * @param hDestHistory
        *	目的历史流通道句柄
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamUnSync", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamUnSync(HSTREAM hHistory, HSTREAM hDestHistory);

        /*!
        * @brief
        *	设置历史流快放速度
        * @param hHistory
        *   历史通道句柄
        * @param dwSpeed
        *	历史流快放速度（2x 4x 8x 16x）
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamSetSpeed", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamSetSpeed(HSTREAM hHistory, uint dwSpeed);

        /*!
        * @brief
        *	设置历史流回调接口
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param cbStreamMedia
        *	流媒体回调接口
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamCB(HSTREAM hStream, Common.CB_StreamMedia cbStreamMedia, uint dwUserData);

        /*!
        * @brief
        *	连接语音流
        * @param hStream
        *	语音流句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param cStreamPara
        *	语音流连接信息
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_VoiceStreamConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_VoiceStreamConnect(ref HSTREAM hStream, HUSER hUser, Common.VoiceStreamPara cStreamPara);

        /*!
        * @brief
        *	断开语音流
        * @param hStream
        *	语音流句柄,通过连接语音流获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_VoiceStreamDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_VoiceStreamDisconnect(HSTREAM hStream);

        /*!
        * @brief
        *	发送语音流数据接口
        * @param hStream
        *	语音流句柄,通过连接语音流获取
        * @param cFrame
        *	语音流帧数据,可从语音回调获取
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_VoiceStreamSend", CharSet = CharSet.Ansi)]
        public static extern int HieClient_VoiceStreamSend(HSTREAM hStream, ref Common.Buffer cFrame);

        /*!
        * @brief
        *	设置语音流回调接口
        * @param hStream
        *	语音流句柄,通过连接语音流获取
        * @param cbStreamVoice
        *	语音流回调接口,若值为空(NULL),表示不再需要回调
        * @param dwUserData
        *	语音流回调时回传给应用层
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	由于此回调函数在语音流数据接收与捕获线程调用,
        *	为了保证实时性,请尽可能减少在回调中的阻塞式工作,以避免出现丢帧.
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_VoiceStreamCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_VoiceStreamCB(HSTREAM hStream, Common.CB_StreamVoice cbStreamVoice, uint dwUserData);

        /*!
        * @brief
        *	设置流媒体回调接口
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param cbStreamMedia
        *	流媒体回调接口,若值为空(NULL),表示不再需要回调
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_StreamMediaCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_StreamMediaCB(HSTREAM hStream, Common.CB_StreamMedia cbStreamMedia, uint dwUserData);

        /*!
        * @brief
        *	流媒体控制接口,控制流媒体的启动与停止
        * @param hStream
        *	流媒体句柄,通过连接流获取
        * @param eMediaControl
        *	流媒体控制命令
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_StreamMediaControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_StreamMediaControl(HSTREAM hStream, Common.eTaskControl eMediaControl);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_SetStreamMediaLinkID", CharSet = CharSet.Ansi)]
        public static extern int HieClient_SetStreamMediaLinkID(HSTREAM hStream, uint dwLinkID);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_QueryStreamMediaLinkID", CharSet = CharSet.Ansi)]
        public static extern int HieClient_QueryStreamMediaLinkID(HUSER hUser, Common.eMediaFunctionType eType, ref Common.MediaLinkIDQueryResult cQueryResult);

        /*!
        * @brief
        *	历史流查询连接操作（仅支持单一录像类型和所有录像类型）
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamQueryConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamQueryConnect(ref HSTREAM_QUERY hStreamQuery, HUSER hUser, ref Common.HistoryStreamQueryFactor cStreamQueryFactor);

        /*!
        * @brief
        *	历史流查询连接操作（支持多种录像类型的组合）
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamMultiTypeQueryConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamMultiTypeQueryConnect(ref HSTREAM_QUERY hStreamQuery, HUSER hUser, ref Common.HistoryStreamMultiTypeQueryFactor cStreamQueryFactor);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamQueryDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamQueryDisconnect(HSTREAM_QUERY hStreamQuery);

        /*!
        * @brief
        *	历史流查询下一条（仅支持单一录像类型和所有录像类型）
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @param pStreamQueryResult
        *	历史流查询的一条记录
        * @return
        *	返回0表示成功, 2为查询忙,3为查询已经完成,其它值为错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamQueryNext", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamQueryNext(HSTREAM_QUERY hStreamQuery, ref Common.HistoryStreamQueryResult pStreamQueryResult);

        /*!
        * @brief
        *	历史流查询下一条（支持多种录像类型的组合）
        * @param hStreamQuery
        *	历史流查询句柄,通过连接历史流查询获取
        * @param pStreamQueryResult
        *	历史流查询的一条记录
        * @return
        *	返回0表示成功, 2为查询忙,3为查询已经完成,其它值为错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryStreamMultiTypeQueryNext", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryStreamMultiTypeQueryNext(HSTREAM_QUERY hStreamQuery, ref Common.HistoryStreamMultiTypeQueryResult pStreamQueryResult);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileQueryConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileQueryConnect(ref HFILE_QUERY hFileQuery, HUSER hUser, ref Common.FileQueryFactor pFileQueryFactor);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileQueryDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileQueryDisconnect(HFILE_QUERY hFileQuery);

        /*!
        * @brief
        *	文件查询下一条
        * @param hFileQuery
        *	文件查询句柄 ,通过连接文件查询获取
        * @param pFileQueryResult
        *	文件查询的一条记录
        * @return
        *	返回0表示成功, 2为查询忙,3为查询已经完成,其它值为错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileQueryNext", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileQueryNext(HFILE_QUERY hFileQuery, ref Common.FileQueryResult pFileQueryResult);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryLogQueryConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryLogQueryConnect(ref HLOG_QUERY hLogQuery, HUSER hUser, ref Common.LogQueryFactor pLogQueryFactor);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryLogQueryDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryLogQueryDisconnect(HLOG_QUERY hLogQuery);

        /*!
        * @brief
        *	历史日志查询下一条
        * @param hLogQuery
        *	历史日志查询句柄,通过连接历史日志查询获取
        * @param pLogQueryResult
        *	历史日志查询的一条记录
        * @return
        *	返回0表示成功, 2为查询忙,3为查询已经完成,其它值为错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_HistoryLogQueryNext", CharSet = CharSet.Ansi)]
        public static extern int HieClient_HistoryLogQueryNext(HLOG_QUERY hLogQuery, ref Common.LogQueryResult pLogQueryResult);

        /*!
        * @brief
        *	透明通道连接操作
        * @param hTransparent
        *	透明通道句柄,连接后接口填充该句柄值
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param pTransparentPara
        *	透明通道连接参数
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransparentChannelConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransparentChannelConnect(ref HTRANSPARENT hTransparent, HUSER hUser, ref Common.TransparentChannelPara pTransparentPara);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransparentChannelDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransparentChannelDisconnect(HTRANSPARENT hTransparent);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransparentChannelWrite", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransparentChannelWrite(HTRANSPARENT hTransparent, ref Common.Buffer pBuffer);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransparentChannelCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransparentChannelCB(HTRANSPARENT hTransparent, Common.CB_TransparentChannel cbTransparent, uint dwUserData);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileUploadConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileUploadConnect(ref HFILE_TRANSFER hFileTransfer, HUSER hUser, ref Common.FileUploadPara pFileUploadPara);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileUploadControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileUploadControl(HFILE_TRANSFER hFileTransfer, Common.eFileTransferControl eControlCode);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileUploadDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileUploadDisconnect(HFILE_TRANSFER hFileTransfer);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileUploadCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileUploadCB(HFILE_TRANSFER hFileTransfer, Common.CB_FileUpload cbFileUpload, uint dwUserData);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileDownloadConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileDownloadConnect(ref HFILE_TRANSFER hFileTransfer, HUSER hUser, ref Common.FileDownloadPara pFileDownloadPara);


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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileDownloadControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileDownloadControl(HFILE_TRANSFER hFileTransfer, Common.eFileTransferControl eControlCode);


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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileDownloadControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileDownloadDisconnect(HFILE_TRANSFER hFileTransfer);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_FileDownloadCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_FileDownloadCB(HFILE_TRANSFER hFileTransfer, Common.CB_FileDownload cbFileDownload, uint dwUserData);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceRegisterListenStart", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceRegisterListenStart(char[] sListenIP, uint dwListenPort);

        /*!
         * \brief
         *	停止监听程序
         * \return
         *   TRUE表示成功， FALSE表示失败
         */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceRegisterListenStop", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceRegisterListenStop();

        /*!
        * \brief
        *    设置设备注册回调函数
        * \param cbDeviceRegister
        *    回调函数指针
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceRegisterCB", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceRegisterCB(Common.CB_DeviceRegister cbDeviceRegister);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_PTZControl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_PTZControl(HUSER hUser, uint dwChannel, Common.ePTZControlCmdCode eCommandCode, uint dwParam1, uint dwParam2, uint dwParam3, uint dwParam4);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DataExistCheck", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DataExistCheck(HUSER hUser, uint dwChannel, uint dwMajorType, uint dwMinorType, char[] szYearMonth, ref uint dwResult);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DiskGroupManage", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DiskGroupManage(HUSER hUser, Common.eDiskGroupOperation eOperationCode, uint dwParam1, uint dwParam2, uint dwParam3);

        /*!
        * @brief
        *	获取数据大小（支持多种录像类型的组合）
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannelBits
        *	0－31位分别表示0－31通道的选中状态，1表示选中
        * @param eDskGroupType
        *	磁盘分组类型
        * @param eStreamType
        *	录像类型
        * @param szStartTime
        *	开始时间
        * @param szEndTime
        *	结束时间
        * @param dwDataSize
        *	数据大小
        * @param dwUserData
        *	用户数据
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetMultiTypeDataSize", CharSet = CharSet.Ansi)]
        public static extern int HieClient_GetMultiTypeDataSize(HUSER hUser, uint dwChannelBits, Common.eDiskGroupType eDskGroupType, uint dwStreamType, char[] szStartTime, char[] szEndTime, uint dwUserData, ref uint dwDataSize);

        /*!
        * @brief
        *	获取数据大小（仅支持单一录像类型和所有录像类型）
        * @param hUser
        *	用户句柄,通过用户登录获取的句柄
        * @param dwChannelBits
        *	0－31位分别表示0－31通道的选中状态，1表示选中
        * @param eDskGroupType
        *	磁盘分组类型
        * @param eStreamType
        *	录像类型
        * @param szStartTime
        *	开始时间
        * @param szEndTime
        *	结束时间
        * @param dwDataSize
        *	数据大小
        * @param dwUserData
        *	用户数据
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetDataSize", CharSet = CharSet.Ansi)]
        public static extern int HieClient_GetDataSize(HUSER hUser, uint dwChannelBits, Common.eDiskGroupType eDskGroupType, Common.eHistoryStreamType dwStreamType, char[] szStartTime, char[] szEndTime, uint dwUserData, ref uint dwDataSize);

        /// <summary>
        /// 设置强制删除已登录用户回调接口
        /// </summary>
        /// <param name="cbDeleteUserForce">用户句柄,通过用户登录获取的句柄</param>
        /// <param name="dwUserData">发送命令时回传给应用层</param>
        /// <returns>返回0表示成功, 否则表示错误码</returns>
        /// <remarks>
        /// 应用层可使用此接口设置强制删除已登录用户回调,
        /// 由此接口通知用户已被设备端强制删除,
        /// 由于此回调函数在需要回复设备,
        /// 为了保证实时性,请尽可能减少在回调中的阻塞式工作.  
        /// 接口类型: 阻塞式
        /// </remarks>
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeleteUserForceCB", CallingConvention = CallingConvention.StdCall)]
        public static extern int HieClient_DeleteUserForceCB(Common.CB_DeleteUserForce cbDeleteUserForce, uint dwUserData);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_StringToLattice", CharSet = CharSet.Ansi)]
        public static extern int HieClient_StringToLattice(char[] pString, ref Common.FontPara cFont, IntPtr buffer, ref uint dwBuflen, ref uint dwWidth, ref uint dwHeight);


        /*
        * @brief
        *	分屏模式控制接口
        * @param dwUserID
        *	用户ID
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_SetScreenSplitMode", CharSet = CharSet.Ansi)]
        public static extern int HieClient_SetScreenSplitMode(HUSER hUser, uint dwSplitNum, ref uint pChanList);


        /*!
        * \brief
        *	使用HTTP协议读取服务端的配置信息
        * \param pURL
        *	服务端URL路径,可用格式:
        *	纯域名:			http:// xxx.xxx.com
        *	纯IP地址:		http:// xxx.xxx.xxx.xxx
        *	域名与端口:		http://xxx.xxx.com:port
        *	IP地址与端口	http://xxx.xxx.xxx.xxx:port
        * \param cConfig
        *   来自HTTP的设备配置
        * \return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetDeviceConfigFromHttp", CharSet = CharSet.Ansi)]
        public static extern int HieClient_GetDeviceConfigFromHttp(char[] pURL, ref Common.DeviceConfigFromHttp cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceProbeStart", CallingConvention = CallingConvention.StdCall,CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceProbeStart( Common.DeviceProbeParameter cParameter, Common.CB_DeviceProbe cbDeviceProbe);

        /*!
        * \brief
        *	停止探测设备
        * \return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceProbeStop", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceProbeStop();

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceProbeSetDeviceConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceProbeSetDeviceConfig( byte[] pMac, ushort
            
            wProbePort, ref Common.DeviceProbeConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceProbeSetAllDeviceConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceProbeSetAllDeviceConfig(ref Common.DeviceProbeParameter cParameter, ref Common.DeviceProbeConfig cConfig);

        /*!
        * \brief
        *	通过探测方式设置设备配置
        * \param pMac
        *	设备MAC地址, 同DeviceProbeConfig结构中MAC地址信息, 长度为 MAC_BINARY_ADDRESS_LEN
        * \param wProbePort
        *	设备探测端口
        * \param cUser
        *	设备用户信息
        * \param cConfig
        *	设备配置信息
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceProbeSetConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceProbeSetConfig(
            byte[] pMac, ushort wProbePort, ref Common.DeviceProbeUserLoginPara cUser, ref Common.DeviceProbeConfigInformation cConfig);

        /*!
        * \brief
        *	通过探测方式获取设备配置
        * \param pMac
        *	设备MAC地址, 同DeviceProbeConfig结构中MAC地址信息, 长度为 MAC_BINARY_ADDRESS_LEN
        * \param wProbePort
        *	设备探测端口
        * \param cUser
        *	设备用户信息
        * \param cConfig
        *	设备配置信息
        * \return
        *	返回0表示成功, 否则表示错误码
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceProbeGetConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceProbeGetConfig(
            byte[] pMac, ushort wProbePort, ref Common.DeviceProbeUserLoginPara cUser, ref Common.DeviceProbeConfigInformation cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_PanelGetStatus", CharSet = CharSet.Ansi)]
        public static extern int HieClient_PanelGetStatus(ref HUSER hUser, ref  Common.PanelStatusInfo cStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_PanelSetStatus", CharSet = CharSet.Ansi)]
        public static extern int HieClient_PanelSetStatus( ref HUSER hUser, ref Common.PanelStatusInfo cStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_PanelControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_PanelControl(ref HUSER hUser, ref Common.PanelControlParameter cControl);

        /*!
        * @brief
        *	远程设置恢复色度默认参数
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_SetChromaDefault", CharSet = CharSet.Ansi)]
        public static extern int HieClient_SetChromaDefault(ref HUSER hUser, ref uint dwChannel, ref uint dwTimeSection);

        /*
        * @brief
        *	默认文件头获取
        * @param inStreamID
        *	媒体流ID
        * @param pBuffer
        *	存放默认文件头的缓冲区
        * @param pBufferLen
        *	默认文件头长度
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetFileHeader", CharSet = CharSet.Ansi)]
        public static extern int HieClient_GetFileHeader(ref uint inStreamID,ref string pBuffer,ref uint pBufferLen);
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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_StreamTransferConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_StreamTransferConnect(ref HSTREAM hStream, ref HUSER hUser, ref uint dwChannel, ref  Common.StreamTransferPara cStreamPara);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_StreamTransferSend", CharSet = CharSet.Ansi)]
        public static extern int HieClient_StreamTransferSend(ref HSTREAM hStream, ref  Common.StreamTransferFrame cFrame);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_StreamTransferDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_StreamTransferDisconnect(ref HSTREAM hStream);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderDynamicConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderDynamicConnect(ref HUSER hUser, ref uint dwChannel, ref  Common.DecoderDynamicInformation cStreamPara);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderDynamicDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderDynamicDisconnect(ref HUSER hUser, ref uint dwChannel);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderSetConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderSetConfig(ref HUSER hUser, ref Common.DecoderDeviceConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetConfig(ref HUSER hUser, ref Common.DecoderDeviceConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderSetChannelConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderSetChannelConfig(ref HUSER hUser, ref uint dwChannel, ref  Common.DecoderChannelConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetChannelConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetChannelConfig(ref HUSER hUser, ref uint dwChannel, ref Common.DecoderChannelConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderSetVideoStandard", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderSetVideoStandard(ref HUSER hUser, ref uint dwChannel, ref  Common.DecoderVideoStandardConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetVideoStandard", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetVideoStandard(ref HUSER hUser, ref uint dwChannel, ref Common.DecoderVideoStandardConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderSetChannelLoopConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderSetChannelLoopConfig(ref HUSER hUser, ref uint dwChannel, ref Common.DecoderChannelLoopConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetChannelLoopConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetChannelLoopConfig(ref HUSER hUser, ref uint dwChannel, ref Common.DecoderChannelLoopConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelSetLoopEnable", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelSetLoopEnable(ref HUSER hUser, ref uint dwChannel, ref Common.eDecoderChannelStatus eStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelGetLoopEnable", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelGetLoopEnable(ref HUSER hUser, ref uint dwChannel, ref Common.eDecoderChannelStatus eStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetLoopEnable", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetLoopEnable(ref HUSER hUser, ref Common.DecoderLoopStatus cStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetChannelInformation", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetChannelInformation(ref HUSER hUser, ref uint dwChannel, ref Common.DecoderChannelInformation cInformation);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelGetStatus", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelGetStatus(ref HUSER hUser, ref Common.DecoderChannelStatus cStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelSetPlayStatus", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelSetPlayStatus(ref HUSER hUser, ref uint dwChannel, ref Common.DecoderSetPlayStatusParameter cControl);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelGetPlayStatus", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelGetPlayStatus(ref HUSER hUser, ref Common.DecoderGetPlayStatusParameter cControl);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_UploadLogo", CharSet = CharSet.Ansi)]
        public static extern int HieClient_UploadLogo(ref HUSER hUser, ref uint dwChannel, ref Common.UploadLogoParameter cParameter);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelLogoSwitch", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelLogoSwitch(ref HUSER hUser, ref uint dwChannel, ref Common.eDeviceChannelControl eOperationCode);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderChannelDisplayControl", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderChannelDisplayControl(ref HUSER hUser, ref uint dwDisplay, ref Common.DecoderDisplayStatus cStatus);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderSetDisplayConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderSetDisplayConfig(ref HUSER hUser, ref uint dwDisplay, ref  Common.DecoderDisplayConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DecoderGetDisplayConfig", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DecoderGetDisplayConfig(ref HUSER hUser, ref uint dwDisplay, ref Common.DecoderDisplayConfig cConfig);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransmitHistoryStreamQueryConnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransmitHistoryStreamQueryConnect(ref HSTREAM_QUERY hStreamQuery, ref HUSER hUser, ref Common.TransmitHistoryStreamQueryFactor cStreamQueryFactor);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransmitHistoryStreamQueryDisconnect", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransmitHistoryStreamQueryDisconnect(ref HSTREAM_QUERY hStreamQuery);

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
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_TransmitHistoryStreamQueryNext", CharSet = CharSet.Ansi)]
        public static extern int HieClient_TransmitHistoryStreamQueryNext(ref HSTREAM_QUERY hStreamQuery, ref Common.HistoryStreamMultiTypeQueryResult pStreamQueryResult);

        /*!
        * @brief
        *	检测设备是否在线
        * @param cAddress
        *	设备地址
        * @param dwCommandPort
        *	设备信令端口
        * @param dwTimeOut
        *	检测超时时间(毫秒),若小于100则将使用默认时间
        * @return
        *	返回0表示成功(在线), 1表示失败(不在线)
        * @note
        *	接口类型: 阻塞式
        */
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_DeviceIsOnline", CharSet = CharSet.Ansi)]
        public static extern int HieClient_DeviceIsOnline(ref string cAddress, ref uint dwCommandPort, ref uint dwTimeOut);

        /*!
        * @brief
        *	识别指定的设备使用网络库的新旧版本
        *	阻塞操作
        * @param sServerIP
        *	设备的IP地址
        * @param dwCommandPort
        *	端口号
        * @param dwTimeOut
        *	超时时间(毫秒),若为0则将使用默认时间10秒
        * @param nType
        *	网络库的版本类型
        * @return
        *	返回0表示成功, 否则表示错误码
        * @note
        *	接口类型: 阻塞式
        
        [DllImport("HieClientUnit.dll", EntryPoint = "HieClient_GetSDKVersion", CharSet = CharSet.Ansi)]
        public static int HieClient_GetSDKVersion( ref string sServerIP, ref uint dwCommandPort, ref uint dwTimeOut, ref Common.eNetworkSDKVersion nType);
        */


    }
}

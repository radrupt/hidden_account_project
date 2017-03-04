using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HieClientLib
{
    class ConfigureV1
    {
        public const int HY_V1_DVR_MAX_CHANNUM = 16; 			/*最大通道数*/
        public const int HY_V1_DVR_NAME_LEN = 32; 			/*用户名最大长度*/
        public const int HY_V1_DVR_SERIALNO_LEN = 48; 			/*序列号最大长度*/
        public const int HY_V1_DVR_MACADDR_LEN = 8; 			/*设备物理地址长度*/
        public const int HY_V1_DVR_PASSWD_LEN = 16; 			/*用户密码最大长度*/
        public const int HY_V1_DVR_MAX_ALARMIN = 16; 			/*最大报警输入个数*/
        public const int HY_V1_DVR_MAX_ALARMOUT = 4; 			/*最大报警输出个数*/
        public const int HY_V1_DVR_MAX_STREAM_TYPES = 2; 			/*最大编码格式个数*/
        public const int HY_V1_DVR_MAX_TIMESEGMENT = 4; 			/*最大时间段个数*/
        public const int HY_V1_DVR_MAX_PRESET = 128; 			/*最大预置点个数*/
        public const int HY_V1_DVR_MAX_DAYS = 7; 			/*周最大天数*/
        public const int HY_V1_DVR_MAX_SHELTERNUM = 4; 			/*遮挡区域数*/
        public const int HY_V1_DVR_MAX_EXCEPTIONNUM = 16; 			/*最大异常个数*/
        public const int HY_V1_DVR_MAX_PTZ_NUMS = 40; 			/*最大PTZ个数*/
        public const int HY_V1_DVR_PTZ_NAME_LEN = 10; 			/*最大PTZ名字长度*/

        /*PTZ协议信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_PTZ
        {
   	        public byte     PTZNum;							/*系统支持PTZ协议的个数(只读)*/ 
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst = HY_V1_DVR_MAX_PTZ_NUMS * HY_V1_DVR_PTZ_NAME_LEN )]
            public string    PTZName;/*系统当前支持的PTZ协议名称(只读)*/
        }

        /*时间配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_TIME
        {
	        ushort dwYear;	//年
	        ushort dwMonth;	//月
	        ushort dwDay;		//日
	        ushort dwHour;	//时
	        ushort dwMinute;	//分
	        ushort dwSecond;	//秒
        }

        /*网络配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_NETCFG
        {
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string  sDVRIP;						/* 服务器IP地址 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string  sDVRIPMask;					/* 服务器IP地址掩码 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string  sGatewayIP;					/* 网关地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst =HY_V1_DVR_MACADDR_LEN )]
	        public byte[]  byMACAddr;	/* 服务器的物理地址(只读)*/
	        public uint  dwPPPOE;							/* 0-不启用,1-启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_NAME_LEN)]
	        public byte[]  sPPPoEUser;		/* PPPoE用户名 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_PASSWD_LEN )]
            public string  sPPPoEPassword;/* PPPoE密码 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string  sPPPoEIP;						/* PPPoE IP地址(只读) */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string  sPrimaryDNS;					/* 主DNS服务器 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string  sSecondaryDNS;				/* 备用DNS */
	        public ushort  dwAutoReg;						/* 0-不启用自动注册, 1-启用自动注册 */
	        public ushort  dwAutoRegIntervals;				/* 注册间隔时间, 1-999秒 */
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst =16 )]
            public byte[]  sManageHostIP;				/* 远程管理主机地址 */
	        public uint  wManageHostPort;				    /* 远程管理主机端口 */
	        public ushort  wLocalCmdPort;					/* 服务器本地命令端口 */
	        public ushort  wLocalMediaPort;					/* 服务器本地媒体端口 */
	        public uint dwNAT;							/* 0--不启用NAT, 1-启用NAT(停用)*/
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst =16 )]
            public byte[]  sNATIP;						/* NAT地址(停用)*/
	        public ushort  byLinkMode;						/* 0：TCP	1：UDP	2：多播(停用)*/
        }

        /*设备配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi )]
        public struct HY_V1_DVR_DEVICECFG{ 
	        public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst =HY_V1_DVR_NAME_LEN )]
	        public byte[] sDVRName;			/* 服务器名称 */ 
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst =HY_V1_DVR_SERIALNO_LEN )]
            public byte[] sSerialNumber;/* 序列号(只读)*/ 
	        public uint dwSystemVideoStandard;			/*服务器视频制式：0--NTSC 1--PAL 2--SECAMA*/
	        public uint dwDVRID;						    /* 服务器 ID，用于遥控器，-1为未设置，设置范围0-99 */ 
	        public uint dwRecycleRecord; 				    /* 是否循环录像，0：不是；1：是 */ 
	        public uint dwSoftwareVersion;  				/* 软件版本号,高字高8位是主版本,低字高8位是次版本号， 低字低8位是修订版本(只读)*/ 
	        public uint dwSoftwareBuildDate; 				/* 软件生成日期,0xYYYYMMDD(只读)*/ 
	        public uint dwDSPSoftwareVersion; 			/* DSP软件版本,高字高8位是主版本,低字高8位是次版本号， 低字低8位是修订版本(只读)*/
	        public uint dwDSPSoftwareBuildDate;			/* DSP软件生成日期,0xYYYYMMDD(只读)*/
	        public uint dwPanelVersion; 					/* 前面板版本,高16位是主版本,低16位是次版本(只读)*/ 
	        public uint dwHardwareVersion; 				/* 硬件版本,高16位是主版本,低16位是次版本(只读)*/ 
	        public byte byAlarmInPortNum; 					/* 服务器报警输入个数(只读)*/ 
	        public byte byAlarmOutPortNum; 				/* 服务器报警输出个数(只读)*/ 
	        public byte byRS232Num; 						/* 服务器232串口个数(只读)*/ 
	        public byte byRS485Num; 						/* 服务器485串口个数(只读)*/ 
	        public byte byDiskCtrlNum; 					/* 服务器硬盘控制器个数(只读)*/ 
	        public byte byDiskNum;   						/* 服务器硬盘个数(只读)*/ 
	        public byte byDVRType; 						/* 服务器类型：(只读)
                                                    1． 1500 4CIF DVR
                                                    2． 1500 8CIF DVR
                                                    3． 1500 12CIF DVR
                                                    4． 1500 16CIF DVR
                                                    5． 1500 4D1 DVR
                                                    6． 1500 8D1 DVR
                                                    7． 8950 4CIF DVR
                                                    8． 1700 8CIF DVR
                                                    9． 1700 16CIF DVR
                                                    10．1700 4D1 DVR
                                                    11．1700 8D1 DVR
                                                    12．1700 16D1 DVR
                                                    13．1D1 DVS
                                                    14．2D1 DVS
                                                    15．4CIF DVS
                                                    16．1D1 DVS-ATA
                                                    17．2D1 DVS-ATA
                                                    18．4CIF DVS-ATA*/
	        public byte byChanNum; 						/* 服务器通道个数(只读)*/ 
	        public byte byDecordChans; 					/* 服务器解码路数(只读)*/ 
	        public byte byVGANum; 							/* VGA口的个数(只读)*/ 
	        public byte byUSBNum; 							/* USB口的个数(只读)*/ 
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =3 )]
            public string reservedData; 					/* 保留 */
        
        }

        public const int  NOACTION  =  			0x0	;			/*无响应*/
        public const int  WARNONMONITOR  =  		0x1	;			/*监视器上警告*/
        public const int  WARNONAUDIOOUT  =  		0x2	;			/*声音警告*/
        public const int  UPTOCENTER  =  			0x4	;			/*上传中心*/
        public const int  TRIGGERALARMOUT  =  		0x8	;			/*触发报警输出*/
        public const int EMAILNOTIFY = 0x10;			/*Email通知*/

        /*异常处理信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_HANDLEEXCEPTION
        {
	        public uint	dwHandleType;			        /*处理方式,处理方式的"或"结果*/
											        /*0x00: 无响应*/
											        /*0x01: 监视器上警告（预览图像上叠加）*/
											        /*0x02: 声音警告*/
											        /*0x04: 上传中心*/
											        /*0x08: 触发报警输出*/
											        /*0x10: Email通知*/
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst =HY_V1_DVR_MAX_ALARMOUT )]
            public byte[] byRelAlarmOutx;/*报警触发的输出通道,报警触发的输出,为1表示触发该输出*/
        }

        /*时间段配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_SCHEDTIME
        {
	        public byte byStartHour;                       /*开始时间*/
	        public byte byStartMin;
	        public byte byStopHour;                        /*结束时间*/
	        public byte byStopMin;
        }

        /*报警输入配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_ALARMINCFG{
	        public uint dwSize;
	        public HY_V1_DVR_HANDLEEXCEPTION struAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_DAYS * HY_V1_DVR_MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
	        public HY_V1_DVR_SCHEDTIME[] struAlarmTime; 
	        /*共七个时间段，分别为星期一、二、三、四、五、六、日*/
	        public byte byAlarmType;						/* 报警器类型,0：常开,1：常闭 */
	        public byte byAlarmInHandle;					/* 是否处理, 0:不处理 ，1：处理 */
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byRelRecordChan;/* 报警触发的录象通道,为1表示触发该通道 */
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byEnablePreset;/* 是否调用预置点 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byPresetNo;	/* 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点。*/
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byEnableCruise;		/* 是否调用巡航 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byCruiseNo;	/* 巡航 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byEnablePtzTrack;/* 是否调用轨迹 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byPTZTrack;	/* 调用的云台的轨迹序号 */
        }

        /*报警输出配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_ALARMOUTCFG
        {
	        public uint dwSize;
	        public uint dwAlarmOutDelay;					/* 输出保持时间(有效参数5, 10, 30 ,60, 120, 300, 600, -1手动关闭) */
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_DAYS*HY_V1_DVR_MAX_TIMESEGMENT,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_SCHEDTIME[] struAlarmTime;
	        /*共七个时间段，分别为星期一、二、三、四、五、六、日*/
	        public byte byEnableTimeTable;					/*是否启用时间表*/
	        public byte byAlarmOutHandle;					/* 是否处理*/ 
        }

        /*异常配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_EXCEPTION{
	        public uint dwSize; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =HY_V1_DVR_MAX_EXCEPTIONNUM ,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_HANDLEEXCEPTION[] struExceptionHandleType; 
	        /*数组0-硬盘满,1- 局域网内IP 地址冲突,2-硬盘出错,3-非法访问, 4-网线断*/ 
        } 

        /*解码器配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_DECODERCFG{
	        public uint dwSize; 							/* 此结构的大小 */ 
	        public uint dwBaudRate; 						/* 波特率(bps)，0－1200，1－2400，2－4800，3－9600 */ 
	        public byte byDataBit; 						/* 数据有几位 0－4位，1－5位，2－6位，3－7位，4-8位; */ 
	        public byte byStopBit; 						/* 停止位 0－1位，1－1.5位,  2 -2位; */ 
	        public byte byParity; 							/* 校验 0－无校验，1－奇校验，2－偶校验; */ 
	        public byte byFlowcontrol; 					/* 0－无*/ 
	        public ushort wDecoderType;  					/* 解码器类型*/
	        public ushort wDecoderAddress;					/* 解码器地址:0 - 255 */ 
	        public ushort wComType;	 						/* 0－COM1 RS232，1－COM2 RS485 */
	        public HY_V1_DVR_PTZ   strPTZInfo;				/* PTZ信息 */
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_PRESET)]
            public byte[] bySetPreset;	/* 预置点是否设置,0-没有设置,1-设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_PRESET)]
	        public byte[] bySetCruise;	/* 巡航是否设置,0-没有设置,1-设置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_PRESET)]
	        public byte[] bySetTrack;		/* 轨迹是否设置,0-没有设置,1-设置*/ 
        }

        /*时间段录像配置*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_RECORDSCHED{ 
	        public HY_V1_DVR_SCHEDTIME struRecordTime; 
	        public byte byRecordType;						/* 0：定时录像1：报警录像2：移动侦测3：动测|报警，255：不启用*/ 
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =3 )]
            public string reservedData;					/* 保留 */ 
        } 

        /*全天录像配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_RECORDDAY
        { 
	        public ushort wAllDayRecord;						/* 是否全天录像 */ 
	        public byte byRecordType;						/* 0：定时录像1：报警录像2：移动侦测3：动测|报警*/ 
            public char reservedData; 
        }

        /*录像配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_RECORDCFG{
	        public uint dwSize;							/* 此结构的大小 */ 
	        public uint dwRecord; 						/* 是否录像 0-否 1-是 */ 
	        public uint dwRecordTime; 					/* 录象时间长度 */ 
	        public uint dwPreRecordTime;					/* 预录时间0-10（单位：秒）*/
            public uint dwDelayTime;						/* 延迟录像时间 0-1800(单位：秒) */
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =HY_V1_DVR_MAX_DAYS + 1,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_RECORDDAY[] struRecAllDay;/* 全天录像 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =(HY_V1_DVR_MAX_DAYS + 1)*(HY_V1_DVR_MAX_TIMESEGMENT - 2) ,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_RECORDSCHED[] struRecordSched; 
	        /*共八个时间段，分别为星期日、一、二、三、四、五、六、整个星期*/
        }      /* 时间段录像 */

        //移动侦测
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_MOTION{ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12*16)]
            public byte[] byMotionScope; 			/* 侦测区域,共有12*16个小宏块,为0表示该宏块是移动侦测区域,1-表示不是 */ 
	        public byte byMotionSenstive; 					/* 移动侦测灵敏度, 0 - 2,值越小越灵敏,0xff关闭 */ 
	        public byte byEnableHandleMotion; 				/* 是否处理移动侦测 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM)]
            public byte[] byRelRecordChan;/*报警触发的录象通道,为0表示不触发，为1表示触发该通道 */
	        public HY_V1_DVR_HANDLEEXCEPTION strMotionHandleType;/* 处理方式 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =(HY_V1_DVR_MAX_DAYS)*(HY_V1_DVR_MAX_TIMESEGMENT) ,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_SCHEDTIME[] struAlarmTime;/*布防时间(停用)*/
        }

        //信号丢失报警 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_VILOST{ 
	        public byte byEnableHandleVILost; 				/* 是否处理信号丢失报警 */ 
	        public HY_V1_DVR_HANDLEEXCEPTION strVILostHandleType;	/* 处理方式 */ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =(HY_V1_DVR_MAX_DAYS)*(HY_V1_DVR_MAX_TIMESEGMENT) ,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_SCHEDTIME[] struAlarmTime;/*布防时间(停用)*/
        } 

        /*马赛克遮挡*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_MOSAIC
            { 
	        public ushort wHideAreaTopLeftX;					/* 马赛克区域的x坐标 */ 
	        public ushort wHideAreaTopLeftY; 				/* 马赛克区域的y坐标 */ 
	        public ushort wHideAreaWidth; 					/* 马赛克区域的宽 */ 
	        public ushort wHideAreaHeight; 					/* 马赛克区域的高 */ 
        }

        /*通道配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_PICCFG
        { 
	        public uint dwSize;							/* 此结构的大小 */
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_NAME_LEN)]
            public byte[] sChanName;		/* 通道名称 */ 
	        public uint dwVideoFormat;					/* 只读 视频制式 0-NTSC 1-PAL 2-SECAMA,视频制式配置以设备配置中的视频制式为主(只读).*/ 
	        public byte byBrightness; 						/* 亮度,0-255 */ 
	        public byte byContrast; 						/* 对比度,0-128 */ 
	        public byte bySaturation; 						/* 饱和度,0-128 */ 
	        public byte byHue; 							/* 色调,0-255 */ 
	        public uint dwShowChanName;					/* 预览的图象上是否显示通道名称,0-不显示,1-显示*/
	        public ushort wShowNameTopLeftX; 				/* 通道名称显示位置的x坐标, 0-379 */ 
	        public ushort wShowNameTopLeftY; 				/* 通道名称显示位置的y坐标, 0-459 */ 
	        public HY_V1_DVR_VILOST struVILost; 				/* 信号丢失报警 */
	        public HY_V1_DVR_MOTION struMotion; 				/* 移动侦测 */
	        public uint dwEnableHide;						/* 是否启动马赛克遮挡 ,0-否,1-是*/
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_SHELTERNUM,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_MOSAIC[] struMosaic; /*马赛克遮挡*/
	        /*OSD */
	        public uint dwShowOsd; 						/* 预览的图象上是否显示OSD,0-不显示,1-显示*/
	        public ushort wOSDTopLeftX;						/* OSD的x坐标, 0-379 */
	        public ushort wOSDTopLeftY;						/* OSD的y坐标, 0-499 */
	        public byte byOSDType; 						/* OSD类型(主要是年月日格式)(只读)*/
											        /* 0: XXXX-XX-XX 年月日 */
											        /* 1: XX-XX-XXXX 日月年 */
											        /* 2: XXXX年XX月XX日 */
											        /* 3: XX日XX月 XXXX年 */
											        /*若为其它值,表示该功能未实现*/
	        public byte byDispWeek; 						/* 是否显示星期(只读)*/
											        /*0为不显示,1为显示,其它为未实现*/
	        public byte byOSDAttrib;						/* OSD属性:透明，闪烁(只读)*/ 
											        /* 1: 透明,闪烁 */
											        /* 2: 透明,不闪烁 */
											        /* 3: 不透明,闪烁 */
											        /* 4: 不透明,不闪烁 */
											        /*若为其它值,表示该功能未实现*/
        }

        public const int CAP_NORMAL_H264 = 0x01; 	/*普通型H264*/
        public const int CAP_ADVANCED_H264 = 0x02; 	/*增强型H264*/

        public const int CAP_RESOLUTION_QCIF = 0x01; 	/*QCIF*/
        public const int CAP_RESOLUTION_CIF = 0x02; 	/*CIF*/
        public const int CAP_RESULUTION_D1 = 0x04; 	/*D1*/
        public const int CAP_RESULUTION_SD = 0x08; 	/*SD*/
        public const int CAP_RESULUTION_4CIF = 0x10; 	/*4CIF*/
        public const int CAP_RESULUTION_MD = 0x20; 	/*MD*/

        /*码流格式信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_COMPRESSCAP{
     	        public byte byStreamFormatCap; 			/* 支持的码流格式,所有支持的格式相或的结果(只读)*/
     	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_STREAM_TYPES)]
                public byte[] byResolutionCap;/* 每种编码格式下支持的分辨率格式,支持所有的格式相或的结果(只读)*/
		        /*HY_V1_DVR_COMPRESSIONCFG	vCfg;
		        获取主码流支持码流格式例子：
		        if (vCfg.struRecordPara.strCompressCap.byStreamFormatCap & CAP_NORMAL_H264 )
			        //支持普通型H264
		        if (vCfg.struRecordPara.strCompressCap.byStreamFormatCap & CAP_ADVANCED_H264 )
			        //支持增强型H264
		        获取子码流支持码流格式例子：
		        if (vCfg.struNetPara.strCompressCap.byStreamFormatCap & CAP_NORMAL_H264 )
			        //支持普通型H264
		        if (vCfg.struNetPara.strCompressCap.byStreamFormatCap & CAP_ADVANCED_H264 )
			        //支持增强型H264
		        获取主码流当前编码格式下支持的分辨率格式例子：
		        if(vCfg.struRecordPara.strCompressCap.byResolutionCap[vCfg.struRecordPara. byStreamFormat] & CAP_RESOLUTION_CIF)
			        //主码流当前编码格式下支持CIF分辨率压缩
		        获取子码流当前编码格式下支持的分辨率格式例子：
		        if(vCfg.struNetPara.strCompressCap.byResolutionCap[vCfg.struNetPara.byStreamFormat] & CAP_RESOLUTION_CIF)
			        //子码流当前编码格式下支持CIF分辨率压缩
		        */
        }

        /*压缩信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_COMPRESSION_INFO
        {
	        public uint dwVideoFrameRate; 				/* 帧率
                                    		        主码流：0-FULL; 1-1; 2-2; 3-4; 4-6; 5-8; 6-10; 7-12; 8-16; 9-20 
                                    		        子码流：0-1;1-2;2-3;3-4;4-5;5-6;6-7;7-8;8-9;9-10;11-12;12-13;13-14;14-15 */
	        public byte byStreamType; 						/* 码流类型0-视频流,1-复合流；子码流无意义 */ 
	        public byte byResolution;						/* 主码流：分辨率 0 - CIF，1 - D1，2 - SD ；
											        子码流：分辨率  0 - QCIF， 1 - CIF */
	        public ushort byStreamWidth; 					/* 码流宽度(停用)*/
	        public ushort byStreamHeight;					/* 码流高度(停用)*/
	        public byte byStreamFormat; 					/* 码流格式 0-普通型H.264, 1---增强型H.264 */ 
            public byte byBitrateType; 					/* 码率类型0：变码率，1：定码率，2：限码率；子码流无意义*/
	        public uint dwVideoBitrate; 					/* 视频码率 0-16K 1-32K 2-48k 3-64K 4-80K 5-96K 6-128K 7-160k 8-192K 9-224K 10-256K 11-320K 12-384K 13-448K 14-512K 15-640K 16-768K 17-896K 18-1024K 19-1280K 20-1536K 21-1792K 22-2048K。大于100的值表示自定义码流，实际码流值为dwVideoBitrate - 100。最小值16k 最大值8192k */

	        public byte byPicQuality; 						/* 图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差 此信息在变码流时有效；子码流无意义*/
            public HY_V1_DVR_COMPRESSCAP strCompressCap;		/* 压缩能力， 仅在获取参数时有效（只读）*/
        }

        /*压缩配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_COMPRESSIONCFG
        { 
	        public uint dwSize; 							/* 此结构的大小，目前被复用为是否启动子码流 ,该参数为1时表示启用子码流,为0时表示不启用子码流.*/
	        public HY_V1_DVR_COMPRESSION_INFO struRecordPara;	/* 设备本地录像（主码流）*/
	        public HY_V1_DVR_COMPRESSION_INFO struNetPara;	/* 网络视频传输（子码流）*/
        }
        public const int HY_V1_DVR_MAX_RIGHT = 16; 				/*最大权限个数*/
        public const int HY_V1_DVR_MAX_USERNUM = 8;        		/*最大用户个数*/

        /*用户权限信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_USER_INFO{ 
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_NAME_LEN)]
            public byte[] sUserName;		/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_PASSWD_LEN)]
	        public byte[] sPassword;		/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_RIGHT)]
	        public byte[] byLocalRight;	/* 权限 */
											        /* 数组0: 本地控制云台*/ 
											        /* 数组1: 本地手动录象*/ 
											        /* 数组2: 本地回放*/ 
											        /* 数组3: 本地备份*/
											        /* 数组4: 本地系统设置*/ 
											        /* 数组5: 本地高级操作(日志，清除报警，异常处理，重启，升级)*/
											        /* 数组6: 关机*/ 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_STREAM_TYPES)]
	        public byte  byRemoteRight;	/* 权限 */ 
											        /* 数组0: 控制云台*/ 
											        /* 数组1: 手动录象*/ 
											        /* 数组2: 远程回放*/ 
											        /* 数组3: 本地查看状态、日志*/ 
											        /* 数组4: 本地系统设置*/ 			
											        /* 数组5: 本地高级操作(升级，重启，关机， 清除报警， 异常处理)*/
											        /* 数组6: 远程发起语音对讲*/ 
											        /* 数组7: 远程预览*/ 
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string sUserIP;						/* 用户IP地址(为0时表示允许任何地址) */ 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MACADDR_LEN)]
            public byte[] byMACAddr;		/* 物理地址 */ 
        
        }

        /*用户配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_USER{ 
	        public uint dwSize; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_USERNUM,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_USER_INFO[] struUser; 
        }

        /*用户权限信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_USER_INFO_EX{ 
             [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_NAME_LEN)]
	        public byte[] sUserName;		/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =HY_V1_DVR_PASSWD_LEN )]
	        public byte[] sPassword;		/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =HY_V1_DVR_MAX_RIGHT )]
	        public uint[] dwLocalRight;	/* 权限 */
											        /* 数组0: 本地控制云台*/ 
											        /* 数组1: 本地手动录象*/ 
											        /* 数组2: 本地回放*/ 
											        /* 数组3: 本地备份*/
											        /* 数组4: 本地系统设置*/ 
											        /* 数组5: 本地高级操作(日志，清除报警，异常处理，重启，升级)*/
											        /* 数组6: 关机*/ 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =HY_V1_DVR_MAX_RIGHT )]
	        public uint[] dwRemoteRight;	/* 权限 */ 
											        /* 数组0: 控制云台*/ 
											        /* 数组1: 手动录象*/ 
											        /* 数组2: 远程回放*/ 
											        /* 数组3: 本地查看状态、日志*/ 
											        /* 数组4: 本地系统设置*/ 			
											        /* 数组5: 本地高级操作(升级，重启，关机， 清除报警， 异常处理)*/
											        /* 数组6: 远程发起语音对讲*/ 
											        /* 数组7: 远程预览*/ 
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =16 )]
            public string sUserIP;						/* 用户IP地址(为0时表示允许任何地址) */ 
             [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst =HY_V1_DVR_MACADDR_LEN )]
	        public byte[] byMACAddr;		/* 物理地址 */ 
        }

        /*用户配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_USER_EX{ 
	        public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public HY_V1_DVR_USER_INFO_EX[] struUser; 
        }

        public const int  HY_V1_DVR_MAX_HARDDISK_NUM  =  	8 ; 				/*服务器最大硬盘数*/

        /*硬盘状态信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_HARDDISK_STATE
        {
	        public uint dwCapacity;							/* 容量,M为单位(只读)*/
	        public uint dwSpare;								/* 剩余容量(只读)*/
	        public byte  byWriteState;							/* 当前写入状态 0表示无写入，1表示正在写入(只读)*/
        }

        /*通道状态信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_CHANNEL_STATE
        {
	        public uint dwVideoBitrate;						/* 主码流码率(只读)*/
	        public uint dwChildVideoBitrate;					/* 子码流码率(只读)*/
	        public byte  byVideo;								/* 录像状态(只读)*/
	        public byte  bySignal;								/* 信号状态(只读)*/
	        public byte  byUseChildBitrate;					/* 是否启动子码流(只读)*/
        
        }

        /*设备状态配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_DEVICE_STATE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_HARDDISK_NUM, ArraySubType = UnmanagedType.Struct)]
	        public HY_V1_DVR_HARDDISK_STATE[] cHardState;  /*硬盘状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = HY_V1_DVR_MAX_CHANNUM, ArraySubType = UnmanagedType.Struct)]
	        public HY_V1_DVR_CHANNEL_STATE[] cChanState;
												        /*通道状态*/
        }
        public const int  HY_V1_DVR_MAX_DDNS  =    				128 ; 			//最大动态域名长度
        public const int  HY_V1_DVR_MAX_DDNS_USER  =  		64 ; 			//最大动态域名用户长度
        public const int  HY_V1_DVR_MAX_DDNS_PASS =		64 ; 			//最大动态域名密码长度

        /*设备DDNS配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct  HY_V1_DVR_DDNS_CFG
        {
	        public byte bytState;								/*状态,0为不支持,1为启用,2为禁用*/
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_DDNS )]
            public string sDDNS;				/*动态域名地址*/
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_DDNS_USER )]
            public string sUserName;		/*动态域名用户名称*/
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_DDNS_PASS )]
            public string sUserPass;		/*动态域名用户密码*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =3 )]
	        public byte[] bytReserve;							/*保留位*/
        }

        /*设备HTTP配置信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct  HY_V1_DVR_HTTP_CFG 
        {
	        public byte bytState;								/*状态,0为不支持,1为启用,2为禁用*/
	        public byte bytReserve;							/*保留位*/
	        public ushort wHTTPort;								/*HTTP端口*/
        }
        public const int  HY_V1_DVR_RECORDSEG_DAY  =  		24   * 2	;	//每天录像时间段

        /*设备本地录像配置信息,以30分钟为一个时间段单位*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct  HY_V1_DVR_RECORDEX_CFG
        {
	        public byte bytState;								/*状态,0为不支持,1为启用,2为禁用*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =3  )]
	        public byte[] bytReserve;							/*保留位*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_DAYS* HY_V1_DVR_RECORDSEG_DAY )]
	        public byte[] bytRecordTime ;/*录像状态*/
	        /*0:不录像, 1:定时录像, 2:报警录像 3:移动侦测 4:动测|报警*/
	        public uint dwPreRecordTime;						/* 预录时间0-10(单位:秒)*/
            public uint dwDelayTime;							/* 延迟录像时间 0-1800(单位:秒) */
        }

        /*如果需要控制前端摄像头,采用以下结构体*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_CAMERA_CFG
        {
	        public ushort wBLC;									/* 自动亮度控制,0 关闭 1 打开*/
	        public ushort wMIRROR;								/* 镜像		0 关闭 1 打开*/
	        public ushort wAGC;									/* 自动增益控制	0 关闭 1 打开*/
	        public ushort wAE;									/* 自动曝光	0 关闭 1 打开*/
	        public ushort wATW;									/* 自动白平衡	保留*/
	        public ushort wPOSNEG;								/* 负片		保留*/
	        public ushort wAESHUT;								/* 电子快门	保留*/
	        public ushort wReserve;								/* 保留*/
        }
        //点阵串行数组信息长度 24 * 24(24点阵双字) * 10 (最大10个双字)
        public const int  LATTICE_ARRAY_LEN  =  		24  * 24 * 10; 

        /*通道名点阵信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_CHNAME_LATTICE_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =LATTICE_ARRAY_LEN  )]
	        public byte[] bytLattice;			//点阵信息
        }

        //字体名长度
        public const int  FONT_NAME_LEN  =  	32 ; 

        /*字体信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_FONT_INFO
        {
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =FONT_NAME_LEN )]
            public string cFontName;				//字体名
	        public byte bytFontSize;							//字体大小
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =7  )]
	        public byte[] bytReserve ;							//保留
        }

        //点阵通道名最大长度,含结束符
        public const int  LATTICE_CHANNEL_NAME  =  	20   + 1;

        /*点阵通道名配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_LATTICE_CHNAME_CFG
        {
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =LATTICE_CHANNEL_NAME )]
            public string strName;			//通道名称(字符串)
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =3  )]
	        public byte[] bytReserve;							//保留
	        public ushort wWidth;								//通道名称完整字符串宽度
	        public ushort wHeight;								//通道名称完整字符串高度
	        public HY_V1_DVR_FONT_INFO	cFontInfo;				//通道名字体信息
        }
        /*设置点阵通道名配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_SET_LATTICE_CHNAME_CFG
        {
	        public HY_V1_DVR_LATTICE_CHNAME_CFG		cChannelNameCfg;//通道名配置
	        public HY_V1_DVR_CHNAME_LATTICE_INFO	cLatticeChNameInfo;	//通道名点阵信息
        }
        /*获取点阵通道名配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_GET_LATTICE_CHNAME_CFG
        {
	       public HY_V1_DVR_LATTICE_CHNAME_CFG		cChannelNameCfg;	//通道名配置
        }
        //服务器端DDNS支持最大个数
        public const int  MAX_DDNS_CAPABILITY  =  		40 ; 
        //服务器端DDNS信息最大长度(含结束符)
        public const int  MAX_DDNS_ADDR_LEN  =  			64 ; 

        /*服务器端DDNS支持结构信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_DDNS_CAPABILITY
        {
	        public byte bytValidDNSNum;						//有效DNS个数
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
	        public byte[] bytReserve ;							//保留
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_DDNS_CAPABILITY*MAX_DDNS_ADDR_LEN )]
            public string strDDNS;//DDNS名称(字符串)
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct   HY_V1_DVR_ALARMINCFG_EX
        {
	        public uint dwSize;
	        public HY_V1_DVR_HANDLEEXCEPTION struAlarmHandleType;					/* 处理方式 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_DAYS*HY_V1_DVR_RECORDSEG_DAY )]
	        public byte[] bytProcessTime;	/*布防时间表,其中1为布防时间，0为撤防时间*/
	
	        public byte byAlarmType;											/*报警器类型,0：常开,1：常闭 */
	        public byte byAlarmInHandle;										/* 是否处理 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
            public byte[] byRelRecordChan;					/*报警触发的录象通道,为1表示触发该通道 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
           public byte[] byEnablePreset;					/* 是否调用预置点 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
           public byte[] byPresetNo;						/* 调用的云台预置点序号,一个报警输入可以调        用多个通道的云台预置点, 0xff表示不调用预置点。 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
           public byte[] byEnableCruise;					/* 是否调用巡航 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
           public byte[] byCruiseNo;						/* 巡航 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
           public byte[] byEnablePtzTrack;					/* 是否调用轨迹 */
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =HY_V1_DVR_MAX_CHANNUM )]
           public byte[] byPTZTrack;						/* 调用的云台的轨迹序号 */
        }

        //最大移动服务数
        public const int  MAX_MOBILE_SERVICE_NUMBER  =  			16 ; 
        //移动服务名最大长度
        public const int  MAX_MOBILE_SERVICE_NAME_LEN  =  			64 ; 

        /*移动设备服务信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_MOBILE_SERVICE_INFO
        {
	        public uint	dwStatus;											//服务状态,	0为不支持,1为启用,2为禁用
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_MOBILE_SERVICE_NAME_LEN )]
            public string	cServiceName;			//服务名
	        public uint	dwServicePort;										//服务端口
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
	        public uint[]	dwReserve;										//保留
        }

        /*移动设备服务*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_MOBILE_SERVICE
        {
	        public uint	dwServiceNumber;									//有效服务数
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=MAX_MOBILE_SERVICE_NUMBER,ArraySubType= UnmanagedType.Struct)]
	        public HY_V1_DVR_MOBILE_SERVICE_INFO[]	cService;//服务信息
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
	        public uint[]	dwReserve ;										//保留
        }

        //Email最大用户名长度
        public const int  MAX_EMAIL_USERNAME_LEN  =  				32 ; 
        //Email最大密码长度
        public const int  MAX_EMAIL_PASSWORD_LEN  =  				32 ; 
        //Email服务器地址最大长度
        public const int  MAX_EMAIL_SERVER_ADDR_LEN  =  			64 ; 
        //Email最大邮件地址长度
        public const int  MAX_EMAIL_ADDRESS_LEN  =  				64 ; 
        //Email最大收件人个数
        public const int  MAX_EMAIL_ADDRESSEE_NUMBER  =  			16 ; 

        /*Email服务器信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_EMAIL_SERVER_INFO
        {
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_EMAIL_SERVER_ADDR_LEN )]
            public string	cServerAddress;			//服务器地址
	        public uint	dwServerPort;										//服务器端口
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
	        public uint[]	dwReserve;										//保留
        }

        /*Email发件人信息*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_EMAIL_ADDRESSER_INFO
        {
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_EMAIL_USERNAME_LEN )]
            public string	cUserName;					//用户名称
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_EMAIL_PASSWORD_LEN )]
            public string	cPassWord;					//用户密码
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_EMAIL_ADDRESS_LEN )]
            public string	cAddresser;					//发件人地址
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
	        public uint[]	dwReserve ;										//保留
        }
        /*Email收件人*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct HY_V1_DVR_EMAIL_ADDRESSEE
        {
	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_EMAIL_ADDRESS_LEN )]
            public string	cAddressee;					//收件人地址
             [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
	        public uint[]	dwReserve;										//保留
        }

        /*Email收件人信息*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_EMAIL_ADDRESSEE_INFO
        {
	        public uint	dwNumber;											//收件人个数
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=MAX_EMAIL_ADDRESSEE_NUMBER,ArraySubType= UnmanagedType.Struct)]
	        public HY_V1_DVR_EMAIL_ADDRESSEE[]	cAddressee;//收件人
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
            public uint[]	dwReserve;										//保留
        }

        /*Email通知*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_EMAIL_NOTIFY_CFG
        {
	        public uint	dwEnable;											//Email通知,	0为不支持,1为启用,2为禁用
	        public HY_V1_DVR_EMAIL_SERVER_INFO	cServerInfo;					//服务器信息
	        public HY_V1_DVR_EMAIL_ADDRESSER_INFO	cAddresserInfo;					//发件人信息
	        public HY_V1_DVR_EMAIL_ADDRESSEE_INFO	cAddresseeInfo;					//收件人信息
        }

        /*控制通道是否被锁定*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_CTRL_CAMERA_NODE_CFG 
        {
	        public uint dwLockCamera;											// 当前通道是否被锁定，0，未锁定，1，被锁定
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=8,ArraySubType= UnmanagedType.U4)]
            public uint[] dwReserved ;										// 保留，以防以后扩展
        }

        /*通道控制状态*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_CTRL_CAMERA_CFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=HY_V1_DVR_MAX_CHANNUM,ArraySubType= UnmanagedType.Struct)]
	       public HY_V1_DVR_CTRL_CAMERA_NODE_CFG[] sControl;	// 最大16个通道
	       [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=4,ArraySubType= UnmanagedType.U4)]
            public uint[] dwReserved;
        }

        //服务器地址最大长度
        public const int  MAX_SERVER_ADDR_LEN  =  				64 ; 

        /*Higheasy专用DDNS服务*/
        [StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct  HY_V1_DVR_SPECIAL_DDNS_CFG
        {
	        public uint dwEnable;												// 当前是否启用DDNS服务, 0为禁用,1为启用

	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_SERVER_ADDR_LEN )]
            public string acServer;							// DDNS服务器的IP地址或域名,默认一般为higheasy.cn
	        public uint dwPort;												// DDNS服务器的端口,默认一般为80
	        public uint dwInterval;											// 更新的时间，大于等于5秒

	        [MarshalAsAttribute(UnmanagedType.ByValTStr,SizeConst =MAX_SERVER_ADDR_LEN )]
            public string acDomainName;						// 当前的域名，更新成功后可以获取到当前设备的域名(只读)
            [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=16,ArraySubType= UnmanagedType.U4)]
	        public uint[] acResvered;										// 保留
        }

        /*编码模式配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_ENCODER_MODE_CFG
        {
	        public uint	dwEnable;											//是否启用,	0为不支持,1为启用,2为禁用
	        public uint 	dwCodecResouse;									//资源分配模式
	        // 4路:0-默认4路CIF; 1- 4HD1; 2- 1D1 + 3CIF; 3- 4D1
	        // 8路:0-默认8路CIF; 1- 1D1 + 7CIF; 2- 2HD1 + 6CIF; 3- 8HD; 4-8D1;
	        public uint 	dwCodecResouseMask;									//通道掩码
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
            public uint[]	dwReserve;										//保留
        }

        /*联动通道放大显示配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_CH_ENLARGE_CFG
        {
	        public uint	dwEnable;											//是否启用,	0为不支持,1为启用,2为禁用
	        public uint	dwChannel;										//联动通道,按位移
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
            public uint[]	dwReserve;										//保留
        }

        /*报警联动通道放大显示配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_V1_DVR_ALARM_CH_ENLARGE_CFG
        {
	        public uint	dwEnable;											//是否启用,	0为不支持,1为启用,2为禁用
	        public uint	dwInterval;										//显示间隔时间
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=HY_V1_DVR_MAX_ALARMIN,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_CH_ENLARGE_CFG[]	cAlarmIn;				//报警输入
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=HY_V1_DVR_MAX_CHANNUM,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_CH_ENLARGE_CFG[]	cMotion;				//移动侦测
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=HY_V1_DVR_MAX_CHANNUM,ArraySubType=UnmanagedType.Struct)]
            public HY_V1_DVR_CH_ENLARGE_CFG[]	cVideoLost;			//视频丢失
	        [MarshalAsAttribute(UnmanagedType.ByValArray,SizeConst=3,ArraySubType= UnmanagedType.U4)]
            public uint[]	dwReserve;										//保留
        }

        /*配置结构长度*/
        public readonly int HY_V1_DVR_NET_CFG_LEN				= Marshal.SizeOf(new  HY_V1_DVR_NETCFG());
        public readonly int HY_V1_DVR_PIC_CFG_LEN				= Marshal.SizeOf(new  HY_V1_DVR_PICCFG());
        public readonly int HY_V1_DVR_COMPRESS_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_COMPRESSION_INFO());
        public readonly int HY_V1_DVR_COMPRESSCFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_COMPRESSIONCFG());
        public readonly int HY_V1_DVR_DECODER_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_DECODERCFG());
        public readonly int HY_V1_DVR_ALARMIN_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_ALARMINCFG());
        public readonly int HY_V1_DVR_ALARMOUT_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_ALARMOUTCFG());
        public readonly int HY_V1_DVR_RECORD_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_RECORDCFG());
        public readonly int HY_V1_DVR_USER_CFG_LEN				= Marshal.SizeOf(new  HY_V1_DVR_USER());
        public readonly int HY_V1_DVR_DEVICESTATE_CFG_LEN		= Marshal.SizeOf(new  HY_V1_DVR_DEVICE_STATE());
        public readonly int HY_V1_DVR_RECORDCFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_RECORDCFG());
        public readonly int HY_V1_DVR_EXCEPTION_CFG_LEN		= Marshal.SizeOf(new  HY_V1_DVR_EXCEPTION());
        public readonly int HY_V1_DVR_DEVICEINFO_CFG_LEN		= Marshal.SizeOf(new  HY_V1_DVR_DEVICECFG());
        public readonly int HY_V1_DVR_DDNS_CFG_LEN				= Marshal.SizeOf(new  HY_V1_DVR_DDNS_CFG());
        public readonly int HY_V1_DVR_HTTP_CFG_LEN				= Marshal.SizeOf(new  HY_V1_DVR_HTTP_CFG());
        public readonly int HY_V1_DVR_RECORDEX_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_RECORDEX_CFG());
        public readonly int HY_V1_DVR_CAMERA_CFG_LEN			= Marshal.SizeOf(new  HY_V1_DVR_CAMERA_CFG());
        public readonly int ALARMIN_CFG_EX_LEN				= Marshal.SizeOf(new  HY_V1_DVR_ALARMINCFG_EX());

        /*命令字*/
        public const int  HY_V1_DVR_GET_NETCFG  =   				102 ;  	/*获取网络参数 */
        public const int  HY_V1_DVR_SET_NETCFG  =   				103 ;  	/*设置网络参数*/
        public const int  HY_V1_DVR_GET_PICCFG  =   				104 ;  	/*获取图象参数 */
        public const int  HY_V1_DVR_SET_PICCFG  =   				105 ;  	/*设置图象参数 */
        public const int  HY_V1_DVR_GET_COMPRESSCFG  =   			106 ;  	/*获取压缩参数 */
        public const int  HY_V1_DVR_SET_COMPRESSCFG  =   			107 ;  	/*设置压缩参数*/
        public const int  HY_V1_DVR_GET_RECORDCFG  =   			108 ;  	/*获取本地录像参数 */
        public const int  HY_V1_DVR_SET_RECORDCFG  =   			109 ;  	/*设置本地录像参数*/
        public const int  HY_V1_DVR_GET_ALARMINCFG  =   			110 ; 		/*获取报警输入参数*/
        public const int  HY_V1_DVR_SET_ALARMINCFG  =  			111 ; 		/*设置报警输入参数*/
        public const int  HY_V1_DVR_GET_ALARMOUTCFG  =   			112 ; 		/*获取报警输出参数*/
        public const int  HY_V1_DVR_SET_ALARMOUTCFG  =  			113 ; 		/*设置报警输出参数*/
        public const int  HY_V1_DVR_GET_DECODERCFG  =  			114 ; 		/*获取解码器参数*/
        public const int  HY_V1_DVR_SET_DECODERCFG  =  			115 ; 		/*设置解码器参数*/
        public const int  HY_V1_DVR_GET_DEVICECFG  =  			116 ; 		/*获取设备参数*/
        public const int  HY_V1_DVR_SET_DEVICECFG  =  			117 ; 		/*设置设备参数*/
        public const int  HY_V1_DVR_CONTROL_PTZ  =           		118 ; 		/*云台控制*/
        public const int  HY_V1_DVR_CONTROL_RECORD  =      		119 ; 		/*本地录像控制*/
        public const int  HY_V1_DVR_CONTROL_REBOOT  =      		120 ; 		/*远程重启*/
        public const int  HY_V1_DVR_GET_TIMECFG  =  				121 ; 		/*获取设备系统时间*/
        public const int  HY_V1_DVR_SET_TIMECFG  =  				122 ; 		/*设置设备系统时间*/
        public const int  HY_V1_DVR_CONTROL_SAVEIMG  =  			123 ; 		/*Remote SaveImg*/
        public const int  HY_V1_DVR_CONTROL_GETUSER  =  124 ; 		/*获得用户名*/
        public const int  HY_V1_DVR_CONTROL_SETUSER  =   		125 ; 		/*设置用户名*/
        public const int  HY_V1_DVR_GET_EXCEPTIONCFG  =  			126 ;      /*获取异常配置参数*/
        public const int  HY_V1_DVR_SET_EXCEPTIONCFG  =  			127 ;      /*设置异常配置参数*/
        public const int  HY_V1_DVR_GET_PREVIEWCFG  =  			128 ;      /*获取预览*/
        public const int  HY_V1_DVR_SET_PREVIEWCFG  =  			129 ;      /*设置预览*/
        public const int  HY_V1_DVR_GET_DEVICESTATE  =  			130 ; 		/*获取设备状态*/
        public const int  HY_V1_DVR_SET_DEFAULTCFG  =  			131 ; 		/*恢复默认配置*/
        public const int  HY_V1_DVR_GET_RECORDSTATE  =  			132 ; 		/*获取设备录像状态*/
        public const int  HY_V1_DVR_GET_ALARMINSTATE  =  			133 ;      /*获取设备IO输入状态*/
        public const int  HY_V1_DVR_GET_ALARMOUTSTATE  =  		134 ;      /*获取设备IO输出状态*/
        public const int  HY_V1_DVR_SET_ALARMOUTSTATE  =  		135 ;      /*设置设备IO输出状态*/
        public const int  HY_V1_DVR_MANUAL_RECORD  =  			136 ;      /*设备手动录像*/
        public const int  HY_V1_DVR_ADVANCED_PTZ_CONTROL  =  		137 ;      /*高级PTZ云台控制功能*/
        public const int  HY_V1_DVR_GET_DDNSCFG  =  				138 ; 		/*获取动态域名参数*/
        public const int  HY_V1_DVR_SET_DDNSCFG  =  				139 ; 		/*设置动态域名参数*/
        public const int  HY_V1_DVR_GET_HTTPCFG  =  				140 ; 		/*获取HTTP配置信息*/
        public const int  HY_V1_DVR_SET_HTTPCFG  =  				141 ; 		/*设置HTTP配置信息*/
        public const int  HY_V1_DVR_GET_RECORDEXCFG  =   			142 ;  	/*获取本地录像扩展参数*/
        public const int  HY_V1_DVR_SET_RECORDEXCFG  =   			143 ;  	/*设置本地录像扩展参数*/

        public const int  HY_V1_DVR_GET_CAMERACFG  =  			144 ; 		/*获取摄像头参数*/
        public const int  HY_V1_DVR_SET_CAMERACFG  =  			145 ; 		/*设置摄像头参数*/      

        public const int  HY_V1_DVR_GET_LATTICE_CHANNEL_NAME  =  	146 ; 		/*获取点阵通道名参数*/
        public const int  HY_V1_DVR_SET_LATTICE_CHANNEL_NAME  =  	147 ; 		/*设置点阵通道名参数*/
        public const int  HY_V1_DVR_GET_DDNS_CAPABILITY  =  		148 ; 		/*获取服务器支持DDNS能力信息*/

        public const int  HY_V1_DVR_GET_ALARMINEX_CFG  =  		149 ; 		/*获取报警输入的配置(包括新定义的布防时间表)*/
        public const int  HY_V1_DVR_SET_ALARMINEX_CFG  =  		150 ; 		/*设置报警输入的配置(包括新定义的布防时间表)*/

        public const int  HY_V1_DVR_GET_MOBILE_SERVICE_CFG  =  	151 ; 		/*获取移动设备服务配置*/
        public const int  HY_V1_DVR_SET_MOBILE_SERVICE_CFG  =  	152 ; 		/*设置移动设备服务配置*/

        public const int  HY_V1_DVR_GET_EMAIL_NOTIFY_CFG  =  		153 ; 		/*获取Email通知配置*/
        public const int  HY_V1_DVR_SET_EMAIL_NOTIFY_CFG  =  		154 ; 		/*设置Email通知配置*/

        public const int  HY_V1_DVR_DISK_FORMAT  =  				155 ; 		/*远程格式化硬盘*/

        public const int  HY_V1_DVR_SET_CTRL_CMAERA_CFG  =  		156 ; 		/*设置当前CAMERA控制情况*/
        public const int  HY_V1_DVR_GET_CTRL_CMAERA_CFG  =  		157 ; 		/*获取当前CAMERA控制情况*/

        public const int  HY_V1_DVR_SET_SPECIAL_DDNS_CFG  =  		158 ; 		/*设置当前Higheasy专用DDNS配置*/
        public const int  HY_V1_DVR_GET_SPECIAL_DDNS_CFG  =  		159 ; 		/*获取当前Higheasy专用DDNS配置*/

        public const int  HY_V1_DVR_GET_ENCODER_MODE_CFG  =  		160 ; 		/*获取编码模式配置*/
        public const int  HY_V1_DVR_SET_ENCODER_MODE_CFG  =  		161 ; 		/*设置编码模式配置*/

        public const int  HY_V1_DVR_GET_ALARM_CH_ENLARGE_CFG  =  	162 ; 		/*获取报警联动通道放大配置*/
        public const int  HY_V1_DVR_SET_ALARM_CH_ENLARGE_CFG  =  	163 ; 		/*设置报警联动通道放大配置*/

        public const int  HY_V1_DVR_CONTROL_GETUSER_EX		=164 ; 		/*获得用户扩展配置*/
        public const int  HY_V1_DVR_CONTROL_SETUSER_EX	=	165 ; 		/*设置用户扩展配置*/

    }
}

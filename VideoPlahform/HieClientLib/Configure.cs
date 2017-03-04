using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HieCiULib
{
    public class Configure
    {
        /**************************************************************************
        *                           板型定义                                     *
        ************************************************************************* */

        //1500系列
        /** @brief 4路 CIF嵌入式											                    			 */
        public const int DVR_1500_4CIF = 1;

        /** @brief 8路 CIF嵌入式											                    			 */
        public const int DVR_1500_8CIF = 2;

        /** @brief 12路 CIF嵌入式											                    			 */
        public const int DVR_1500_12CIF = 3;

        /** @brief 16路 CIF嵌入式											                    			 */
        public const int DVR_1500_16CIF = 4;

        /** @brief 4路 D1嵌入式															          			 */
        public const int DVR_1500_4D1 = 5;

        /** @brief 8路 D1嵌入式															         			 */
        public const int DVR_1500_8D1 = 6;


        //8550系列
        /** @brief 4路 CIF嵌入式											                    			 */
        public const int DVR_8550_4CIF = 7;

        //1700系列
        /** @brief 8路 1700 CIF嵌入式											                    		 */
        public const int DVR_1700_8CIF = 8;

        /** @brief 16路 1700 CIF嵌入式											                    		 */
        public const int DVR_1700_16CIF = 9;

        /** @brief 4路 1700 D1嵌入式											                    		 */
        public const int DVR_1700_4D1 = 10;

        /** @brief 8路 1700 D1嵌入式											                    		 */
        public const int DVR_1700_8D1 = 11;

        /** @brief 16路 1700 D1F嵌入式											                    		 */
        public const int DVR_1700_16D1 = 12;


        /**************************************************************************
        *                           常量定义                                     *
        ************************************************************************* */

        /** @brief 最大视屏输入数											                    		 */
        public const int DVR_MAX_VIDEOIN_NUM = 16;

        /** @brief 最大PTZ数											                    			 */
        public const int DVR_MAX_PTZ_NUM = DVR_MAX_VIDEOIN_NUM;

        /** @brief 最大视频解码器个数										                    		 */
        public const int DVR_MAX_VIDEOENC_NUM = DVR_MAX_VIDEOIN_NUM;

        /** @brief 最大报警输入数											                    		 */
        public const int DVR_MAX_ALARMIN_NUM = 16;

        /** @brief 最大报警输出数											                    		 */
        public const int DVR_MAX_ALARMOUT_NUM = 8;

        /** @brief 最大异常个数											                    			 */
        public const int DVR_MAX_EXCEPTION_NUM = 8;

        /** @brief 最大视屏输出个数											                    		 */
        public const int DVR_MAX_VIDEOOUT_NUM = 8;

        /** @brief 一星期天数															          		 */
        public const int DVR_MAX_DAYS = 7;

        /** @brief 一天最大时间段（每5分钟一个时间段）						                    		 */
        public const int DVR_MAX_TIMESEGMENT = 24 * 60 / 5;

        /** @brief 最大IP地址长度														          		 */
        public const int DVR_MAX_IP_STR_LEN = 16;

        /** @brief 最大MAC地址长度														          		 */
        public const int DVR_MAX_MAC_STR_LEN = 18;

        /** @brief MAC地址数字个数															          	 */
        public const int DVR_MAX_MAC_NUM_LEN = 6;

        /** @brief 最大支持DDNS协议个数													          		 */
        public const int DVR_MAX_DDNSPRO_NUM = 32;

        /** @brief 最大DDNS协议名称长度													          		 */
        public const int DVR_MAX_DDNSPRONAME_LEN = 128;

        /** @brief 最大域名长度															          		 */
        public const int DVR_MAX_DOMAINNAME_LEN = 256;

        /** @brief 最大Email长度														          		 */
        public const int DVR_MAX_EMAIL_LEN = 256;

        /** @brief 设备最大用户数														          		 */
        public const int DVR_MAX_USER_NUM = 16;

        /** @brief 最大用户名长度														          		 */
        public const int DVR_MAX_USERNAME_LEN = 64;

        /** @brief 最大密码长度															          		 */
        public const int DVR_MAX_PASSWORD_LEN = 64;

        /** @brief 最大权限个数															          		 */
        public const int DVR_MAX_RIGHT_NUM = 32;

        /** @brief 最大预置点个数															       		 */
        public const int DVR_MAX_PRESET_NUM = 128;

        /** @brief 移动侦测区域个数														          		 */
        public const int DVR_MAX_MOTIONSCOPE_NUM = 32;

        /** @brief 最大色度单元个数											          		 */
        public const int DVR_MAX_PICSEC_NUM = 24;

        /** @brief 最大马赛克区域											          		 */
        public const int DVR_MAX_VIDEOSHELTER_NUM = 8;

        /** @brief 字体名长度												          		 */
        public const int DVR_MAX_FONTNAME_LEN = 32;

        /** @brief 点阵通道名最大长度,含结束符								          		 */
        public const int DVR_MAX_CHANNELNAME_LEN = 20 + 1;

        /** @brief PTZ协议名称长度															 */
        public const int DVR_MAX_PTZPRONAME_LEN = 48;

        /** @brief PTZ协议个数=安装和未安装各256;											 */
        public const int DVR_MAX_PTZPRO_NUM = 512;

        /** @brief 点阵宽度															    	 */
        public const int DVR_MAX_LATTICE_WIDTH = 24 * 10;

        /** @brief 点阵高度													          		 */
        public const int DVR_MAX_LATTICE_HEIGHT = 24;

        /** @brief 点阵总体大小												          		 */
        public const int DVR_MAX_LATTICE_BUFSIZE = 24 * 24 * 10 / 8;

        /** @brief 设备节点名长度											          		 */
        public const int DVR_MAX_PARTITION_DEVNODE_LEN = 16;

        /** @brief 单个硬盘中最大分区个数													 */
        public const int DVR_MAX_PARTITION_IN_HARDDISK = 15;

        /** @brief 最大设备节点个数											          		 */
        public const int DVR_MAX_PARTITION_IN_DISKGROUP = 32;

        /** @brief 最大硬盘个数												          		 */
        public const int DVR_MAX_HARDDISK_NUM = 8;

        /** @brief 最大硬盘组个数											          		 */
        public const int DVR_MAX_DISKGROUP_NUM = 8;

        /** @brief 最大支持分辨率个数										          		 */
        public const int DVR_MAX_VGARESOLVING_NUM = 16;

        /** @brief 设备版本名称长度												          	 */
        public const int DVR_MAX_VERSION_LEN = 32;

        /** @brief Hxht普通字符串长度																*/
        public const int DVR_COMMON_STRING_LEN = 32;

        /**************************************************************************
        *                             配置信息主、次类型定义                      *
        ************************************************************************* */

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取网络参数												          	 */
        public const int HY_DVR_GET_NETCFG = 101;

        /** @brief 设置网络参数												          	 */
        public const int HY_DVR_SET_NETCFG = 102;

        //次类型
        /** @brief 全部网络配置												          	 */
        public const uint NETCFG_ALL = 0xFFFFFFFF;

        /** @brief Dhcp配置信息												          	 */
        public const int NETCFG_DHCP_CONF = 1;

        /** @brief Dhcp状态信息												          	 */
        public const int NETCFG_DHCP_STATE = 2;

        /** @brief Eth网络信息												          	 */
        public const int NETCFG_ETH_IF = 4;

        /** @brief PPPoE配置												          	 */
        public const int NETCFG_PPPOE_CONF = 8;

        /** @brief PPPoE网络信息											          	 */
        public const int NETCFG_PPPOE_IF = 16;

        /** @brief DNS配置信息												          	 */
        public const int NETCFG_DNS_CONF = 32;

        /** @brief DDNS配置信息												          	 */
        public const int NETCFG_DDNS_CONF = 64;

        /** @brief http配置													          	 */
        public const int NETCFG_HTTP_CONF = 128;

        /** @brief 服务端口配置信息												       	 */
        public const int NETCFG_LISTENPORT_CONF = 256;

        /** @brief 手机端口配置信息												       	 */
        public const int NETCFG_MOBILE_CONF = 512;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取服务器配置参数												   	 */
        public const int HY_DVR_GET_NETSERVERCFG = 103;

        /** @brief 设置服务器配置参数												     */
        public const int HY_DVR_SET_NETSERVERCFG = 104;

        //次类型
        /** @brief 全部网络配置														     */
        public const uint NETSERVERCFG_ALL = 0xFFFFFFFF;

        /** @brief CMS配置信息														 	 */
        public const int NETSERVERCFG_CMS_CONF = 1;

        /** @brief CMS状态信息													    	 */
        public const int NETSERVERCFG_CMS_STATE = 2;

        /** @brief AMS配置信息														   	 */
        public const int NETSERVERCFG_AMS_CONF = 4;

        /** @brief NTP配置信息														  	 */
        public const int NETSERVERCFG_NTP_CONF = 8;

        /** @brief EML配置信息														 	 */
        public const int NETSERVERCFG_EML_CONF = 16;

        /** @brief EML=1.1.0版本;配置信息											 	 */
        public const int NETSERVERCFG_EML_1_CONF = 32;

        /** @brief CMS协议信息														 	 */
        public const int NETSERVERCFG_CMS_PROTOCOL = 64;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取图像配置参数													  	 */
        public const int HY_DVR_GET_PICCFG = 105;

        /** @brief 设置图像配置参数													  	 */
        public const int HY_DVR_SET_PICCFG = 106;

        //次类型
        /** @brief 全部网络配置														 	 */
        public const uint PICCFG_ALL = 0xFFFFFFFF;

        /** @brief 视频制式配置信息													 	 */
        public const int PICCFG_VIDEOSTANDARD_CONF = 1;

        /** @brief OSD信息															   	 */
        public const int PICCFG_OSD_CONF = 2;

        /** @brief 通道别名信息															   	 */
        public const int PICCFG_CHNAME_CONF = 4;

        /** @brief 时间段信息														   	 */
        public const int PICCFG_TIMESEC_CONF = 8;

        /** @brief 视频丢失配置信息													   	 */
        public const int PICCFG_VIDEOLOST_CONF = 16;

        /** @brief 移动侦测配置信息													   	 */
        public const int PICCFG_MOTION_CONF = 32;

        /** @brief 马赛克配置信息													   	 */
        public const int PICCFG_MOSAIC_CONF = 64;

        /** @brief 通道别名信息（Unicode）											   	 */
        public const int PICCFG_CHNAME_UNICODE_CONF = 128;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取编码配置参数													  	 */
        public const int HY_DVR_GET_COMPRESSCFG = 107;

        /** @brief 设置编码配置参数													  	 */
        public const int HY_DVR_SET_COMPRESSCFG = 108;

        //次类型
        /** @brief 全部网络配置														  	 */
        public const uint COMPRESSCFG_ALL = 0xFFFFFFFF;

        /** @brief 工作模式配置信息													  	 */
        public const int COMPRESSCFG_WORKMODE_CONF = 1;

        /** @brief 编码能力信息														 	 */
        public const int COMPRESSCFG_COMPRESS_CAP = 2;

        /** @brief 编码配置信息															 */
        public const int COMPRESSCFG_COMPRESS_CONF = 4;



        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取本地录像参数														 */
        public const int HY_DVR_GET_RECORDCFG = 109;

        /** @brief 设置本地录像参数														 */
        public const int HY_DVR_SET_RECORDCFG = 110;

        //次类型
        /** @brief 全部录像配置															 */
        public const uint RECORDCFG_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取系统时间参数														 */
        public const int HY_DVR_GET_SYSTIME = 111;

        /** @brief 设置系统时间参数														 */
        public const int HY_DVR_SET_SYSTIME = 112;

        //次类型
        /** @brief 系统时间配置															 */
        public const uint SYSTIME_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取云台参数															 */
        public const int HY_DVR_GET_PTZCFG = 113;

        /** @brief 设置云台参数															 */
        public const int HY_DVR_SET_PTZCFG = 114;

        //次类型
        /** @brief 云台参数配置															 */
        public const uint PTZCFG_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取串口配置参数														 */
        public const int HY_DVR_GET_SERIALCFG = 115;

        /** @brief 设置串口配置参数														 */
        public const int HY_DVR_SET_SERIALCFG = 116;

        //次类型
        /** @brief 全部串口参数配置														 */
        public const uint SERIALCFG_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取报警配置参数														 */
        public const int HY_DVR_GET_ALARMCFG = 117;

        /** @brief 设置报警配置参数														 */
        public const int HY_DVR_SET_ALARMCFG = 118;

        //次类型
        /** @brief 全部报警参数配置														 */
        public const uint ALARMCFG_ALL = 0xFFFFFFFF;

        /** @brief 报警工作模式															 */
        public const int ALARMCFG_WORKMODE_CONF = 1;

        /** @brief 快照配置																 */
        public const int ALARMCFG_IMGCAPTURE_CONF = 2;

        /** @brief 报警输入配置															 */
        public const int ALARMCFG_ALARMIN_CONF = 4;

        /** @brief 报警输出配置															 */
        public const int ALARMCFG_ALARMOUT_CONF = 8;

        /** @brief 异常处理配置															 */
        public const int ALARMCFG_EXCEPTION_CONF = 16;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取显示输出配置参数													 */
        public const int HY_DVR_GET_VIDEOOUTCFG = 119;

        /** @brief 设置显示输出配置参数													 */
        public const int HY_DVR_SET_VIDEOOUTCFG = 120;

        //次类型
        /** @brief 全部显示输出参数配置													 */
        public const uint VIDEOOUTCFG_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取用户信息参数														 */
        public const int HY_DVR_GET_USERCFG = 121;

        /** @brief 设置用户信息参数														 */
        public const int HY_DVR_SET_USERCFG = 122;

        //次类型
        /** @brief 用户信息参数															 */
        public const uint USERCFG_ALL = 0xFFFFFFFF;

        /** @brief 单个用户信息															 */
        public const int USERCFG_ONE = 1;

        /** @brief 添加用户																 */
        public const int USERCFG_CREATE = 2;

        /** @brief 删除用户																 */
        public const int USERCFG_REMOVE = 4;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取设备信息参数														 */
        public const int HY_DVR_GET_DEVICEINFO = 123;

        /** @brief 设置设备信息参数，只读												 */
        public const int HY_DVR_SET_DEVICEINFO = 124;

        //次类型
        /** @brief 全部设备信息参数														 */
        public const uint DEVICEINFO_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取设备配置参数														 */
        public const int HY_DVR_GET_DEVICECFG = 125;

        /** @brief 设置设备配置参数														 */
        public const int HY_DVR_SET_DEVICECFG = 126;

        //次类型
        /** @brief 全部设备配置参数														 */
        public const uint DEVICECFG_ALL = 0xFFFFFFFF;

        /** @brief 锁屏时间参数															 */
        public const int DEVICECFG_LOCKSCREENTIME_CONF = 1;

        /** @brief 语言																	 */
        public const int DEVICECFG_LANGUAGE_CONF = 2;

        /** @brief 日期时间格式															 */
        public const int DEVICECFG_DATETIME_CONF = 4;

        /** @brief 夏时制																 */
        public const int DEVICECFG_DST_CONF = 8;

        /** @brief 设备ID，用于遥控器													 */
        public const int DEVICECFG_REMOTECONTROL_ID = 16;

        /** @brief 时区																	 */
        public const int DEVICECFG_TIMEZONE_CONF = 32;

        /** @brief 视频制式配置信息														 */
        public const int DEVICECFG_VIDEOSTANDARD_CONF = 64;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取存储系统信息														 */
        public const int HY_DVR_GET_STORAGEINFO = 127;

        /** @brief 设置存储系统信息，只读												 */
        public const int HY_DVR_SET_STORAGEINFO = 128;

        //次类型
        /** @brief 全部设备信息参数														 */
        public const uint STORAGEINFO_ALL = 0xFFFFFFFF;

        /** @brief 存储系统工作模式参数													 */
        public const int STORAGEINFO_WORKMODE_CONF = 1;

        /** @brief 硬盘信息参数															 */
        public const int STORAGEINFO_DISK_INFO = 2;

        /** @brief 磁盘组信息参数														 */
        public const int STORAGEINFO_DISKGROUP_INFO = 4;

        /** @brief 磁盘效验码															 */
        public const int STORAGEINFO_DISK_MARK = 8;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取设备状态信息														 */
        public const int HY_DVR_GET_DEVICESTATE = 129;

        /** @brief 设置设备状态信息，空出不用											 */
        public const int HY_DVR_SET_DEVICESTATE = 130;

        //次类型
        /** @brief 全部设备状态信息														 */
        public const uint DEVICESTATE_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取设备自动维护配置													 */
        public const int HY_DVR_GET_DEVICEMAINTENANCE = 131;

        /** @brief 设置设备自动维护配置													 */
        public const int HY_DVR_SET_DEVICEMAINTENANCE = 132;

        //次类型
        /** @brief 全部设备自动维护配置													 */
        public const uint DEVICEMAINTENANCE_ALL = 0xFFFFFFFF;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取设备功能定制配置													 */
        public const int HY_DVR_GET_DEVICECUSTOM = 133;

        /** @brief 设置设备功能定制配置													 */
        public const int HY_DVR_SET_DEVICECUSTOM = 134;

        //次类型
        /** @brief 全部设备功能定制配置													 */
        public const uint DEVICECUSTOM_ALL = 0xFFFFFFFF;

        /** @brief HC板型编解码资源分配模式信息（暂时不用）								 */
        public const int DEVICECUSTOM_HCRESMODE_CONF = 8;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取HXHT配置															 */
        public const int HY_DVR_GET_HXHT = 135;

        /** @brief 设置HXHT配置															 */
        public const int HY_DVR_SET_HXHT = 136;

        //次类型
        /** @brief 全部HXHT配置															 */
        public const uint HXHT_ALL = 0xFFFFFFFF;

        /** @brief NAT状态配置															 */
        public const int HXHT_NATSTATUS = 1;

        /** @brief 语音呼叫配置															 */
        public const int HXHT_VOICEPARAM = 2;

        /** @brief CMS存储注册配置信息													 */
        public const int HXHT_CMS_STORAGE = 4;

        /** @brief 前端设备ID															 */
        public const int HXHT_DEVICE_ID = 8;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //主类型
        /** @brief 获取要加载的协议名称（私有协议是必须加载的，不在其中）				 */
        public const int HY_DVR_GET_PROTOCOL = 137;

        /** @brief 设置要加载的协议名称													 */
        public const int HY_DVR_SET_PROTOCOL = 138;

        //次类型
        /** @brief 全部要加载的协议名称													 */
        public const uint PROTOCOL_ALL = 0xFFFFFFFF;




        /**************************************************************************
        *                        配置信息结构体定义                              *
        ************************************************************************* */

        ////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_SCHEDTIME
        *  @brief	时间计划, 24小时等分成48个时间段
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_SCHEDTIME
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DAYS * DVR_MAX_TIMESEGMENT)]
            public string bySetType;	/*!< 0为不布防，1为布防 */
            public byte byState;										/*!< 状态,0为全天布防,1为启用,2为不支持 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string byReserve;									/*!< 保留位 */

        }

        /*!
        *  @struct	HY_DVR_WORKMODE
        *  @brief	工作模式
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_WORKMODE
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int nWorkMode;												/*!< 高级模式，0为普通模式，1为高级模式 */
        }

        /*!
        *  @struct	HY_DVR_FONT_INFO
        *  @brief	字体信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_FONT_INFO
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_FONTNAME_LEN)]
            public string szFontName;						/*!< 字体名 */
            public int nFontStyle;												/*!< 字体风格 */
            public int nFontSize;												/*!< 字体大小 */
        }

        /*!
        *  @struct	HY_DVR_ALIAS_CFG
        *  @brief	别名配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_ALIAS_CFG
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_CHANNELNAME_LEN)]
            public string strName;						/*!< 通道名称 */
            public int nCharSet;												/*!< 通道名字符集: 0-GB, 1-unicode */
            public int nWidth;													/*!< 通道名称完整字符串宽度(以像素为单位)，最大宽度 DVR_MAX_LATTICE_WIDTH */
            public int nHeight;												/*!< 通道名称完整字符串高度(以像素为单位)，最大高度 DVR_MAX_LATTICE_HEIGHT */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_LATTICE_BUFSIZE)]
            public string byLattice;			/*!< 点阵，按行扫描，宽为(nWidth+7)/8，高为nHeight，目前最大支持10个24*24的点阵 */
            HY_DVR_FONT_INFO struFontInfo;								/*!< 通道名字体信息 */

        }

        /*!
        *  @struct	HY_DVR_ALIAS_CFG_1
        *  @brief	别名配置(Unicode)
        */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct HY_DVR_ALIAS_CFG_1
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_CHANNELNAME_LEN, ArraySubType = UnmanagedType.U2)]
            public ushort[] strName;			/*!< 通道名称 */
            public int nCharSet;												/*!< 通道名字符集: 0-GB, 1-unicode */
            public int nWidth;													/*!< 通道名称完整字符串宽度(以像素为单位)，最大宽度 DVR_MAX_LATTICE_WIDTH */
            public int nHeight;												/*!< 通道名称完整字符串高度(以像素为单位)，最大高度 DVR_MAX_LATTICE_HEIGHT */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_LATTICE_BUFSIZE)]
            public string byLattice;			/*!< 点阵，按行扫描，宽为(nWidth+7)/8，高为nHeight，目前最大支持10个24*24的点阵 */
            HY_DVR_FONT_INFO struFontInfo;								/*!< 通道名字体信息 */

        }

        ////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_DDNS
        *  @brief	DDNS协议信息结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DDNS
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bDdnsEnable;											/*!< 0-不启用自动注册, 1-启用自动注册  */
            public int nDdnsState;												/*!< 目前DDNS只读，0表示已经停止，1表示正在启动，2表示已经启动，3表示正在停止 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DDNSPRONAME_LEN)]
            public string szProtocolName;				/*!< 协议名称(只读)  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_USERNAME_LEN)]
            public string szUserName;						/*!< 注册用户名  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PASSWORD_LEN)]
            public string szUserPassword;					/*!< 注册用户密码  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szDomainName;					/*!< 注册用户域名  */
            public ushort wRegisterPort;								/*!< 服务端口  */
            public int nAutoRegIntervals;										/*!< 自动注册间隔时间，单位为秒  */

        }

        /*!
        *  @struct	HY_DVR_DDNS_CONF
        *  @brief	DDNS功能配置结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DDNS_CONF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int nDdnsProtocolCount;										/*!< 支持的DDNS协议总数(只读) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_DDNSPRO_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_DDNS[] struDdnsProtocol;			/*!< DDNS协议信息 */

        }

        /*!
        *  @struct	HY_DVR_PPPOE_CONF
        *  @brief	PPPoE功能配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_PPPOE_CONF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bPPPoEEnable;											/*!< 0-不启用PPPoE，1-启用 PPPoE  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_USERNAME_LEN)]
            public string szPPPoEUser;						/*!< PPPoE用户名  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PASSWORD_LEN)]
            public string szPPPoEPassword;					/*!< PPPoE密码  */

        }

        /*!
        *  @struct	HY_DVR_PPPOE_IF
        *  @brief	PPPoE网络信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_PPPOE_IF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int nPPPoEState;											/*!< PPPoE 连接状态(只读),0表示没有连接，1表示正在连接，2表示连接成功，3表示正在挂断 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szPPPoEIP;							/*!< PPPoE IP地址(只读)  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szPPPoESubnetMask;					/*!< PPPoE 子网掩码(只读)  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szPPPoEGateway;					/*!< PPPoE 网关(只读)  */

        }

        /*!
        *  @struct	HY_DVR_DHCP_CONF
        *  @brief	DHCP配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DHCP_CONF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bDhcpEnable;											/*!< 0-不启用Dhcp,1-启用Dhcp  */

        }

        /*!
        *  @struct	HY_DVR_DHCP_STATE
        *  @brief	DHCP状态信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DHCP_STATE
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int nDhcpState;												/*!< Dhcp状态 (只读)，目前不支持 */

        }

        /*!
        *  @struct	HY_DVR_DNS_CONF
        *  @brief	DNS配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DNS_CONF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szPrimaryDNS;						/*!< 主DNS服务器  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szSecondaryDNS;					/*!< 备用DNS  */

        }

        /*!
        *  @struct	HY_DVR_HTTP_CONF
        *  @brief	http配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_HTTP_CONF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bEnable;												/*!< 启用或禁用http服务 */
            public ushort wHttpPort;									/*!< Http端口  */

        }

        /*!
        *  @struct	HY_DVR_LISTENPORT_CONF
        *  @brief	网络监听端口信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_LISTENPORT_CONF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public ushort wLocalCmdPort;								/*!< 本地命令端口  */
            public ushort wLocalMediaPort;								/*!< 本地媒体端口  */
            public ushort wRtpPort;									/*!< 本地RTP端口  */

        }

        /** @brief 网络监听端口结构长度																				 */
        public readonly int LISTENPORT_CONF_LEN = Marshal.SizeOf(new HY_DVR_LISTENPORT_CONF());

        //浪潮手机客户端配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_TIDE_MOBILE
        {
            public int bValid;					//! 本结构体是否有效，=0表示无效

            public ushort wMobilePort;	/*手机端口 */

        }

        //手机监听端口信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_MOBILE_CONF
        {
            public int bValid;					//! 本结构体是否有效，=0表示无效

            public HY_DVR_TIDE_MOBILE struTide;	/*浪潮手机配置 */

        }

        public readonly int MOBILE_CONF_LEN = Marshal.SizeOf(new HY_DVR_MOBILE_CONF());

        /*!
        *  @struct	HY_DVR_ETH_IF
        *  @brief	ETH0配置结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_ETH_IF
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szIPAddress;						/*!< DVR IP地址  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szSubnetMask;						/*!< DVR 子网掩码  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szGateway;							/*!< 网关地址  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_MAC_STR_LEN)]
            public string szMacAddress;						/*!< 只读：服务器的物理地址  */

        }


        ////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_NET_CFG
        *  @brief	本地网络适配器配置结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_NET_CFG
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_DHCP_CONF struDhcp_Conf;								/*!< Dhcp配置信息 */
            public HY_DVR_DHCP_STATE struDhcp_State;							/*!< Dhcp状态信息 */
            public HY_DVR_ETH_IF struEth;										/*!< Eth网络信息 */
            public HY_DVR_PPPOE_CONF struPPPoE_Conf;							/*!< PPPoE配置 */
            public HY_DVR_PPPOE_IF struPPPoE_IF;								/*!< PPPoE网络信息 */
            public HY_DVR_DNS_CONF struDNS;									/*!< DNS配置信息 */
            public HY_DVR_DDNS_CONF struDDNS;									/*!< DDNS配置信息 */
            public HY_DVR_HTTP_CONF struHttp;									/*!< HTTP配置 */
            public HY_DVR_LISTENPORT_CONF struListenPort;						/*!< 服务端口配置信息 */
            public HY_DVR_MOBILE_CONF struMobilePort;							/*!< 手机端口配置信息*/

        }

        /** @brief 本地网络适配器配置结构长度																		 */
        public readonly int NET_CFG_LEN = Marshal.SizeOf(new HY_DVR_NET_CFG());
        ////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_NET_CMS
        *  @brief	中心管理服务器配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_NET_CMS
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bCmsEnable;												/*!< 0-不启用管理服务器, 1-启用  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szManageHostAddress;			/*!< 远程管理主机地址，长度考虑域名  */
            public ushort wManageHostPort;								/*!< 远程管理主机端口  */
            public int nAutoRegIntervals;										/*!< 注册间隔时间，单位为秒  */

        }

        /*!
        *  @struct	HY_DVR_CMS_STATE
        *  @brief	中心管理服务器连接状态
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_CMS_STATE
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int nCmsState;												/*!< CMS注册状态,目前系统不支持  */

        }

        /*!
        *  @struct	HY_DVR_NET_AMS
        *  @brief	报警管理服务器配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_NET_AMS
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bAmsEnable;												/*!< 0-不启用报警服务器, 1-启用  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szAlarmHostAddress;			/*!< 报警服务器 */
            public ushort wAlarmHostPort;								/*!< 报警服务器端口  */

        }

        /*!
        *  @struct	HY_DVR_NET_NTP
        *  @brief	Ntp服务器配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_NET_NTP
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bNtpEnable;												/*!< 0-不启用, 1-启用  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szNtpHostAddress;				/*!< Ntp服务器，长度考虑域名  */
            public ushort wNtpHostPort;								/*!< Ntp服务器，端口  */
            public int nAutoRegIntervals;										/*!< Ntp服务器同步间隔时间，单位为秒  */

        }

        /*!
        *  @struct	HY_DVR_NET_EML
        *  @brief	Email服务器配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_NET_EML
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bEmlEnable;												/*!< 0-不启用, 1-启用  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szSmtpServer;					/*!< smtp服务器地址  */
            public ushort wSmtpPort;									/*!< smtp服务器端口  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_EMAIL_LEN)]
            public string szSenderAddress;					/*!< 发件人邮箱地址  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_USERNAME_LEN)]
            public string szSmtpUserName;					/*!< smtp服务器帐号  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PASSWORD_LEN)]
            public string szSmtpPassword;					/*!< smtp服务器密码  */

        }

        /*!
        *  @struct	HY_DVR_NET_EML_1
        *  @brief	Email服务器配置信息(1.1.0版本使用)
        */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_NET_EML_1
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效		*/
            public int bEmlEnable;												/*!< 0-不启用, 1-启用					*/

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szSmtpServer;					/*!< smtp服务器地址						*/
            public ushort wSmtpPort;									/*!< 端口								*/
            public int nSmtpAuth;												/*!< 验证方式(0为NONE, 1为LOGIN)		*/
            public int nSmtpTLS;												/*!< 是否使用安全登陆(0为off, 1为on)	*/

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_EMAIL_LEN)]
            public string szSenderEmail;						/*!< 发件人邮箱地址						*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PASSWORD_LEN)]
            public string szSenderPassword;				/*!< 发件人邮箱密码						*/

            //以下参数暂不使用
            public int nProtocol;												/*!< (目前为smtp)						*/
            public int nTimeout;												/*!< 超时配置							*/
            public int nAuto_from;

            //以下为TLS相关高级参数，暂不使用
            public int nTls_starttls;
            public int nTls_certcheck;
            public int nTls_force_sslv3;
            public int nTls_min_dh_prime_bits;
            public int nTls_priorities;

        }

        /*!
        *  @struct	HY_DVR_CMS_FOR_PROTOCOL
        *  @brief	CMS对应的协议名称
        */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_CMS_FOR_PROTOCOL
        {
            public int bValid;											//本结构体是否有效，=0表示无效

            public uint dwCMSProtocol;								/*!< 协议名称：在CMS中需要填写IP地址，需要告诉设备端，这个
														        CMS中的IP地址是为哪一个协议准备的，如果是私有协议，那么设备
														        将注册到NVMS中；如果是Hxht协议，则设备注册到Hxht的接入服务器中。
														        该参数只能表示一种协议。*/
        }

        ////////////////////////////////////////////////////////////////////////////
        /*!
        *  @struct	HY_DVR_NET_MANAGER
        *  @brief	网络管理结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_NET_MANAGER
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_NET_CMS struCMS;										/*!< 中心管理服务器信息 */
            public HY_DVR_NET_AMS struAMS;										/*!< 报警服务器信息 */
            public HY_DVR_NET_NTP struNTP;										/*!< NTP服务器信息 */
            public HY_DVR_NET_EML struEML;										/*!< Email服务器信息 */
            public HY_DVR_CMS_FOR_PROTOCOL struCMSForProtocol;					/*!< 中心管理服务器信息对应的协议（和CMS是绑定的）*/
            public HY_DVR_CMS_STATE struCmsState;								/*!< 中心管理服务器连接状态 */
            public HY_DVR_NET_EML_1 struEML_1;									/*!< Email(1.1.0版本)服务器信息 */

        }

        /** @brief 网络管理结构长度																				 */
        public readonly int NET_MANAGER_LEN = Marshal.SizeOf(new HY_DVR_NET_MANAGER());
        ////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_CHROMA_SEC
        *  @brief	色度单元配置结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_CHROMA_SEC
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bTimeSegEnable;											/*!< 启用时间片  */
            public int nStartTime;												/*!< 开始时间 相对00:00时的秒数   */
            public int nStopTime;												/*!< 结束时间 相对00:00时的秒数   */
            public int nBrightness;											/*!< 亮度,0-255  */
            public int nContrast;												/*!< 对比度,0-255  */
            public int nSaturation;											/*!< 饱和度,0-255  */
            public int nHue;													/*!< 色调,0-255  */

        }

        /*!
        *  @struct	HY_DVR_CHROMA_SEC
        *  @brief	色度配置结构(每通道支持24个时间段)
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_CHROMA
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_PICSEC_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_CHROMA_SEC[] struChromaSec;		/*!< 色度单元 */

        }


        /*异常处理方式 */

        /** @brief 无响应																				 */
        public const uint DVR_ALARM_TRIGGER_NONE = 0;

        /** @brief 触发屏幕显示，通道参数由录像参数决定													 */
        public const uint DVR_ALARM_TRIGGER_PREVIEW = 0x1;

        /** @brief 声音警告																				 */
        public const uint DVR_ALARM_TRIGGER_AUDIOOUT = (0x1 << 1);

        /** @brief 触发录像																				 */
        public const uint DVR_ALARM_TRIGGER_RECORD = (0x1 << 2);

        /** @brief 触发拍照																				 */
        public const uint DVR_ALARM_TRIGGER_SNAPSHOT = (0x1 << 3);

        /** @brief 触发报警输出																			 */
        public const uint DVR_ALARM_TRIGGER_ALARMOUT = (0x1 << 4);

        /** @brief 上传报警中心																			 */
        public const uint DVR_ALARM_TRIGGER_NOTIFYAMS = (0x1 << 5);

        /** @brief 触发PTZ																				 */
        public const uint DVR_ALARM_TRIGGER_PTZ = (0x1 << 6);

        /** @brief 发生EMail通知																		 */
        public const uint DVR_ALARM_TRIGGER_EMAIL = (0x1 << 7);

        /** @brief 屏幕提示																				 */
        public const uint DVR_ALARM_TRIGGER_NOTIFYGUI = (0x1 << 8);

        /*!
        *  @struct	HY_DVR_HANDLEEXCEPTION
        *  @brief	联动处理结构体
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_HANDLEEXCEPTION
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效  */

            public uint dwHandleType;									/*!< 处理方式,处理方式的"或"结果  */
            public int nRecordChannelMask;										/*!< 联动录像通道标识，按位表示，若位0为1，表示通道1被选中，以此类推  */
            public int nSnapshotChannelMask;									/*!< 联动快照通道标识，按位表示，若位0为1，表示通道1被选中，以此类推  */
            public int nAlarmOutChannelMask;									/*!< 联动报警输出通道标识，按位表示，若位0为1，表示通道1被选中，以此类推  */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_PTZ_NUM, ArraySubType = UnmanagedType.I4)]
            public int[] nPtzAction;							/*!< 联动的动作,  0表示无动作 , 1联动预置点, 2联动巡航 3联动轨迹  */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_PTZ_NUM, ArraySubType = UnmanagedType.I4)]
            public int[] nPtzIndex;								/*!< 联动的预置点/巡航/轨迹的编号  */
            public int nMonitorMask;											/*!< 0-不在显示器上显示 1-主显 2-SPOT1 4-SPOT2 8-SPOT3 16-SPOT4，若需要多种组合，将这些值相加即可 */

        }

        /*!
        *  @struct	HY_DVR_MOTION
        *  @brief	移动侦测
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_MOTION
        {
            public int bValid;																		/*!< 本结构体是否有效，0表示无效  */

            public int bEnable;																	/*!< 是否处理移动侦测  */
            public int nSenstive;																	/*!< 移动侦测灵敏度, 0 - 5,越低越灵敏,0xff关闭  */
            public int nXScope;																	/*!< X方向宏块范围：0-32（目前9000设备支持0-16）  */
            public int nYScope;																	/*!< Y方向宏块范围：0-32（目前9000设备支持0-12）*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_MOTIONSCOPE_NUM * DVR_MAX_MOTIONSCOPE_NUM)]
            public string byMotionScope;	/*!< 侦测区域,1表示改宏块是移动侦测区域,0表示不是  */
            public HY_DVR_HANDLEEXCEPTION struHandleType;											/*!< 处理方式  */
            public HY_DVR_SCHEDTIME struSchedTime;													/*!< 布防计划  */

        }

        /*!
        *  @struct	HY_DVR_VILOST
        *  @brief	信号丢失报警
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_VILOST
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bEnable;												/*!< 是否处理信号丢失报警  */
            public HY_DVR_HANDLEEXCEPTION struHandleType;						/*!< 处理方式  */
            public HY_DVR_SCHEDTIME struSchedTime;								/*!< 布防计划  */

        }

        /*!
        *  @struct	HY_DVR_MOSAIC_INFO
        *  @brief	马赛克区域信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_MOSAIC_INFO
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bEnable;												/*!< 是否启用  */
            public int nHideAreaTopLeftX;										/*!< 马赛克区域的左上点x相对坐标（0-703）  */
            public int nHideAreaTopLeftY;										/*!< 马赛克区域的左上点y相对坐标（0-575）  */
            public int nHideAreaBottomRightX;									/*!< 马赛克区域的右下点x相对坐标（0-703）  */
            public int nHideAreaBottomRightY;									/*!< 马赛克区域的右下点y相对坐标（0-575）  */

        }

        /*!
        *  @struct	HY_DVR_MOSAIC
        *  @brief	马赛克配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_MOSAIC
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int bEnableMosaic;											/*!< 是否启动遮挡 ,0-否,1-是 区域为704*576  */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOSHELTER_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_MOSAIC_INFO[] struMosaicInfo;/*!< 马赛克区域 */

        }

        /*!
        *  @struct	HY_DVR_OSD_CFG
        *  @brief	OSD配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_OSD_CFG
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public int nShowOsd;												/*!< 0为通道时间均不显示、1为显示通道，2、为显示时间、3为两者均显示  */
            public int nTimeTopLeftX;											/*!< 时间的x坐标（0-703）  */
            public int nTimeTopLeftY;											/*!< 时间的y坐标（0-575）  */
            public int nNameTopLeftX;											/*!< 名称的x坐标（0-703）  */
            public int nNameTopLeftY;											/*!< 名称的y坐标（0-575）  */

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_OSD_CFG
        *  @brief	图像参数结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_PIC_CFG
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_WORKMODE struWorkMode;								/*!< 工作模式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_OSD_CFG[] struOsd;				/*!< OSD配置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALIAS_CFG[] struChName;			/*!< 通道别名配置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_CHROMA[] struChroma;				/*!< 色度配置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_VILOST[] struViLost;				/*!< 信号丢失报警 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_MOTION[] struMotion;				/*!< 移动侦测 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_MOSAIC[] struMosaic;				/*!< 马赛克 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALIAS_CFG_1[] struChName_1;		/*!< 通道别名配置(Unicode) */

        }

        /** @brief 图像配置信息长度																	 */
        public readonly int PIC_CFG_LEN = Marshal.SizeOf(new HY_DVR_PIC_CFG());
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////
        //编码类型

        /** @brief 流类型个数（普通H264，高级H264）																				 */
        public const int DVR_MAX_STREAM_TYPE = 2;

        /** @brief 普通H264																				 */
        public const int DVR_CAP_NORMAL_H264 = 0x01;

        /** @brief 高级H264																				 */
        public const int DVR_CAP_ADVANCED_H264 = 0x02;

        /** @brief QCIF分辨率																			 */
        public const int DVR_CAP_RESOLUTION_QCIF = 0x01;

        /** @brief CIF分辨率																			 */
        public const int DVR_CAP_RESOLUTION_CIF = 0x02;

        /** @brief D1分辨率																				 */
        public const int DVR_CAP_RESOLUTION_D1 = 0x04;

        /** @brief SD分辨率																				 */
        public const int DVR_CAP_RESOLUTION_SD = 0x08;

        /*!
        *  @struct	HY_DVR_COMPRESS_CAP
        *  @brief	编码能力结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_COMPRESS_CAP
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            public int nStreamFormatCap;										/*!< 支持的码流格式， 所有支持的格式相或的结果  */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_STREAM_TYPE, ArraySubType = UnmanagedType.U4)]
            public uint[] dwResolutionCap;			/*!< 每种编码格式下支持的分辨率格式,支持所有的格式相或的结果  */

        }

        /*!
        *  @struct	HY_DVR_COMPRESSION_INFO
        *  @brief	编码配置结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_COMPRESSION_INFO
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            public int nVideoFrameRate;										/*!< 帧率 1-20，ff为全帧率 */
            public int nVideoBitrateH;											/*!< 码率上限,单位Kbit/s */
            public int nVideoBitrateL;											/*!< 码率下限,单位Kbit/s */
            public int nQuotiety;												/*!< 量化系数:0-6 */
            public int nResolution;											/*!< 分辨率0-CIF, 1-D1 */
            public int nStreamType;											/*!< 码流类型 0-视频流 1-复合流 */
            public int nPicQuality;											/*!< 录像质量 0-高质量 1-标准 2-低 3-自定义 */
            public int nBitrateType;											/*!< 码率类型 0-变码率率 1-定码 2-限定码率 */

        }

        /*!
        *  @struct	HY_DVR_AUX_COMPRESSION_INFO
        *  @brief	子码流编码参数结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_AUX_COMPRESSION_INFO
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            public int bEnalbe;												/*!< 是否启用子码流  */
            public int nVideoFrameRate;										/*!< 帧率 1-20，ff为全帧率 */
            public int nVideoBitrate;											/*!< 码率:32Kbin/s,48Kbit/s,64Kbit/s,128Kbit/s,256Kbit/s,512Kbit/s */

        }

        /*!
        *  @struct	HY_DVR_COMPRESSION_CHANNEL
        *  @brief	通道编码参数结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_COMPRESSION_CHANNEL
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */
            public int nDelayRecordTime;										/*!< 录象延迟时间:10s、30s、自定义（10s-300s） */
            public int nPreRecordTime;											/*!< 预录时间:0表示最大预录时间、5秒、10秒 */
            public HY_DVR_COMPRESS_CAP struCompressCap;						/*!< 压缩能力（只读）  */
            public HY_DVR_AUX_COMPRESSION_INFO struNetPara;					/*!< 子码流  */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_COMPRESSION_INFO[] struRecordPara;					/*!< 0基本配置,1事件录像,2手动录像  */

        }

        /*!
        *  @struct	HY_DVR_HC_RESMODE
        *  @brief	HC板型编解码资源分配模式
        */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_HC_RESMODE
        {
            public int bValid;													/*!< 本结构体是否有效，0表示无效 */

            //该结构体只在HC或HS板型中有效

            /*  工作模式取值范围为(0 - 2)
            16路板型: 
            模式0时，系统支持16路实时CIF或非实时2CIF、D1格式编解码
            模式1时，系统支持8路实时2CIF 加8路实时CIF 编解，4路2CIF实时解码
            模式2时，系统支持4路实时D1 加12路实时CIF编码，1路D1实时解码
            8路板型: 
            模式0时，系统支持8路实时CIF或非实时2CIF、D1格式编解码
            模式1时，系统支持2路实时D1 加6路实时CIF 编解，4路D1实时解码
            模式2时，系统支持6路实时D1 加2路实时CIF编码，1路D1实时解码
            */
            public int nMode;

            public uint dwChMask;												/*!< 通道掩码，第0位为1表示通道1支持2CIF或D1格式编码，依次类推 */

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_COMPRESSION_CFG
        *  @brief	编码参数结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_COMPRESSION_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public HY_DVR_WORKMODE struWorkMode;											/*!< 工作模式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_COMPRESSION_CHANNEL[] struCompressChannel;	/*!< 各通道编码参数 */

        }

        /** @brief 编码配置结构体长度																	 */
        public readonly int COMPRESS_CFG_LEN = Marshal.SizeOf(new HY_DVR_COMPRESSION_CFG());
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_RECORD_SCHED
        *  @brief	录像计划结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_RECORD_SCHED
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_SCHEDTIME struRecordTime;					/*!< 录像时间计划 */

        }

        /** @brief 录像计划结构体长度																	 */
        public readonly int RECORD_SCHED_LEN = Marshal.SizeOf(new HY_DVR_RECORD_SCHED());
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_TIME
        *  @brief	配置系统时间参数
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_TIME
        {
            public int nYear;																/*!< 年 */
            public int nMonth;																/*!< 月 */
            public int nDay;																/*!< 日 */
            public int nHour;																/*!< 时 */
            public int nMinute;															/*!< 分 */
            public int nSecond;															/*!< 秒 */

        }

        /** @brief 配置系统时间结构体长度																 */
        public readonly int TIME_CFG_LEN = Marshal.SizeOf(new HY_DVR_TIME());
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_PTZPRO
        *  @brief	云台协议信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_PTZPRO
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nUseable;															/*!< PTZ协议的状态（0：未安装 1：已安装）  */
            public int bSystem;															/*!< PTZ协议的状态（0：非系统固有的，可删除 1：系统固有的，不能删除）  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PTZPRONAME_LEN)]
            public string szPTZName;									/*!< PTZ协议名称  */

        }

        /*!
        *  @struct	HY_DVR_PTZPRO_CFG
        *  @brief	云台协议管理信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_PTZPRO_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nPtzCount;															/*!< 系统支持PTZ协议的个数  */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_PTZPRO_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_PTZPRO[] struPTZProtocol;						/*!< 系统PTZ  */

        }

        /** @brief 云台协议管理结构体长度																 */
        public readonly int PTZPRO_CFG_LEN = Marshal.SizeOf(new HY_DVR_PTZPRO_CFG());
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_PTZ
        *  @brief	云台信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_PTZ
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PRESET_NUM)]
            public string bySetPreset;							/*!< 预置点是否设置,0-没有设置,1-设置  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PRESET_NUM)]
            public string bySetCruise;							/*!< 巡航是否设置: 0-没有设置,1-设置  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PRESET_NUM)]
            public string bySetTrack;							/*!< 轨迹是否设置,0-没有设置,1-设置  */

        }

        /*!
        *  @struct	HY_DVR_SERIAL
        *  @brief	串口配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_SERIAL
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nBaudRate;															/*!< 波特率(bps)，1200，2400，4800，9600 */
            public int nDataBit;															/*!< 只读 */
            public int nStopBit;															/*!< 只读 */
            public int nParity;															/*!< 只读 */
            public int nFlowcontrol;														/*!< 只读 */
            public int nDecoderAddress;													/*!< 解码器地址:0 - 255  */
            public int nComType;															/*!< 0－COM1 RS232，1－COM2 RS485  */

        }

        /*!
        *  @struct	HY_DVR_DECODER_CHANNEL
        *  @brief	解码通道配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DECODER_CHANNEL
        {
            public int bValid;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PTZPRONAME_LEN)]
            public string szPTZName;									/*!< PTZ协议名称  */
            public HY_DVR_SERIAL struSerial;												/*!< 串口配置 */

        }

        /*!
        *  @struct	HY_DVR_DECODER_CFG
        *  @brief	设备解码配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DECODER_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_PTZ_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_DECODER_CHANNEL[] struDecoder;					/*!< 各解码通道配置 */

        }

        /** @brief 设备解码配置结构体长度																 */
        public readonly int DECODER_CFG_LEN = Marshal.SizeOf(new HY_DVR_DECODER_CFG());
        ///////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_ALARMIN_CHANNEL
        *  @brief	各通道报警输入配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_ALARMIN_CHANNEL
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nAlarmInType;														/*!< 报警器类型,0：常开,1：常闭  */
            public HY_DVR_HANDLEEXCEPTION struHandleType;									/*!< 处理方式  */
            public HY_DVR_SCHEDTIME struSchedTime;											/*<! 报警输入布防时间 */

        }

        /*!
        *  @struct	HY_DVR_ALARMOUT_CHANNEL
        *  @brief	各通道报警输出配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_ALARMOUT_CHANNEL
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nAlarmOutDelay;														/*!< 输出保持时间(0为无限，需要手动关闭)  */
            public HY_DVR_SCHEDTIME struSchedTime;											/*!< 报警输出布防时间 */

        }

        /*!
        *  @struct	HY_DVR_ALARM_CAPTURE_PIC
        *  @brief	报警联动抓图配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_ALARM_CAPTURE_PIC
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nCapPicCount;														/*!< 联动快照张数 */
            public int nCapPicIntervalTime;												/*!< 联动快照间隔时间 */

        }

        /*!
        *  @struct	HY_DVR_SYSTEM_EXCEPTION
        *  @brief	系统异常检测
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_SYSTEM_EXCEPTION
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int bEnable;															/*!< 是否启动检测 */
            public int nCheckTime;															/*!< 检测时间间隔，单位为秒（目前不支持） */
            public HY_DVR_HANDLEEXCEPTION struHandleType;									/*!< 联动处理方式  */

        }

        /*!
        *  @struct	HY_DVR_ALARM_CFG
        *  @brief	报警设置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_ALARM_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public HY_DVR_WORKMODE struWorkMode;											/*!< 工作模式 */
            public HY_DVR_ALARM_CAPTURE_PIC struCapPic;									/*!< 联动快照 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_ALARMIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALARMIN_CHANNEL[] struAlarmIn;				/*!< 报警输入配置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_ALARMOUT_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALARMOUT_CHANNEL[] struAlarmOut;				/*!< 报警输入配置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_EXCEPTION_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_SYSTEM_EXCEPTION[] struSystemException;		/*!< 系统异常处理：0-硬盘故障,1-IP冲突,2-非法访问,3-网线断,4-盘组满,5-盘组异常 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_ALARMIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALIAS_CFG[] struAlarmInAlias;					/*!< 报警输入通道别名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_ALARMOUT_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALIAS_CFG[] struAlarmOutAlias;				/*!< 报警输出通道别名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_ALARMIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALIAS_CFG_1[] struAlarmInAlias_1;				/*!< 报警输入通道别名(Unicode) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_ALARMOUT_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_ALIAS_CFG_1[] struAlarmOutAlias_1;			/*!< 报警输出通道别名(Unicode) */

        }

        /** @brief 报警设置结构体长度																 */
        public readonly int ALARM_CFG_LEN = Marshal.SizeOf(new HY_DVR_ALARM_CFG());
        //////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_VO_CFG
        *  @brief	显示参数
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_VO_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nScreenSaveTime;													/*!< 屏幕保护时间：0表示从不启动屏幕保护，60s-3600s表示屏幕保护时间）  */
            public int nMenuAlphaValue;													/*!< 菜单与背景图象对比度：0表示不透明，4表示4:1，8表示3:1  */

        }

        /** @brief 显示参数结构体长度																 */
        public readonly int VO_CFG_LEN = Marshal.SizeOf(new HY_DVR_VO_CFG());
        /////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_USER_ENABLE
        *  @brief	用户使能信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_USER_ENABLE
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int bEnable;															/*!< 是否启用:0表示禁用，1表示启用 */

        }

        /*!
        *  @struct	HY_DVR_USER_INFO
        *  @brief	用户基本信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_USER_INFO
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PASSWORD_LEN)]
            public string szPassword;									/*!< 密码  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_MAC_NUM_LEN)]
            public string byLimitMac;							/*!< 限制访问物理地址  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_IP_STR_LEN)]
            public string szLimitIP;										/*!< 限制访问IP地址  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_EMAIL_LEN)]
            public string szEmail;										/*!< Email地址 */

        }

        /*!
        *  @struct	HY_DVR_USER_RIGHT
        *  @brief	用户权限信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_USER_RIGHT
        {
            public int bValid;								/*!< 本结构体是否有效，0表示无效 */

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_RIGHT_NUM, ArraySubType = UnmanagedType.I4)]
            public int[] nLocalRight;		/*!< 本地权限：数组0-本地系统设置；1-文件管理；2-录像备份；3-系统关闭；4-邮件通知 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_RIGHT_NUM, ArraySubType = UnmanagedType.I4)]
            public int[] nRemoteRight;	/*!< 远程权限：数组0-系统设置；1-语音对讲；2-串口输出；3-报警输出；4-远程升级；5-关闭系统 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_RIGHT_NUM, ArraySubType = UnmanagedType.I4)]
            public int[] nChannelRight;	/*!< 通道权限：数组0-本地回放；1-本地预览；2-本地云台控制；3-本地手动录像；4-远程回放；5-远程预览；6-远程云台控制；7-远程手动录像 */

        }

        /*!
        *  @struct	HY_DVR_USER
        *  @brief	用户信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_USER
        {
            public int bValid;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_USERNAME_LEN)]/*!< 本结构体是否有效，0表示无效 */
            public string szUserName;									/*!< 用户名 最大16位  */
            public HY_DVR_USER_ENABLE struUserEnable;										/*!< 用户使能信息 */
            public HY_DVR_USER_INFO struUserInfo;											/*!< 用户基本信息 */
            public HY_DVR_USER_RIGHT struUserRight;										/*!< 用户权限信息 */

        }

        /*!
        *  @struct	HY_DVR_USER_CFG
        *  @brief	用户配置信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_USER_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public HY_DVR_WORKMODE struWorkMode;											/*!< 工作模式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_USER_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_USER[] struUser;									/*!< 用户信息 */

        }

        /** @brief 用户配置信息结构体长度														 */
        public readonly int USER_CFG_LEN = Marshal.SizeOf(new HY_DVR_USER_CFG());
        /////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_DEVICE_INFO
        *  @brief	设备信息结构(只读)
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DEVICE_INFO
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public uint dwBoardType;												/*!< 板型编号 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_VERSION_LEN)]
            public string szTypeName;									/*!< 设备型谱名称，如HY-2016HC */
            public uint dwSerialNum;												/*!< 序列号  */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_VERSION_LEN)]
            public string szHardwareVersion;							/*!< 硬件版本 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_VERSION_LEN)]
            public string szSoftwareVersion;							/*!< 软件版本 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_VERSION_LEN)]
            public string szDspVersion;									/*!< 编码版本 */

            public byte byVideoInChannels;										/*!< 视频输入路数  */
            public byte byAudioInChannels;										/*!< 音频输入路数  */
            public byte byAlarmInChannels;										/*!< 报警输入路数  */
            public byte byAlarmOutChannels;										/*!< 报警输出路数  */

            public byte byVGAChannels;											/*!< VGA接口个数  */

            public byte bySpotOutChannles;										/*!< SPOT输出路数  */
            public byte byVoiceInChannels;										/*!< 语音对讲输入路数  */

        }

        /** @brief 设备信息结构体长度														 */
        public readonly int DEVICE_INFO_LEN = Marshal.SizeOf(new HY_DVR_DEVICE_INFO());
        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_DST
        *  @brief	夏时制信息结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DST
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int bEnableDST;															/*!< 是否启用夏时制:0表示禁用，1表示启用 */
            public HY_DVR_TIME struStart;													/*!< 开始时间，年无效 */
            public HY_DVR_TIME struEnd;													/*!< 结束时间，年无效 */
            public int nDSTBias;															/*!< 调整时间，以秒为单位，正数表示延时，负数表示提前，取值范围60-3600 */

        }

        /*!
        *  @struct	HY_DVR_GUI
        *  @brief	日期时间格式配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_GUI
        {
            public int bValid;					/*!< 本结构体是否有效，0表示无效 */
            public int nAutoLockScreenTime;	/*!< 自动锁屏时间：0表示从不锁屏， 60s-3600s表示锁屏时间*/
            public int nMenuLanguage;			/*!< 语言：0(Default)-简体中文；1-繁体中文；2-英文 */
            public int nDateFormat;			/*!< 日期格式：0(Default)-YYYY-MM-DD W hh:mm:ss, 1-YYYY-MM-DD hh:mm:ss, 2-MM-DD-YYYY W hh:mm:ss, 3-MM-DD-YYYY hh:mm:ss */
            public int nTimeFormat;			/*!< 时间格式：0-12小时制，1-24小时制 */

        }

        /*!
        *  @struct	HY_DVR_TIMEZONE
        *  @brief	时区配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_TIMEZONE
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nTimeZone;															/*!< 时区时差 */

        }

        /*!
        *  @struct	HY_DVR_DVRID
        *  @brief	DVRID配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DVRID
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */
            public int nDvrID;																/*!< DVR ID,用于遥控器  */

        }

        /*!
        *  @struct	HY_DVR_STANDARD
        *  @brief	视频制式配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_STANDARD
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public int nVideoInStandard;													/*!< 视频输入制式:1-NTSC,2-PAL,4-SECAM */
            public int nVideoOutStandard;													/*!< 视频输出制式（通常和输入制式一样） */

        }

        /*!
        *  @struct	HY_DVR_DEVICE_CFG
        *  @brief	设备基本配置结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DEVICE_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_DVRID struDvrID;													/*!< DVRID配置 */

            public HY_DVR_GUI struGUI;														/*!< 日期时间格式配置 */

            public HY_DVR_TIMEZONE struTimeZone;											/*!< 时区配置 */

            public HY_DVR_DST struDST;														/*!< 夏时制配置 */

            public HY_DVR_ALIAS_CFG struDvrAlias;											/*!< 设备别名配置 */

            public HY_DVR_STANDARD struStandard;											/*!< 设备只是配置 */

            public HY_DVR_ALIAS_CFG_1 struDvrAlias_1;										/*!< 设备别名配置(Unicode) */

        }

        /** @brief 设备基本配置结构体长度														 */
        public readonly int DEVICE_CFG_LEN = Marshal.SizeOf(new HY_DVR_DEVICE_CFG());
        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_DISK_PARTITION
        *  @brief	分区信息结构（只读）
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DISK_PARTITION
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PARTITION_DEVNODE_LEN)]
            public string szDevNode;							/*!< 设备节点名, 如 /dev/sda1 */
            public uint dwCapacity;												/*!< 总容量, M为单位 */
            public uint dwSpare;													/*!< 剩余容量 */
            public uint dwStartCHS;												/*!< 分区起始扇区 */
            public uint dwEndtCHS;												/*!< 分区结束扇区 */

        }

        /*!
        *  @struct	HY_DVR_DISK_MARK
        *  @brief	硬盘标记（只读）
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DISK_MARK
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public int nVerifyMark;														/*!< 验证码(配置改变时，验证码会同步改变) */

        }

        /** @brief 硬盘标记结构体长度														 */
        public readonly int DISK_MARK_LEN = Marshal.SizeOf(new HY_DVR_DISK_MARK());

        /*!
        *  @struct	HY_DVR_DISK
        *  @brief	硬盘信息结构（只读）
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DISK
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public int bEnable;															/*!< 是否安装有此硬盘:0表示没有，1表示有 */

            public HY_DVR_DISK_MARK struVerifyMark;										/*!< 验证码(配置改变时，验证码会同步改变) */

            public int nRestState;															/*!< 硬盘状态:0-正常，1-正在写入，2-异常，3-警告 */
            public int nWriteState;														/*!< 硬盘状态:0-没有写，非0-正在写入 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_VERSION_LEN)]
            public string szSerialNum;									/*!< 序列号 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_VERSION_LEN)]
            public string szTypeName;									/*!< 硬盘类型 */
            public int nBusNum;															/*!< 物理位置,如SATA1 */

            public uint dwCapacity;												/*!< 容量,M为单位 */
            public int nPartitionCount;													/*!< 分区数量 */

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_PARTITION_IN_HARDDISK, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_DISK_PARTITION[] struPartitionInfo;	/*!< 分区信息 */

        }

        /*!
        *  @struct	HY_DVR_DISK_GROUP
        *  @brief	盘组信息结构（只读）
         */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DISK_GROUP
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public int bEnable;															/*!< 是否启用本盘组 */
            public int nGroupType;															/*!< 盘组类型:0-普通盘组，1-报警盘组，2-冗余盘组，3-备份盘组 */
            public int nBindChannelMask;													/*!< 绑定的通道:按位标记，若第0位为1，表示通道1被选中，以此类推 */
            public int nDataKeepTime;														/*!< 数据保持时间:0x80000000-一直保持，24 * 3600-保持1天，2 * 24 * 3600-保持2天，7 * 24 * 3600-保持7天，其他值-最大保持 */
            public int nPartitionCount;													/*!< 已添加的分区数量 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_PARTITION_IN_DISKGROUP * DVR_MAX_PARTITION_DEVNODE_LEN)]
            public string szPartitionList;/*!< 设备节点列表,支持32个分区 */

        }

        /*!
        *  @struct	HY_DVR_STORAGE_CFG
        *  @brief	存储管理结构
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_STORAGE_CFG
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_WORKMODE struWorkMode;											/*!< 工作模式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_HARDDISK_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_DISK struDisk;								/*!< 硬盘信息（只读） */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_DISKGROUP_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_DISK_GROUP struDiskGroup;					/*!< 盘组信息（只读） */

        }

        /** @brief 设备存储管理结构体长度														 */
        public readonly int STORAGE_CFG_LEN = Marshal.SizeOf(new HY_DVR_STORAGE_CFG());
        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_CHANNEL_STATE
        *  @brief	设备通道状态
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_CHANNEL_STATE
        {
            public int bValid;																/*本结构体是否有效，表示无效(即系统没有该通道) */

            public int nVideoBitrate;														/*!< 主码流码率（128kbit/s-5120kbit/s） */
            public int nChildVideoBitrate;													/*!< 子码流码率（32 48 64 128 256 512kbit/s） */
            public int bUseAuxVideoStream;													/*!< 是否启动子码流：0-不启用，1-启用 */

            public int nRecordType;														/*!< 录像状态：0-无录像，1-报警录像类型，2-普通类型，3-手动录像类型，4-移动录像 */
            public int bViLost;															/*!< 是否有无视频信号报警(0表示无信号，1表示有信号) */
            public int bMotion;															/*!< 是否有移动报警(尚不支持) */

        }

        /*!
        *  @struct	HY_DVR_DEVICE_STATE
        *  @brief	设备状态信息
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DEVICE_STATE
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DVR_MAX_VIDEOIN_NUM, ArraySubType = UnmanagedType.Struct)]
            public HY_DVR_CHANNEL_STATE[] struChannelState;				/*!< 通道状态 */

        }

        /** @brief 设备状态结构体长度														 */
        public readonly int DEVICE_STATE_LEN = Marshal.SizeOf(new HY_DVR_DEVICE_STATE());
        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_MAINTENANCE_COMMON
        *  @brief	设备自动维护基本配置
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_MAINTENANCE_COMMON
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public int bEnable;															/*!< 是否启用 */
            public int nMode;																/*!< 维护模式:0-每天,1-每周,2-单次 */
            public int nWeekDayMask;														/*!< 星期模式时，第0位表示星期天,第1位表示星期1,对应位为1表示当天有效 */
            public HY_DVR_TIME struTime;													/*!< 单次模式，则年月日时分有效；其它模式，时分有效 */

        }

        /*!
        *  @struct	HY_DVR_DEVICE_MAINTENANCE
        *  @brief	设备自动维护
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DEVICE_MAINTENANCE
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_MAINTENANCE_COMMON struCommon;									/*!< 设备自动维护基本配置 */

        }

        /** @brief 设备自动维护结构体长度														 */
        public readonly int DEVICE_MAINTENANCE_LEN = Marshal.SizeOf(new HY_DVR_DEVICE_MAINTENANCE());
        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_DEVICE_CUSTOM
        *  @brief	设备功能定制
         */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DEVICE_CUSTOM
        {
            public int bValid;																/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_WORKMODE struUser;												/*!< 高级用户管理模式 */

            public HY_DVR_WORKMODE struAlarm;												/*!< 高级报警管理模式 */

            public HY_DVR_WORKMODE struPic;												/*!< 高级图像参数配置 */

            public HY_DVR_WORKMODE struEmail;												/*!< Email报警联动 */

            public HY_DVR_WORKMODE struRecord;												/*!< 高级录像参数配置 */

            public HY_DVR_WORKMODE struCMS;												/*!< 中心管理服务器配置 */

            public HY_DVR_WORKMODE struAMS;												/*!< 报警服务器配置 */

            public HY_DVR_WORKMODE struNTP;												/*!< NTP服务器配置 */

            public HY_DVR_WORKMODE struStorage;											/*!< 高级存储管理 */

            public HY_DVR_WORKMODE struMaintenance;										/*!< 自动维护配置 */

            public HY_DVR_HC_RESMODE struHCResMode;										/*!< HC板型编解码资源分配模式 */

        }

        /** @brief 设备功能定制结构体长度														 */
        public readonly int DEVICE_CUSTOM_LEN = Marshal.SizeOf(new HY_DVR_DEVICE_CUSTOM());
        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_THRESHOLD
        *  @brief	告警阀值（暂不实现）
        */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_THRESHOLD
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */

            public float fCPUUseRatio;												/*!< CPU利用率 */
            public float fMemoryUseRatio;											/*!< 内存利用率 */
            public float fDiskSpaceRatio;											/*!< 硬盘利用率 */
            public uint dwDiskSpaceBalance;										/*!< 硬盘剩余空间 */

        }

        /*!
        *  @struct	HY_DVR_DOWNLOAD_MODE
        *  @brief	下载方式配置（可以依据NAT的配置来区分，暂不实现）
        */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_DOWNLOAD_MODE
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */
            public uint dwDownloadMode;											/*!< 下载方式， 0-hxht, 1-STREAM_TCP */

        }

        //////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_NATSTATUS
        *  @brief	NAT配置
        */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_NATSTATUS
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */

            public uint dwIsNAT;													/*!< 是否在NAT后面，0表示不在后面，1表示在NAT后面 */

        }


        //////////////////////////////////////////////////////////////////////////////
        /*
        压缩格式 语音通道数 采样深度   采样速率    
        G711U.HX      1        16        8000  
        G711A.HX      1        16        8000  
        G722.HX       1        16    8000或16000
        */

        /*!
        *  @struct	HY_DVR_VOICEPARAM
        *  @brief	语音参数配置
        */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_VOICEPARAM
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_COMMON_STRING_LEN)]
            public string szAudioEncoder;						/*!< 支持的语音编码格式，可以有多个，以逗号隔开；G711U.HX G711A.HX G722.HX */
            public uint dwChannel;												/*!< 通道数 */
            public uint dwBitsPerSample;											/*!< 采样深度 */
            public uint dwSamplesPerSec;											/*!< 采样率 */

        }


        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_NET_CMS_STOREAGE
        *  @brief	中心管理服务器存储注册配置信息
        */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_NET_CMS_STOREAGE
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_MAX_DOMAINNAME_LEN)]
            public string szManageHostAddress;				/*!< 远程管理主机地址，长度考虑域名 */

            public ushort wManageHostPort;									/*!< 远程管理主机端口 */

        }

        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_DEVICE_ID
        *  @brief	前端设备ID（该ID是由Hxht接入服务器分配的）
        */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct HY_DVR_DEVICE_ID
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DVR_COMMON_STRING_LEN)]
            public string szDeviceID;							/*!< 前端设备ID */

        }

        //////////////////////////////////////////////////////////////////////////////
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_HXHT_CFG
        {
            public int bValid;														/*!< 本结构体是否有效，0表示无效 */

            public HY_DVR_NET_CMS_STOREAGE struCmsStorage;							/*!< 中心管理服务器存储注册信息 */
            public HY_DVR_DEVICE_ID struDeviceID;							/*!< 前端设备ID */
            public HY_DVR_NATSTATUS struNATStatus;							/*!< NAT状态 */
            public HY_DVR_VOICEPARAM struVoiceParam;							/*!< 语音呼叫参数 */

        }

        //////////////////////////////////////////////////////////////////////////////

        /*!
        *  @struct	HY_DVR_PROTOCOL_NAME
        *  @brief	协议名称配置
        */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HY_DVR_PROTOCOL_NAME
        {
            public int bValid;														//本结构体是否有效，=0表示无效

            public uint dwProtocolName;											/*!< 协议名称：协议是按位设置和读取的，例如只有手机协议时，该值为1；
																	        只有互信互通协议时，该值为2；两个协议都有时，该值为3。
																	        （私有协议是必须加载的，不在其中） */
        }
    }
}

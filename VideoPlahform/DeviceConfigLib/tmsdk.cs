using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HieCiULib
{
    public class tmsdk
    //矩阵解码 SDK 头文件，在 HieCIU.h 中使用其部分的数据结构
    {

        public enum VideoStandard_t:uint
        {
	        StandardNone		= 0x80000000,
	        StandardNTSC		= 0x00000001,
	        StandardPAL			= 0x00000002,
	        StandardSECAM		= 0x00000004,
        } 
            
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct FRAMES_STATISTICS
        {
	        public uint				VideoFrames;
	        public uint				AudioFrames;
	        public uint				FramesLost;
	        public uint				QueueOverflow;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct FRAME_INFO
        {
	        public int nWidth;
	        public int nHeight;
	        public int nStamp;
	        public int nType;
	        public int nFrameRate;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct FRAME_POS
        {
	        public int nFrameNum;          
	        public uint  nFrameTime;          
	        public int nFilePos;            
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CHANNEL_CAPABILITY
        {
	        public byte 				bAudioPreview;
	        public byte 				bAlarmIO;
	        public byte 				bWatchDog;
        }

        public enum  TypeVideoFormat
        {
	        vdfRGB8A_233              = 0x00000001,
	        vdfRGB8R_332              = 0x00000002,
	        vdfRGB15Alpha             = 0x00000004,
	        vdfRGB16                  = 0x00000008,
	        vdfRGB24                  = 0x00000010,
	        vdfRGB24Alpha             = 0x00000020,
   
	        vdfYUV420Planar           = 0x00000040,
	        vdfYUV422Planar           = 0x00000080,
	        vdfYUV411Planar           = 0x00000100,
	        vdfYUV420Interspersed     = 0x00000200,
	        vdfYUV422Interspersed     = 0x00000400,
	        vdfYUV411Interspersed     = 0x00000800,
	        vdfYUV422Sequence         = 0x00001000,   
	        vdfYUV422SequenceAlpha    = 0x00002000,   
	        vdfMono                   = 0x00004000,  
	        vdfYUV444Planar           = 0x00008000,
        }

        public enum BitrateControlType_t
        {
	        brVBR					= 0,
	        brCBR					= 1,
	        brHBR					= 2,
        }

        public enum FrameType_t
        {
	        PktError				= 0x0000,	
	        PktIFrames				= 0x0001,
	        PktPFrames				= 0x0002,
	        PktBPFrames				= 0x0020,
	        PktBBPFrames			= 0x0004,
	        PktAudioFrames			= 0x0008,
	        PktQCIFIFrames			= 0x0010,
	        PktQCIFPFrames			= 0x0040,
	        PktSysHeader			= 0x0080,
	        PktAimDetection			= 0x0400,
	        PktMotionDetection		= 0x1000	
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VERSION_INFO
        {
	        public uint					DspVersion,		DspBuildNum;
	        public uint					DriverVersion,	DriverBuildNum;
	        public uint					SDKVersion,		SDKBuildNum;
        }

        public enum PictureFormat_t
        {
	        ENC_CIF_FORMAT			= 0,
	        ENC_QCIF_FORMAT			= 1,
	        ENC_2CIF_FORMAT			= 2,
	        ENC_MD_FORMAT			= 3,
	        ENC_D1_FORMAT			= 4,
	        ENC_DCIF_FORMAT			= 5
        }

        public enum BoardFormat_t
        {
	        TYPE_1500_CIF			= 0,
	        TYPE_1500_D1			= 1,
	        TYPE_1300_HALFD1		= 2,
	        TYPE_1300_D1			= 3,
	        TYPE_1500_DCIF			= 5,
	        TYPE_1700_CIF			= 6,
	        TYPE_1700_D1			= 7,
	        TYPE_1700_DCIF			= 8,
	        TYPE_1700_4_CIF			= 9,
        }

        public enum DecoderCardWorkMode_t
        {
	        WorkMode_CIF		= 0x00000000,
	        WorkMode_2CIF		= 0x00000001,
	        WorkMode_D1			= 0x00000002,
        }   
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CHANNEL_INFO
        {
	        public PictureFormat_t			PictureFormat;
	        public BoardFormat_t			BoardFormat;
	        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst=4, ArraySubType=UnmanagedType.U4)]
            public uint[] Reserved;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct REGION_PARAM
        {
	        public int			left;
	        public int			top;
	        public int			width;
	        public int			height;
	        public int	        color;
	        public int			param;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct MOTION_DATA_HEADER
        {
	        public PictureFormat_t			PicFormat;
	        public uint					HorizeBlocks;
	        public uint					VerticalBlocks;
	        public uint					BlockSize;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public	struct DSP_INFO
        {
	        public uint  EncodeChannelCount;
	        public uint  FirstEncodeChannelIndex;
	        public uint  DecodeChannelCount;
	        public uint  FirstDecodeChannelIndex;
	        public uint  DisplayChannelCount;
	        public uint  FirstDisplayChannelIndex;
	        public uint  reserved1;
	        public uint  reserved2;
	        public uint  reserved3;
	        public uint  reserved4;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public	struct BOARD_INFO
        {
	        public uint  DSPCount;
	        public uint  FirstDSPIndex;
	        public uint  EncodeDSPCount;
	        public uint  FirstEncodeDSPIndex;
	        public uint  DecodeDSPCount;
	        public uint  FirstDecodeDSPIndex;
	        public uint  EncodeChannelCount;
	        public uint  FirstEncodeChannelIndex;
	        public uint  DecodeChannelCount;
	        public uint  FirstDecodeChannelIndex;
	        public uint  DisplayChannelCount;
	        public uint  FirstDisplayChannelIndex;
	        public uint  reserved1;
	        public uint  reserved2;
	        public uint  reserved3;
	        public uint  reserved4;
        }


        public const int _OSD_BASE		=	0x9000;
        public const int	_OSD_YEAR4	=		_OSD_BASE+0;
        public const int _OSD_YEAR2		=	_OSD_BASE+1;
        public const int _OSD_MONTH3	=		_OSD_BASE+2;
        public const int _OSD_MONTH2	=		_OSD_BASE+3;
        public const int _OSD_DAY		=	_OSD_BASE+4;
        public const int _OSD_WEEK3		=	_OSD_BASE+5;
        public const int	_OSD_CWEEK1	=		_OSD_BASE+6;
        public const int	_OSD_HOUR24	=		_OSD_BASE+7;
        public const int	_OSD_HOUR12	=		_OSD_BASE+8;
        public const int	_OSD_MINUTE	=		_OSD_BASE+9;
        public const int _OSD_SECOND	=		_OSD_BASE+10;
        public const int	_OSD_0A0D	=		0x0A0D;
        public const int	_OSD_0A		=		0x000A;

        public delegate void	LOGRECORD_CALLBACK(System.IntPtr str, System.IntPtr context);
        public delegate int		STREAM_READ_CALLBACK(uint channelNumber, System.IntPtr context);

        public const int STREAM_TYPE_VIDEO	=1;
        public const int STREAM_TYPE_AUDIO	=2;
        public const int STREAM_TYPE_AVSYNC	=3;

        public const int STREAME_REALTIME	=0;
        public const int STREAME_FILE		=1;

        public const uint	ERR_NO				=				0x00000000;		//No error	

        public const uint	ERR_HANDLE_ALLOC_ERROR		=		0xc0000100	;	//Alloc memory error
        public const uint ERR_DDRAW_CREATE_FAILED		=		0xc0000101;		//DirectDraw Surface create error
        public const uint ERR_RUNTIME_ERROR              =     0xc0000102;		//C Runtime error
        public const uint ERR_INVALID_HANDLE				=	0xc0000103;		//Channel Handle error
        public const uint ERR_WAIT_TIMEOUT				=	0xc0000104;		//Wait TimeOut error
        public const uint ERR_INVALID_ARGUMENT			=	0xc0000105;		//Function Argument error
        public const uint ERR_OVERLAY_CREATE_FAILED		=	0xc0000106	;	//Overlay Surface create error
        public const uint ERR_DATA_ERROR					=	0xc0000107;		//Data error
        public const uint ERR_INVALID_FILENAME			=	0xc0000108;		//FileName error
        public const uint ERR_TMMAN_FAILURE				=	0xc0000109;		//Tmman Failure
        public const uint ERR_OUTOF_MEMORY				=	0xc000010A;
        public const uint ERR_DSP_BUSY					=	0xc000010B;
        public const uint ERR_NOT_SUPPORT				=		0xc000010C;		//Function Not Support 

        public const uint	ERR_DSP_OPEN				=		0xC0000200;		//DSP open error
        public const uint	ERR_DSP_CONTROL				=		0xC0000201;		//DSP control error
        public const uint	ERR_DSP_FILE				=		0xC0000202;		//DSP file error

        public const uint	ERR_PACKET_NO_DATA			=		0xC0000203;		//No data in packet error
        public const uint	ERR_BUFFER_LEN_SHORT		=		0xC0000204;		//Buffer Length too short error
        public const uint	ERR_BMP_READ_ERROR			=		0xC0000205;		//BMP file read error
        public const uint	ERR_BMP_TYPE_ERROR			=		0xC0000206;		//BMP type error

        //#ifndef	ERROR_MAX_THRDS_REACHED
        public const int	ERROR_MAX_THRDS_REACHED		=		0x000000A4;		//No more threads can be created in the system 
        //#endif


    }
}

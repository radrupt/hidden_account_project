using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HieCiULib;
using System.Windows.Forms;
using System.IO;
using TestWindow.CallDLL.interfaces;
using System.Runtime.InteropServices;
namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_StreamControl_Impl:interfaces.Client_StreamControl
    {
        private Hashtable enumDM = new Hashtable();
        private Hashtable enumDSM = new Hashtable();
        private Hashtable enumDFM = new Hashtable();
        private Hashtable enumSQM = new Hashtable();
        private Hashtable enumTC = new Hashtable();
        private Hashtable enumMFT = new Hashtable();
        private bool isStreamPlaying;    //是否正在播放实时流
        private string filePath;
        private string currentIFrameTime; //当前保存录像的第一个I桢的时间,即录像的开始时间
        private bool g_bStartRecord;  //是否开始录制
        private bool g_bReceiveIFrame;//是否正在接收I桢
        private FileStream g_fRecord;       //存储录像的文件
        private string recordfilepath=@"d:\";
        private long filesize = 1 * 1024 * 1024;//每份文件大小
        private int recfileindex = 0;//这个变量得存到文件里去，成为录像文件的唯一标识，但暂时没做这块
        string filename;
        private Common.CB_StreamMedia cb_streammedia;
        public bool G_bStartRecord {
            get {
                return g_bStartRecord; 
            }
            set {
                g_bStartRecord = value;
            }
        }
        public bool IsStreamPlaying
        {
            get
            {
                return isStreamPlaying;
            }
            set
            {
                isStreamPlaying = value;
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        public HieClient_StreamControl_Impl()
        {
            isStreamPlaying = false;
            g_bStartRecord = false;
            g_bReceiveIFrame = false;
            g_fRecord = null;
            cb_streammedia = new Common.CB_StreamMedia(CB_StreamMedia);
            enumDM.Add(interfaces.DecodeMode.SoftDecode, Common.eDecodeMode.eSoftDecode);
            enumDM.Add(interfaces.DecodeMode.HardDecode, Common.eDecodeMode.eHardDecode);
            enumDM.Add(interfaces.DecodeMode.MatrixDecode,Common.eDecodeMode.eMatrixDecode);

            enumDSM.Add(interfaces.DecodeShowMode.NormalMode, Common.eDecodeShowMode.eNormalMode);
            enumDSM.Add(interfaces.DecodeShowMode.OverlayMode, Common.eDecodeShowMode.eOverlayMode);

            enumDFM.Add(interfaces.DrawFuncMode.DrawFuncModeBegin, HieCIUCommon.eDrawFuncMode.eDrawFuncModeBegin);
            enumDFM.Add(interfaces.DrawFuncMode.AutoSizeByShowWnd, HieCIUCommon.eDrawFuncMode.eAutoSizeByShowWnd);
            enumDFM.Add(interfaces.DrawFuncMode.FixedPosition, HieCIUCommon.eDrawFuncMode.eFixedPosition);

            enumSQM.Add(interfaces.ShowQualityMode.ShowQualityModeBegin, HieCIUCommon.eShowQualityMode.eShowQualityModeBegin);
            enumSQM.Add(interfaces.ShowQualityMode.LowQuality, HieCIUCommon.eShowQualityMode.eLowQuality);
            enumSQM.Add(interfaces.ShowQualityMode.HighQuality, HieCIUCommon.eShowQualityMode.eHighQuality);

            enumTC.Add(interfaces.TaskControl.TaskSetSpeed, Common.eTaskControl.eTaskSetSpeed);
            enumTC.Add(interfaces.TaskControl.TaskStart, Common.eTaskControl.eTaskStart);
            enumTC.Add(interfaces.TaskControl.TaskStop, Common.eTaskControl.eTaskStop);

            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionFileDownload, Common.eMediaFunctionType.eMediaFunctionFileDownload);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionFileUpload, Common.eMediaFunctionType.eMediaFunctionFileUpload);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionHistoryStream, Common.eMediaFunctionType.eMediaFunctionHistoryStream);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionRealStream, Common.eMediaFunctionType.eMediaFunctionRealStream);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionTransparent, Common.eMediaFunctionType.eMediaFunctionTransparent);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionTypeBegin, Common.eMediaFunctionType.eMediaFunctionTypeBegin);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionTypeEnd, Common.eMediaFunctionType.eMediaFunctionTypeEnd);
            enumMFT.Add(interfaces.MediaFunctionType.MediaFunctionVoiceStream, Common.eMediaFunctionType.eMediaFunctionVoiceStream);
        }


        public void StreamOpen(IntPtr hStream)
        {
            try
            {
                StreamOpen(hStream, 
                    interfaces.DecodeMode.SoftDecode, 
                    interfaces.DecodeShowMode.NormalMode, 
                    0, 
                    interfaces.DrawFuncMode.AutoSizeByShowWnd, 
                    0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StreamOpen(
            IntPtr hStream, 
            interfaces.DecodeMode dm, 
            interfaces.DecodeShowMode dsm, 
            uint ColorRef, 
            interfaces.DrawFuncMode dfm, 
            uint userData)
        {
            if (enumDM.ContainsKey(dm) && enumDSM.ContainsKey(dsm) && enumDFM.ContainsKey(dfm))
            {
                int nStrmOpenCode = -1;
                
                //打开流解码器,在流解码回放之前调用
                nStrmOpenCode = HieCiULib.HieCIU.HieCIU_StreamOpen(hStream,
                                                   (Common.eDecodeMode)enumDM[dm],
                                                   (Common.eDecodeShowMode)enumDSM[dsm],
                                                   ColorRef,
                                                   (HieCIUCommon.eDrawFuncMode)enumDFM[dfm],
                                                   null,
                                                   userData);
                if (nStrmOpenCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nStrmOpenCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void StreamPlay(IntPtr hStream, IntPtr ComponentHandler)
        //hStream:历史流   ComponentHandler:显示窗口的句柄
        {
            int nStrmPlayCode = -1;
            if (ComponentHandler != new Constants.CConstants().INVALID_HANDLE_VALUE)
            //由于INVALID_HANDLE_VALUE被readonly修饰，只能在上下文定义，所以是定值
            {
                nStrmPlayCode = HieCIU.HieCIU_StreamPlay(hStream, ComponentHandler);
                if (nStrmPlayCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nStrmPlayCode)+"异常");
                }
                isStreamPlaying = true;
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            HieCIU.HieCIU_StreamMediaCB(hStream, cb_streammedia, 0);
        }
        public int CB_StreamMedia(IntPtr hStream, ref Common.StreamMediaFrame cFrame, uint dwUserData)
        {
            currentIFrameTime = String.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                cFrame.cFrameTime.wYear, cFrame.cFrameTime.wMonth, cFrame.cFrameTime.wDay, cFrame.cFrameTime.wHour,
                 cFrame.cFrameTime.wMinute, cFrame.cFrameTime.wSecond, cFrame.cFrameTime.wMillisecond);

            if (true == g_bStartRecord && null != g_fRecord)
            {
                //开始录像

                if (cFrame.dwFrameType ==(int)( Common.eFrameType.eFrameIFrames) && false == g_bReceiveIFrame)
                {
                    byte[] dest = new byte[(int)cFrame.cFrameBuffer.dwBufLen];
                    Marshal.Copy(cFrame.cFrameBuffer.pBuffer,dest,0,(int)cFrame.cFrameBuffer.dwBufLen);
                    g_fRecord.Write(dest,
                        0, (int)cFrame.cFrameBuffer.dwBufLen);
                    g_bReceiveIFrame = true;                
                }
                else
                {
                    byte[] dest = new byte[(int)cFrame.cFrameBuffer.dwBufLen];
                    Marshal.Copy(cFrame.cFrameBuffer.pBuffer, dest, 0, (int)cFrame.cFrameBuffer.dwBufLen);
                    g_fRecord.Write(dest,
                        0, (int)cFrame.cFrameBuffer.dwBufLen);
                    //if (g_fRecord.Length > filesize)
                    //{//为大文件分段，每份文件大小
                    //    recfileindex++;
                    //    g_fRecord.Close();
                    //    g_fRecord = new FileStream(filename + "-" + recfileindex + ".mp4", FileMode.Create);
                    //    if (null == g_fRecord)
                    //    {
                    //        return 1;
                    //    }
                    //}
                }

            }
            else if (false == g_bStartRecord)
            {
                if (null != g_fRecord)
                {                  
                    recfileindex = 0;
                    g_fRecord.Close();
                    g_fRecord = null;
                }
            }
            return 0;
        }

        public int StartRecord(IntPtr hStream) {
            if (!g_bStartRecord) {   //没开始录像
                recfileindex++;
                filename = recordfilepath + currentIFrameTime ;
                g_fRecord = new FileStream(filename + "-" + recfileindex + ".mp4", FileMode.Create);
                
                byte[] DSPHeader = {0X00,0X00,0X01,0XC6,0X4D ,0X50 ,0X47, 0X34, 0x01,
                                       0x00 ,0X19 ,0X20 ,0XA1 ,0x07 ,0x00 ,0X60 ,0x01, 0X20, 0x01};
                g_fRecord.Write(DSPHeader, 0, 19);

		        g_bStartRecord = true;
            }
            return 0;
        }

        public void StopRecord() {
            if (g_bStartRecord) {  //正在录像
                g_bStartRecord = false;
            }
        }

        public void StreamPause(IntPtr hStream)
        {
            if (isStreamPlaying)
            {
                int nStrmPause = -1;
                nStrmPause = HieCIU.HieCIU_StreamPause(hStream, isStreamPlaying);
                isStreamPlaying = false;
                if (nStrmPause != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nStrmPause));
                }
            }
        }

        public void StreamResume(IntPtr hStream)
        {
            if (!isStreamPlaying)
            {
                
                int nStrmResume = -1;
                nStrmResume = HieCIU.HieCIU_StreamPause(hStream, isStreamPlaying);
                isStreamPlaying = true;
                if (nStrmResume != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nStrmResume));
                }
            }
        }

        public void StreamStop(IntPtr hStream)
        {
            int nStrmStop = -1;
            nStrmStop = HieCIU.HieCIU_StreamStop(hStream);
            if (nStrmStop != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStrmStop));
            }
        }


        public void StreamSetVolume(IntPtr hStream, int inVolumn)
        {
            inVolumn = (ushort)(655.35 * inVolumn);
            int nSetVolumeCode = -1;                   //设置声音音量返回码
            nSetVolumeCode = HieCIU.HieCIU_StreamSetVolume(hStream, (ushort)inVolumn);
            if (nSetVolumeCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetVolumeCode));
            }
        }

        public void StreamPlaySound(IntPtr hStream)
        {
            int nPlaySoundCode = -1;                   
            nPlaySoundCode = HieCIU.HieCIU_StreamPlaySound(hStream);
            if (nPlaySoundCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPlaySoundCode));
            }
        }

        public void StreamStopSound(IntPtr hStream)
        {
            int nStopSoundCode = -1;                   
            nStopSoundCode = HieCIU.HieCIU_StreamStopSound(hStream);
            if (nStopSoundCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStopSoundCode));
            }
        }


        public uint StreamGetPlayedTime(IntPtr hStream)
        {
            uint dwPlayedTime = 0;
            int nGetPlayedTimeCode = -1;                  
            nGetPlayedTimeCode = HieCIU.HieCIU_StreamGetPlayedTime(hStream, ref dwPlayedTime);
            if (nGetPlayedTimeCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nGetPlayedTimeCode));
            }
            return dwPlayedTime;
        }

        public uint StreamGetAbsoluteTime(IntPtr hStream)
        {
            uint dwPlayTime = 0;
            int nGetAbsTimeCode = -1;                   
            nGetAbsTimeCode = HieCIU.HieCIU_StreamGetAbsoluteTime(hStream, ref dwPlayTime);
            if (nGetAbsTimeCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nGetAbsTimeCode));
            }
            return dwPlayTime;
        }

        public uint StreamGetPlayedFrameNum(IntPtr hStream)
        {
            uint dwPlayedFrmNums = 0;
            int nGetPlayedFrameNumCode = -1;                  
            nGetPlayedFrameNumCode = HieCIU.HieCIU_StreamGetPlayedFrameNum(hStream, ref dwPlayedFrmNums);
            if (nGetPlayedFrameNumCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nGetPlayedFrameNumCode));
            }
            return dwPlayedFrmNums;
        }


        public void StreamChangeBright(IntPtr hStream, int inBright)
        {
            int nChangeBrightCode = -1;                   //设置亮度返回码
            if (inBright > 0 && inBright <= 100)
            {
                inBright = (int)Math.Ceiling(0.15 * inBright);
                nChangeBrightCode = HieCIU.HieCIU_StreamChangeBright(hStream, (uint)inBright);
                if (nChangeBrightCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nChangeBrightCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void StreamSetPicQuality(IntPtr hStream, interfaces.ShowQualityMode sqm)
        {
            int nSetPicQualityCode = -1;                   //设置图像显示质量返回码
            if (enumSQM.ContainsKey(sqm))
            {
                nSetPicQualityCode = HieCIU.HieCIU_StreamSetPicQuality(hStream, (HieCIUCommon.eShowQualityMode)enumSQM[sqm]);
                if (nSetPicQualityCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nSetPicQualityCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void StreamChangeContrast(IntPtr hStream, int inContrast)
        {
            int nChangeContrastCode = -1;                   //设置对比度返回码
            if (inContrast > 0 && inContrast <= 100)
            {
                inContrast = (int)Math.Ceiling(0.15 * inContrast);
                nChangeContrastCode = HieCIU.HieCIU_StreamChangeContrast(hStream, (uint)inContrast);
                if (nChangeContrastCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nChangeContrastCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }


        public void StreamSnapShot(IntPtr hStream, string inFileName)
        {
            int nStrmSnapShotCode = -1;
            if (Directory.Exists(filePath))
            {
                if (!filePath.EndsWith(@"\"))
                {
                    filePath += @"\";
                }
                inFileName = filePath + inFileName;
                nStrmSnapShotCode = HieCIU.HieCIU_StreamSnapShot(hStream, inFileName);
                
                if (nStrmSnapShotCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nStrmSnapShotCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }


        public void StreamMediaControl(IntPtr hStream, interfaces.TaskControl tc)
        {
            if (enumTC.ContainsKey(tc))
            {
                int nStreamMediaCode = -1;
                nStreamMediaCode = HieCIU.HieCIU_StreamMediaControl(hStream,(Common.eTaskControl)enumTC[tc]);
                if (nStreamMediaCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nStreamMediaCode));
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(
                   (int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void SetStreamMediaLinkID(IntPtr hStream, uint linkId)
        {
            int nSetMediaLinkCode = -1;
            nSetMediaLinkCode = HieCIU.HieCIU_SetStreamMediaLinkID(hStream, linkId);
            if (nSetMediaLinkCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetMediaLinkCode));
            }
        }

        public List<string> QueryStreamMediaLinkID(Client_UserLogin userLogin, interfaces.MediaFunctionType mft)
        {
            HieCiULib.Common.MediaLinkIDQueryResult mliqr = new HieCiULib.Common.MediaLinkIDQueryResult();
            List<string> resultSet = new List<string>();
            if (enumMFT.ContainsKey(mft))
            {
                int nQueryStrMedLinkIDCode = -1;
                nQueryStrMedLinkIDCode = HieCIU.HieCIU_QueryStreamMediaLinkID(userLogin.UserInfo, (Common.eMediaFunctionType)enumMFT[mft], ref mliqr);
                if (nQueryStrMedLinkIDCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nQueryStrMedLinkIDCode));
                }
                int len = mliqr.dwLinkID.Length;
                for (int i = 0; i < len; i ++)
                {
                    resultSet.Add(mliqr.dwLinkID[i].ToString());
                }
            }
            else
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(
                    (int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
            return resultSet;
        }
    }
}

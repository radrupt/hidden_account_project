using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.CallDLL.interfaces;
using HieCiULib;
using System.Drawing;
using System.Collections;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_Matrix_Impl:Client_Matrix
    {
        Hashtable enumVS = new Hashtable();

        public HieClient_Matrix_Impl()
        {
            enumVS.Add(VideoStandard.NTSC, tmsdk.VideoStandard_t.StandardNTSC);
            enumVS.Add(VideoStandard.PAL, tmsdk.VideoStandard_t.StandardPAL);
            enumVS.Add(VideoStandard.SECAM, tmsdk.VideoStandard_t.StandardSECAM);
        }

        public void MatrixSetDisplayRegion(int channel, int regionCount)
        {
            try
            {
                MatrixSetDisplayRegion(channel, regionCount, 0, 0, 300, 300, Color.Red, 0);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void MatrixSetDisplayRegion(int channel, int regionCount, int left, int top, int width, int height, System.Drawing.Color color, int param)
        {
            tmsdk.REGION_PARAM rp = new tmsdk.REGION_PARAM();
            rp.left = left;
            rp.top = top;
            rp.width = width;
            rp.height = height;
            rp.color = CallDLL.Utilies.ColorToCOLORREF(color);
            rp.param = param;

            int nSetRegionCode = -1;                   //设置矩阵显示区域返回码
            nSetRegionCode = HieCIU.HieCIU_MatrixSetDisplayRegion(channel, regionCount, ref rp);
            if (nSetRegionCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetRegionCode));
            }
        }

        public void MatrixSetDisplaySignalStandard(int channel, VideoStandard vs)
        {
            if(enumVS.ContainsKey(vs)){
                int nSetSigStrdCode = -1;                   //设置矩阵卡显示通道的制式返回码
                nSetSigStrdCode = HieCIU.HieCIU_MatrixSetDisplaySignalStandard(channel, (tmsdk.VideoStandard_t)enumVS[vs]);
                if (nSetSigStrdCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nSetSigStrdCode));
                }
            }
            else
            {
                throw new Exception(
                    Constants.ErrorConstants.getErrorString((int)Constants.ErrorConstants.ClientError.ErrorParameter));
            }
        }

        public void MatrixSetDisplayPosition(int channel, int regionCount, int left, int top)
        {
            int nSetPositCode = -1;                   //调整矩阵显示位置返回码
            nSetPositCode = HieCIU.HieCIU_MatrixSetDisplayPosition(channel, regionCount, left, top);
            if (nSetPositCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetPositCode));
            }
        }

        public void MatrixClearRegion(int channel, int regionCount)
        {
            int nClrRegionCode = -1;                   //清除矩阵显示区域返回码
            nClrRegionCode = HieCIU.HieCIU_MatrixClearRegion(channel, regionCount);
            if (nClrRegionCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nClrRegionCode));
            }
        }

        public void MatrixFillRegion(int channel, int regionCount, string fileName)
        {
            int nFilRegionCode = -1;
            nFilRegionCode = HieCIU.HieCIU_MatrixFillRegion(channel,regionCount,fileName);
            if (nFilRegionCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nFilRegionCode));
            }
        }

        public void StreamSetDecVideoOut(IntPtr streamHandle, int port, bool isOpened, int channel, int region)
        {
            int nVideoOutCode = -1;                   //解码通道的视频外部输出返回码
            nVideoOutCode = HieCIU.HieCIU_StreamSetDecVideo(streamHandle, port, isOpened, channel, region);
            if (nVideoOutCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nVideoOutCode));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 矩阵显示视频制式 枚举类型
    /// </summary>
    public enum VideoStandard
    {
        NTSC = 0,
        PAL,
        SECAM
    }

    /// <summary>
    /// 矩阵解码操作的处理
    /// 
    /// Version:1.0
    /// Date:2012/06/25
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_Matrix
    {
        /// <summary>
        /// 设置矩阵显示区域 
        /// </summary>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="regionCount">区域个数</param>
        void MatrixSetDisplayRegion(int channel, int regionCount);

        /// <summary>
        /// 设置矩阵显示区域
        /// </summary>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="regionCount">区域个数</param>
        /// <param name="left">区域左边界</param>
        /// <param name="top">区域上边界</param>
        /// <param name="width">区域宽度</param>
        /// <param name="height">区域高度</param>
        /// <param name="color">区域颜色</param>
        /// <param name="param">区域扩展参数</param>
        void MatrixSetDisplayRegion(int channel,int regionCount,
            int left, int top, int width, int height, Color color, int param);

        /// <summary>
        /// 设置矩阵卡显示通道的制式
        /// </summary>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="vs">矩阵显示视频制式</param>
        void MatrixSetDisplaySignalStandard(int channel, VideoStandard vs);

        /// <summary>
        /// 调整矩阵显示位置 
        /// </summary>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="regionCount">区域个数</param>
        /// <param name="left">调整后的位置x坐标</param>
        /// <param name="top">调整后的位置y坐标</param>
        void MatrixSetDisplayPosition(int channel, int regionCount, int left, int top);

        /// <summary>
        /// 清除矩阵显示通道的某区域，显示使用接口 MatrixSetDisplayRegion 设置的矩阵显示区域背景色
        /// </summary>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="regionCount">区域个数</param>
        void MatrixClearRegion(int channel, int regionCount);

        /// <summary>
        /// 用自定义的图像填充显示区域 
        /// </summary>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="regionCount">区域个数</param>
        /// <param name="fileName">待填充的图像文件名。fileName 所指向图象 必须和接口 MatrixSetDisplayRegion 中设置的图像大小相同，否则图像无法正常显示.</param>
        void MatrixFillRegion(int channel, int regionCount, string fileName);

        /// <summary>
        /// 设置解码通道的视频外部输出（矩阵输出）。
        /// 把视频图像从 streamHandle 所使用的解码通道的第 port 路，送到第 channel 个显示通道的第 region 个区域。
        /// 此函数中 inPort 目前为 0（一路解码通道支持一路外部视频矩阵输出）。
        /// 如果 isOpened 为 false，则 channel、region 无意义。 
        /// </summary>
        /// <param name="streamHandle">流句柄，通过连接实时流或历史流获取</param>
        /// <param name="port">解码通道的输出端口</param>
        /// <param name="isOpend">true 开 false 关</param>
        /// <param name="channel">矩阵卡显示通道号</param>
        /// <param name="regionCount">要输出显示的区域</param>
        void StreamSetDecVideoOut(IntPtr streamHandle, int port, bool isOpened, int channel, int region);
    }
}

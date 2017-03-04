using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 其他操作处理过程
    /// 操作前，请先确保将UserInfo字段初始化
    /// 
    /// Version:1.0
    /// Date:2012/06/18
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_OtherOp
    {
        /// <summary>
        /// 用户登录句柄 
        /// </summary>
        IntPtr UserInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 通道编号（从 0 开始） 
        /// </summary>
        int PTZChannel
        {
            get;
            set;
        }

        /// <summary>
        /// 速度[ 0 - 10，0 表示默认速度，1 - 10 表示速度级别 ]
        /// </summary>
        int Speed
        {
            get;
            set;
        }
        
        /// <summary>
        /// 关闭（或断开）所有开关
        /// </summary>
        void Ptz_AllOff();

        #region 部件开关设置
        /// <summary>
        /// 接通摄像机电源
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_CameraPower(bool isOpen);

        /// <summary>
        /// 接通灯光电源
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_LightPower(bool isOpen);

        /// <summary>
        /// 接通雨刷开关
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Wiper(bool isOpen);

        /// <summary>
        /// 接通风扇开关
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Fans(bool isOpen);

        /// <summary>
        /// 接通加热器开关
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Heater(bool isOpen);

        /// <summary>
        /// 接通辅助设备开关
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_AuxEquiment(bool isOpen);
        #endregion

        /// <summary>
        /// 停止所有连续量（镜头，云台）动作
        /// </summary>
        void Ptz_StopContinue();

        #region 摄像头参数设置
        /// <summary>
        /// 焦距变大（倍率变大）
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_ZoomIn(bool isOpen);

        /// <summary>
        /// 焦距变小（倍率变小）
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_ZoomOut(bool isOpen);

        /// <summary>
        /// 焦点前调
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_FocusNear(bool isOpen);

        /// <summary>
        /// 焦点后调
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_FocusFar(bool isOpen);

        /// <summary>
        /// 光圈扩大
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_ApertureUp(bool isOpen);

        /// <summary>
        /// 光圈缩小
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_ApertureDown(bool isOpen);

        /// <summary>
        /// 开自动焦距（自动倍率）
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_AutoZoom(bool isOpen);

        /// <summary>
        /// 开自动调焦
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_AutoFocu(bool isOpen);

        /// <summary>
        /// 开自动光圈
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_AutoAperture(bool isOpen);
        #endregion

        #region 设备转向
        /// <summary>
        /// 云台上仰
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Up(bool isOpen);

        /// <summary>
        /// 云台下俯
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Down(bool isOpen);

        /// <summary>
        /// 云台左转
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Left(bool isOpen);

        /// <summary>
        /// 云台右转
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_Right(bool isOpen);

        /// <summary>
        /// 云台上仰和左转
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_UpLeft(bool isOpen);

        /// <summary>
        /// 云台上仰和右转
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_UpRight(bool isOpen);

        /// <summary>
        /// 云台下俯和左转
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_DownLeft(bool isOpen);

        /// <summary>
        /// 云台下俯和右转
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_DownRight(bool isOpen);

        /// <summary>
        /// 云台左右自动扫描
        /// </summary>
        /// <param name="isOpen">是否开始，true表示开始，false表示停止</param>
        void Ptz_AutoLeftRight(bool isOpen);
        #endregion

        #region 预置点
        /// <summary>
        /// 设置预置点
        /// </summary>
        /// <param name="preset">预置点序号 [ >=0 ] </param>
        void Ptz_PresetSet(int preset);

        /// <summary>
        /// 清除预置点
        /// </summary>
        /// <param name="preset">预置点序号 [ >=0 ] </param>
        void Ptz_PresetClear(int preset);

        /// <summary>
        /// 转到预置点
        /// </summary>
        /// <param name="preset">预置点序号 [ >=0 ] </param>
        void Ptz_PresetCall(int preset);
        #endregion

        #region 巡航
        /// <summary>
        /// 启动巡航记忆
        /// </summary>
        /// <param name="curiser">巡航路线号 [ >=0 ]</param>
        void Ptz_CuriserSetStart(int curiser);

        /// <summary>
        /// 关闭巡航记忆
        /// </summary>
        /// <param name="curiser">巡航路线号 [ >=0 ]</param>
        void Ptz_CuriserSetStop(int curiser);

        /// <summary>
        /// 将预置点加入巡航序列
        /// </summary>
        /// <param name="curiser">巡航路线号 [ >=0 ]</param>
        /// <param name="preset"></param>
        /// <param name="stopTime"></param>
        void Ptz_CuriserAddPreset(int curiser, int preset, int stopTime);

        /// <summary>
        /// 开始巡航
        /// </summary>
        /// <param name="curiser">巡航路线号 [ >=0 ]</param>
        void Ptz_CuriserRunStart(int curiser);

        /// <summary>
        /// 停止巡航
        /// </summary>
        /// <param name="curiser">巡航路线号 [ >=0 ]</param>
        void Ptz_CuriserRunStop(int curiser);
        #endregion

        #region 轨迹
        /// <summary>
        /// 启动轨迹记忆
        /// </summary>
        /// <param name="trace">轨迹号 [ >=0 ]</param>
        void Ptz_TraceSetStart(int trace);

        /// <summary>
        /// 关闭轨迹记忆
        /// </summary>
        /// <param name="trace">轨迹号 [ >=0 ]</param>
        void Ptz_TraceSetStop(int trace);

        /// <summary>
        /// 开始轨迹
        /// </summary>
        /// <param name="trace">轨迹号 [ >=0 ]</param>
        void Ptz_TraceRunStart(int trace);

        /// <summary>
        /// 停止轨迹
        /// </summary>
        /// <param name="trace">轨迹号 [ >=0 ]</param>
        void Ptz_TraceRunStop(int trace);
        #endregion

        /// <summary>
        /// 系统复位
        /// </summary>
        void Ptz_SystemReset();
    }
}

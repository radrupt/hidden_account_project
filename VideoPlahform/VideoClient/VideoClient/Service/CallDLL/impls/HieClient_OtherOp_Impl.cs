using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.CallDLL.interfaces;
using HieCiULib;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_OtherOp_Impl : Client_OtherOp
    {
        private IntPtr userInfo;
        private int ptzChannel;
        private int speed = 0;

        public IntPtr UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
            }
        }

        public int PTZChannel
        {
            get
            {
                return ptzChannel;
            }
            set
            {
                ptzChannel = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        public void Ptz_AllOff()
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeAllOff, 0, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_CameraPower(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeCameraPower, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_LightPower(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeLightPower, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Wiper(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeWiper, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Fans(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeFans, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Heater(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeHeater, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_AuxEquiment(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeAuxEquiment, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_StopContinue()
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeStopContinue, 0, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_ZoomIn(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeZoomIn, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_ZoomOut(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeZoomOut, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_FocusNear(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeFocusNear, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_FocusFar(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeFocusFar, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_ApertureUp(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeApertureUp, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_ApertureDown(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeApertureDown, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_AutoZoom(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeAutoZoom, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_AutoFocu(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeAutoFocus, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_AutoAperture(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeAutoAperture, isopen, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Up(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeUp, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Down(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeDown, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Left(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeLeft, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_Right(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeRight, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_UpLeft(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeUpLeft, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_UpRight(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeUpRight, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_DownLeft(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeDownLeft, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_DownRight(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeDownRight, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_AutoLeftRight(bool isOpen)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            int isopen = Convert.ToInt32(isOpen);
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeAutoLeftRight, isopen, speed, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_PresetSet(int preset)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码

            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodePresetSet, preset, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_PresetClear(int preset)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodePresetClear, preset, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_PresetCall(int preset)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodePresetCall, preset, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_CuriserSetStart(int curiser)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStart, curiser, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_CuriserSetStop(int curiser)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeCuriserSetStop, curiser, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_CuriserAddPreset(int curiser, int preset, int stopTime)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeCuriserAddPreset, curiser, preset, stopTime, speed);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_CuriserRunStart(int curiser)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeCuriserRunStart, curiser, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_CuriserRunStop(int curiser)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeCuriserRunStop, curiser, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_TraceSetStart(int trace)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeTraceSetStart, trace, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_TraceSetStop(int trace)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeTraceSetStop, trace, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_TraceRunStart(int trace)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStart, trace, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_TraceRunStop(int trace)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeTraceRunStop, trace, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }

        public void Ptz_SystemReset()
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码
            nPTZCtrlCode = HieCIU.HieCIU_PtzControl(
                (uint)userInfo.ToInt32(),
                (uint)ptzChannel,
                Common.ePTZControlCmdCode.ePTZControlCodeSystemReset, 0, 0, 0, 0);
            if (nPTZCtrlCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }
        }
    }
}

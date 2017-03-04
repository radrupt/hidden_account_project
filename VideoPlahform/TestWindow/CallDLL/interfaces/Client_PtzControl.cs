using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HieCiULib;
using HUSER = System.IntPtr;

namespace TestWindow.CallDLL.interfaces
{
    
    public interface Client_PtzControl
    {
        /// <summary>
        /// PTZ云台控制
        /// </summary>
        /// <param name="hUser">用户登录句柄</param>
        /// <param name="hUser">通道编号（从 0 开始）</param>
        /// <param name="hUser">PTZ 命令字</param>
        /// <param name="hUser">PTZ 命令参数 1</param>
        /// <param name="hUser">PTZ 命令参数 2</param>
        /// <param name="hUser">PTZ 命令参数 3</param>
        /// <param name="hUser">PTZ 命令参数 4</param>
        /// <remarks>参数错误或者是操作失败会以异常的形式抛出</remarks>
        void PtzControl(HUSER hUser, uint dwChnnl, Common.ePTZControlCmdCode ePTZCtrlCmdCode,
                    uint dwCmdCode1, uint dwCmdCode2, uint dwCmdCode3, uint dwCmdCode4);
    }
}

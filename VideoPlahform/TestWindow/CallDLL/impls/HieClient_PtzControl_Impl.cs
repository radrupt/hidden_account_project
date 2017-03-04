using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWindow.CallDLL.interfaces;
using HUSER = System.IntPtr;
using HieCiULib;

namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2013/12/02
    /// Author:wangd933
    /// </summary>

    public class HieClient_PtzControl_Impl : interfaces.Client_PtzControl
    {
        public void PtzControl(HUSER hUser, uint dwChnnl, Common.ePTZControlCmdCode ePTZCtrlCmdCode,
                    uint dwCmdCode1, uint dwCmdCode2, uint dwCmdCode3, uint dwCmdCode4)
        {
            int nPTZCtrlCode = -1;                   //PTZ云台控制返回码

           // nPTZCtrlCode = Unit.HieClient_PTZControl(hUser, dwChnnl, ePTZCtrlCmdCode,
           //     dwCmdCode1, dwCmdCode2, dwCmdCode3, dwCmdCode4);
            nPTZCtrlCode = Unit.HieClient_PTZControl(hUser,
                                          dwChnnl,
                                          ePTZCtrlCmdCode, 
                                          dwCmdCode1, 
                                          dwCmdCode2,
                                          dwCmdCode3,
                                          dwCmdCode4 );
            if (nPTZCtrlCode != 0)
            {

                throw new Exception(Constants.ErrorConstants.getErrorString(nPTZCtrlCode));
            }

        }
    }
}

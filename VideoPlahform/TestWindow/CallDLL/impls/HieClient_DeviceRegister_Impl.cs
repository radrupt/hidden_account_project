using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWindow.CallDLL.interfaces;
using HieCiULib;
using System.Collections;

namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_DeviceRegister_Impl:Client_DeviceRegister
    {
        public event DeviceRegistered DeviceRegister;

        private Common.CB_DeviceRegister callback_deviceRegisterEvent;

        public void OnDeviceRegistered(Hashtable e)
        {
            if (DeviceRegister != null)
            {
                DeviceRegister(e);
            }
        }

        private Hashtable cB_info = new Hashtable();

        public Hashtable CB_info
        {
            get { return cB_info; }
            set 
            { 
                if (!cB_info.Equals(value))
                {
                    OnDeviceRegistered(value);
                }
                cB_info = value;
            }
        }

        public void DeviceRegisterListenStart(string ipAddress, int port)
        {
            int nStartDevRegCode = -1;
            nStartDevRegCode = HieCIU.HieCIU_DeviceRegisterListenStart(ipAddress, (uint)port);
            if (nStartDevRegCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStartDevRegCode));
            }

            callback_deviceRegisterEvent = new Common.CB_DeviceRegister(callBack_DeviceRegister);
            int nSetDevRegCBCode = -1;
            nSetDevRegCBCode = HieCIU.HieCIU_DeviceRegisterCB(callback_deviceRegisterEvent);

            if (nSetDevRegCBCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nSetDevRegCBCode));
            }
        }

        public void DeviceRegisterListenStop()
        {
            int nStopDevRegCode = -1;
            nStopDevRegCode = HieCIU.HieCIU_DeviceRegisterListenStop();
            if (nStopDevRegCode != 0)
            {
                throw new Exception(Constants.ErrorConstants.getErrorString(nStopDevRegCode));
            }
        }

        private int callBack_DeviceRegister(ref HieCiULib.Common.DeviceRegisterInfo dri)
        {
            Hashtable info = new Hashtable();
            info.Add("ip", dri.cDeviceIP);
            info.Add("MAC", dri.cDeviceMAC);
            info.Add("Version",dri.cDeviceVersion);
            info.Add("CmdPort", dri.dwDeviceCmdPort);
            info.Add("ID", dri.dwDeviceID);
            info.Add("MAXConnect", dri.dwDeviceMaxConnect);
            info.Add("type", dri.dwDeviceType);
            info.Add("httpPort", dri.dwHTTPPort);
            
            CB_info = info;
            return 0;
        }
    }
}

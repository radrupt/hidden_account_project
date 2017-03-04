using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.Device
{
    /// <summary>
    /// 所有设备的统一接口,通过实现此抽象类中功能完成具体设备的相应功能
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public abstract class AbstractDevice
    {
        public abstract void Init();
        public abstract void SDKAttribute();
        public abstract void Login();
        public abstract void Logout();
        public abstract void DeviceConfig();
        public abstract void SubscribeEvent();
        public abstract void UnSubscribeEvent();
        public abstract void DeleteUserForce();
        public abstract void RealStream();
        public abstract void HistoryStream();
        public abstract void Query();
        public abstract void StreamControl();
        public abstract void RemoteControl();
        public abstract void OtherOp();
        public abstract void Matrix();
        public abstract void TransparentChannel();
        public abstract void DeviceRegister();
        public abstract void FileDownload();
        public abstract void FileUpload();
        public abstract void VoiceStream();
        public abstract void PtzControl();
    }
}

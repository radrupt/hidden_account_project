using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.Device;
using VideoClient.Service.Device.impls;

namespace VideoClient.Service.DeviceBuilder.impls
{
    /// <summary>
    /// 设备Hie_MGTC26对应的建造器
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    class Hie_MGTC26_Builder:AbstractDeviceBuilder
    {
        private AbstractDevice device;

        public override void Build_Init()
        {
            device = new Hie_MGTC26_Device();
            device.Init();
        }

        public override void Build_SDKAttribute()
        {
            device.SDKAttribute();
        }

        public override void Build_Login()
        {
            device.Login();
        }

        public override void Build_Logout()
        {
            device.Logout();
        }

        public override void Build_DeviceConfig()
        {
            device.DeviceConfig();
        }

        public override void Build_SubscribeEvent()
        {
            device.SubscribeEvent();
        }

        public override void Build_UnSubscribeEvent()
        {
            device.UnSubscribeEvent();
        }

        public override void Build_DeleteUserForce()
        {
            device.DeleteUserForce();
        }

        public override void Build_RealStream()
        {
            device.RealStream();
        }

        public override void Build_HistoryStream()
        {
            device.HistoryStream();
        }

        public override void Build_StreamControl()
        {
            device.StreamControl();
        }

        public override void Build_Query()
        {
            device.Query();
        }

        public override void Build_RemoteControl()
        {
            device.RemoteControl();
        }

        public override void Build_OtherOp()
        {
            device.OtherOp();
        }

        public override void Build_Matrix()
        {
            device.Matrix();
        }

        public override void Build_TransparentChannel()
        {
            device.TransparentChannel();
        }

        public override void Build_FileDownload()
        {
            device.FileDownload();
        }

        public override void Build_FileUpload()
        {
            device.FileUpload();
        }

        public override void Build_DeviceRegister()
        {
            device.DeviceRegister();
        }

        public override void Build_VoiceStream()
        {
            device.VoiceStream();
        }

        public override AbstractDevice getDevice()
        {
            return device;
        }

        public override void Build_PtzControl(){
            device.PtzControl();
        }
    }
}

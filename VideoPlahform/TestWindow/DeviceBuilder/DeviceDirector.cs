using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.DeviceBuilder
{
    /// <summary>
    /// 用于组装设备
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public class DeviceDirector
    {
        /// <summary>
        /// 此函数用于将设备对象组装成一个具体的设备
        /// </summary>
        /// <param name="builder">建造器</param>
        public void Construct(AbstractDeviceBuilder builder)
        {
            builder.Build_Init();
            builder.Build_Login();
            builder.Build_Logout();
            builder.Build_SDKAttribute();
            builder.Build_SubscribeEvent();
            builder.Build_UnSubscribeEvent();
            builder.Build_DeleteUserForce();
            builder.Build_DeviceConfig();
            builder.Build_RealStream();
            builder.Build_HistoryStream();
            builder.Build_StreamControl();
            builder.Build_Query();
            builder.Build_RemoteControl();
            builder.Build_OtherOp();
            builder.Build_Matrix();
            builder.Build_TransparentChannel();
            builder.Build_DeviceRegister();
            builder.Build_FileDownload();
            builder.Build_FileUpload();
            builder.Build_VoiceStream();
            builder.Build_PtzControl();
        }
    }
}

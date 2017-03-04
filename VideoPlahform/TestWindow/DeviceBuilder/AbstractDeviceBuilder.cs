using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWindow.Device;

namespace TestWindow.DeviceBuilder
{
    /// <summary>
    /// 采用了builder设计模式，对具体设备的具体功能整体抽象成一个设备，再此基础上进一步开发
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public abstract class AbstractDeviceBuilder
    {
        abstract public void Build_Init();
        abstract public void Build_SDKAttribute();
        abstract public void Build_Login();
        abstract public void Build_Logout();
        abstract public void Build_DeviceConfig();
        abstract public void Build_SubscribeEvent();
        abstract public void Build_UnSubscribeEvent();
        abstract public void Build_DeleteUserForce();
        abstract public void Build_RealStream();
        abstract public void Build_HistoryStream();
        abstract public void Build_StreamControl();
        abstract public void Build_Query();
        abstract public void Build_RemoteControl();
        abstract public void Build_OtherOp();
        abstract public void Build_Matrix();
        abstract public void Build_TransparentChannel();
        abstract public void Build_DeviceRegister();
        abstract public void Build_FileDownload();
        abstract public void Build_FileUpload();
        abstract public void Build_VoiceStream();
        abstract public void Build_PtzControl();
        //不断增加中...,这边增加的话，相应的DeviceDirector也要相应的增加,

        /// <summary>
        /// 返回设备对象
        /// </summary>
        /// <returns>设备对象</returns>
        abstract public AbstractDevice getDevice();
    }
}

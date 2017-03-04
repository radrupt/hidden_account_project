using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWindow.CallDLL.interfaces;
using TestWindow.CallDLL.impls;

namespace TestWindow.Device.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public class Hie_MGTC26_Device : AbstractDevice
    {
        private Client_Init client_init;
        private Client_UserLogin client_userLogin;
        private Client_UserLogout client_userLogout;
        private Client_DeleteUserForceCB client_deleteUserForceCB;
        private Client_DeviceConfig client_deviceConfig;
        private Client_SDKAttribute client_sdkAttribute;
        private Client_SubscribeEvent client_subscribeEvent;
        private Client_UnSubscribeEvent client_unSubscribeEvent;
        private Client_RealStream client_realStream;
        private Client_HistoryStream client_histroyStream;
        private Client_StreamControl client_streamControl;
        private Client_Query client_query;
        private Client_RemoteControl client_remoteControl;
        private Client_OtherOp client_otherOp;
        private Client_Matrix client_matrix;
        private Client_TransparentChannel client_transparentChannel;
        private Client_DeviceRegister client_deviceRegister;
        private Client_FileDownload client_fileDownload;
        private Client_FileUpload client_fileUpload;
        private Client_VoiceStream client_voiceStream;
        private Client_PtzControl client_ptzControl;
        
        public Client_StreamControl Client_streamControl
        {
            get { return client_streamControl; }
            set { client_streamControl = value; }
        }

        public Client_Init Client_init
        {
            get { return client_init; }
            set { client_init = value; }
        }
        
        public Client_UserLogin Client_userLogin
        {
            get { return client_userLogin; }
            set { client_userLogin = value; }
        }

        public Client_UserLogout Client_userLogout
        {
            get { return client_userLogout; }
            set { client_userLogout = value; }
        }
        
        public Client_DeleteUserForceCB Client_deleteUserForceCB
        {
            get { return client_deleteUserForceCB; }
            set { client_deleteUserForceCB = value; }
        }

        public Client_DeviceConfig Client_deviceConfig
        {
            get { return client_deviceConfig; }
            set { client_deviceConfig = value; }
        }

        public Client_SDKAttribute Client_sdkAttribute
        {
            get { return client_sdkAttribute; }
            set { client_sdkAttribute = value; }
        }

        public Client_SubscribeEvent Client_subscribeEvent
        {
            get { return client_subscribeEvent; }
            set { client_subscribeEvent = value; }
        }

        public Client_UnSubscribeEvent Client_unSubscribeEvent
        {
            get { return client_unSubscribeEvent; }
            set { client_unSubscribeEvent = value; }
        }

        public Client_RealStream Client_realStream
        {
            get { return client_realStream; }
            set { client_realStream = value; }
        }

        public Client_HistoryStream Client_historyStream
        {
            get { return client_histroyStream; }
            set { client_histroyStream = value; }
        }

        public Client_Query Client_query
        {
            get { return client_query; }
            set { client_query = value; }
        }

        public Client_RemoteControl Client_remoteControl
        {
            get { return client_remoteControl; }
            set { client_remoteControl = value; }
        }

        public Client_OtherOp Client_otherOp
        {
            get { return client_otherOp; }
            set { client_otherOp = value; }
        }

        public Client_Matrix Client_matrix 
        {
            get { return client_matrix; }
            set { client_matrix = value; }
        }

        public Client_TransparentChannel Client_transparentChannel
        {
            get { return client_transparentChannel; }
            set { client_transparentChannel = value; }
        }

        public Client_FileDownload Client_fileDownload
        {
            get { return client_fileDownload; }
            set { Client_fileDownload = value; }
        }

        public Client_FileUpload Client_fileUpload
        {
            get { return client_fileUpload; }
            set { Client_fileUpload = value; }
        }

        public Client_DeviceRegister Client_deviceRegister
        {
            get { return client_deviceRegister; }
            set { client_deviceRegister = value; }
        }

        public Client_VoiceStream Client_voiceStream
        {
            get { return client_voiceStream; }
            set { client_voiceStream = value; }
        }
        public Client_PtzControl Client_ptzControl
        {
            get { return client_ptzControl; }
            set { client_ptzControl = value; }
        }
        public override void Init()
        {
            client_init = new HieClient_Init_Impl();
        }

        public override void SDKAttribute()
        {
            client_sdkAttribute = new HieClient_SDKAttribute_Impl();
        }

        public override void Login()
        {
            client_userLogin = new HieClient_UserLogin_Impl();
        }

        public override void Logout()
        {
            client_userLogout = new HieClient_UserLogout_Impl();
        }

        public override void DeviceConfig()
        {
            client_deviceConfig = new HieClient_DeviceConfig_Impl();
        }

        public override void SubscribeEvent()
        {
            client_subscribeEvent = new HieClient_SubscribeEvent_Impl();
        }

        public override void UnSubscribeEvent()
        {
            client_unSubscribeEvent = new HieClient_UnSubscribeEvent_Impl();
        }

        public override void DeleteUserForce()
        {
            client_deleteUserForceCB = new HieClient_DeleteUserForceCB_Impl();
        }

        public override void RealStream()
        {
            client_realStream = new HieClient_RealStream_Impl();
        }

        public override void HistoryStream()
        {
            client_histroyStream = new HieClient_HistoryStream_Impl();
        }

        public override void StreamControl()
        {
            client_streamControl = new HieClient_StreamControl_Impl();
        }

        public override void Query()
        {
            client_query = new HieClient_Query_Impl();
        }

        public override void RemoteControl()
        {
            client_remoteControl = new HieClient_RemoteControl_Impl();
        }

        public override void OtherOp()
        {
            client_otherOp = new HieClient_OtherOp_Impl();
        }

        public override void Matrix()
        {
            client_matrix = new HieClient_Matrix_Impl();
        }

        public override void TransparentChannel()
        {
            client_transparentChannel = new HieClient_TransparentChannel_Impl();
        }

        public override void DeviceRegister()
        {
            client_deviceRegister = new HieClient_DeviceRegister_Impl();
        }

        public override void FileDownload()
        {
            client_fileDownload = new HieClient_FileDownload_Impl();
        }

        public override void FileUpload()
        {
            client_fileUpload = new HieClient_FileUpload_Impl();
        }

        public override void VoiceStream()
        {
            client_voiceStream = new HieClient_VoiceStream_Impl();
        }
        public override void PtzControl()
        {
            client_ptzControl = new HieClient_PtzControl_Impl();
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoClient.DeviceServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://video.pandawork.net/device/", ConfigurationName="DeviceServiceReference.DeviceService")]
    public interface DeviceService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/addDevice", ReplyAction="http://video.pandawork.net/device/DeviceService/addDeviceResponse")]
        bool addDevice(VideoCommon.com.pandawork.common.entity.Device device);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/addDevice", ReplyAction="http://video.pandawork.net/device/DeviceService/addDeviceResponse")]
        System.Threading.Tasks.Task<bool> addDeviceAsync(VideoCommon.com.pandawork.common.entity.Device device);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getDeviceById", ReplyAction="http://video.pandawork.net/device/DeviceService/getDeviceByIdResponse")]
        VideoCommon.com.pandawork.common.dto.DeviceDTO getDeviceById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getDeviceById", ReplyAction="http://video.pandawork.net/device/DeviceService/getDeviceByIdResponse")]
        System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO> getDeviceByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/updateDevice", ReplyAction="http://video.pandawork.net/device/DeviceService/updateDeviceResponse")]
        void updateDevice(VideoCommon.com.pandawork.common.entity.Device device);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/updateDevice", ReplyAction="http://video.pandawork.net/device/DeviceService/updateDeviceResponse")]
        System.Threading.Tasks.Task updateDeviceAsync(VideoCommon.com.pandawork.common.entity.Device device);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/delDevice", ReplyAction="http://video.pandawork.net/device/DeviceService/delDeviceResponse")]
        void delDevice(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/delDevice", ReplyAction="http://video.pandawork.net/device/DeviceService/delDeviceResponse")]
        System.Threading.Tasks.Task delDeviceAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getAllDevices", ReplyAction="http://video.pandawork.net/device/DeviceService/getAllDevicesResponse")]
        VideoCommon.com.pandawork.common.dto.DeviceDTO[] getAllDevices();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getAllDevices", ReplyAction="http://video.pandawork.net/device/DeviceService/getAllDevicesResponse")]
        System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO[]> getAllDevicesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getDevicesOnMap", ReplyAction="http://video.pandawork.net/device/DeviceService/getDevicesOnMapResponse")]
        VideoCommon.com.pandawork.common.dto.DeviceDTO[] getDevicesOnMap(int mapId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getDevicesOnMap", ReplyAction="http://video.pandawork.net/device/DeviceService/getDevicesOnMapResponse")]
        System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO[]> getDevicesOnMapAsync(int mapId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getDevicesByRegionId", ReplyAction="http://video.pandawork.net/device/DeviceService/getDevicesByRegionIdResponse")]
        VideoCommon.com.pandawork.common.dto.DeviceDTO[] getDevicesByRegionId(int regionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://video.pandawork.net/device/DeviceService/getDevicesByRegionId", ReplyAction="http://video.pandawork.net/device/DeviceService/getDevicesByRegionIdResponse")]
        System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO[]> getDevicesByRegionIdAsync(int regionId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DeviceServiceChannel : VideoClient.DeviceServiceReference.DeviceService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DeviceServiceClient : System.ServiceModel.ClientBase<VideoClient.DeviceServiceReference.DeviceService>, VideoClient.DeviceServiceReference.DeviceService {
        
        public DeviceServiceClient() {
        }
        
        public DeviceServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DeviceServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DeviceServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DeviceServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool addDevice(VideoCommon.com.pandawork.common.entity.Device device) {
            return base.Channel.addDevice(device);
        }
        
        public System.Threading.Tasks.Task<bool> addDeviceAsync(VideoCommon.com.pandawork.common.entity.Device device) {
            return base.Channel.addDeviceAsync(device);
        }
        
        public VideoCommon.com.pandawork.common.dto.DeviceDTO getDeviceById(int id) {
            return base.Channel.getDeviceById(id);
        }
        
        public System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO> getDeviceByIdAsync(int id) {
            return base.Channel.getDeviceByIdAsync(id);
        }
        
        public void updateDevice(VideoCommon.com.pandawork.common.entity.Device device) {
            base.Channel.updateDevice(device);
        }
        
        public System.Threading.Tasks.Task updateDeviceAsync(VideoCommon.com.pandawork.common.entity.Device device) {
            return base.Channel.updateDeviceAsync(device);
        }
        
        public void delDevice(int id) {
            base.Channel.delDevice(id);
        }
        
        public System.Threading.Tasks.Task delDeviceAsync(int id) {
            return base.Channel.delDeviceAsync(id);
        }
        
        public VideoCommon.com.pandawork.common.dto.DeviceDTO[] getAllDevices() {
            return base.Channel.getAllDevices();
        }
        
        public System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO[]> getAllDevicesAsync() {
            return base.Channel.getAllDevicesAsync();
        }
        
        public VideoCommon.com.pandawork.common.dto.DeviceDTO[] getDevicesOnMap(int mapId) {
            return base.Channel.getDevicesOnMap(mapId);
        }
        
        public System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO[]> getDevicesOnMapAsync(int mapId) {
            return base.Channel.getDevicesOnMapAsync(mapId);
        }
        
        public VideoCommon.com.pandawork.common.dto.DeviceDTO[] getDevicesByRegionId(int regionId) {
            return base.Channel.getDevicesByRegionId(regionId);
        }
        
        public System.Threading.Tasks.Task<VideoCommon.com.pandawork.common.dto.DeviceDTO[]> getDevicesByRegionIdAsync(int regionId) {
            return base.Channel.getDevicesByRegionIdAsync(regionId);
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VideoClient.RegionServiceReference;
using VideoClient.DeviceServiceReference;
using VideoClient.View.Device;
using VideoCommon.com.pandawork.common.dto;
using VideoCommon.com.pandawork.common.entity;
namespace VideoClient.ViewModel
{
    public class RegionManageViewModel : ViewModelBase
    {
        #region 成员变量
        RegionServiceClient client;
        IList<DeviceDTO> deviceList;
        IList<Device> deviceProbeList;
        Boolean addDeviceButtonIsEnabled = false;
        TreeView treeView;//当前的区域树
        #endregion

        #region 属性
        public ObservableCollection<RegionViewModel> RegionTree { get; private set; }    //树里的节点数据
        public NodeInfo SelectNodeInfo { get; private set; }  //区域树里被选中的节点

        public IList<DeviceDTO> DeviceList
        {
            get { return deviceList; }
            set { deviceList = value; }
        }

        public IList<Device> DeviceProbeList
        {
            get { return deviceProbeList; }
            set { deviceProbeList = value; }
        }

        public Boolean AddDeviceButtonIsEnabled
        {
            get {
                return addDeviceButtonIsEnabled;
            }
            set {
                addDeviceButtonIsEnabled = value;              
            }
        }

        #endregion
        
        #region 命令
        public RelayCommand<RoutedPropertyChangedEventArgs<object>> 
            SelectedItemChangedCommand { get; private set; }
        public RelayCommand<Object> ReNameCommand { get; private set; }  //更改树节点名
        public RelayCommand AddCommand { get; private set; }     //添加树节点名
        public RelayCommand DelCommand { get; private set; }     //删除树节点名
        public RelayCommand<Window> AddDeviceCommand { get; private set; }
        public RelayCommand EditDeviceCommand { get; private set; }
        public RelayCommand DelDeviceCommand { get; private set; }
        #endregion

        

        #region 命令对应的事件
        /// <summary>
        /// 当用户选择区域树某一项相应的操作
        /// </summary>
        /// <param name="e"></param>
        private void selectedItemChangedHandler(RoutedPropertyChangedEventArgs<object> e)
        {
            treeView = e.Source as TreeView;
            TreeViewItem destNode = e.Source as TreeViewItem;
            SelectNodeInfo.SelectedNode = treeView.SelectedValue as RegionViewModel;

            DeviceServiceClient deviceClient = new DeviceServiceClient();
            deviceList = deviceClient.getDevicesByRegionId(SelectNodeInfo.SelectedNode.RegionDTO.Id);
            deviceClient.Close();
        }
        private void ReNameCommandHandler(Object e)
        {
            //待做
        }
        private void AddCommandHandler()
        {
            if (treeView == null)//如果没有选择节点,则不做操作
            {
                MessageBox.Show("请选择区域");
                return; 
            }
            Region childRegion = new Region();
            childRegion.Name = SelectNodeInfo.SelectedNode.Name + "-" + (SelectNodeInfo.SelectedNode.Childs.Count + 1);
            childRegion.Pid = SelectNodeInfo.SelectedNode.RegionDTO.Id;
            //向服务器添加信息
            client = new RegionServiceClient();
            client.addRegion(childRegion);
            client.Close();

            SelectNodeInfo.SelectedNode.Append(
                new RegionViewModel(SelectNodeInfo.SelectedNode.Name + "-" + 
                    (SelectNodeInfo.SelectedNode.Childs.Count+1),SelectNodeInfo.SelectedNode.Level + 1,
                     SelectNodeInfo.SelectedNode, SelectNodeInfo));
            
        }
        private void DelCommandHandler()
        {
            if (treeView == null)//如果没有选择节点,则不做操作
            {
                MessageBox.Show("请选择区域");
                return;
            }
            client = new RegionServiceClient();
            client.delRegion(SelectNodeInfo.SelectedNode.RegionDTO.Id);
            client.Close();
            if (SelectNodeInfo.SelectedNode.Parent != null)
                SelectNodeInfo.SelectedNode.Parent.Childs.Remove(SelectNodeInfo.SelectedNode); //这样会有问题,字节点没有删除
            
           
        }

        /// <summary>
        /// 添加某区域设备事件
        /// </summary>
        /// <param name="window"></param>
        private void AddDeviceCommandHandler(Window window)
        {
            #region xx
            /*new Device
                    {
                        Name = addDeviceView.DeviceName.Text,
                        Ip = addDeviceView.DeviceIp.Text,
                        CmdPort = addDeviceView.DeviceCmd.Text,
                        LoginName = addDeviceView.DeviceUserName.Text,
                        Pwd = addDeviceView.DeviceUserPass.Text,
                        RegionId = SelectNodeInfo.SelectedNode.RegionDTO.Id
                        //可能添加设备的所需信息要多些
                    }*/
            //没有做锁定当前屏幕使用户只能做操作此AddDeviceView window
            //缺少验证功能,如果要添加,可以在这里操作AddDeviceView
            #endregion
            
            AddDeviceView addDeviceView = (window as AddDeviceView);
            if (treeView == null) { MessageBox.Show("未选区域"); return; };

            #region 向服务器发送新添加的设备
            try
            {
                DeviceServiceClient deviceServiceClient = new DeviceServiceClient();
                deviceServiceClient.addDevice(addDeviceView.Device);
                deviceServiceClient.Close();
            }
            catch (Exception e) {
                throw new Exception("添加设备失败："+e.Message);
            }
            #endregion

            #region 向当前区域设备列表里添加新设备,这一步应该是由服务器推送DeviceDTO消息完成的,暂时先本地实现
            DeviceList.Add(
                new DeviceDTO { 
                    DeviceName = addDeviceView.DeviceName.Text,
                    Ip = addDeviceView.DeviceIp.Text,
                    CmdPort = addDeviceView.DeviceCmd.Text,
                    LoginName = addDeviceView.DeviceUserName.Text,
                    Pwd = addDeviceView.DeviceUserPass.Text
            });
            #endregion

            //关闭添加设备窗口
            addDeviceView.Close();

        }

        private void EditDeviceCommandHandler()
        {

        }
        private void DelDeviceCommandHandler()
        {

        }
        #endregion

        #region 构造方法
        public RegionManageViewModel()
        {

            #region 绑定事件
            SelectedItemChangedCommand = 
                new RelayCommand<RoutedPropertyChangedEventArgs<object>>(e => selectedItemChangedHandler(e));
            ReNameCommand = new RelayCommand<Object>((e) => ReNameCommandHandler(e));
            AddCommand    = new RelayCommand(() => AddCommandHandler());
            DelCommand    = new RelayCommand(() => DelCommandHandler());
            AddDeviceCommand = new RelayCommand<Window>((e) => AddDeviceCommandHandler(e));
            EditDeviceCommand = new RelayCommand(() => EditDeviceCommandHandler());
            DelDeviceCommand = new RelayCommand(() => DelDeviceCommandHandler());
            #endregion

            SelectNodeInfo = new NodeInfo();
            RegionTree = new ObservableCollection<RegionViewModel>();
            try
            {
                //RegionViewModel dd = new RegionViewModel("cc", 1, null, SelectNodeInfo);
                //RegionTree.Add(dd);
                client = new RegionServiceClient();

                RegionTree.Add(CreateTree(client.createTreeById(0, 1), 0, null, SelectNodeInfo));
                client.Close();
                SelectNodeInfo.SelectedNodeChanged += (s, e) => RefreshCommands();
            }
            catch (Exception e) {
              //  MessageBox.Show(e.Message+"ccccccccccccc");
            }
        }
        #endregion
        
        #region 一般方法
        private void RefreshCommands()
        {
                SelectedItemChangedCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region 初始化region树数据
        private RegionViewModel CreateTree(RegionDTO region, int level, RegionViewModel parent, NodeInfo nodeInfo)
        {
            RegionViewModel regionViewModel = new RegionViewModel(region.Name, level, parent, nodeInfo);
            regionViewModel.RegionDTO = region;
            foreach (var item in region.Children)
            {
                regionViewModel.Childs.Add(CreateTree(item, level + 1, regionViewModel, nodeInfo));
            }
            return regionViewModel;
        }
        #endregion

        #region

        #endregion

        #region INotifyPropertyChanged Members

        #endregion
    }
   
    //表示当前选择的区域树被选的项
    public class NodeInfo
    {
        RegionViewModel selectedNode;

        public RegionViewModel SelectedNode
        {
            get { return selectedNode; }
            set
            {
                if (selectedNode != value)
                {
                    selectedNode = value;
                    OnSelectedNodeChanged();
                }
            }
        }

        public event EventHandler SelectedNodeChanged;

        protected virtual void OnSelectedNodeChanged()
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, EventArgs.Empty);
        }

        #region INotifyPropertyChanged Members

        #endregion
    }
}

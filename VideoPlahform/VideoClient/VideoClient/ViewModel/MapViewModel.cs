using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using VideoCommon.com.pandawork.common.dto;
using System.Windows.Input;
using System.Threading;
using VideoCommon.com.pandawork.common.entity;
using VideoClient.View.Map;
using VideoClient.RegionServiceReference;
using VideoClient.MapServiceReference;
using VideoClient.DeviceServiceReference;
using VideoClient.UpLoadServiceReference;
using System.Net;
using System.ComponentModel;
using System.Collections;
using VideoClient.Pandawork;
using log4net;

namespace VideoClient.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region 属性以及get/set方法
        //地图树的绑定
        private List<MapDTO> mapTree;
        public List<MapDTO> MapTree
        {
            get { return mapTree; }
            set { mapTree = value; }
        }
        //地图预览面板
        private Canvas imageCanvas;
        public Canvas ImageCanvas
        {
            get { return imageCanvas; }
            set { imageCanvas = value; }
        } 
        //鼠标点击的位置，x,y坐标
        private Point mouseDownPoint;
        public Point MouseDownPoint
        {
            get { return mouseDownPoint; }
            set { mouseDownPoint = value; }
        }
        //添加地图对应的mapDto
        private MapDTO newMapDto;
        public MapDTO NewMapDto
        {
            get { return newMapDto; }
            set { newMapDto = value; }
        }
        //要添加的设备对应的实体

        //记录地图存储路径（太长了）
        public  string mapPath;
        //记录当前选中节点
        public MapDTO currentNode;

        private FileUploadMessage fum;
        private UpLoadServiceClient usc;
        private Thread _uploadWatchThread;

        #endregion

        #region 命令及其处理方法
        //选中节点改变的命令
        public RelayCommand<RoutedPropertyChangedEventArgs<object>> SelectedItemChangedCommand { get; private set; }
        private void selectedItemChangedHandler(RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tv = e.Source as TreeView;
            MapDTO mm = tv.SelectedValue as MapDTO;
            this.currentNode = mm;
            if (File.Exists(mapPath + mm.ImageName))
            {
                BitmapImage mapImage = new BitmapImage(new Uri(mapPath + mm.ImageName));
                this.ImageCanvas.Background = new ImageBrush(mapImage);
                initCanvas(mm.Id);
            }
            else
            {
                MessageBox.Show("没有找到地图");
            }
        }
        //重命名map节点
        public RelayCommand<Int32> renameCommand { get; private set; }
        private void renameCommandHandler(Int32 id)
        {
            MessageBox.Show("sdfc");
        }
        //删除map节点
        public RelayCommand<Int32> delMapCommand { get; private set; }
        private void delMapCommandHandler(Int32 id)
        {

        }
        //获取鼠标点击位置的命令及其处理函数
        public RelayCommand<object> getMousePointCommond { get; private set; }
        private void getMousePointCommondHandler(object e)
        {
            RoutedEventArgs re = e as RoutedEventArgs;
            this.MouseDownPoint = Mouse.GetPosition(re.Source as FrameworkElement);
        }
        //添加地图的对话框中添加地图确定按钮处理命令
        public RelayCommand<Window> addMapCommond { get; private set; }
        private void addMapCommondHandler(Window e)
        {
            var filepath = NewMapDto.ImageName;
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
                return;  
            //如果是添加管理中心的按钮
            if ((e as AddMapDialog).Tag == null)
            {
                NewMapDto.Posx = Int32.Parse(MouseDownPoint.X.ToString());
                NewMapDto.Posy = Int32.Parse(MouseDownPoint.Y.ToString());
            }
            upLoadMap(filepath);
            e.Close();
            if (!_uploadWatchThread.Join(500))
            {
                try
                {
                    _uploadWatchThread.Abort();
                }
                catch
                { }
            }              
        }
        //添加设备的对话款中添加设备确定按钮处理命令
        public RelayCommand<Window> addDeviceCommond { get; private set; }
        private void addDeviceCommondHandler(Window window)
        {
            Device device = ((window as AddDeviceDialog).deviceComboBox.SelectedItem as ComboBoxItem).Tag as Device;
            device.Posx = Int32.Parse(MouseDownPoint.X.ToString());
            device.Posy = Int32.Parse(MouseDownPoint.Y.ToString());
            window.Close();
            addIcoOnCanvas(false, device, device.Posx, device.Posy) ;
        }

        #endregion

        #region 构造函数以及前台调用的方法

        public MapViewModel()
        {
            DownloadMap.downloadMaps();
            this.mapPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Properties.Settings.Default.NAME + "\\maps\\";
            mapTree = new List<MapDTO>();
            MapDTO mapDto = createTreeById(0);
            mapTree.Add(mapDto);            
            SelectedItemChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<object>>(e => selectedItemChangedHandler(e));
            renameCommand = new RelayCommand<Int32>(e => renameCommandHandler(e));
            delMapCommand = new RelayCommand<int>(e => delMapCommandHandler(e));
            getMousePointCommond = new RelayCommand<object>(e => getMousePointCommondHandler(e));
            addMapCommond = new RelayCommand<Window>(e => addMapCommondHandler(e));
            addDeviceCommond = new RelayCommand<Window>(e => addDeviceCommondHandler(e));
        }
        public void initAddDeviceDialog(ComboBox comboBox)
        {
            //TODO向服务器发请求去拿区域列表假设用户id是1
            List<Region> regions = getRegionByUserId(Pandawork.Cookie.UserId);
            if (regions.Count != 0)
            {
                ComboBoxItem comboxItem;
                comboxItem = new ComboBoxItem();
                comboxItem.Content = "请选择";
                comboBox.Items.Add(comboxItem);
                comboBox.SelectedIndex = 0;
                foreach (Region region in regions)
                {
                    comboxItem = new ComboBoxItem();
                    comboxItem.Content = region.Name;
                    comboxItem.Tag = region.Id;
                    comboBox.Items.Add(comboxItem);
                }
            }
            else
            {
                ComboBoxItem comboxItem;
                comboxItem = new ComboBoxItem();
                comboxItem.Content = "暂无数据";
                comboBox.Items.Add(comboxItem);
                comboBox.SelectedIndex = 0;
            }
        }
        public void initAddDeviceDialogDevices(ComboBox comboBox,Int32 regionId)
        {
            List<Device> devices = getDeviceByRegionId(regionId);
            if (devices.Count != 0)
            {
                ComboBoxItem comboxItem;
                comboxItem = new ComboBoxItem();
                comboxItem.Content = "请选择";
                comboBox.Items.Add(comboxItem);
                comboBox.SelectedIndex = 0;
                foreach (Device device in devices)
                {
                    comboxItem = new ComboBoxItem();
                    comboxItem.Content = device.Name;
                    comboxItem.Tag = device;
                    comboBox.Items.Add(comboxItem);
                }
            }
            else
            {
                ComboBoxItem comboxItem;
                comboxItem = new ComboBoxItem();
                comboxItem.Content = "暂无数据";
                comboBox.Items.Add(comboxItem);
                comboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #region 本程序需要用的私有方法
        //向当前面板上加载已经存在的设备和地图
        public void initCanvas(Int32 mapId)
        {
            if (this.currentNode.Children != null && this.currentNode.Children.Count != 0)
            {
                foreach (MapDTO map in this.currentNode.Children)
                {
                    addIcoOnCanvas(true, map, map.Posx, map.Posy);
                }
            }

            List<DeviceDTO> deviceList = getDevicesOnMap(mapId);
            if (deviceList != null && deviceList.Count != 0)
            {
                foreach (DeviceDTO device in deviceList)
                {
                    addIcoOnCanvas(false, device, device.Posx, device.Posy);
                }
            }
        }
        private void addIcoOnCanvas(bool isMap, object o,int x,int y)
        {
            BitmapImage image;
            if (isMap)
            {
                image = new BitmapImage(new Uri("../../Skin/img/mao_ico.png", UriKind.Relative));
            }
            else
            {
                image = new BitmapImage(new Uri("../../Skin/img/video.png", UriKind.Relative));
            }
            Image mapIco = new Image();
            mapIco.Source = image;
            mapIco.Tag = o;
            Canvas.SetLeft(mapIco, x);
            Canvas.SetTop(mapIco, y);
            this.ImageCanvas.Children.Add(mapIco);
        }

        #endregion

        #region 调用小熊的接口

        /// <summary>
        /// 根据树的节点id构造以这个节点为根节点的树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private MapDTO createTreeById(Int32 id)
        {
            //MapDTO result = new MapDTO();
            //result.Id = 0;
            //result.Name = "管理中心";
            try
            {
                MapServiceClient ms = new MapServiceClient();
                MapDTO result = ms.createTreeById(id);
                log.Debug("createTreeById方法" + id + "返回值id：" + result.Id);
                return result;
            }
            catch (Exception e)
            {
                log.Debug("createTreeById方法" + id + " 抛出异常：" + e);
                return null;
            }
        }

        /// <summary>
        /// 根据地图id获取地图上所有的设备列表
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        private List<DeviceDTO> getDevicesOnMap(Int32 mapId)
        {
            try
            {
                DeviceServiceClient ds = new DeviceServiceClient();
                List<DeviceDTO> result = new List<DeviceDTO>();
                object[] temp = ds.getDevicesOnMap(mapId);
                log.Debug("getDevicesOnMap " + mapId + "得到数据：" + temp.Length + " 个");
                if (temp != null && temp.Length != 0)
                {
                    foreach (DeviceDTO d in temp)
                    {
                        log.Debug("getDevicesOnMap result:" + d.Id);
                        result.Add(d);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                log.Debug("getDevicesOnMap抛出异常：" + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据用户id获取用户有权限的区域列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<Region> getRegionByUserId(Int32 userId)
        {
            List<Region> regions = new List<Region>();
            Region r1 = new Region();
            r1.Id = 1;
            r1.Name = "区域1";
            r1.Pid = 0;
            regions.Add(r1);
            Region r2 = new Region();
            r2.Id = 1;
            r2.Name = "区域1";
            r2.Pid = 0;
            regions.Add(r2);
            Region r3 = new Region();
            r3.Id = 1;
            r3.Name = "区域1";
            r3.Pid = 0;
            regions.Add(r3);
            return regions;
        }
        /// <summary>
        /// 根据区域id 获取区域下所有设备
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        private List<Device> getDeviceByRegionId(Int32 regionId)
        {
            List<Device> result = new List<Device>();
            Device d = new Device();
            d.Id = 1;
            d.Name = "设备1";
            result.Add(d);
            Device d2 = new Device();
            d2.Id = 1;
            d2.Name = "设备2";
            result.Add(d2);
            Device d3 = new Device();
            d3.Id = 1;
            d3.Name = "设备3";
            result.Add(d3);
            return result;
        }
        
        private Boolean upLoadMap(String filepath)
        {
            try
            {
                fum = new FileUploadMessage();
                fum.FileName = Path.GetFileName(filepath);
                fum.FileData = File.OpenRead(filepath);
                fum.FileSize = (int)fum.FileData.Length;
                fum.FileUniqueID = Guid.NewGuid().ToString();
                usc = new UpLoadServiceClient();
                usc.UploadFileAsync(fum.FileName, fum.FileSize, fum.FileUniqueID, "", fum.FileData);
                _uploadWatchThread = new Thread(UploadFileInfo);
                _uploadWatchThread.Start(fum);
                usc.UploadFileCompleted += new EventHandler<AsyncCompletedEventArgs>(usc_UploadFileCompleted);

                return true;
            }
            catch (Exception e)
            {
                log.Debug("upLoadMap抛出异常：" + e);
                return false;
            }
        }
        //监视上传过程
        private void UploadFileInfo(object obj)
        {
            log.Debug("开始上传");            
        }
        //上传完成
        void usc_UploadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("上传完成");
            try
            {
                object[] uploadSize = usc.GetUploadFileInfo(fum.FileUniqueID);
                this.NewMapDto.ImageName = uploadSize[1].ToString();
                log.Debug("上传完成，返回图片名称是：" + this.newMapDto.ImageName);

                Map tempMap = new Map();
                tempMap.Name = this.newMapDto.Name;
                tempMap.Pid = this.NewMapDto.Pid;
                tempMap.Posx = this.NewMapDto.Posx;
                tempMap.Posy = this.NewMapDto.Posy;
                tempMap.ImageName = this.NewMapDto.ImageName;
                MapServiceClient msc = new MapServiceClient();
                if (this.NewMapDto.Id != 0)
                {
                    msc.addMap(tempMap);
                    log.Debug("添加子地图节点完成；" + tempMap.Id + "  " + tempMap.Name + "  " + tempMap.ImageName);
                    addIcoOnCanvas(true, this.newMapDto, this.newMapDto.Posx, this.newMapDto.Posy);
                    DownloadMap.downloadMaps();
                }
                else
                {
                    msc.updateMap(tempMap);
                    log.Debug("更新根节点完成；" + tempMap.Id + "  " + tempMap.Name + "  " + tempMap.ImageName);
                    DownloadMap.downloadMaps();
                    if (File.Exists(mapPath + tempMap.ImageName))
                    {
                        BitmapImage mapImage = new BitmapImage(new Uri(mapPath + tempMap.ImageName));
                        this.ImageCanvas.Background = new ImageBrush(mapImage);
                        initCanvas(tempMap.Id);
                    }
                    else
                    {
                        MessageBox.Show("没有找到地图");
                    }
                }
            }
            catch (Exception ee)
            {
                log.Debug("usc_UploadFileCompleted 抛出异常：" + ee);
            }                
        }
        #endregion

    }        
}

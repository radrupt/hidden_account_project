using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoClient.ViewModel;

namespace VideoClient.View.Map
{
    /// <summary>
    /// MapImgView.xaml 的交互逻辑
    /// </summary>
    public partial class MapImgView : UserControl
    {
        //我们把所有需要向ViewModel里面传控件的全写在这里，需要注意的是执行顺序是：viewModel里面构造函数，然后这里，然后是loaded
        public MapImgView()
        {
            InitializeComponent();
            MapViewModel mapViewModel = this.mapImgViewCanvas.DataContext as MapViewModel;
            mapViewModel.ImageCanvas = this.mapImgViewCanvas;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MapViewModel mapViewModel = this.mapImgViewCanvas.DataContext as MapViewModel;
            mapViewModel.NewMapDto = new VideoCommon.com.pandawork.common.dto.MapDTO();
            mapViewModel.NewMapDto.Name = "222";
            AddMapDialog addMapDialog = new AddMapDialog();
            addMapDialog.ShowDialog();

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddDeviceDialog addDeviceDialog = new AddDeviceDialog();
            MapViewModel mapViewModel = this.mapImgViewCanvas.DataContext as MapViewModel;
            mapViewModel.initAddDeviceDialog(addDeviceDialog.regionComboBox);
            addDeviceDialog.ShowDialog();
        }
    }
}

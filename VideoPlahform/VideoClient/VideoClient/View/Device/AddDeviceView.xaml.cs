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
using System.Windows.Shapes;
using entiDevice= VideoCommon.com.pandawork.common.entity;
using deviceNameScope = VideoCommon.com.pandawork.common.entity;
namespace VideoClient.View.Device
{
    /// <summary>
    /// AddDeviceView.xaml 的交互逻辑
    /// </summary>
    public partial class AddDeviceView : Window
    {
        private deviceNameScope.Device device;
        public deviceNameScope.Device Device {
            get {
                return device;
            }
            set {
                device = value;
            }
        }
        public AddDeviceView()
        {
            InitializeComponent();
            this.device = new deviceNameScope.Device();
        }
        public AddDeviceView(deviceNameScope.Device device)
        {
            InitializeComponent();
            this.device = device;

        }
        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void X_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

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
using VideoClient.ViewModel;

namespace VideoClient.View.Map
{
    /// <summary>
    /// AddDeviceDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddDeviceDialog : Window
    {
        public AddDeviceDialog()
        {
            InitializeComponent();
        }

        private void regionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if ((sender as ComboBox).SelectedIndex > 0)
            {
                ComboBoxItem cbi = this.regionComboBox.SelectedItem as ComboBoxItem;
                MapViewModel mapViewMode = this.addDeviceDialogGrid.DataContext as MapViewModel;
                mapViewMode.initAddDeviceDialogDevices(this.deviceComboBox, Int32.Parse(cbi.Tag.ToString()));
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoClient.ViewModel;

namespace VideoClient.View.Map
{
    /// <summary>
    /// AddMapDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddMapDialog : Window
    {
        public AddMapDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.RestoreDirectory = true;
            op.Filter = "地图文件（*.jpg;*.bmp;*.png）|*.jpg;*.bmp;*.png";
            op.Multiselect = false;
            op.ShowDialog();
            mapPathTb.Text = op.FileName;
            (this.addMapGrid.DataContext as MapViewModel).NewMapDto.ImageName = op.FileName;
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

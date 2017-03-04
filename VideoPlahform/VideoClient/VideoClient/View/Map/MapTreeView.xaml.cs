using System;
using System.Collections.Generic;
using System.IO;
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
using VideoCommon.com.pandawork.common.dto;

namespace VideoClient.View.Map
{
    /// <summary>
    /// MapTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class MapTreeView : UserControl
    {
        public MapTreeView()
        {
            InitializeComponent();
        }

        private void mapTree_Loaded(object sender, RoutedEventArgs e)
        {
            TreeView tv = sender as TreeView;
            foreach (object md in tv.Items)
            {
                if ((md as MapDTO).Id == 0)
                {
                    TreeViewItem tvi = (TreeViewItem)tv.ItemContainerGenerator.ContainerFromItem(md);
                    tvi.IsExpanded = true;
                    tvi.Focus();
                    break;
                }
            }
            if ((tv.SelectedItem as MapDTO).ImageName == null || (tv.SelectedItem as MapDTO).ImageName.Trim().Equals(""))
            {
                MapViewModel mvd = tv.DataContext as MapViewModel;
                mvd.NewMapDto = tv.SelectedItem as MapDTO;
                AddMapDialog amd = new AddMapDialog();
                amd.DataContext = mvd;
                amd.Tag = 1;
                amd.ShowDialog();
            }
            else
            {
                MapViewModel mvd = tv.DataContext as MapViewModel;
                MapDTO mm = tv.SelectedValue as MapDTO;
                mvd.currentNode = mm;
                if (File.Exists(mvd.mapPath + mm.ImageName))
                {
                    BitmapImage mapImage = new BitmapImage(new Uri(mvd.mapPath + mm.ImageName));
                    mvd.ImageCanvas.Background = new ImageBrush(mapImage);
                    mvd.initCanvas(mm.Id);
                }
                else
                {
                    MessageBox.Show("没有找到地图");
                }
            }
        }
    }
}

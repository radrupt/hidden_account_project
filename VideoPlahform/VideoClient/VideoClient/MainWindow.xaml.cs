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
using VideoClient.View.Map;
using VideoClient.View.Device;
using WPF.DazzleUI2.Controls;
using VideoClient.Pandawork;
using VideoClient.View.Video;
using VideoClient.View.Group;

namespace VideoClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : DazzleWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            initUserControl();
            this.AllowsTransparency = false;
        }
        private void initUserControl()
        {
            /* MapView mv = new MapView(this);
             this.mapGrid.Children.Add(mv);
            RegionView rv = new RegionView(this);
            this.deviceGrid.Children.Add(rv);*/
            VideoView vv = new VideoView(this);
             this.videoGrid.Children.Add(vv);
            GroupView gv = new GroupView(this);
            this.groupGrid.Children.Add(gv);
            
        } 

        /// <summary>
        /// 关闭按钮图片点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                top.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(@"../../Skin/img/top.jpg", UriKind.Relative))
                };
                this.WindowState = System.Windows.WindowState.Maximized;
                
            }
            else
            {
                top.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(@"../../Skin/img/top_small.jpg", UriKind.Relative))
                };
                this.WindowState = System.Windows.WindowState.Normal;
                
            }
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}

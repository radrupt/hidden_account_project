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
using VideoClient.View.Video;

namespace VideoClient.View.Group
{
    /// <summary>
    /// PlayerGroupView.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerGroupView : UserControl
    {
        public PlayerGroupView()
        {
            InitializeComponent();

        }

        private double nowContionterHeight = 0;
        private double nowContionterWidth = 0;
        private int nowSelectCount = 4;

        private void playerContionter_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void oneBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void fourBtn_Click(object sender, RoutedEventArgs e)
        {
            nowSelectCount = 4;
            createPlayer(this.playerContionter, nowSelectCount);
        }

        private void nineBtn_Click(object sender, RoutedEventArgs e)
        {
            nowSelectCount = 9;
            createPlayer(this.playerContionter, nowSelectCount);
        }

        private void changeSize()
        {
            this.nowContionterHeight = this.playerContionter.ActualHeight;
            this.nowContionterWidth = this.playerContionter.ActualWidth;
        }

        private void createPlayer(WrapPanel wp,int count)
        {
            destoryPlayer(wp);
            changeSize();
            for (int i = 0; i < count; i++)
            {
                PlayerView pv = new PlayerView();
                pv.Height = this.nowContionterHeight / (Math.Sqrt(count)) - 1.0;
                pv.Width = this.nowContionterWidth / (Math.Sqrt(count)) - 1.0;
                wp.Children.Add(pv);
            }

        }

        private void destoryPlayer(WrapPanel wp)
        {
            wp.Children.Clear();
        }

        private void playerContionter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            createPlayer(this.playerContionter, nowSelectCount);
        }

        private void oneCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            nowSelectCount = 1;
            fourCheckBox.IsChecked = false;
            nineCheckBox.IsChecked = false;
            createPlayer(this.playerContionter, nowSelectCount);
        }

        private void fourCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            nowSelectCount = 4;
            oneCheckBox.IsChecked = false;
            nineCheckBox.IsChecked = false;
            createPlayer(this.playerContionter, nowSelectCount);
        }

        private void nineCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            nowSelectCount = 9;
            fourCheckBox.IsChecked = false;
            oneCheckBox.IsChecked = false;
            createPlayer(this.playerContionter, nowSelectCount);
        }

     
    }
}

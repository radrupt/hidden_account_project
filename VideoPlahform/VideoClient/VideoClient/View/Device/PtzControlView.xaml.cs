using System;
using System.Collections;
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
using HieCiULib;
using VideoClient.ViewModel;

namespace VideoClient.View.Device
{
    /// <summary>
    /// PtzControl.xaml 的交互逻辑
    /// </summary>
    public partial class PtzControlView : UserControl
    {
        public PtzControlView()
        {
            InitializeComponent();
            DeviceViewModel deviceViewModel = (DeviceViewModel)this.FindResource("deviceViewModel");
            //云台上转
            buttonUp.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonUp.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台下转
            buttonDown.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonDown.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台左转
            buttonLeft.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonLeft.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台右转
            buttonRight.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonRight.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台缩小
            buttonZoomIn.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonZoomIn.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台放大
            buttonZoomOut.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonZoomOut.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台光圈变大
            buttonApertureUp.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonApertureUp.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台光圈变小
            buttonApertureDown.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonApertureDown.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台焦点后移
            buttonFocusFar.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonFocusFar.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);
            //云台焦点前移
            buttonFocusNear.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(deviceViewModel.PtzDownControlCommandHandler), true);
            buttonFocusNear.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(deviceViewModel.PtzUpControlCommandHandler), true);

        }


        private void YuZhiDianComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("1");
            data.Add("2");
            data.Add("3");
            data.Add("4");
            data.Add("5");
            data.Add("6");
            data.Add("7");
            data.Add("8");
            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void YuZhiDianComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FuZuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FuZuComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();


            data.Add("全部");
            data.Add("摄像机");
            data.Add("灯光");
            data.Add("雨刷");
            data.Add("风扇");
            data.Add("加热器");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }
    }
}

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

namespace VideoClient.View.Device
{
    /// <summary>
    /// CuriserAddPreset.xaml 的交互逻辑
    /// </summary>
    public partial class CuriserAddPreset : Window
    {
        uint curiserIndex;
        public uint CuriserIndex
        {
            get { return curiserIndex; }
            set { ;}
        }
        public CuriserAddPreset()
        {
            InitializeComponent();
            curiserPreset.ItemsSource = new List<CuriserPreset>();
        }

        public CuriserAddPreset(uint curiserIndex)
        {
            InitializeComponent();
            this.curiserIndex = curiserIndex;
            curiserPreset.ItemsSource = new List<CuriserPreset>();
        }

        private void PresetComboBox_Loaded(object sender, RoutedEventArgs e)
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

        private void CursierSpeedComboBox_Loaded(object sender, RoutedEventArgs e)
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
            data.Add("9 ");
            data.Add("10");
            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        public void ClearCuriserList(object sender, RoutedEventArgs e)
        { 
            curiserPreset.ItemsSource = null;
        }

        public void AddCuriserPreset(object sender, RoutedEventArgs e)
        {
            List<CuriserPreset> presetList = (List<CuriserPreset>)curiserPreset.ItemsSource;
            presetList.Add(new CuriserPreset
            {
                presetIndex = preset.Text,
                speed = cursierSpeed.Text,
                remainTime = remainTime.Text
            });
            curiserPreset.ItemsSource = presetList;
        }
        public void ButtonOK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
    public class CuriserPreset
    {
        public string presetIndex { get; set; }
        public string speed { get; set; }
        public string remainTime { get; set; }
    }
}

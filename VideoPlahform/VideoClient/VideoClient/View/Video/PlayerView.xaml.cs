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
using formsPanel = System.Windows.Forms;

namespace VideoClient.View.Video
{
    /// <summary>
    /// PlayerView.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerView : UserControl
    {
 
        public PlayerView()
        {
            InitializeComponent();
            videoPanel = this.video;
        }

        private formsPanel.Panel videoPanel;
        public formsPanel.Panel VideoPanel 
        {
            get { return videoPanel; }
            set { videoPanel = value; }
        }

        
    }
}

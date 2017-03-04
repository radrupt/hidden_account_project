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
using VideoClient.Pandawork.Common.Util;
using common = VideoCommon.com.pandawork.common.entity;
namespace VideoClient.View.User
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : UserControl
    {
        public NowToDo nowToDo;

        public common.User nowOperateUser;

        public UserView()
        {
            nowToDo = NowToDo.AddState;
            InitializeComponent();
        }

        private void toAddUserDialog(UserView userView)
        {
            nowToDo = NowToDo.AddState;
            OpUserDialog opUserDialog = new OpUserDialog(userView);
            opUserDialog.Show();
        }

        private void toEditUserDialog(UserView userView)
        {
            nowToDo = NowToDo.EditState;
            OpUserDialog opUserDialog = new OpUserDialog(userView);
            opUserDialog.Show();
        }


        private bool callServiceDelUser(int userId)
        {
            //TODO 调用服务器删除指定用户,成功返回true，失败返回false
            return false;
        }

    }


}

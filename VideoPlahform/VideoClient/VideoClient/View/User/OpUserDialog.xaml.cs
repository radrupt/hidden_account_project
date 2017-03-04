using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoClient.Pandawork.Common.Util;
using common = VideoCommon.com.pandawork.common.entity;

namespace VideoClient.View.User
{
    /// <summary>
    /// OpUserDialog.xaml 的交互逻辑
    /// </summary>
    public partial class OpUserDialog : Window
    {

        private bool showAdd = true;
        private UserView _userView;
        private int nowUserId = -1;

        public OpUserDialog(UserView userView)
        {
            this._userView = userView;
            if (_userView.nowToDo == NowToDo.AddState)
            {
                showAdd = true;
            }
            else
            {
                showAdd = false;
                common.User user = this._userView.nowOperateUser;
                this.userNameTB.Text = user.LoginName;
                this.password1PB.Password = "";
                this.password2PB.Password = "";
                this.phoneTB.Text = user.Phone;
                this.realNameTB.Text = user.Name;
                nowUserId = user.Id;
            }
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (allCheck())
            {
                common.User user = new common.User();
                user.LoginName = this.userNameTB.Text;
                user.Name = this.realNameTB.Text;
                user.Phone = this.phoneTB.Text;
                user.Pwd = this.password1PB.Password;
                //TODO 这里如果为0表示管理员，如果为1表示一般用户
                user.UserType = this.RoleCB.SelectedIndex;
                if (showAdd)
                {
                    //接下来要请求服务器将这个用户添加上，添加成功以后，
                    //返回用户id
                    user.Id = 1;//替换服务器请求
                }
                else
                {
                    //调用服务器修改方法
                }
                this._userView.nowOperateUser = user;
                //准备关闭窗体显示内容
                this.Close();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
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

        private bool allCheck()
        {
            if (!checkLength(this.userNameTB.Text, 1, 30))
                return false;
            if (!checkPassword(this.password1PB.Password,this.password2PB.Password, 1, 30))
                return false;
            if (!checkLength(this.realNameTB.Text, 1, 30))
                return false;
            if (!checkPhone(this.phoneTB.Text))
                return false;
            return true;
        }

        private static bool checkLength(String text, int min, int max)
        {
            if (text != null && text.Length >= min && text.Length <= max)
            {
                return true;
            }
            return false;
        }

        private static bool checkPassword(String p1, String p2, int min, int max)
        {
            if (p1 != null && p2 != null && checkLength(p1, min, max) && checkLength(p2, min, max) && p1 == p2)
                return true;
            return false;
        }

        //TODO 检查是否可以成功
        private static bool checkPhone(String phone)
        {
            //18612341234  0431-84525285
            return Regex.IsMatch(phone, @"^[1-9][0-9](10)$") || Regex.IsMatch(phone, @"^[0-9](4)-[0-9](6,8)$");
        }
    }
}

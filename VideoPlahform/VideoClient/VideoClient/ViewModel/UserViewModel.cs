using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VideoClient.Pandawork.Common.Util;
using log4net;
using System.Reflection;
using VideoCommon.com.pandawork.common.entity;
using System.Windows;
using System.IO;

namespace VideoClient.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); 

        #region 需要用到的属性以及其封装方法
        private User user;
        private String serviceIp;  
        private String servicePort;
        private Boolean isSave;

        public User User
        {
            get { return user; }
            set { user = value; }
        }
        public String ServiceIp
        {
            get { return serviceIp; }
            set { serviceIp = value; }
        }
        public String ServicePort
        {
            get { return servicePort; }
            set { servicePort = value; }
        }
        public Boolean IsSave
        {
            get { return isSave; }
            set { isSave = value; }
        }
        #endregion

        #region 需要用到的命令

        public RelayCommand<object> loginCommand { get; private set; }
        private void loginCommandHandler()
        {
            if (this.IsSave)
            {
                Save();
            }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Properties.Settings.Default.NAME+"\\maps"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Properties.Settings.Default.NAME+"\\maps");
            }
            else
            {
                //这块儿写加载地图的过程
            }
            Pandawork.Cookie.UserId = 1;
            Application.Current.Windows[0].Visibility = Visibility.Hidden;
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        #endregion

        public UserViewModel()
        {
            loginCommand = new RelayCommand<object>(e => loginCommandHandler());
            this.User = new User();
            this.User.Name = "";
            this.User.Pwd = "";
            this.ServiceIp = "";
            this.ServicePort = "";
            //读取配置文件拿到是否保存的信息
            this.IsSave = Boolean.Parse(ConfigurationManager.AppSettings["isSave"].ToString());
            if (this.IsSave)
            {
                Load();
            }
        }

        /// <summary>
        /// 启动程序从配置文件中将相关的加密信息读入程序中并且解密，初始化页面空间内容
        /// </summary>
        private void Load()
        {
            try
            {
                string usernameEnc = ConfigurationManager.AppSettings["username"].ToString();
                string passwordEnc = ConfigurationManager.AppSettings["password"].ToString();
                string serviceIpEnc = ConfigurationManager.AppSettings["serviceIp"].ToString();
                string servicePortEnc = ConfigurationManager.AppSettings["servicePort"].ToString();

                this.ServiceIp = CodeUtil.Decode(serviceIpEnc).Substring(2);
                this.ServicePort = CodeUtil.Decode(servicePortEnc).Substring(2);
                this.User.LoginName = CodeUtil.Decode(usernameEnc).Substring(2);
                this.User.Pwd = CodeUtil.Decode(passwordEnc).Substring(2);
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message + "#####" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 保存加密后的配置信息
        /// </summary>
        private void Save()
        {//引入随机串进行加密
            string serviceIpTmp = "ip" + this.ServiceIp;
            string servicePortTmp = "po" + this.ServicePort;
            string usernameTmp = "ba" + this.User.LoginName;
            string passwordTmp = "rn" + this.User.Pwd;
            logger.Debug("serviceIpTmp = " + serviceIpTmp);
            logger.Debug("servicePortTmp = " + servicePortTmp);
            logger.Debug("usernameTmp = " + usernameTmp);
            logger.Debug("passwordTmp = " + passwordTmp);
            string serviceIpEnc = CodeUtil.Encode(serviceIpTmp);
            string servicePortEnc = CodeUtil.Encode(servicePortTmp);
            string usernameEnc = CodeUtil.Encode(usernameTmp);
            string passwordEnc = CodeUtil.Encode(passwordTmp);
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["serviceIp"].Value = serviceIpEnc;
            cfa.AppSettings.Settings["servicePort"].Value = servicePortEnc;
            cfa.AppSettings.Settings["username"].Value = usernameEnc;
            cfa.AppSettings.Settings["password"].Value = passwordEnc;
            cfa.AppSettings.Settings["isSave"].Value = "true";
            cfa.Save(); 
        }

        /// <summary>
        /// 清空已有的加密数据
        /// </summary>
        private void Clean()
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["serviceIp"].Value = "";
            cfa.AppSettings.Settings["servicePort"].Value = "";
            cfa.AppSettings.Settings["username"].Value = "";
            cfa.AppSettings.Settings["password"].Value = "";
            cfa.AppSettings.Settings["isSave"].Value = "false";
            cfa.Save(); 
        }
    }
}

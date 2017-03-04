using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoClient.Service.Reg;
using System.Text.RegularExpressions;

namespace VideoClient.Service.Reg.impls.hie
{
    class CheckUserLoginImpl:Utilis.Base, interfaces.CheckUserLogin
    {
        private Boolean isPort(string input)
        {

            return CKReg(input, @"^(\d|[1-9]\d|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d{1}|6553[0-5])$");
        }

        private Boolean isUserName(string input)
        {
            return CKReg(input, @"^[A-Za-z0-9]{1,32}$");
        }

        private Boolean isPassword(string input)
        {
            return CKReg(input, @"^\d{0,32}$");
        }

        private Boolean CKReg(String input, String reg)
        {

            return Regex.IsMatch(input, reg);
        }

        string interfaces.CheckUserLogin.isLegalLoginString(string ip, string port, string userName, string password)
        {

            if (!base.BCKisIP(ip))
            {
                return "ip地址格式不正确";
            }
            else if (!isPort(port))
            {
                return "端口地址格式不正确";
            }
            else if (!isUserName(userName))
            {
                return "用户名格式不正确";
            }
            else if (!isPassword(password))
            {
                return "密码格式不正确";
            }
            return "";
        }
    }
}

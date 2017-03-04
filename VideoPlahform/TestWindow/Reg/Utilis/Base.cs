using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestWindow.Reg.Utilis
{
    class Base
    {


        //测试字符长度是否小于给定len
        protected Boolean BCKltLen(string input,int len)
        {
            if (input.Length < len)
                return true;
            return false;
        }


        //测试字符长度是否大于给定len
        protected Boolean BCKgtLen(string input, int len)
        {
            if (input.Length > len)
                return true;
            return false;
        }
        
        //测试字符长度是否小于等于给定len
        protected Boolean BCKlqLen(string input, int len)
        {
            if (input.Length <= len)
                return true;
            return false;
        }

        //测试字符长度是否大于等于给定len
        protected Boolean BCKgqLen(string input, int len)
        {
            if (input.Length >= len)
                return true;
            return false;
        }

        //测试字符长度是否等于给定len
        protected Boolean BCKqLen(string input, int len)
        {
            if (input.Length == len)
                return true;
            return false;
        }


        //测试字符长度是否在某个闭区间内[len1,len2]
        protected Boolean BCKlcrc(string input, int len1, int len2)
        {
            if (input.Length >= len1 && input.Length <= len2)
                return true;
            return false;
        }

        //测试字符长度是否在某个左开右闭区间内(len1,len2]
        protected Boolean BCKlorc(string input, int len1, int len2)
        {
            if (input.Length > len1 && input.Length <= len2)
                return true;
            return false;
        }

        //测试字符长度是否在某个左闭开右开区间内[len1,len2)
        protected Boolean BCKlcro(string input, int len1, int len2)
        {
            if (input.Length >= len1 && input.Length < len2)
                return true;
            return false;
        }

        //测试字符长度是否在某个开区间内(len1,len2)
        protected Boolean BCKloro(string input, int len1, int len2)
        {
            if (input.Length > len1 && input.Length < len2)
                return true;
            return false;
        }

        //测试字符长度是否符合某个给定的正则表达式
        private Boolean BCKreg(string input, string reg)
        {
            return Regex.IsMatch(input, reg);
        }

        protected Boolean BCKisEchar(string input)
        {
            return BCKreg(input,@"^[a-zA-Z]+$");
        }

        protected Boolean BCKisNChar(string input)
        {
            return BCKreg(input, @"^[0-9]+$");
        }

        protected Boolean BCKisIP(string input)
        {
            return BCKreg(input, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        protected Boolean BCKisMAC(string input)
        {
            return false;
        }

        protected Boolean BCKisPort(string input)
        {
            return false;
        }
    }
}

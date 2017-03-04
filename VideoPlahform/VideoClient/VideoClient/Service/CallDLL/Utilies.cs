using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VideoClient.Service.CallDLL
{
    /// <summary>
    /// 工具类，用于处理常用的一些操作
    /// 
    /// Version:1.0
    /// Date:2012/06/10
    /// Author:baobaoyeye
    /// </summary>
    public class Utilies
    {
        /// <summary>
        /// 将csharp中的Color对象转换成windows中的colorref
        /// </summary>
        /// <param name="c">Color对象</param>
        /// <returns>colorref变量</returns>
        public static int ColorToCOLORREF(Color c)
        {
            return c.R | (c.G << 8) | (c.B << 0x10);
        }

        /// <summary>
        /// 将windows中的colorref转换成csharp中的Color对象
        /// </summary>
        /// <param name="cr">colorref变量</param>
        /// <returns>Color对象</returns>
        public static Color COLORREFToColor(int cr)
        {
            return Color.FromArgb(
                cr & 0xFF, 
                cr >> 8 & 0xFF, 
                cr >> 0x10 & 0xFF); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWindow.CallDLL.interfaces;
using HieCiULib;

namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// 用于处理HieClient设备对应的一些通用的转换过程
    /// 
    /// Version:1.0
    /// Date:2012/06/13
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_CommonUtilies
    {
        /// <summary>
        /// 转换时间，将通用格式转换成设备独有的时间格式
        /// </summary>
        /// <param name="time">时间信息结构</param>
        /// <returns>指定设备Hie中时间信息结构</returns>
        public static Common.TimeInfo TimeInfoCopy(TimeInfo time)
        {
            Common.TimeInfo timeInfo = new Common.TimeInfo();
            timeInfo.wYear = time.year;
            timeInfo.wMonth = time.month;
            timeInfo.wDay = time.day;
            timeInfo.wHour = time.hour;
            timeInfo.wMinute = time.minute;
            timeInfo.wSecond = time.second;
            return timeInfo;
        }

        /// <summary>
        /// 转换时间将通用格式转换成形如:20120618102054 的字符串格式，为了适配设备的某种时间格式
        /// </summary>
        /// <param name="time">事件信息结构</param>
        /// <returns>时间信息对应的字符串格式</returns>
        public static string TimeInfoToString(TimeInfo time)
        {
            StringBuilder retTime = new StringBuilder();
            retTime.Append(time.year);
            if (time.month < 10)
            {
                retTime.Append("0");
            }
            retTime.Append(time.month);
            if (time.day < 10)
            {
                retTime.Append("0");
            }
            retTime.Append(time.day);
            if (time.hour < 10)
            {
                retTime.Append("0");
            }
            retTime.Append(time.hour);
            if (time.minute < 10)
            {
                retTime.Append("0");
            }
            retTime.Append(time.minute);
            if (time.second < 10)
            {
                retTime.Append("0");
            }
            retTime.Append(time.second);
            return retTime.ToString();
        }
    }
}

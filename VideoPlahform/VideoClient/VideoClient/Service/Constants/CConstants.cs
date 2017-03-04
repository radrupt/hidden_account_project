using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.Constants
{
    /// <summary>
    /// 基本常数
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public class CConstants
    {
        /// <summary>
        /// 一个系统中永远不能达到的指针类型,用户初始化句柄
        /// </summary>
        public readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
    }
}

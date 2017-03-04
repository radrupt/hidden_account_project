using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// SDK属性类型枚举类型
    /// </summary>
    public enum SDKAttr
    {
        ConnectTimeOut = 0,
        CommandTimeOut,
        MediaTimeOut
    }

    /// <summary>
    /// 对SDK属性的操作接口
    /// 
    /// Version:1.0
    /// Date:2012/06/01
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_SDKAttribute
    {
        /// <summary>
        /// 设置SDK属性
        /// </summary>
        /// <param name="attrType">欲设置的属性类型</param>
        /// <param name="attrNum">此属性对应的值</param>
        void SetSDKAttr(SDKAttr attrType,uint attrNum);
        
        /// <summary>
        /// 获取SDK属性
        /// </summary>
        /// <param name="attrType">欲获取的属性类型</param>
        /// <returns>对应的属性值</returns>
        uint GetSDKAttr(SDKAttr attrType);
    }
}

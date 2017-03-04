using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using HieCiULib;
using TestWindow.Constants;
using TestWindow.CallDLL.interfaces;
namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_SDKAttribute_Impl:Client_SDKAttribute
    {
        private Hashtable enumTT = new Hashtable();

        /// <summary>
        /// 构造函数
        /// </summary>
        public HieClient_SDKAttribute_Impl()
        {
            enumTT.Add(
                SDKAttr.CommandTimeOut, 
                Common.eClientSDKAttributeType.eAttributeCommandTimeOut
            );
            enumTT.Add(
                SDKAttr.ConnectTimeOut, 
                Common.eClientSDKAttributeType.eAttributeConnectTimeOut
            );
            enumTT.Add(
                SDKAttr.MediaTimeOut,
                Common.eClientSDKAttributeType.eAttributeMediaTimeOut
            );
        }

        /// <summary>
        /// 设置HieClient的SDK属性
        /// </summary>
        /// <param name="attrType">通用平台SDK属性枚举类型</param>
        /// <param name="attrNum">需要修改的SDK的属性值</param>
        void interfaces.Client_SDKAttribute.SetSDKAttr(SDKAttr attrType, uint attrNum)
        {
            int eSetSDKCode = -1;
            if (enumTT.ContainsKey(attrType))
            {
                Common.eClientSDKAttributeType eAttrType = (Common.eClientSDKAttributeType)enumTT[attrType];
                
                eSetSDKCode = Unit.HieClient_SetAttribute(eAttrType, attrNum);
                if (eSetSDKCode == 0)
                {
                    return;
                }
                throw new Exception(ErrorConstants.getErrorString(eSetSDKCode));
            }
            
        }

        /// <summary>
        /// 获得HieClient的SDK属性
        /// </summary>
        /// <param name="attrType">通用平台SDK属性枚举类型</param>
        /// <param name="attrNum">需要获得的SDK的属性值</param>
        /// <returns>失败返回一个0，成功返回指定参数的值</returns>
        uint Client_SDKAttribute.GetSDKAttr(SDKAttr attrType)
        {
            int eGetSDKCode = -1;
            if (enumTT.ContainsKey(attrType))
            {
                uint attrNum  = 0;
                Common.eClientSDKAttributeType eAttrType = (Common.eClientSDKAttributeType)enumTT[attrType];
                
                eGetSDKCode = Unit.HieClient_GetAttribute(eAttrType, ref attrNum);
                if (eGetSDKCode == 0)
                {
                    return attrNum;
                }
                throw new Exception(ErrorConstants.getErrorString(eGetSDKCode));
            }
            return 0;
        }
    }
}

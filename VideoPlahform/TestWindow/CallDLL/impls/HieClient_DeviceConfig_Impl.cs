using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TestWindow.CallDLL.interfaces;
namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_DeviceConfig_Impl:interfaces.Client_DeviceConfig
    {
        private Hashtable enumLanguage = new Hashtable();

        /// <summary>
        /// 构造函数
        /// </summary>
        public HieClient_DeviceConfig_Impl()
        {
            enumLanguage.Add(
                eLanguage.English, 
                HieCiULib.HieDeviceConfig.eLanguageSelect.eEnglish
            );
            enumLanguage.Add(
                eLanguage.SimplifiedChinese,
                HieCiULib.HieDeviceConfig.eLanguageSelect.eSimplifiedChinese
            );
            enumLanguage.Add(
                eLanguage.TraditionalChinese,
                HieCiULib.HieDeviceConfig.eLanguageSelect.eTraditionalChinese
            );
        }

        public void DeviceConfig(Client_UserLogin userLogin, eLanguage els, string userName)
        {
            if (enumLanguage.ContainsKey(els))
            {
                int isCFG_Start = -1;
                if ((isCFG_Start = HieCiULib.HieDeviceConfig.HieCFG_Start()) == 0)
                {
                    int isCFG_Config = -1;
                    if ((isCFG_Config = HieCiULib.HieDeviceConfig.HieCFG_Configutation(
                        userLogin.UserInfo.ToInt32(),
                        (HieCiULib.HieDeviceConfig.eLanguageSelect)enumLanguage[els],
                        userName)) == 0)
                    {
                        HieCiULib.HieDeviceConfig.HieCFG_Stop();
                    }
                    else
                    {
                        throw new Exception(Constants.ErrorConstants.getErrorString(isCFG_Config));
                    }
                }
                else
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(isCFG_Start));
                }
            }
        }
    }
}

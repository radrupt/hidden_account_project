using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HieCiULib;
using System.Collections;
using TestWindow.CallDLL.interfaces;

namespace TestWindow.CallDLL.impls
{
    /// <summary>
    /// 
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_Init_Impl:interfaces.Client_Init
    {
        private Hashtable enumDCWM = new Hashtable();
        public HieClient_Init_Impl()
        {
            enumDCWM.Add(
               DecoderCardWorkMode.WorkMode_CIF  ,tmsdk.DecoderCardWorkMode_t.WorkMode_CIF

           );//哈希表：键：矩阵卡解码工作模式，值：矩阵解码 SDK 头文件
            enumDCWM.Add(
                DecoderCardWorkMode.WorkMode_2CIF,
                tmsdk.DecoderCardWorkMode_t.WorkMode_2CIF
            );
            enumDCWM.Add(
                DecoderCardWorkMode.WorkMode_D1,
                tmsdk.DecoderCardWorkMode_t.WorkMode_D1
            );
        }

        bool interfaces.Client_Init.Client_Start()
        {
            int isServerStarted = -1;
            try
            {
                isServerStarted = Unit.HieClient_Start();
                //isServerStarted == 0 时候表示登陆成功
                if (isServerStarted == 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        bool interfaces.Client_Init.Client_Stop()
        {
            int isServerStoped = -1;
            try
            {
                isServerStoped = Unit.HieClient_Stop();
                //isServerStoped == 0 时候表示退出成功
                if (isServerStoped == 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        bool interfaces.Client_Init.init(DecoderCardWorkMode dcwm)//dcwn：解码工作模式
        {
            if (enumDCWM.ContainsKey(dcwm))
            {
                int nInitCode = -1;
                try
                {
                    HieCIUCommon.SYSTEM_INFO tSysInfo = new HieCIUCommon.SYSTEM_INFO();
                    tSysInfo.dwSysCaps = HieCIUCommon.SYSINFO_CAPS_MATRIX_DEC_WORK_MODE; //表示矩阵解码的工作模式字段有效
                    tSysInfo.dwMatrixDecodeWorkMode = (tmsdk.DecoderCardWorkMode_t)enumDCWM[dcwm];         //矩阵解码工作模式: CIF
                    nInitCode = HieCIU.HieCIU_Initialize(ref tSysInfo);
                    if (nInitCode == 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }
    }
}

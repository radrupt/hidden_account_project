using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoClient.Service.CallDLL.interfaces
{
    /// <summary>
    /// 矩阵卡解码工作模式枚举类型
    /// </summary>
    public enum DecoderCardWorkMode
    {
        WorkMode_CIF = 0x00000000,
        WorkMode_2CIF = 0x00000001,
        WorkMode_D1 = 0x00000002
    }   
    
    /// <summary>
    /// SDK初始化处理操作接口
    /// 
    /// Version:1.0
    /// Date:2012/06/11
    /// Author:baobaoyeye
    /// </summary>
	public interface Client_Init
	{
        /// <summary>
        /// 初始化服务，设置矩阵卡解码工作模式
        /// </summary>
        /// <param name="dcwm">矩阵卡解码工作模式枚举</param>
        /// <returns>设置成功</returns>
        Boolean init(DecoderCardWorkMode dcwm);
        
        /// <summary>
        /// 启动服务,申请必要的内存，线程等资源。
        /// </summary>
        /// <returns>启动成功</returns>
        Boolean Client_Start();
        
        /// <summary>
        /// 停止服务
        /// 释放服务所使用的内存，线程等资源 
        /// </summary>
        /// <returns>停止成功</returns>
        Boolean Client_Stop();
	}
}

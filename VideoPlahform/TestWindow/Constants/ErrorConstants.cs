using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.Constants
{
    /// <summary>
    /// 错误或异常参数处理
    /// 
    /// Version:1.0
    /// Date:2012/06/14
    /// Author:baobaoyeye
    /// </summary>
    public class ErrorConstants
    {
        /// <summary>
        /// 错误码枚举类
        /// </summary>
        public enum ClientError
        {
            //0-33    10000-10003     10100-10101   10200-10206

            ErrorNone = 0,			                            /*!< 无错误									*/
            ErrorFailed,										/*!< 失败									*/
            ErrorNoInitializeSDK,								/*!< 未初始化SDK							*/
            ErrorHandle,										/*!< 句柄错误								*/
            ErrorParameter,									    /*!< 参数错误								*/
            ErrorBufferNoEnough,								/*!< 缓冲区太小								*/
            ErrorMemory,										/*!< 内存错误								*/
            ErrorSystemFailed,									/*!< 操作系统返回错误						*/
            ErrorNoIdleResource,								/*!< 无可用资源								*/
            ErrorTimeOut,										/*!< 超时错误								*/
            ErrorFunctionVersionLow,							/*!< 功能版本低于服务器						*/
            ErrorFunctionVersionHigh,							/*!< 功能版本高于服务器						*/
            ErrorTaskNoRun,									    /*!< 任务未启动								*/
            ErrorAlreadyRun,									/*!< 重复启动								*/
            ErrorConnectFailed,								    /*!< 连接失败								*/
            ErrorSessionDisconnect,							    /*!< 连接断开								*/
            ErrorCommandSendFailed,							    /*!< 命令发送失败							*/
            ErrorServerReject,									/*!< 服务器拒绝								*/
            ErrorInvalidUser,									/*!< 用户句柄非法							*/
            ErrorCallFail,										/*!< 远程调用失败							*/
            ErrorCallReplyError,								/*!< 远程调用应答出错						*/
            
            ErrorUserName,										/*!< 用户名错误								*/
            ErrorUserPass,										/*!< 用户密码错误							*/
            ErrorIPLimited,									    /*!< 用户IP限制								*/
            ErrorMACLimited,									/*!< 用户MAC限制								*/
            ErrorUserNumOverflow,								/*!< 登陆用户过多							*/
            ErrorUserHeartBeat,								    /*!< 用户心跳订阅失败						*/
            ErrorGetConfigurationPort,							/*!< 登陆时获取端口配置信息失败				*/
            ErrorServiceReseting,								/*!< 网络服务重启中							*/
            ErrorInvalidURL,									/*!< 无效的URL地址							*/
            
            ErrorNoSupportCommand,								/*!< 不支持的命令	                        */
            ErrorNotImplement,									/*!< 未实现									*/

            eErrorUnserialize,									/*!< 反序列化失败							*/
            eErrorServerNoSupportInterface,						/*!< 服务器不支持的接口                     */

            eErrorStreamType,									/*!< 关闭流时，流类型错误					*/

            //为了与设备上配置信息操作错误码匹配，配置信息错误码定义如下：(其他的错误码请在上面定义)
            eErrorMODULE_ID = 16000,
            eErrorUNSUPPORTED_MAJORTYPE,						/*!< 不支持的主命令字						*/
            eErrorUNSUPPORTED_MINORTYPE,						/*!< 不支持的次命令字						*/
            eErrorSTRUCT_UNVALID,								/*!< 结构体无效								*/
            eErrorSTRUCT_READONLY,								/*!< 结构体是只读的							*/
            eErrorBUF_SMALL,									/*!< 缓冲区太小								*/
            eErrorSTRUCT_UNSUPPORTED,							/*!< 系统不支持该结构体的设置或读取			*/
            eErrorNO_RIGHT,										/*!< 没有权限								*/
            eErrorPARAM_OVERFLOW,								/*!< 参数溢出								*/
            eErrorCALLSDK_FAIL,									/*!< 调用SDK失败							*/
        }
        static  Hashtable ChineseErrorInfo=new Hashtable(){{0,"无错误"},{1,"失败"},{2,"未初始化SDK"},{3,"句柄错误"},
            {4,"参数错误"},{5,"缓冲区太小"},{6,"内存错误"},//0-6
            {7,"操作系统返回错误"},{8,"无可用资源"},{9,"超时错误"},{10,"功能版本低于服务器"},{11,"功能版本高于服务器"},
            {12,"任务未启动"},{13,"重复启动"},{14,"连接失败"},//7-14
            {15,"连接断开"},{16,"命令发送失败"},{17,"服务器拒绝"},{18,"用户句柄非法"},{19,"远程调用失败"},{20,"远程调用应答出错"},//15-20
            {21,"用户名错误"},{22,"用户密码错误"},{23,"用户IP限制"},{24,"用户MAC限制"},{25,"登陆用户过多"},{26,"用户心跳订阅失败"},//21-26
            {27,"登陆时获取端口配置信息失败"},{28,"网络服务重启中"},{29,"无效的URL地址"},{30,"不支持的命令"},{31,"未实现"},//27-31
            {32,"反序列化失败"},{33,"服务器不支持的接口"},{34,"关闭流时，流类型错误"},
            {16000,"不支持的主命令字"},{16001,"不支持的次命令字"},{16002,"结构体无效"},{16003,"结构体是只读的"},
            {16004,"缓冲区太小"},{16005,"系统不支持该结构体的设置或读取"},{16006,"没有权限"},
            {16007,"参数溢出"},{16008,"失败"}};
        static ArrayList Errorindex = new ArrayList{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,
                                         10000,10001,10002,10003,10100,10101,10200,10201,10202,10203,10204,10205,10206};
        /// <summary>
        /// 根据错误码返回错误对应的字符形式
        /// </summary>
        /// <param name="ErrorCode">错误码</param>
        /// <returns>错误码对应的字符形式</returns>
        static public string getErrorString(int ErrorCode)
        {
            Object error= ChineseErrorInfo[ErrorCode];
            if ( error != null )
                return error.ToString();
            else return "无当前错误码对应的中文信息,错误码是: " + ErrorCode;
        }
    }
}

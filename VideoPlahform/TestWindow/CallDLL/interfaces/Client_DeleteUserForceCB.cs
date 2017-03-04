using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWindow.CallDLL.interfaces
{
    /// <summary>
    /// 设置强制删除已登录用户回调接口
    /// 设备端可强制删除已登录的用户，删除时会通过此接口设置的回调函数通知该用户。
    /// 
    /// Version:1.0
    /// Date:2012/06/01
    /// Author:baobaoyeye
    /// </summary>
    public interface Client_DeleteUserForceCB
    {
        /// <summary>
        /// 指明用户是否已经被强制删除
        /// </summary>
        Boolean IsUserForceDeleted
        {
            get;
            set;
        }

        /// <summary>
        ///  强制删除已登录用户
        /// </summary>
        void DeleteUserForce();
    }
}

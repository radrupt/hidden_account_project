using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HUSER=System.IntPtr;
using HieCiULib;

namespace VideoClient.Service.CallDLL.impls
{
    /// <summary>
    /// Version:1.0
    /// Date:2012/07/12
    /// Author:baobaoyeye
    /// </summary>
    public class HieClient_DeleteUserForceCB_Impl:interfaces.Client_DeleteUserForceCB
    {
        private Boolean isUserForceDeleted;

        public HieClient_DeleteUserForceCB_Impl()
        {
            isUserForceDeleted = false;
        }

        public void DeleteUserForce()
        {
            if (!isUserForceDeleted)
            {
                int nSetDelUserCBCode = -1;
                Common.CB_DeleteUserForce cbduf = new Common.CB_DeleteUserForce(callback_DelUserForceOp);
                nSetDelUserCBCode = Unit.HieClient_DeleteUserForceCB(cbduf, 0);
                if (nSetDelUserCBCode != 0)
                {
                    throw new Exception(Constants.ErrorConstants.getErrorString(nSetDelUserCBCode));
                }
            }
        }

        private int callback_DelUserForceOp(HUSER hUser, uint dwUserData)
        {
            isUserForceDeleted = true;
            return 0;
        }

        public bool IsUserForceDeleted
        {
            get
            {
                return isUserForceDeleted;
            }
            set
            {
                isUserForceDeleted = value;
            }
        }
    }
}

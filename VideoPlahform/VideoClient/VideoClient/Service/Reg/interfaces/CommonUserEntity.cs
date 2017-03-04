using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common = VideoCommon.com.pandawork.common.entity;

namespace VideoClient.Service.Reg.interfaces
{
    interface CommonUserEntity
    {
        string isLegalUserEnityString(common.User user);
    }
}

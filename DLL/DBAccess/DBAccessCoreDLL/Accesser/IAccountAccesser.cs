using DBAccessBaseDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Text;
using DBAccessCoreDLL.EF.Entity;

namespace DBAccessCoreDLL.Accesser
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountAccesser : IAccesser<Account, UInt64>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passport"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        Account Get(ulong? key = null,string username = "", string passport = "", string email = "", string phone = "");
    }
}

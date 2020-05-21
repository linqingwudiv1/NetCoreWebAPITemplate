using DBAccessBaseDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Text;
using DBAccessCoreDLL.EF.Entity;

namespace DBAccessCoreDLL.Accesser
{
    /// <summary>
    /// 查询Account的方式
    /// </summary>
    public enum EFindAccountWay 
    {
        NotFound ,
        Id,
        UserName,
        Passport,
        EMail,
        Phone
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAccountAccesser : IAccesser<Account, Int64>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passport"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        Tuple< Account, EFindAccountWay> Get(long? key = null,string username = "", string passport = "", string email = "", string phone = "");

    }
}

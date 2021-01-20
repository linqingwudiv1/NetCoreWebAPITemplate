using DBAccessBaseDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Text;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.EFORM.Context;

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
        /// DB Layer
        /// </summary>
        CoreContextDIP db { get; protected set; }

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

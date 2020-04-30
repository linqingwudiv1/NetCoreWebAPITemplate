using DBAccessBaseDLL.Accesser;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessCoreDLL.DAO
{
    /// <summary>
    /// 实体数据......来自缓存或数据库
    /// </summary>
    public class AccountAccesser : IAccesser<Account>
    {
        public readonly CoreContextDIP db;
        /// <summary>
        /// 
        /// </summary>
        public AccountAccesser(CoreContextDIP _db) 
        {
            db = _db;
        }

        public int Add(Account newEntiy)
        {
            throw new NotImplementedException();
        }

        public int Delete<Key>(Key key)
        {
            throw new NotImplementedException();
        }

        public Account Get<Key>(Key key)
        {
            throw new NotImplementedException();
        }

        public int Update(Account deleteEntiy)
        {
            throw new NotImplementedException();
        }
    }
}

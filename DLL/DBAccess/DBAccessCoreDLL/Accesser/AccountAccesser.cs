using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBAccessCoreDLL.Accesser
{

    /// <summary>
    /// 实体数据......来自缓存或数据库
    /// </summary>
    public class AccountAccesser : IAccountAccesser
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly CoreContextDIP db;


        /// <summary>
        /// 
        /// </summary>
        public AccountAccesser(CoreContextDIP _db)
        {
            db = _db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(Account newEntiy)
        {
            db.Accounts.Add(newEntiy);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<Account> newEntiys)
        {
            db.Accounts.AddRange(newEntiys);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(long key)
        {
            Account temp_account = Get(key);
            db.Accounts.Remove(temp_account);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(IList<long> keys)
        {
            IList<Account> temp_accounts = Get(keys);
            db.Accounts.RemoveRange(temp_accounts);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Account Get(long key)
        {
            return db.Accounts.Find(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IList<Account> Get(IList<long> keys)
        {
            return db.Accounts.Where(x => keys.Contains(x.Id)).ToArray();
        }

        /// <summary>
        /// 多ID方式用户查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="username"></param>
        /// <param name="passport"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Tuple<Account, EFindAccountWay> Get(long? key = null, string username = "", string passport = "", string email = "", string phone = "")
        {
            Account ret_account = null;
            EFindAccountWay ret_enum = EFindAccountWay.NotFound;
            
            if (key != null) 
            {
                //try query of id
                long temp_key = (long)key;
                ret_account = Get(temp_key);
                ret_enum = EFindAccountWay.Id;
            }
            else if ( String.IsNullOrWhiteSpace(username) ) 
            {
                //try query of username
                ret_account = db.Accounts.Where(x => x.Username == username).DefaultIfEmpty(null).FirstOrDefault();
                ret_enum = EFindAccountWay.UserName;
            }
            else if ( String.IsNullOrWhiteSpace(passport) ) 
            {
                ret_account = db.Accounts.Where(x => x.Passport == passport).DefaultIfEmpty(null).FirstOrDefault();
                ret_enum = EFindAccountWay.Passport;
            }

            else if ( String.IsNullOrWhiteSpace(email) )
            {
                ret_account = db.Accounts.Where(x => x.Email == email).DefaultIfEmpty(null).FirstOrDefault();
                ret_enum = EFindAccountWay.EMail;
            }

            else if ( String.IsNullOrWhiteSpace(phone) ) 
            {
                ret_account = db.Accounts.Where(x => x.Phone == phone).DefaultIfEmpty(null).FirstOrDefault();
                ret_enum = EFindAccountWay.Phone;
            }
            if (ret_account == null) 
            {
                ret_enum = EFindAccountWay.NotFound;
            }

            return new Tuple<Account, EFindAccountWay>(ret_account, ret_enum);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiy"></param>
        /// <returns></returns>
        public int Update(Account modifyEntiy)
        {
            db.Accounts.Update(modifyEntiy);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiys"></param>
        /// <returns></returns>
        public int Update(IList<Account> modifyEntiys)
        {
            db.Accounts.UpdateRange(modifyEntiys);
            return db.SaveChanges();
        }
    }
}

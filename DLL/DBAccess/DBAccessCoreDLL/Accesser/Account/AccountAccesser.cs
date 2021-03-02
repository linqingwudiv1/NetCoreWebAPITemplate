using BaseDLL.DTO;
using BaseDLL.Helper.SMS;
using BaseDLL.Helper.Smtp;
using DBAccessBaseDLL.Accesser;
using DBAccessBaseDLL.EF;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.DTO.API.Users;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Validator;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public AccountAccesser(CoreContextDIP _db)
        {
            db = _db;
        }

        CoreContextDIP IAccountAccesser.db { get => db; set => db = value; }
        private CoreContextDIP db;


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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DTO_PageableModel<Account>> Get(DTO_PageableQueryModel<DTO_GetUsers> model) 
        {
            IQueryable<Account> query = (from 
                                            x 
                                         in 
                                             db.Accounts.Include(x => x.AccountRoles)
                                                        .ThenInclude(c => c.role) 
                                         select x);
            var predicate = PredicateBuilder.New<Account>(true);
            if (!String.IsNullOrEmpty(model.data.searchWord)) 
            {
                if (model.data.bId) 
                {
                    try
                    {
                        Int64.Parse(model.data.searchWord);
                        predicate = predicate.Or(x => x.Id.ToString().Contains(model.data.searchWord));
                    }
                    catch (Exception) 
                    {

                    }
                }

                if ( model.data.bPassport ) 
                {
                    predicate = predicate.Or(x => x.Passport != null && x.Passport.Contains(model.data.searchWord, StringComparison.CurrentCultureIgnoreCase));
                }

                if (model.data.bUserName) 
                {
                    predicate = predicate.Or(x => x.Username != null && x.Username.Contains(model.data.searchWord));
                }

                if (model.data.bEmail) 
                {
                    predicate = predicate.Or(x => x.Email != null && x.Email.Contains(model.data.searchWord, StringComparison.CurrentCultureIgnoreCase));
                }

                if ( model.data.bPhone ) 
                {
                    predicate = predicate.Or(x => x.Phone != null && x.Phone.Contains(model.data.searchWord));
                }
            }
            var ret_data = query.Where(predicate).QueryPages(model.pageSize, model.pageNum) .ToArray();
            return new DTO_PageableModel<Account>
            {
                data = ret_data,
                pageNum = model.pageNum,
                pageSize = model.pageSize,
                total =  query.Where(predicate).LongCount() //model.pageSize
            }; 
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
            else if ( !String.IsNullOrWhiteSpace(username) && AccountValidator.bValidUserName(username) ) 
            {
                //try query of username
                ret_account = db.Accounts.Where(x => x.Username == username).DefaultIfEmpty().First();
                ret_enum = EFindAccountWay.UserName;
            }
            else if ( !String.IsNullOrWhiteSpace(passport) && AccountValidator.bValidPassport(passport) ) 
            {
                ret_account = db.Accounts.Where(x => x.Passport == passport.ToLower() ).DefaultIfEmpty().First();
                ret_enum = EFindAccountWay.Passport;
            }
            
            else if ( !String.IsNullOrWhiteSpace(email) && EmailHepler.IsValid(email))
            {
                ret_account = db.Accounts.Where(x => x.Email == email.ToLower()).DefaultIfEmpty().First();
                ret_enum = EFindAccountWay.EMail;
            }

            else if ( !String.IsNullOrWhiteSpace(phone) && PhoneHelper.IsValid(phone) ) 
            {
                var tuple = PhoneHelper.Split(phone);
                var areaCode = tuple.Item1;
                var p = tuple.Item2;
                ret_account = db.Accounts.Where(x => x.PhoneAreaCode == areaCode && x.Phone == p).DefaultIfEmpty().First();
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

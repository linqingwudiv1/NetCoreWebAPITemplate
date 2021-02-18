using AdminServices.Command.Account;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using MassTransit;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DBAccessBaseDLL.Static;
using Dapper;
using System.Data;
using Dapper.Contrib.Extensions;
using DBAccessCoreDLL.EFORM.Entity;
using System.Collections.Generic;
using Account_Alias = DBAccessCoreDLL.EFORM.Entity.Account;
using Npgsql;

namespace AdminServices.Event.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountDomainEvent :
        IConsumer<UpdateAccountsRoleCommand>,
        IConsumer<ChangePasswordCommand>,
        IConsumer<RegisterAccountByEmailCommand>,
        IConsumer<RegisterAccountByPhoneCommand>,
        IConsumer<RegisterAccountByPassportCommand>
    {

        /// <summary>
        /// DAO层
        /// </summary>
        protected IAccountAccesser accesser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_accesser"></param>
        /// <param name="_IDGenerator"></param>
        public AccountDomainEvent(IAccountAccesser _accesser, IIDGenerator _IDGenerator)
        {
            this.accesser        = _accesser;
            this.IDGenerator     = _IDGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UpdateAccountsRoleCommand> context)
        {
            var data = context.Message;

            var query = (from
                             x
                         in
                             this.accesser.db.AccountRoles
                         where
                             data.users.Contains(x.AccountId)
                         select
                             x);
            
            this.accesser.db.AccountRoles.RemoveRange(query);
            
            foreach(var roleId in data.roles) 
            {
                foreach (var userId in data.users) 
                {
                    this.accesser.db.AccountRoles.Add(new AccountRole
                    {
                        Id = this.IDGenerator.GetNewID<AccountRole>(),
                        AccountId = userId,
                        RoleId = roleId
                    });
                }
            }

            this.accesser.db.SaveChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<ChangePasswordCommand> context)
        {
            var data = context.Message;
            var account = this.accesser.Get(key: data.key).Item1;

            account.Password = data.newPassword;
            this.accesser.Update(account);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<RegisterAccountByEmailCommand> context)
        {
            var data = context.Message;

            var account = new Account_Alias
            {
                Id          = data.id,
                Email       = data.email.ToLower(),
                DisplayName = $"User_{data.id}",
                Password    = data.password,
            };

            this.accesser.Add(account);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<RegisterAccountByPhoneCommand> context)
        {
            var data = context.Message;


            var account = new Account_Alias
            {
                Id              = data.id,
                Phone           = data.phone,
                PhoneAreaCode   = data.phoneAreaCode,
                DisplayName     = $"User_{data.id}",
                Password        = data.password,

            };

            this.accesser.Add(account);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<RegisterAccountByPassportCommand> context)
        {
            var data = context.Message;

            var account = new Account_Alias
            {
                Id = data.id,
                Passport    = data.passport.ToLower(),
                DisplayName = $"User_{data.id}",
                Password = data.password,
            };


            this.accesser.Add(account);
        }
    }
}

using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DTOModelDLL.API.Users;
using DTOModelDLL.Common.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApplictionServiceDLL;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDLL.Extensison
{


    /// <summary>
    /// 登录状态
    /// </summary>
    public enum EM_LoginState 
    {
        Pass,
        NoExist,
        PasswordError
    }

    /// <summary>
    /// 通用全局业务扩展，例如登录，注销
    /// </summary>
    public static class StaticBizExtend 
    {
        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public EM_LoginState LoginLogic(this Controller controller, DTOAPI_Login data)
        {
            ExamContext db = new ExamContext();

            Account account = ( from 
                                x 
                            in 
                                db.Accounts.Include( obj => obj.AccountRoles )
                            where 
                                x.Username == data.username 
                            select x ).FirstOrDefault();

            if (account == null) 
            {
                return EM_LoginState.NoExist;
            }

            if (account.Password == data.password)
            {
                IList<string> roles = ( from x in account.AccountRoles select x.role.Name).ToList();

                DTO_StoreAccount storeAccount = new DTO_StoreAccount
                {
                    id = account.Id                     ,
                    username = account.Username         ,
                    password = account.Password         ,
                    avatar = account.Avatar             ,
                    email = account.Email               ,
                    name = account.Name                 ,
                    introduction = account.Introduction ,
                    phone = account.Phone               ,
                    roles = roles
                };

                controller.HttpContext.Session.SetStoreAccount(storeAccount);

                return EM_LoginState.Pass;
            }
            else 
            {
                return EM_LoginState.PasswordError;
            }
        }

        /// <summary>
        /// 注销操作
        /// </summary>
        /// <param name="controller"></param>
        static public void LogoutLogic(this Controller controller)
        {
            controller.HttpContext.Session.SetStoreAccount(null);
        }
    }
}

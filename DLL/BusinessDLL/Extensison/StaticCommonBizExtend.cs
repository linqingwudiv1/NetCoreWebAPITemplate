using DBAccessDLL.EF.Context;
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
        /// 模拟数据
        /// </summary>
        static DTO_StoreAccount temp_storeAccount = new DTO_StoreAccount
        {
            id = 0,
            username = "admin",
            name = "linqing",
            avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif",
            introduction = " role ",
            email = "aa875191946@qq.com",
            phone = "184****8004",
            roles = new List<string> { "admin", "editor" }

        };



        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public EM_LoginState LoginLogic(this Controller controller, DTOAPI_Login data)
        {
            ExamContext db = new ExamContext();

            var account = ( from x in db.Accounts.Include(obj=>obj.AccountRoles)
                            where 
                                x.Username == data.username 
                            select x ).FirstOrDefault();

            if (account == null) 
            {
                return EM_LoginState.NoExist;
            }

            if (data.username == temp_storeAccount.username)
            {
                controller.HttpContext.Session.SetStoreAccount(temp_storeAccount);

                return EM_LoginState.Pass; //new { accessToken = "Admin-Token" };
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

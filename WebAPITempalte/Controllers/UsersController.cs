﻿using BusinessDLL.Extensison;
using DTOModelDLL.API.Users;
using DTOModelDLL.Common;
using DTOModelDLL.Common.Store;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetApplictionServiceDLL;
using NetApplictionServiceDLL.Filter;
using WebAPI.Model.Static;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Vue项目展示接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    public class UsersController : BaseController
    {

        /// <summary>
        /// Vue项目测试接口
        /// </summary>
        public UsersController()
        {

        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        public DTO_ReturnModel<dynamic> Login([FromBody] DTOAPI_Login loginInfo)
        {
            return new DTO_ReturnModel<dynamic>(this.LoginLogic(loginInfo), 20000);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthFilter]
        public DTO_ReturnModel<string>  Logout()
        {
            this.LogoutLogic();
            return new DTO_ReturnModel<string>(null, 20000);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthFilter]
        public DTO_ReturnModel<dynamic> Info([FromBody] DTOAPI_Info Info)
        {
            DTO_StoreAccount store_account = this.GetStoreAccount();
            return new DTO_ReturnModel<dynamic>(store_account, 20000);
        }
    }
}
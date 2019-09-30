using DTOModelDLL.Common.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;



namespace NetApplictionServiceDLL.Filter
{
    /// <summary>
    /// 类型
    /// </summary>
    public enum ActionAuthType
    {
        /// <summary>
        /// 未验证不做任何处理
        /// </summary>
        Normal,

        /// <summary>
        /// 未验证重定向登录页面
        /// </summary>
        RedirectLogin,
        /// <summary>
        /// 未验证返回401
        /// </summary>
        Unauth
    }

    /// <summary>
    ///  身份验证(等待扩展:支持DB缓存/NoSql DB缓存)
    /// </summary>
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action类型
        /// </summary>
        private ActionAuthType ActionType = ActionAuthType.Unauth;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActionType"></param>
        public AuthFilterAttribute(ActionAuthType ActionType = ActionAuthType.Unauth)
        {
            this.ActionType = ActionType;
        }

        /// <summary>
        /// 
        /// </summary>
        ~AuthFilterAttribute()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Handle(filterContext);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        private void Handle(ActionExecutingContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;

            //token
            String Token = filterContext.HttpContext.Request.Query[GVariable.QingTokenAvg];

            switch (Token)
            {
                case string topic when String.IsNullOrWhiteSpace(topic): 
                {
                    break;
                }
                case string topic when String.IsNullOrWhiteSpace(topic):
                default:
                {
                    break;
                }
            }

            if (controller != null)
            {
                DTO_StoreAccount storeAccount = controller.HttpContext.Session.GetStoreAccount();

                switch (storeAccount) 
                {
                    case DTO_StoreAccount acc when acc != null: 
                    {
                        break;
                    }
                    case null:
                    default:
                    {
                        UnauthorizedHandle(filterContext);
                        break;
                    }
                }

                return;
            }
            else
            {
                UnauthorizedHandle(filterContext);
                return;
            }
        }

        /// <summary>
        /// 未通过身份验证处理
        /// </summary>
        /// <param name="filterContext"></param>
        private void UnauthorizedHandle(ActionExecutingContext filterContext)
        {
            switch (ActionType)
            {
                case ActionAuthType.Normal:
                {
                    break;
                }
                case ActionAuthType.RedirectLogin:
                {
                    filterContext.Result = new EmptyResult();
                    filterContext
                        .HttpContext
                        .Response
                        .Redirect(GVariable.LoginUri);
                    break;
                }
                case ActionAuthType.Unauth:
                {
                    filterContext.Result = new UnauthorizedResult();
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        private String GetToken(ActionExecutingContext filterContext) 
        {
            String ret_token = String.Empty;

            String HeaderToken = filterContext.HttpContext.Request.Headers[GVariable.QingTokenHeader];
            String CookieToken = filterContext.HttpContext.Request.Cookies[GVariable.QingTokenAvg];

            if (!String.IsNullOrEmpty(HeaderToken) && !String.IsNullOrWhiteSpace(HeaderToken))
            {
                ret_token = HeaderToken;
            }
            else if (!String.IsNullOrEmpty(CookieToken) && !String.IsNullOrWhiteSpace(CookieToken))
            {
                ret_token = HeaderToken;
            }


            return ret_token;
        }

    }
}

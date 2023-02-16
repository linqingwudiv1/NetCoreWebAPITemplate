using BaseDLL.DTO.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;



namespace NetApplictionServiceDLL.Filter
{
    /// <summary>
    /// 类型 change 2
    /// </summary>
    public enum AuthType
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
    public class AccountAuthFilterAttribute : AuthFilterAttribute
    {
        /// <summary>
        /// Action类型
        /// </summary>
        private ActionAuthType ActionType = ActionAuthType.Unauth;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActionType"></param>
        public AccountAuthFilterAttribute(ActionAuthType ActionType = ActionAuthType.Unauth)
        {
            this.ActionType = ActionType;
        }

        /// <summary>
        /// 
        /// </summary>
        ~AccountAuthFilterAttribute()
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
            String Token = filterContext.HttpContext.Request.Query[GWebVariable.QingTokenAvg];

            switch (Token)
            {
                case string topic when String.IsNullOrWhiteSpace(topic): 
                {
                    break;
                }
                default:
                {
                    break;
                }
            }

            if ( controller != null )
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
                        .Redirect(GWebVariable.LoginUri);
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

            String HeaderToken = filterContext.HttpContext.Request.Headers[GWebVariable.QingTokenHeader];
            String CookieToken = filterContext.HttpContext.Request.Cookies[GWebVariable.QingTokenAvg];

            if ( !String.IsNullOrEmpty(HeaderToken) && !String.IsNullOrWhiteSpace(HeaderToken) )
            {
                ret_token = HeaderToken;
            }
            else if ( !String.IsNullOrEmpty(CookieToken) && !String.IsNullOrWhiteSpace(CookieToken) )
            {
                ret_token = HeaderToken;
            }

            return ret_token;
        }

    }
}

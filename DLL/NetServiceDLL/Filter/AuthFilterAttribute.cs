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

    public class AuthFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action类型
        /// </summary>
        private ActionAuthType ActionType = ActionAuthType.Unauth;
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

        private void Handle(ActionExecutingContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                var storeAccount = controller.HttpContext.Session.GetStoreAccount();
                if (storeAccount != null)
                {
                    return;
                }
                else
                {
                    UnauthorizedHandle(filterContext);
                }
            }
            else
            {
                UnauthorizedHandle(filterContext);
            }

        }

        /// <summary>
        /// 
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
                        .Redirect("http://www.bing.com");
                    break;
                }
                case ActionAuthType.Unauth:
                {
                    filterContext.Result = new UnauthorizedResult();
                    break;
                }
            }
        }
    }
}

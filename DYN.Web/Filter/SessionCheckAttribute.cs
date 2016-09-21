using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Maticsoft.Common;

namespace DYN.Framwork.Filter
{
    /// <summary>
    /// session过滤器
    /// </summary>
    public class SessionCheckAttribute : AuthorizeAttribute  
    {
        public SessionCheckAttribute()
        {
            IsChecked = true;
        }

        public bool IsChecked { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext) 
        {
            if (IsChecked)
            {
                SessionUser User = RequestSession.GetSessionUser();
                if (User == null)
                {
                    HttpContext.Current.Session["script"] = "top.location = \"/Account/Auth/Login\";";
                   
                    filterContext.Result = new RedirectResult("/Account/Auth/Login");
                }
            }
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYN.DAL;
using DYN.Model;
using Maticsoft.Common;

namespace DYN.Framwork.Filter
{
    /// <summary>
    /// 用户权限过滤器
    /// </summary>
    public class RoleCheckAttribute : ActionFilterAttribute
    {
        public RoleCheckAttribute()
        {
            IsChecked = true;
        }

        public bool IsChecked
        {
            get;
            set;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsChecked)
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                SessionUser User = RequestSession.GetSessionUser();
                string[] strQuanXian = User.QuanXian.ToString().Split(',');//用户权限


                        

                string AreaName = filterContext.RouteData.DataTokens["area"].ToString();
                string ControllerName = filterContext.RouteData.Values["controller"].ToString();
                string AcitonName = filterContext.RouteData.Values["action"].ToString();
                string CurrentURL = string.Format("/{0}/{1}/{2}", AreaName, ControllerName, AcitonName);

                int CurrentMenuID = -1;//当前的页面对应的菜单ID

                var queryMenu = unitOfWork.GetRepository<SysMenu>().ReadEntities()
                        .Select(m => new { m.ID, m.NavigateUrl })
                        .Where(m => m.NavigateUrl==CurrentURL)
                        .ToList();

                if (queryMenu.Count <= 0)
                {
                    filterContext.Result = new RedirectResult("/Account/Auth/Login");
                }
                //获取当前的页面对应的菜单ID
                CurrentMenuID = queryMenu
                        .Single()
                        .ID;

                //判断当前页面是否在 权限中
                if (!strQuanXian.Contains(CurrentMenuID.ToString()))
                {
                    filterContext.Result = new RedirectResult("/Account/Auth/Login");
                }

            }
            base.OnActionExecuting(filterContext);

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }


    }
}
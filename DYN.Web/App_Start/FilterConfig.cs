﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYN.Web.App_Start
{
    public class FilterConfig
    {
        //这个方法是用于注册全局过滤器（在Global中被调用）
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthCheckAttribute() { SessionCheck = true,RoleCheck = true});  
        }
       
    }
}
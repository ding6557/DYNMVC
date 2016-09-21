using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Maticsoft.Common
{
    /// <summary>
    /// Session 帮助类
    /// </summary>
    public class RequestSession
    {
        public RequestSession()
        {

        }
        private static string SESSION_USER = "SESSION_USER";
        public static void AddSessionUser(SessionUser user)
        {
            System.Web.HttpContext rq = System.Web.HttpContext.Current;
            rq.Session[SESSION_USER] = user;
        }
        public static SessionUser GetSessionUser()
        {
            System.Web.HttpContext rq = System.Web.HttpContext.Current;
            return (SessionUser)rq.Session[SESSION_USER];
        }

        public static void RemoveCurrentUser()
        {
            System.Web.HttpContext rq = System.Web.HttpContext.Current;
            rq.Session.Abandon();
        }
    }
}

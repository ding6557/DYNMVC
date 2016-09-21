using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYN.BLL;
using DYN.Framwork.Filter;
using DYN.Model.ViewModel;
using Maticsoft.Common;

namespace DYN.Web.Areas.Account.Controllers
{
    public class AuthController : Controller
    {


        public IAccountService service { get;set; }

        /// <summary>
        /// 登陆后的主界面
        /// </summary>
        [SessionCheck]
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// 登陆界面
        /// </summary>      
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登陆入口
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Pwd">密码</param>
        /// <param name="code">验证码</param>
        [HttpPost]       
        public int Login(string Account, string Pwd, string code)
        {
            if (Session["dt_session_code"].ToString().ToLower() != code.ToLower())
            {
                return 1;//验证码错误
            }
            int flag = service.Login(Account, Pwd);
            return flag;
        }

        [SessionCheck]
        public ActionResult Home()
        {
            LoginInfo loginInfo = service.GetLoginInfoViewModel();
            return View(loginInfo);
        }



        /// <summary>
        /// 加载手风琴列表项
        /// </summary>
        [SessionCheck]
        public ActionResult LoadFirstMenu()
        {
            string MenuJson = service.GetMenuJson();

            return Content(MenuJson);

        }

        /// <summary>
        /// 获得验证码
        /// </summary>
        public ActionResult GetVerifyCode()
        {
            string code = string.Empty;
            byte[] bytes = new VerifyCodeHelper().GetVerifyCode(ref code);
            //验证码写入Session
            Session["dt_session_code"] = code;
            return File(bytes, @"image/jpeg");
        }
    }
}
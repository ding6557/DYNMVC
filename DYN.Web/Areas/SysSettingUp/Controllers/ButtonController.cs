using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DYN.BLL;
using DYN.BLL.Imp;
using DYN.Framwork.Filter;
using DYN.Web;

namespace DYN.Framwork.Areas.SysSettingUp.Controllers
{
    [SessionCheck]
    public class ButtonController : Controller
    {
        public ISettingUpService service { get; set; }

        /// <summary>
        /// 获得当前页面的按钮
        /// </summary>
        /// <returns>按钮的HTML</returns>
        public ActionResult GetButtons()
        {
            string html_buttons = service.GetAuthButtons();

            return Content(html_buttons);
        }

    }
}
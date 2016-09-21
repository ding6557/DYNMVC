using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYN.BLL;
using DYN.BLL.Imp;
using DYN.Framwork.Filter;
using DYN.Model;
using Maticsoft.Common;

namespace DYN.Framwork.Areas.SysSettingUp.Controllers
{
    [SessionCheck]
    public class RoleController : Controller
    {
        public ISettingUpService service { get; set; }

        [RoleCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(string pqGrid_PageIndex, string pqGrid_PageSize, string pqGrid_Sort, string Parm_Key_Value)
        {
            int PageIndex = Convert.ToInt32(pqGrid_PageIndex);
            int PageSize = Convert.ToInt32(pqGrid_PageSize);

            string JsonList = service.GetRoleListJson(PageIndex, PageSize, pqGrid_Sort, Parm_Key_Value);
            return Content(JsonList);
        }

        public ActionResult Edit(int id)
        {
           var role = service.GetRole(id);
           return View(role);
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                service.UpdateRole(role);
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0006);
            }
            else
            {
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0003);
            }

            return View();
        }

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View(new Role());
        }

        [HttpPost]
        public ActionResult Add(Role role)
        {
            if (ModelState.IsValid)
            {
                service.AddRole(role);
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0006);
            }
            else
            {
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0003);
            }

            return View();
        }

        public ActionResult Delete(int id)
        {

            if (service.DeleteRole(id))
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
            

        }
        public ActionResult AllotAuth(int id)
        {
            ViewBag.StrTree_Menu = service.GetMenuTableTree_Allow(id);
            ViewBag.Role = service.GetRole(id).MingCheng;
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult AllotAuth(string QuanXian,string id)
        {
            ViewBag.Script = service.AllotAuthToUser(QuanXian,Convert.ToInt32(id));
            return View();
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYN.BLL;
using DYN.BLL.Imp;
using DYN.Framwork.Controllers;
using DYN.Framwork.Filter;
using DYN.Model;
using Maticsoft.Common;

namespace DYN.Framwork.Areas.SysSettingUp.Controllers
{
    [SessionCheck]
    [RoleCheck] 
    public class BuMenController : BaseController
    {
        public ISettingUpService service { get; set; }

       
        //
        // GET: /SysSettingUp/BuMen/
        public ActionResult Index()
        {
            ViewBag.TableTree = service.GetBuMenTableTree();
            return View();
        }

        public ActionResult Add(string ParentID)
        {
            BuMen buMen = new BuMen();
            if (!string.IsNullOrEmpty(ParentID))
            {
                buMen.ParentID = Convert.ToInt32(ParentID);
            }
            
            ViewData["select"] = new SelectList(service.XiaLaList(), "ID", "MingCheng");

            return View(buMen);
        }

        [HttpPost]
        public ActionResult Add(BuMen buMen)
        {
            ViewData["select"] = new SelectList(service.XiaLaList(), "ID", "MingCheng");
            if (ModelState.IsValid)
            {
                service.AddBuMen(buMen);
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0006);
            }
            else
            {
                //返回操作提示
                TempData["script"] = ShowMsgHelper.Alert_ErrorMsg(this.ErrorMessage);
            }
            return View(buMen);
        }

        public ActionResult Edit(string ID)
        {
            BuMen buMen = new BuMen();
            if (!string.IsNullOrEmpty(ID))
            {
                buMen = service.GetBuMen(Convert.ToInt32(ID));
            }

            ViewData["select"] = new SelectList(service.XiaLaList(), "ID", "MingCheng");

            return View(buMen);
        }

        [HttpPost]
        public ActionResult Edit(BuMen buMen)
        {
            ViewData["select"] = new SelectList(service.XiaLaList(), "ID", "MingCheng");
            if (ModelState.IsValid)
            {
                service.UpdateBuMen(buMen);
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0006);
            }
            else
            {
       
                //返回操作提示
                TempData["script"] = ShowMsgHelper.Alert_ErrorMsg(this.ErrorMessage);
            }
            return View(buMen);
        }

    }
}
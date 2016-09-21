using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYN.BLL;
using DYN.BLL.Imp;
using DYN.Framwork.Filter;
using DYN.Model;
using DYN.Model.ViewModel;
using DYN.Web;
using Maticsoft.Common;

namespace DYN.Framwork.Areas.SysSettingUp.Controllers
{
    [SessionCheck]
    [RoleCheck] 
    public class MenuController : Controller
    {

        public ISettingUpService service { get; set; }

        /// <summary>
        /// 菜单设置界面
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得tableTreeMenu的HTML
        /// </summary>      
        [RoleCheck(IsChecked = false)] 
        public ActionResult MenuTree()
        {
            string tableTreeMenu = service.GetMenuTableTree();
            return Content(tableTreeMenu);
        }

        /// <summary>
        /// 增加一个菜单节点
        /// </summary>
        /// <param name="id"></param>      
        public ActionResult AddMenu(int? id)
        {
            //获得VieModel - MenuInfo
            MenuInfo menuInfo = service.GetMenuInfoViewModel(id);

            //获得本页面下拉菜单的数据
            var XiaLaCaiDan = menuInfo.JieDianXiaLa;
            //定义一个下拉菜单
            ViewData["select"] = new SelectList(XiaLaCaiDan.OrderBy(m => m.ID), "ID", "Menu_Name");

            return View(new MenuInfo() { SysMenu = new SysMenu() { ParentID = menuInfo.SysMenu.ID} });//给下拉菜单赋默认值
        }

        [HttpPost]
        public ActionResult AddMenu(MenuInfo menuInfo)
        {
            if (ModelState.IsValid)
            {
                switch (menuInfo.SysMenu.Target)
                {
                    case "Click":
                        menuInfo.SysMenu.Menu_Type = 1;
                        break;
                    case "Iframe":
                        menuInfo.SysMenu.Menu_Type = 2;
                        break;
                    case "Onclick":
                        menuInfo.SysMenu.Menu_Type = 3;
                        break;                      
                }
                service.AddMenu(menuInfo.SysMenu);
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0005);

            }
            else
            {
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0001);
            }

            return AddMenu(menuInfo.SysMenu.ID);
        }

        /// <summary>
        /// 编辑一个菜单节点
        /// </summary>
        /// <param name="id"></param>
        public ActionResult EditMenu(int? id)
        {
            //获得VieModel - MenuInfo
            MenuInfo menuInfo = service.GetMenuInfoViewModel(id);

            //获得本页面下拉菜单的数据
            var XiaLaCaiDan = menuInfo.JieDianXiaLa;

            //定义一个下拉菜单
            ViewData["select"] = new SelectList(XiaLaCaiDan.OrderBy(m=>m.ID), "ID", "Menu_Name");



            return View(menuInfo);
        }

        [HttpPost]
        public ActionResult EditMenu(MenuInfo menuInfo)
        {
            if (ModelState.IsValid)
            {
                switch (menuInfo.SysMenu.Target)
                {
                    case "Click":
                        menuInfo.SysMenu.Menu_Type = 1;
                        break;
                    case "Iframe":
                        menuInfo.SysMenu.Menu_Type = 2;
                        break;
                    case "Onclick":
                        menuInfo.SysMenu.Menu_Type = 3;
                        break;
                }
                
                service.UpdateMenu(menuInfo.SysMenu);
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0006);

            }
            else
            {
                //返回操作提示
                TempData["script"] = ShowMsgHelper.AlertCallback(MessageHelper.MSG0001);
            }

            return EditMenu(menuInfo.SysMenu.ID);
        }


        [HttpPost]
        public ActionResult DeleteMenu(int id)
        {
            try
            {
                service.DeleteMenu(id);
                return Content("1");
            }
            catch (Exception)
            {

                return Content("0");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Size"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [RoleCheck(IsChecked = false)] 
        public ActionResult GetIcons(string Size, string PageIndex)
        {
            int pageIndex = Convert.ToInt32(PageIndex);
            int rowcount = 0;
            int rowbegin = 0;
            int rowend = 0;

            
            ViewBag.Pages = service.GetIcons(Size, pageIndex, ref rowcount, ref  rowbegin, ref  rowend);
            ViewBag.rowcount = rowcount;
            ViewBag.size = Size;
            ViewBag.pageIndex = pageIndex;
            ViewBag.rowbegin = rowbegin;
            ViewBag.rowend = rowend;
            
                       
            return View();
        }



    }
}
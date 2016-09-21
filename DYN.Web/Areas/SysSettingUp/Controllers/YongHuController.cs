using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DYN.BLL;
using DYN.BLL.Imp;

namespace DYN.Framwork.Areas.SysSettingUp.Controllers
{
    public class YongHuController : Controller
    {
        public ISettingUpService service { get; set; }

        //
        // GET: /SysSettingUp/YongHu/
        public ActionResult Index(string BuMenID,string YongHuName,string pqGrid_PageIndex, string pqGrid_PageSize, string pqGrid_Sort)
        {
            int _BuMenID = Convert.ToInt32(BuMenID);
            int _pqGrid_PageIndex = Convert.ToInt32(pqGrid_PageIndex);
            int _pqGrid_PageSize = Convert.ToInt32(pqGrid_PageSize);
            ViewBag.YongHuList = service.GetYongHuListJson(YongHuName, _BuMenID, _pqGrid_PageIndex, _pqGrid_PageSize, pqGrid_Sort);
            return View();
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;

namespace DYN.Framwork.Areas.UserControl.Controllers
{
    public class PageController : Controller
    {
        #region 参数
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get
            ; set
                ; }
        /// <summary>
        /// 开始显示数量
        /// </summary>
        public int PagestartNumber
        {
            get
            {
                return ((PageIndex - 1) * PageSize) + 1;
            }
        }
        /// <summary>
        /// 结束显示数量
        /// </summary>
        public int PageendNumber
        {
            get
            {
                return (PageIndex) * PageSize;
            }
        }
        private int _RecordCount;
        /// <summary>
        /// 总数量
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _RecordCount;
            }
            set
            {
                this._RecordCount = value;
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotaPage
        {
            get
            {
                return RecordCount % PageSize == 0 ? RecordCount / PageSize : RecordCount / PageSize + 1;
            }
        }
        #endregion

        //
        // GET: /UserControl/Page/
        public ActionResult Index()
        {

            return View();
        }


	}
}
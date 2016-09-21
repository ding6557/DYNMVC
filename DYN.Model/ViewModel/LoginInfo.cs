using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DYN.Model.ViewModel
{
    public class LoginInfo
    {
        public string Name { get; set; }

        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LatestIP { get; set; }
        /// <summary>
        /// 上一次登录IP
        /// </summary>
        public string LastIP { get; set; }
        /// <summary>
        /// 最新登录时间
        /// </summary>
        public DateTime? LatestTime { get; set; }
        /// <summary>
        /// 上一次登录时间
        /// </summary>
        public DateTime? LastTime { get; set; }
        /// <summary>
        /// 本月登录总次数
        /// </summary>
        public int ThisMonthLoginCount { get; set; }
    }
}
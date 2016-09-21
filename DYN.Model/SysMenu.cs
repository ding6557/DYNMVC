using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    [Description("系统菜单表")]
    public class SysMenu
    {
        #region 属性

        [Description("主键")]
        public int ID { get; set; }

        [Description("父ID")]
        public int ParentID { get; set; }

        [Description("菜单名称"),StringLength(50),Required]
        public string Menu_Name { get; set; }

        [Description("菜单标题"), StringLength(50)]
        public string Menu_Title { get; set; }

        [Description("菜单图标地址"), StringLength(200)]
        public string Menu_Img { get; set; }

        [Description("菜单类型")]
        public int Menu_Type { get; set; }

        [Description("菜单导航地址"), StringLength(200)]
        public string NavigateUrl { get; set; }

        [Description("地址类型"), StringLength(50)]
        public string Target { get; set; }

        [Description("顺序编号")]
        public int? SortCode { get; set; }

        [Description("备注"), StringLength(200)]
        public string BeiZhu { get; set; }

        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    [Description("部门表")]
    public class BuMen
    {
        #region 属性

        [Description("主键")]
        public int ID { get; set; }

        [Description("父ID")]
        public int ParentID { get; set; }

        [Description("部门名称"), StringLength(50), Required]
        public string MingCheng { get; set; }

        [Description("部门编码"), StringLength(50)]
        public string BianMa { get; set; }

        [Description("联系人"), StringLength(50)]
        public string LianXiRen { get; set; }

        [Description("联系电话"), StringLength(50)]
        public string LianXiDianHua { get; set; }

        [Description("地址"), StringLength(200)]
        public string DiZhi { get; set; }

        [Description("主管单位"), StringLength(50)]
        public string ZhuGuanDanWei { get; set; }

        [Description("顺便编号"), RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "顺便编号只能为1到8位的数字")]
        public int? SortCode { get; set; }

        [Description("路线"), StringLength(2000)]
        public string LuXian { get; set; }

        [Description("描述"), StringLength(1000)]
        public string Description { get; set; }

        #endregion
    }
}

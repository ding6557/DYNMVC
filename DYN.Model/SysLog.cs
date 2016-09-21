using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    public class SysLog
    {
        #region 属性
        [Description("主键")]
        public int ID { get; set; }

        [Description("用户ID"), Required,ForeignKey(null)]
        public int  YongHuID { get; set; }

        [Description("用户姓名"), StringLength(50), Required]
        public string YongHuName { get; set; }

        [Description("所在部门"), StringLength(50), Required]
        public string BuMen { get; set; }

        [Description("所在IP"), StringLength(50), Required]
        public string IP { get; set; }

        [Description("执行的SQL"), StringLength(4000), Required]
        public string ExcuteSQL { get; set; }

        [Description("创建时间"), Required]
        public DateTime CreateTime { get; set; }

        [Description("备注"), StringLength(500)]
        public string BeiZhu { get; set; }


        #endregion
    }
}

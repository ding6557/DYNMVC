using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    [Description("角色表")]
    public class Role
    {

        #region 属性
        [Description("主键")]
        public int ID { get; set; }


        [Description("角色名称"), Required]
        public string MingCheng { get; set; }


        [Description("权限"), StringLength(1000)]
        public string QuanXian { get; set; }


        [Description("备注"), StringLength(50)]
        public string BeiZhu { get; set; } 
        #endregion

        #region 导航属性
        public virtual ICollection<YongHuRole> YongHuRoles { get; set; }
    } 
        #endregion
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    [Description("用户表")]
    public class YongHu
    {
        #region 属性
        [Description("主键")]
        public int ID { get; set; }

        [Description("用户姓名"),StringLength(50)]
        public string Name { get; set; }

        [Description("登陆名"),StringLength(50),Required]
        public string LoginName { get; set; }

        [Description("登陆密码"), StringLength(50), Required]
        public string MiMa { get; set; }

        [Description("部门ID")]
        public int? BuMenID { get; set; }

        [Description("登陆时间")]
        public DateTime? DengLuTime { get; set; }

        [Description("登陆失败次数")]
        public int? ErrCount { get; set; }

        [Description("用户权限"),StringLength(1000)]
        public string QuanXian { get; set; }

        [Description("用户类型")]
        public int? Type { get; set; }

        [Description("备注"),StringLength(50)]
        public string BeiZhu { get; set; }
        #endregion

        #region 导航属性
        public virtual ICollection<YongHuRole> YongHuRoles { get; set; }
        public virtual BuMen BuMen { get; set; } 
        #endregion


    }
}

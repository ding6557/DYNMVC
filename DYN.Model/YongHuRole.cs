using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    [Description("用户角色表")]
    public class YongHuRole
    {
        #region 属性
        [Description("主键")]
        public int ID { get; set; }

        [Description("角色ID")]
        public int RoleID { get; set; }

        [Description("用户ID")]
        public int YongHuID { get; set; }
        #endregion

        #region 导航属性
        public virtual YongHu YongHu { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }
}

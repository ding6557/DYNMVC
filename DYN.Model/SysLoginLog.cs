using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.Model
{
    [Description("用户登陆日志表")]
    public class SysLoginLog
    {
        #region 属性
        [Description("主键")]
        public int ID { get; set; }

        [Description("用户ID")]
        public int YongHuID { get; set; }

        [Description("登陆IP"), StringLength(50)]
        public string SYS_LOGINLOG_IP { get; set; }

        [Description("登陆时间")]
        public DateTime? SYS_LOGINLOG_TIME { get; set; } 
        #endregion

        #region 导航属性
        public virtual YongHu YongHu { get; set; } 
        #endregion
    }
}

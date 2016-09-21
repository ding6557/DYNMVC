using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.Common
{
 
    /// <summary>
    /// 存 Session对象
    /// </summary>
    [Serializable()]  
    public class SessionUser 
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int YongHuID { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登陆账户
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string MiMa { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public object QuanXian { get; set; }

        /// <summary>
        /// 用户角色 逗号隔开的角色列表如 1,4,7
        /// </summary>
        public object JueSe { get; set; }

        /// <summary>
        /// 用户所在单位ID
        /// </summary>
        public int BuMenID { get; set; }

        /// <summary>
        /// 用户所在单位
        /// </summary>
        public string BuMenMingCheng { get; set; }

        /// <summary>
        /// 用户主管单位
        /// </summary>
        public string BuMenZhuGuanDanWei { get; set; }

        /// <summary>
        /// 用户单位标识，用户生成介绍信编号
        /// </summary>
        public int BuMenBiaoShi { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 用户最近登录IP
        /// </summary>
        public object IP { get; set; }

        /// <summary>
        /// 用户最近登录时间
        /// </summary>
        public object lastLoginTime { get; set; }


        public SessionUser(int YongHuID, string Name, string LoginName, string MiMa, object QuanXian, object JueSe)
        {
            this.YongHuID = YongHuID;
            this.Name = Name;
            this.LoginName = LoginName;
            this.MiMa = MiMa;
            this.QuanXian = QuanXian;
            this.JueSe = JueSe;
        }
        public SessionUser()
        {
        }
    }
}

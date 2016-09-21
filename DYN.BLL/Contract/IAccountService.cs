using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DYN.Model;
using DYN.Model.ViewModel;

namespace DYN.BLL
{
    public interface IAccountService:IDependency
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="LoginName">用户账户</param>
        /// <param name="Password">用户密码</param>
        /// <returns></returns>
        int Login(string LoginName,string Password);

        /// <summary>
        /// 用户登出
        /// </summary>
        void Logout();

        /// <summary>
        ///获得用户权限下的菜单Json 
        /// </summary>
        /// <returns></returns>
        string GetMenuJson();

        /// <summary>
        /// 获取前最新两行登录记录
        /// </summary>
        /// <returns></returns>
        LoginInfo GetLoginInfoViewModel();
    }
}

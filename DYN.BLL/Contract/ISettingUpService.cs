using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DYN.Model;
using DYN.Model.ViewModel;

namespace DYN.BLL
{
    public interface ISettingUpService : IDependency
    {
        /// <summary>
        /// 获得系统的全部菜单
        /// </summary>
        /// <returns>HTML字符串</returns>
        string GetMenuTableTree();


        /// <summary>
        /// 获得一个角色的所有权限菜单列表
        /// </summary>
        /// <returns>HTML字符串</returns>
        string GetMenuTableTree_Allow(int RoleID);


        /// <summary>
        /// 获得当前权限和当前页面下的按钮
        /// </summary>
        /// <returns>HTML字符串</returns>
        string GetAuthButtons();


        /// <summary>
        /// 获得所有图标
        /// </summary>
        /// <param name="Size">32/16</param>
        string GetIcons(string Size, int PageIndex, ref int RowCounts, ref int rowbegin, ref int rowend);

        /// <summary>
        /// 获得VieModel-MenuInfo
        /// </summary>
        /// <param name="MenuID">当前的菜单ID</param>
        /// <returns>VieModel.Menu</returns>
        MenuInfo GetMenuInfoViewModel(int? MenuID);

        /// <summary>
        /// 更新一个菜单节点
        /// </summary>
        /// <param name="sysMenu">菜单实体</param>
        void UpdateMenu(SysMenu sysMenu);

        /// <summary>
        /// 新增一个菜单节点
        /// </summary>
        /// <param name="sysMenu">菜单实体</param>
        void AddMenu(SysMenu sysMenu);

        /// <summary>
        /// 删除一个菜单节点
        /// </summary>
        /// <param name="ID"></param>
        void DeleteMenu(int ID);


        /// <summary>
        /// 获得角色列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="pqGrid_Sort">筛选要显示的字段</param>
        /// <param name="MingCheng">角色名称（模糊筛选）</param>
        /// <returns></returns>
        string GetRoleListJson(int pageIndex, int pageSize,string pqGrid_Sort, string MingCheng);

        /// <summary>
        /// 获得一个Role实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Role GetRole(int id);

        /// <summary>
        /// 更新一个Role实体
        /// </summary>
        /// <param name="role">Role</param>
        void UpdateRole(Role role);

        /// <summary>
        /// 将权限赋值给用户
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        string AllotAuthToUser(string auth,int RoleID);


        /// <summary>
        /// 增加一个Role实体
        /// </summary>
        /// <param name="role"></param>
        void AddRole(Role role);


        /// <summary>
        /// 删除一个Role实体
        /// </summary>
        /// <param name="id"></param>
        bool DeleteRole(int id);


        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <returns>html</returns>
        string GetBuMenTableTree();

        /// <summary>
        /// 获得部门
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        BuMen GetBuMen(int ID);

        /// <summary>
        /// 新增一个部门
        /// </summary>
        /// <param name="buMen"></param>
        void AddBuMen(BuMen buMen);

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="buMen"></param>
        void UpdateBuMen(BuMen buMen);
        
        /// <summary>
        /// 获得下拉菜单
        /// </summary>
        /// <returns></returns>
        List<BuMen> XiaLaList();

        /// <summary>
        /// 获得一个用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        YongHu GetYongHu(int ID);

        /// <summary>
        /// 获得一个用户列表Json
        /// </summary>
        string GetYongHuListJson(string Name , int BuMenID, int pageIndex, int pageSize, string pqGrid_Sort);

        /// <summary>
        /// 增加一个用户
        /// </summary>
        /// <param name="yongHu"></param>
        /// <returns></returns>
        YongHu AddYongHu(YongHu yongHu);

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="yongHu"></param>
        void UpdateYongHu(YongHu yongHu);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="yongHu"></param>
        void DeleteYongHu(YongHu yongHu);



    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DYN.BLL.Support;
using DYN.DAL;
using DYN.Model;
using DYN.Model.ViewModel;
using Maticsoft.Common;

namespace DYN.BLL.Imp
{
    public class SettingUpService : ISettingUpService
    {
        private IUnitOfWork unitOfWork;

        public SettingUpService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string GetMenuTableTree()
        {
            StringBuilder TableTree_Menu = new StringBuilder();
            var list = unitOfWork.GetRepository<SysMenu>().ReadEntities()//.Where(m => m.Menu_Type != 3)
                .OrderBy(m=>m.SortCode)
                .ToList();

            int eRowIndex = 0;
            foreach (SysMenu menu in list.Where(m=>m.ParentID==0))
            {
                string trID = "node-" + eRowIndex;
                TableTree_Menu.Append("<tr id='" + trID + "'>");
                TableTree_Menu.Append("<td style='width: 230px;padding-left:20px;'><span class=\"folder\">" +
                                      menu.Menu_Name + "</span></td>");
                if (!string.IsNullOrEmpty(menu.Menu_Img))
                    TableTree_Menu.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/" +
                                          menu.Menu_Img +
                                          "' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                else
                    TableTree_Menu.Append(
                        "<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/5005_flag.png' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                TableTree_Menu.Append("<td style='width: 60px;text-align:center;'>" +
                                      this.GetMenu_Type(menu.Menu_Type) + "</td>");
                TableTree_Menu.Append("<td style='width: 60px;text-align:center;'>" + menu.Target + "</td>");
                TableTree_Menu.Append("<td style='width: 60px;text-align:center;'>" + menu.SortCode +
                                      "</td>");
                TableTree_Menu.Append("<td>" + menu.NavigateUrl + "</td>");
                TableTree_Menu.Append("<td style='display:none'>" + menu.ID + "</td>");
                TableTree_Menu.Append("</tr>");
                //创建子节点
                TableTree_Menu.Append(GetTableTreeNode(menu.ID,list, trID));
                eRowIndex++;
            }

            return TableTree_Menu.ToString();
        }

        public string GetMenuTableTree_Allow(int RoleID)
        {
        
            StringBuilder StrTree_Menu = new StringBuilder();
            var List = unitOfWork.GetRepository<SysMenu>().ReadEntities()
                .OrderBy(m => m.SortCode)
                .ToList(); ;
            string strRoleRight = unitOfWork.GetRepository<Role>().GetByID(RoleID).QuanXian;
            if (List.Count>0)
            {
                var buttonList = List.Where(m => m.Menu_Type == 3).ToList();
                var menuList = List.Where(m => m.Menu_Type < 3).ToList();
               
                int eRowIndex = 0;
                foreach (var menu in menuList.Where(m=>m.ParentID==0))
                {
                    string trID = "node-" + eRowIndex.ToString();
                    StrTree_Menu.Append("<tr id='" + trID + "'>");
                    StrTree_Menu.Append("<td style='width: 200px;padding-left:20px;'><span class=\"folder\">" + menu.Menu_Name + "</span></td>");
                    if (!string.IsNullOrEmpty(menu.Menu_Img))
                        StrTree_Menu.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/" + menu.Menu_Img + "' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                    else
                        StrTree_Menu.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/5005_flag.png' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                    StrTree_Menu.Append("<td style=\"width: 23px; text-align: left;\"><input id='ckb" + trID + "' onclick=\"ckbValueObj(this.id)\" style='vertical-align: middle;margin-bottom:2px;' type=\"checkbox\" " + GetChecked(menu.ID.ToString(), strRoleRight) + "  value=\"" + menu.ID + "\" name=\"checkbox\" /></td>");
                    StrTree_Menu.Append("<td>" + GetButton(menu.ID, buttonList, trID, strRoleRight) + "</td>");
                    StrTree_Menu.Append("</tr>");
                    //创建子节点
                    StrTree_Menu.Append(GetTableTreeNode(menu.ID, menuList, trID, buttonList, strRoleRight));
                    eRowIndex++;
                }
            }
            return StrTree_Menu.ToString();
        }


        public string GetAuthButtons()
        {
            StringBuilder sb_Button = new StringBuilder();//返回的按钮

            string[] strQuanXian = RequestSession.GetSessionUser().QuanXian.ToString().Split(',');//用户权限

            string CurrentURL = RequestHelper.GetScriptName;//当前的访问URL

            int CurrentMenuID = -1;//当前的页面对应的菜单ID

            var queryMenu = unitOfWork.GetRepository<SysMenu>().ReadEntities();

            //获取当前的页面对应的菜单ID
            CurrentMenuID =
                queryMenu.Select(m => new {m.ID, m.NavigateUrl})
                    .Where(m => m.NavigateUrl == CurrentURL)
                    .ToList()
                    .Single()
                    .ID;

            //获取用户权限下的当前页面的按钮集合
            List<SysMenu> list_Button = queryMenu
                .Where(
                    m =>
                        strQuanXian.Contains(m.ID.ToString()) //当前权限下
                        && 
                        m.Menu_Type == 3 //获取按钮（3代表按钮）
                        && 
                        m.ParentID == CurrentMenuID//获取当前页面的按钮
                        ).ToList();

            if (list_Button.Count>0)
            {
                foreach (SysMenu button in list_Button)
                {
                    if (button.NavigateUrl==null)
                    {
                        button.NavigateUrl = "";
                    }
                    string Action = "";
                    string NavigateUrl = button.NavigateUrl.TrimEnd("\r\n".ToCharArray());
                    try
                    {
                        
                        Action = NavigateUrl.Substring(
                            NavigateUrl.LastIndexOf('/') + 1,
                            NavigateUrl.Length - NavigateUrl.LastIndexOf('/') - 1
                            );

                    }
                    catch
                    {
                    }
                    sb_Button.Append("<a title=\"" + button.Menu_Title + "\" onclick=\"" + Action + "('" + NavigateUrl + "/')" + ";\" class=\"button green\">");
                    sb_Button.Append("<span class=\"icon-botton\" style=\"background: url('/Themes/images/16/" + button.Menu_Img + "') no-repeat scroll 0px 4px;\"></span>");
                    sb_Button.Append(button.Menu_Name);
                    sb_Button.Append("</a>");
                }
            }
            else
            {
                sb_Button.Append("<a></a>");
            }
            return sb_Button.ToString();
        }

        public void UpdateMenu(SysMenu sysMenu)
        {
            unitOfWork.GetRepository<SysMenu>().Update(sysMenu);
            unitOfWork.Save();
        }

        public void AddMenu(SysMenu sysMenu)
        {
            unitOfWork.GetRepository<SysMenu>().Insert(sysMenu);
           unitOfWork.Save();
        }

        public void DeleteMenu(int ID)
        {
            unitOfWork.GetRepository<SysMenu>().Delete(ID);
            unitOfWork.Save();
        }

        public MenuInfo GetMenuInfoViewModel(int? MenuID)
        {
            MenuInfo menuInfo = new MenuInfo();
            SysMenu sysMenu;
            if (MenuID != null)
            {

                sysMenu = unitOfWork.GetRepository<SysMenu>().GetByID(MenuID);
                menuInfo.SysMenu = sysMenu;
            }

            menuInfo.JieDianXiaLa = unitOfWork.GetRepository<SysMenu>().ReadEntities()
                .Where(m => m.Menu_Type != 3).ToList();
            menuInfo.JieDianXiaLa.ForEach(m => m.Menu_Name = m.Menu_Type == 1 ? m.Menu_Name + "-父节" : m.Menu_Name + "-子节");
            menuInfo.JieDianXiaLa.Add(new SysMenu{Menu_Name = "菜单根节点", ID = 0});

            return menuInfo;

        }

        
        public string GetIcons(string Size, int  PageIndex, ref int RowCounts, ref int rowbegin, ref int rowend)
        {
            int PageSize = 0;

            StringBuilder strImg = new StringBuilder();
            
            DirectoryInfo dir;
            if (Size == "32")
            {
                PageSize = 100;
                dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Themes/Images/32/"));
            }
            else
            {
                PageSize = 200;
                dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Themes/Images/16/"));
            }
            int rowCount = 0;

            
            
            rowbegin = (PageIndex - 1) * PageSize;
            rowend = PageIndex * PageSize;

            foreach (FileInfo fsi in dir.GetFileSystemInfos())
            {
                if (rowCount >= rowbegin && rowCount < rowend)
                {
                    strImg.Append("<div class=\"divicons\" title='" + fsi.Name + "'>");
                    strImg.Append("<img src=\"/Themes/Images/" + Size + "/" + fsi.Name + "\" />");
                    strImg.Append("</div>");
                }
                rowCount++;
            }
            RowCounts = rowCount;
            return strImg.ToString();
        }


        public string GetRoleListJson(int pageIndex, int pageSize, string pqGrid_Sort,string MingCheng="")
        {
            List<Role> list = unitOfWork.GetRepository<Role>().ReadEntities().
                Where(r => r.MingCheng.Contains(MingCheng)).
                OrderBy(r=>r.ID).
                Skip(pageSize * (pageIndex - 1)).
                Take(pageSize).
                ToList();

            return JsonHelper.PqGridPageJson<Role>(list, pageIndex, pqGrid_Sort, list.Count);
        }

        public Role GetRole(int id)
        {
            var role = unitOfWork.GetRepository<Role>().Entities().Where(r => r.ID == id).ToList();
            return role.FirstOrDefault();
        }
        public void UpdateRole(Role role)
        {
            unitOfWork.GetRepository<Role>().Update(role);
           unitOfWork.Save();
        }
        public void AddRole(Role role)
        {
            unitOfWork.GetRepository<Role>().Insert(role);
            unitOfWork.Save();
        }
        public bool DeleteRole(int id)
        {
            if (
                unitOfWork.GetRepository<Role>().Entities()
                    .Select(m => new {m.ID,m.YongHuRoles.Count})
                    .Where(m => m.ID == id)
                    .ToList()[0].Count == 0)
            {
                unitOfWork.GetRepository<Role>().Delete(id);
                unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
           
            
        }
        public string AllotAuthToUser(string QuanXian, int RoleID)
        {

            List<string> NewQuanXianList = GetNewQuanXian(QuanXian);
            string NewQuanXian = "";
            NewQuanXianList.ForEach(q =>
            {
                NewQuanXian += q + ",";
            });
            try
            {
                if (!string.IsNullOrEmpty(NewQuanXian))
                {
                    var Role = unitOfWork.GetRepository<Role>().GetByID(RoleID);
                    Role.QuanXian = NewQuanXian.Trim(',');
                    unitOfWork.GetRepository<Role>().Update(Role);
                    unitOfWork.Save();
                }
                return ShowMsgHelper.Alert_SuccessMsg("操作成功");
            }
            catch
            {

                return ShowMsgHelper.Alert_Error("操作失败！");
            }


        }
        public string GetBuMenTableTree()
        {
            StringBuilder TableTreeList = new StringBuilder();
            var listBuMen = unitOfWork.GetRepository<BuMen>().ReadEntities().ToList();
            int eRowIndex = 0;
            foreach (BuMen mod in listBuMen)
            {
                if (mod.ParentID == 0)
                {
                    string trID = "node-" + eRowIndex.ToString();
                    TableTreeList.Append("<tr id='" + trID + "'>");
                    TableTreeList.Append("<td style='width: 230px;padding-left:20px;'><img src='/Themes/images/16/house.png' style='vertical-align: middle;' alt=''/><span style='padding-left:8px;'>" + mod.MingCheng + "</span></td>");
                    TableTreeList.Append("<td style='width: 60px;'>" + mod.BianMa + "</td>");
                    TableTreeList.Append("<td style='width: 80px;text-align:center;'>" + mod.LianXiRen + "</td>");
                    TableTreeList.Append("<td style='width: 120px;text-align:center;'>" + mod.LianXiDianHua + "</td>");
                    TableTreeList.Append("<td style='width: 100px;text-align:center;'>" + mod.DiZhi + "</td>");
                    TableTreeList.Append("<td style='width: 100px;text-align:center;'>" + mod.ZhuGuanDanWei + "</td>");
                    //TableTreeList.Append("<td style='width: 100px;text-align:center;'>" + mod.LuXian + "</td>");
                    TableTreeList.Append("<td style='width: 60px;text-align:center;'>" + mod.SortCode + "</td>");
                    //TableTreeList.Append("<td>" + mod.Description + "</td>");
                    TableTreeList.Append("<td style='display:none'>" + mod.ID + "</td>");
                    TableTreeList.Append("</tr>");
                    //创建子节点
                    TableTreeList.Append(GetTableTreeNodeForBuMen(mod.ID, listBuMen, trID));
                    eRowIndex++;
                }
            }
            return TableTreeList.ToString();
        }

        public BuMen GetBuMen(int ID)
        {
            return unitOfWork.GetRepository<BuMen>().GetByID(ID);
        }
        public void UpdateBuMen(BuMen buMen)
        {
            unitOfWork.GetRepository<BuMen>().Update(buMen);
            unitOfWork.Save();
        }
        public void AddBuMen(BuMen buMen)
        {
            unitOfWork.GetRepository<BuMen>().Insert(buMen);
            unitOfWork.Save();
        }

        public YongHu GetYongHu(int ID)
        {
            return unitOfWork.GetRepository<YongHu>().GetByID(ID);
        }

        public string GetYongHuListJson(string Name, int BuMenID, int pageIndex, int pageSize, string pqGrid_Sort)
        {
            if (Name == null)
            {
                Name = "";
            }
            List<YongHu> list = unitOfWork.GetRepository<YongHu>().ReadEntities().
                Where(y => y.Name.Contains(Name) && y.BuMenID == BuMenID).
                OrderBy(r=>r.ID).
                Skip(pageSize * (pageIndex - 1)).
                Take(pageSize).
                ToList();
            return JsonHelper.PqGridPageJson<YongHu>(list, pageIndex, pqGrid_Sort, list.Count);
        }

        public YongHu AddYongHu(YongHu yongHu)
        {
            YongHu Yonghu = unitOfWork.GetRepository<YongHu>().Insert(yongHu);
            unitOfWork.Save();
            return Yonghu;
        }

        public void UpdateYongHu(YongHu yongHu)
        {
            unitOfWork.GetRepository<YongHu>().Update(yongHu);
            unitOfWork.Save();
        }

        public void DeleteYongHu(YongHu yongHu)
        {
            unitOfWork.GetRepository<YongHu>().Delete(yongHu);
            unitOfWork.Save();
        }

        public List<BuMen> XiaLaList()
        {
            var List = unitOfWork.GetRepository<BuMen>().ReadEntities().Select(b => new { b.ID, b.MingCheng }).ToList();
            List<BuMen> buMenList = new List<BuMen>(); 
            List.ForEach(item =>
            {
                buMenList.Add(new BuMen{ID = item.ID,MingCheng = item.MingCheng});
            });
            return buMenList;
        }

        #region 本类私有方法
        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentID">父节点主键</param>
        /// <param name="dtMenu"></param>
        /// <returns></returns>
        private string GetTableTreeNode(int parentID, List<SysMenu> list, string parentTRID)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            int i = 1;
            foreach (SysMenu menu in list.Where(m => m.ParentID == parentID))
            {
                string trID = parentTRID + "-" + i.ToString();
                sb_TreeNode.Append("<tr id='" + trID + "' class='child-of-" + parentTRID + "'>");

                if (menu.Menu_Type == 3)
                {
                    sb_TreeNode.Append("<td style='padding-left:20px;'><span class=\"button\"><img src='/Themes/images/32/" + (menu.Menu_Img ?? "menu.Menu_Img") + "' style='width:16px; height:16px;vertical-align: middle;' alt=''/>" + menu.Menu_Name + "</span></td>");
                }
                else
                {
                    sb_TreeNode.Append("<td style='padding-left:20px;'><span class=\"folder\">" + menu.Menu_Name + "</span></td>");
                }
               
                if (!string.IsNullOrEmpty(menu.Menu_Img))
                    sb_TreeNode.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/" + menu.Menu_Img + "' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                else
                    sb_TreeNode.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/5005_flag.png' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                sb_TreeNode.Append("<td style='width: 60px;text-align:center;'>" + this.GetMenu_Type(menu.Menu_Type) + "</td>");
                sb_TreeNode.Append("<td style='width: 60px;text-align:center;'>" + menu.Target + "</td>");
                sb_TreeNode.Append("<td style='width: 60px;text-align:center;'>" + menu.SortCode + "</td>");
                sb_TreeNode.Append("<td>" + menu.NavigateUrl + "</td>");
                sb_TreeNode.Append("<td style='display:none'>" + menu.ID + "</td>");
                sb_TreeNode.Append("</tr>");
                //创建子节点
                sb_TreeNode.Append(GetTableTreeNode(menu.ID,list, trID));
                i++;
            }
            return sb_TreeNode.ToString();
        }
        /// <summary>
        /// 菜单类型
        /// </summary>
        /// <param name="Menu_Type">类型</param>
        /// <returns></returns>
        private string GetMenu_Type(int Menu_Type)
        {
            if (Menu_Type == 1)
            {
                return "父节";
            }
            else if (Menu_Type == 2)
            {
                return "子节";
            }
            else if (Menu_Type == 3)
            {
                return "按钮";
            }
            else
            {
                return "其他";
            }
        }


        /// <summary>
        /// 获取导航菜单所属按钮
        /// </summary>
        /// <param name="Menu_Type">类型</param>
        /// <returns></returns>
        public string GetButton(int Menu_Id, List<SysMenu> btList, string parentTRID, string strRoleRight)
        {
            StringBuilder ButtonHtml = new StringBuilder(); ;
            var buttonList = btList.Where(b => b.ParentID == Menu_Id).ToList();
            if (buttonList.Count>0)
            {
                int i = 1;
                foreach (var button in buttonList)
                {
                    string trID = parentTRID + "--" + i.ToString();
                    ButtonHtml.Append("<lable><input id='ckb" + trID + "' onclick=\"ckbValueObj(this.id)\" " + GetChecked(button.ID.ToString(), strRoleRight) + " style='vertical-align: middle;margin-bottom:2px;' type=\"checkbox\" value=\"" + button.ID + "\" name=\"checkbox\" />");
                    ButtonHtml.Append("" + button.Menu_Name + "</lable>&nbsp;&nbsp;&nbsp;&nbsp;");
                    i++;
                }
                return ButtonHtml.ToString();
            }
            return ButtonHtml.ToString();
        }
        /// <summary>
        /// 验证权限是否存在
        /// </summary>
        /// <param name="Menu_Id">权限主键</param>
        /// <param name="dt">加载所属角色权限</param>
        /// <returns></returns>
        private string GetChecked(string Menu_Id, string strRoleRight)
        {
            string rusult = "";
            if (string.IsNullOrWhiteSpace(strRoleRight))
            {
                return "";
            }
            string[] arrays = strRoleRight.Split(',');
            foreach (string roles in arrays)
            {
                if (roles == Menu_Id)
                    rusult = "checked=\"checked\"";

            }
            return rusult;

        }


        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentID">父节点主键</param>
        /// <returns></returns>
        private string GetTableTreeNode(int parentID, List<SysMenu> mList, string parentTRID, List<SysMenu> btList, string strRoleRight)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            int i = 1;
            foreach (var menu in mList.Where(m => m.ParentID == parentID))
            {
                string trID = parentTRID + "-" + i.ToString();
                sb_TreeNode.Append("<tr id='" + trID + "' class='child-of-" + parentTRID + "'>");
                sb_TreeNode.Append("<td style='padding-left:20px;'><span class=\"folder\">" + menu.Menu_Name + "</span></td>");
                if (!string.IsNullOrEmpty(menu.Menu_Img))
                    sb_TreeNode.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/" + menu.Menu_Img + "' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                else
                    sb_TreeNode.Append("<td style='width: 30px;text-align:center;'><img src='/Themes/images/32/5005_flag.png' style='width:16px; height:16px;vertical-align: middle;' alt=''/></td>");
                sb_TreeNode.Append("<td style=\"width: 23px; text-align: left;\"><input id='ckb" + trID + "' onclick=\"ckbValueObj(this.id)\" " + GetChecked(menu.ID.ToString(), strRoleRight) + " style='vertical-align: middle;margin-bottom:2px;' type=\"checkbox\" value=\"" + menu.ID + "\" name=\"checkbox\" /></td>");
                sb_TreeNode.Append("<td>" + GetButton(menu.ID, btList, trID, strRoleRight) + "</td>");
                sb_TreeNode.Append("</tr>");
                //创建子节点
                sb_TreeNode.Append(GetTableTreeNode(menu.ID, mList, trID, btList, strRoleRight));
                i++;
            }
            return sb_TreeNode.ToString();
        }


       

        private List<string> GetNewQuanXian(string auths)
        {
            List<string> QuanXian = auths.Split(',').ToList();//赋值权限
            List<string> CurrentQuanXian = RequestSession.GetSessionUser().QuanXian.ToString().Split(',').ToList();//当前权限

            QuanXian.ForEach(q =>
            {
                if (!CurrentQuanXian.Contains(q))
                {
                   CurrentQuanXian.Add(q);
                }
            });

            CurrentQuanXian.ToList().ForEach(q =>
            {
                if (!QuanXian.Contains(q))
                {
                    CurrentQuanXian.Remove(q);
                }
            });


            return CurrentQuanXian;
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentID">父节点主键</param>
        /// <param name="list">菜单集合</param>
        /// <returns></returns>
        private string GetTableTreeNodeForBuMen(int parentID, List<BuMen> list, string parentTRID)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            int i = 1;
            foreach (Model.BuMen entity in list)
            {
                if (entity.ParentID == parentID)
                {
                    string trID = parentTRID + "-" + i.ToString();
                    sb_TreeNode.Append("<tr id='" + trID + "' class='child-of-" + parentTRID + "'>");
                    sb_TreeNode.Append("<td style='padding-left:20px;'><img src='/Themes/images/16/wand.png' style='vertical-align: middle;' alt=''/><span style='padding-left:8px;'>" + entity.MingCheng + "</span></td>");
                    sb_TreeNode.Append("<td style='width: 60px;'>" + entity.BianMa + "</td>");
                    sb_TreeNode.Append("<td style='width: 80px;text-align:center;'>" + entity.LianXiRen + "</td>");
                    sb_TreeNode.Append("<td style='width: 120px;text-align:center;'>" + entity.LianXiDianHua + "</td>");
                    sb_TreeNode.Append("<td style='width: 100px;text-align:center;'>" + entity.DiZhi + "</td>");
                    sb_TreeNode.Append("<td style='width: 100px;text-align:center;'>" + entity.ZhuGuanDanWei + "</td>");
                    //sb_TreeNode.Append("<td style='width: 100px;text-align:center;'>" + entity.LuXian + "</td>");
                    sb_TreeNode.Append("<td style='width: 50px;text-align:center;'>" + entity.SortCode + "</td>");
                    //sb_TreeNode.Append("<td>" + entity.Description + "</td>");
                    sb_TreeNode.Append("<td style='display:none'>" + entity.ID + "</td>");
                    sb_TreeNode.Append("</tr>");
                    //创建子节点
                    sb_TreeNode.Append(GetTableTreeNodeForBuMen(entity.ID, list, trID));
                    i++;
                }
            }
            return sb_TreeNode.ToString();
        }

        #endregion






      

    }
}

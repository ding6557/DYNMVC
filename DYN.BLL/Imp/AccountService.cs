using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DYN.BLL.Imp;
using DYN.BLL.Support;
using DYN.DAL;
using DYN.Model;
using DYN.Model.ViewModel;
using Maticsoft.Common;



namespace DYN.BLL
{
    public class AccountService : IAccountService
    {

        private IUnitOfWork unitOfWork;       

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #region 接口实现
        public int Login(string LoginName, string Password)
        {
            //IPScanerHelper objScan = new IPScanerHelper();
            // objScan.IP = IPAddress;
            // string IPAddress = RequestHelper.GetIPAddress();       
            YongHu yongHu;
            SysLoginLog sysLoginLog = new SysLoginLog();
            yongHu = unitOfWork.GetRepository<YongHu>().Get(u => u.LoginName == LoginName, null, "BuMen").FirstOrDefault();
            try
            {
                if (yongHu == null)
                {
                    return 7; //该用户已不存在！
                }
                else
                {
                    //if (Md5Helper.MD5(Pwd.Trim(), 32) == modYongHu.MiMa)
                    if (Password.Trim() == yongHu.MiMa)
                    {
                        if (Islogin(yongHu.LoginName))
                        {
                            yongHu.DengLuTime = System.DateTime.Now;
                            yongHu.ErrCount = 0;
                            sysLoginLog.YongHuID = yongHu.ID;
                            sysLoginLog.SYS_LOGINLOG_TIME = System.DateTime.Now;
                            sysLoginLog.SYS_LOGINLOG_IP = RequestHelper.GetIPAddress();
                            unitOfWork.GetRepository<YongHu>().Update(yongHu);
                            unitOfWork.GetRepository<SysLoginLog>().Insert(sysLoginLog);

                            new SessionManager().AddSession(yongHu);
                            return 3; //验证成功

                        }
                        else
                        {
                            return 6; //该用户已经登录，不允许重复登录

                        }

                    }
                    else
                    {
                        if (yongHu.ErrCount == null)
                        {
                            yongHu.ErrCount = 0;
                            unitOfWork.GetRepository<YongHu>().Update(yongHu);
                        }
                        //if (yongHu.ErrCount <= 3)
                        //{
                        yongHu.DengLuTime = System.DateTime.Now;
                        yongHu.ErrCount = yongHu.ErrCount + 1;
                        return 4; //账户或者密码有错误！

                        //}
                        //else
                        //{
                        //    DateTime yongHuLoginTime = Convert.ToDateTime(yongHu.DengLuTime);
                        //    if (System.DateTime.Now - yongHuLoginTime < new TimeSpan(0,0,10) )
                        //    {
                        //        return 8; //密码错误三次，请在10分钟后再次登录！

                        //    }

                        //}
                    }
                }
            }
            catch
            {
                unitOfWork.Dispose();
                return 0; //系统错误
            }
            finally
            {
                unitOfWork.Save();
            }

        }

        public void Logout()
        {
            RequestSession.RemoveCurrentUser();
        }

        public LoginInfo GetLoginInfoViewModel()
        {
            LoginInfo loginInfo = new LoginInfo();
            int YonghuID = Convert.ToInt32(RequestSession.GetSessionUser().YongHuID);
            var list = unitOfWork.GetRepository<SysLoginLog>().ReadEntities()
                 .Where(s => s.YongHuID == YonghuID)
                 .OrderByDescending(s => s.ID)
                 .Take(2)
                 .ToList();
            int count = unitOfWork.GetRepository<SysLoginLog>().ReadEntities().Count(s => s.YongHuID == YonghuID);
            loginInfo.LastIP = list.Last().SYS_LOGINLOG_IP;
            loginInfo.LatestIP = list.First().SYS_LOGINLOG_IP;
            loginInfo.LastTime = list.Last().SYS_LOGINLOG_TIME;
            loginInfo.LatestTime = list.First().SYS_LOGINLOG_TIME;
            loginInfo.ThisMonthLoginCount = count;
            loginInfo.Name = RequestSession.GetSessionUser().Name.ToString();

            return loginInfo;
        }

        public string GetMenuJson()
        {
            string[] strQuanXian = RequestSession.GetSessionUser().QuanXian.ToString().Split(',');//用户权限数组

            // string strWhere = " ID in (" + strQuanXian + ") AND Target IN ('Click','Iframe')";

            var list = unitOfWork.GetRepository<SysMenu>().ReadEntities().
                Where(m => strQuanXian.Contains(m.ID.ToString()) && (m.Target == "Click" || m.Target == "Iframe"))
                .OrderBy(m => m.SortCode)
                .ToList();

            string strMenus = JsonHelper.ListToJson<SysMenu>(list, "MENU");

            return strMenus;
        }

        #endregion


        #region 本类私有方法
        /// <summary>
        /// 同一账号不能同时登陆
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="User_Account">账户</param>
        /// <returns></returns>
        private bool Islogin(string User_Account)
        {
            //将Session转换为Arraylist数组
            //ArrayList list = context.Session["GLOBAL_USER_LIST"] as ArrayList;
            //if (list == null)
            //{
            //    list = new ArrayList();
            //}
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (User_Account == (list[i] as string))
            //    {
            //        //已经登录了，提示错误信息 
            //        return false; ;
            //    }
            //}
            ////将用户信息添加到list数组中
            //list.Add(User_Account);
            ////将数组放入Session
            //context.Session.Add("GLOBAL_USER_LIST", list);
            return true;
        }
        #endregion
    }
}

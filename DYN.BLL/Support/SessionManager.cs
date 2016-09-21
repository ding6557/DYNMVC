using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DYN.DAL;
using DYN.Model;
using Maticsoft.Common;

namespace DYN.BLL.Support
{
    public class SessionManager
    {
        private IUnitOfWork unitOfWork;

        public SessionManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public SessionManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public SessionManager()
        {
            unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// 添加登录用户到session
        /// </summary>
        /// <param name="yongHu"></param>
        public  void AddSession(YongHu yongHu)
        {            
            SessionUser user = new SessionUser();
            user.YongHuID = yongHu.ID;
            user.LoginName = yongHu.LoginName;
            user.MiMa = yongHu.MiMa;
            user.type = Convert.ToInt32(yongHu.Type);
            user.JueSe = yongHu.YongHuRoles.ToList();
            user.BuMenID = Convert.ToInt32(yongHu.BuMenID);
            user.IP = RequestHelper.GetIPAddress();
            BuMen modBuMen = yongHu.BuMen;
            user.BuMenID = Convert.ToInt32(yongHu.BuMenID);
            if (modBuMen != null)
            {
                user.BuMenMingCheng = modBuMen.MingCheng;
                user.BuMenZhuGuanDanWei = modBuMen.ZhuGuanDanWei;
                user.BuMenBiaoShi = Convert.ToInt32(modBuMen.SortCode);
            }



            var ListRole =
                unitOfWork.GetRepository<YongHuRole>().ReadEntities()
                    .Where(r => r.YongHu.ID == yongHu.ID)
                    .Select(r => new { r.Role.QuanXian })
                    .ToList();
            string userRight = "-1";

            ListRole.ForEach(quxian => userRight = userRight + "," + quxian.QuanXian);

            user.QuanXian = yongHu.QuanXian + "," + userRight;
            user.QuanXian = user.QuanXian.ToString().TrimEnd(',');
            user.QuanXian = user.QuanXian.ToString().TrimStart(',');
            user.Name = yongHu.Name;
            RequestSession.AddSessionUser(user);
        }

        /// <summary>
        /// 刷新当前用户session
        /// </summary>
        /// <param name="yongHu"></param>
        public void RefreshSession()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            SessionUser user = RequestSession.GetSessionUser();
            YongHu yongHu =
                unitOfWork.GetRepository<YongHu>().ReadEntities()
                    .Where(y => y.ID ==user.YongHuID)
                    .ToList()
                    .Single();

            user.MiMa = yongHu.MiMa;
            user.JueSe = yongHu.YongHuRoles.ToList();
            user.BuMenID = Convert.ToInt32(yongHu.BuMenID);

            BuMen modBuMen = yongHu.BuMen;
            user.BuMenID = Convert.ToInt32(yongHu.BuMenID);
            if (modBuMen != null)
            {
                user.BuMenMingCheng = modBuMen.MingCheng;
                user.BuMenZhuGuanDanWei = modBuMen.ZhuGuanDanWei;
                user.BuMenBiaoShi = Convert.ToInt32(modBuMen.SortCode);
            }

            var ListRole =
                unitOfWork.GetRepository<YongHuRole>().ReadEntities()
                    .Where(r => r.YongHu.ID == yongHu.ID)
                    .Select(r => new { r.Role.QuanXian })
                    .ToList();
            string userRight = "-1";

            ListRole.ForEach(quxian => userRight = userRight + "," + quxian.QuanXian);

            user.QuanXian = yongHu.QuanXian + "," + userRight;
            user.QuanXian = user.QuanXian.ToString().TrimEnd(',');
            user.QuanXian = user.QuanXian.ToString().TrimStart(',');
            user.Name = yongHu.Name;
            RequestSession.AddSessionUser(user);
        }
    }
}

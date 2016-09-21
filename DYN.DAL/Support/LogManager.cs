using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DYN.DAL;
using DYN.Model;
using Maticsoft.Common;

namespace DYN.DAL.Support
{
    public class LogManager
    {   
        public static void LogSqlToDB(string sql,DbContext db)
        {
            SysLog sysLog = new SysLog();
            SessionUser currentUser = RequestSession.GetSessionUser();
            if (currentUser != null)
            {
                sysLog.YongHuID = Convert.ToInt32(currentUser.YongHuID);
                sysLog.YongHuName = currentUser.Name.ToString();
                sysLog.BuMen = currentUser.BuMenMingCheng ?? "无";
                sysLog.IP = currentUser.IP.ToString();
                sysLog.ExcuteSQL = sql;
                sysLog.CreateTime = DateTime.Now;
            }
            
            db.Set<SysLog>().Add(sysLog);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                
                throw ex;
            }
           


        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYN.DAL
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            //默认更新ReportingDbMigrationsConfiguration的配置
         //   Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, ReportingDbMigrationsConfiguration>()); 

            
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataBaseContext>());
        }

       internal class ReportingDbMigrationsConfiguration : DbMigrationsConfiguration<DataBaseContext>
        {
            public ReportingDbMigrationsConfiguration()
            {
             
                AutomaticMigrationsEnabled = true;//任何Model Class的修改將會直接更新DB
                AutomaticMigrationDataLossAllowed = true;
            }
        }
        
    }
   

}

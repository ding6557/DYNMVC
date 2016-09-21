using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DYN.Model;

namespace DYN.DAL
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext()
            : base("DbString")
        {
            //Database.Log = x => { System.Diagnostics.Debug.WriteLine(x); };
            
            DatabaseInitializer.Initialize();
        }

        public DbSet<YongHu> YongHus { get; set; }

        public DbSet<YongHuRole> YongHuRoles { get; set; }

        public DbSet<BuMen> BuMens { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<SysLoginLog> SysLoginLogs { get; set; }

        public DbSet<SysMenu> SysMenus { get; set; }

        public DbSet<SysLog> SysLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //指定单数形式的表名
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            
            //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DYN.DAL;
using DYN.DAL.Support;
using DYN.Model;

namespace DYN.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
              

        #region 私有属性
        private readonly DataBaseContext _context;
        private Dictionary<string, object> _repositoryPool;  
        #endregion

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UnitOfWork()
        {
            _context = new DataBaseContext();
            _repositoryPool = new Dictionary<string, object>();
        }



       
        public GenericRepository<T> GetRepository<T>() where T : class
        {
            string T_Name = typeof(T).FullName;
            object  repository;
            _repositoryPool.TryGetValue(T_Name,out repository);
            if (repository == null)
            {
                repository = new GenericRepository<T>(_context);
                _repositoryPool.Add(T_Name, repository);
            }
            return repository as GenericRepository<T>;
        }
       
       

        #region Save & Dispose
        public void Save(bool isLog = true)
        {
            string sql = "";
            _context.Database.Log = (text) => { sql += text; };
            var rs = _context.SaveChanges();
            if (!String.IsNullOrEmpty(sql) && isLog)
            {
                LogManager.LogSqlToDB(sql,_context);
            }
           
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
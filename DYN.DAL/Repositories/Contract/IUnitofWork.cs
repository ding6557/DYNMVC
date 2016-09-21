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
    public interface IUnitOfWork : IDisposable, IDependency
    {
        /// <summary>
        /// 获得一个实体的仓储
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        GenericRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// 保存当前操作
        /// </summary>
        /// <param name="isLog">是否记录保存当前操作的日志</param>
        void Save(bool isLog=true);

    }

}
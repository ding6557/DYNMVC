using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DYN.Model;

namespace DYN.DAL
{
    interface IGenericRepository<TEntity> :IDependency, IDisposable where TEntity : class

    {
        /// <summary>
        /// 根据条件获得实体集
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="includeProperties">显示加载的导航属性（多个以，号隔开）</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity,bool>> filter=null,Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,string includeProperties="");

        /// <summary>
        /// 获得实体集（状态跟踪）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Entities();
        /// <summary>
        /// 获得实体集（非状态跟踪）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> ReadEntities();

        /// <summary>
        /// 根据主键获得一个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        TEntity GetByID(object id);

        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <param name="entity"></param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="id">主键</param>
        void Delete(object id);

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        void Update(TEntity entity);

        /// <summary>
        /// 保存修改
        /// </summary>
        void Save();

    }


 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XmTest.IRepository
{
    /// <summary>
    /// IDAL数据访问接口层
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity:class,new ()
    {
        void Insert(TEntity tEntity);
        void Delete(TEntity tEntity);
        void Update(TEntity tEntity);

        bool SaveChanges();
        
        TEntity GetModel(Func<TEntity,bool> whereLambda);
        List<TEntity> GetList(Func<TEntity, bool> whereLambda);
        List<TEntity> GetPagedList<TType>(int pageSize, int pageIndex, bool isAsc, Expression<Func<TEntity, TType>> OrderByLambda, Expression<Func<TEntity, bool>> whereLambda);

    }
}

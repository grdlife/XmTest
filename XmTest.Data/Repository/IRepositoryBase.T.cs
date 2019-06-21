using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace XmTest.Data.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity :class,new()
    {
        bool Insert(TEntity tEntity);
        bool Delete(TEntity tEntity);
        bool Update(TEntity tEntity);

        bool SaveChanges();

        TEntity GetModel(Func<TEntity, bool> whereLambda);
        List<TEntity> GetList(Func<TEntity, bool> whereLambda);
        List<TEntity> GetPagedList<TType>(int pageSize, int pageIndex, bool isAsc, Expression<Func<TEntity, TType>> OrderByLambda, Expression<Func<TEntity, bool>> whereLambda);
    }
}

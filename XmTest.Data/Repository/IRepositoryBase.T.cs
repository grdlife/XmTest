using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XmTest.Basic.Web;

namespace XmTest.Data.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class,new()
    {
        bool Insert(TEntity tEntity);
        bool Insert(List<TEntity> tEntitys);

        bool Update(TEntity tEntity);

        bool Delete(TEntity tEntity);
        bool Delete(Expression<Func<TEntity, bool>> predicate);

        bool SaveChanges();
        TEntity GetModel(object keyvalue);
        TEntity GetModel(Expression<Func<TEntity, bool>> whereLambda);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> whereLambda);
        List<TEntity> GetPagedList<TType>(int pageIndex, int pageSize, bool isAsc, Expression<Func<TEntity, TType>> OrderByLambda, Expression<Func<TEntity, bool>> whereLambda);


        IQueryable<TEntity> IQueryable();

        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> FindList(string sqlstr);
        List<TEntity> FindList(string sqlstr, DbParameter[] param);

        List<TEntity> FindList(Page page);

        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Page page);


    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XmTest.Data.DBEntity;
using XmTest.Data.Factory;

namespace XmTest.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        private readonly XmDBConetext _dbContext = DbFactory.GetInstance();
        private readonly DbSet<TEntity> _dbSet;
        public RepositoryBase()
        {
            _dbSet = _dbContext.Set<TEntity>();
        }

        public bool Delete(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbSet.Remove(tEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Insert(TEntity tEntity)
        {
            _dbSet.Add(tEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public List<TEntity> GetList(Func<TEntity, bool> whereLambda)
        {
            return _dbSet.Where(whereLambda).ToList();
        }

        public List<TEntity> GetPagedList<TType>(int pageSize, int pageIndex, bool isAsc, Expression<Func<TEntity, TType>> OrderByLambda, Expression<Func<TEntity, bool>> whereLambda)
        {
            var result = _dbSet.Where(whereLambda);
            result = isAsc ? result.OrderBy(OrderByLambda) : result.OrderByDescending(OrderByLambda);
            result = result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return result.ToList();
        }

        public TEntity GetModel(Func<TEntity, bool> whereLambda)
        {
            return _dbSet.FirstOrDefault(whereLambda);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbContext.Entry(tEntity).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }


    }
}

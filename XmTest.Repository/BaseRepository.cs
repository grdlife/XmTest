using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using XmTest.IRepository;
using XmTest.Models;
using System.Data.Entity;
namespace XmTest.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly _dbContext _dbContext = DbFactory.GetInstance();
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository()
        {
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Delete(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbSet.Remove(tEntity);
        }

        public void Insert(TEntity tEntity)
        {
            _dbSet.Add(tEntity);

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

        public void Update(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbContext.Entry(tEntity).State = EntityState.Modified;

        }
    }
}

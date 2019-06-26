using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using XmTest.Data.Entity;
using XmTest.Data.Factory;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using XmTest.Basic.Web;
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

        public bool Insert(TEntity tEntity)
        {
            _dbSet.Add(tEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Insert(List<TEntity> tEntitys)
        {
            foreach (var tEntity in tEntitys)
            {
                _dbSet.Add(tEntity);
                _dbContext.Entry<TEntity>(tEntity).State = EntityState.Added;
            }
            return _dbContext.SaveChanges() > 0;
        }


        public bool Delete(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            _dbContext.Entry<TEntity>(tEntity).State = EntityState.Deleted;
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitys = _dbSet.Where(predicate).ToList();
            entitys.ForEach(m => _dbContext.Entry<TEntity>(m).State = EntityState.Deleted );
            return _dbContext.SaveChanges() > 0;
        }



        public bool Update(TEntity tEntity)
        {
            _dbSet.Attach(tEntity);
            PropertyInfo[] props = tEntity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(tEntity, null) == null)
                    _dbContext.Entry(tEntity).Property(prop.Name).CurrentValue = null;
                _dbContext.Entry(tEntity).Property(prop.Name).IsModified = true;
            }
            return _dbContext.SaveChanges() > 0;
        }



        public List<TEntity> GetList(Expression<Func<TEntity, bool>> whereLambda)
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


        public IQueryable<TEntity> IQueryable()
        {
            return _dbSet;
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }


        public List<TEntity> FindList(string sqlstr)
        {
            return _dbContext.Database.SqlQuery<TEntity>(sqlstr).ToList();
        }

        public List<TEntity> FindList(string sqlstr, DbParameter[] param)
        {
            return _dbContext.Database.SqlQuery<TEntity>(sqlstr, param).ToList();
        }


        public List<TEntity> FindList(Page page)
        {
            bool isAsc = page.sorttype.ToLower() == "asc" ? true : false;
            string[] _order = page.sortcol.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbContext.Set<TEntity>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            page.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(page.pagesize * (page.pageindex - 1)).Take<TEntity>(page.pagesize).AsQueryable();
            return tempData.ToList();
        }
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Page page)
        {
            bool isAsc = page.sorttype.ToLower() == "asc" ? true : false;
            string[] _order = page.sortcol.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbSet.Where(predicate);
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            page.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(page.pagesize * (page.pageindex - 1)).Take<TEntity>(page.pagesize).AsQueryable();
            return tempData.ToList();
        }

        /// <summary>
        /// 根据主键值查询
        /// </summary>
        public TEntity GetModel(object keyvalue)
        {
            return _dbSet.Find(keyvalue);
        }

        public TEntity GetModel(Expression<Func<TEntity, bool>> whereLambda)
        {
            return _dbSet.FirstOrDefault(whereLambda);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }




    }
}

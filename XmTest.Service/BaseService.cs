using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.IService;
using System.Data;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using XmTest.IRepository;
namespace XmTest.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        public readonly IBaseRepository<TEntity> _baseRepository;
        public BaseService(IBaseRepository<TEntity> baseReposity)
        {
            _baseRepository = baseReposity;
        }
        public bool Add(TEntity tEntity)
        {
            _baseRepository.Insert(tEntity);
            return _baseRepository.SaveChanges();
        }

        public List<TEntity> GetList(Func<TEntity, bool> whereLambda)
        {
            return _baseRepository.GetList(whereLambda);
        }

        public List<TEntity> GetPagedList<TType>(int pageSize, int pageIndex, bool isAsc, Expression<Func<TEntity, TType>> OrderByLambda, Expression<Func<TEntity, bool>> whereLambda)
        {
            return _baseRepository.GetPagedList(pageSize, pageIndex, isAsc, OrderByLambda, whereLambda);
        }

        public TEntity GetModel(Func<TEntity, bool> whereLambda)
        {
            return _baseRepository.GetModel(whereLambda);
        }

        public bool Modify(TEntity tEntity)
        {
            _baseRepository.Update(tEntity);
            return _baseRepository.SaveChanges();
        }

        public bool Remove(TEntity tEntity)
        {
            _baseRepository.Delete(tEntity);
            return _baseRepository.SaveChanges();
        }
    }
}

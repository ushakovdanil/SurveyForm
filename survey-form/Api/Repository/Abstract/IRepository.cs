using System;
using System.Linq.Expressions;
using Api.Models.Entities.Abstract;

namespace Api.Repository.Abstract
{
	public interface IRepository<T> where T: IBaseEntity
	{
        IQueryable<T> List();
        IQueryable<T> FilteredList(Expression<Func<T, bool>> predicate);
        IQueryable<T> FilteredList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}


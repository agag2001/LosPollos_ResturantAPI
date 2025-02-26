using LosPollos.Domain.Entities;
using System.Linq.Expressions;

namespace LosPollos.Application.Specefications
{
    public interface ISpecefication<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }

        void AddInclude(Expression<Func<TEntity, object>> include);
        void AddOrderBy(Expression<Func<TEntity, object>> orderBy);
        void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDesc);
    }
}
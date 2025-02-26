using LosPollos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Specefications
{
    public abstract class Specefication<TEntity> : ISpecefication<TEntity> where TEntity : BaseEntity
    {
        public Specefication()
        {

        }
        public Specefication(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;

        }
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }


        public void AddInclude(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }
        public void AddOrderBy(Expression<Func<TEntity, object>> orderBy) =>
            OrderBy = orderBy;
        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDesc) =>
            OrderByDescending = orderByDesc;

    }
}

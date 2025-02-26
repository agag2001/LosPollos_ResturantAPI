using LosPollos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Specefications
{
    public static class SpeceficationQueryEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecefication<TEntity> specefication)       
        {
            var query = inputQuery;

            if(specefication.Criteria is not null)
            {
                query = query.Where(specefication.Criteria);        
            }
            if(specefication.Includes is not  null)
            {
                query = specefication.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if(specefication.OrderBy is not null)
            {
                query = query.OrderBy(specefication.OrderBy);       
            }
            if (specefication.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specefication.OrderByDescending);       
            }
            return query;
        }
    }
}

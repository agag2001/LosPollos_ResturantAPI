using LosPollos.Application.Specefications;
using LosPollos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(ISpecefication<T>? specification = null);
        Task<T?> GetAsync(Expression<Func<T,bool>> predicate,string? includeProperties = null );
        Task<T> CreateAsync(T entity);    
        Task DeleteAsync(T entity);     
        Task UpdateAsync(T entity); 
    }

}

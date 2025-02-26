using LosPollos.Application.Specefications;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context ;
        internal DbSet<T> _dbSet ;      
        public Repository(AppDbContext context)
        {
            _context = context; 
            _dbSet =  context.Set<T>();     
            
        }
       

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            IQueryable<T> query =  _dbSet;
            if (includeProperties is not null)
            {
                query =  query.Include(includeProperties);
            }
           
            return await query.FirstOrDefaultAsync(predicate);     
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecefication<T>? specification =  null)
        {
            IQueryable<T> query = _dbSet;      
            if(specification is not null)
            {
                query =  SpeceficationQueryEvaluator<T>.GetQuery(_dbSet,specification);       
            }
            return await query.ToListAsync();
          
              
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
           
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
             _dbSet.Remove(entity);    

        }

        public async Task UpdateAsync(T entity)
        {
           _dbSet.Update(entity);   
        }
    }
}

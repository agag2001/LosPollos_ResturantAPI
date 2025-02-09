using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly AppDbContext _context ;
        internal DbSet<T> _dbSet ;      
        public Repository(AppDbContext context)
        {
            _context = context; 
            _dbSet =  context.Set<T>();     
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();      
        }
    }
}

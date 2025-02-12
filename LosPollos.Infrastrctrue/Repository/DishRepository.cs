using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Repository
{
    public class DishRepository:Repository<Dish>,IDishRepository
    {
        private readonly AppDbContext _context;
        public DishRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
    }
}

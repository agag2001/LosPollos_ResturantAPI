using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;


namespace LosPollos.Infrastructrue.Repository
{
    public class RestaurantRepository:Repository<Resturant>,IRestaurantRepository
    {
        private readonly AppDbContext _context;
        public RestaurantRepository(AppDbContext context) :base(context) 
        {
            _context = context;     
            
        }
    }
}

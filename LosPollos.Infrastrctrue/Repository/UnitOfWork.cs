using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;

namespace LosPollos.Infrastructrue.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRestaurantRepository restaurantRepository {  get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            restaurantRepository  = new RestaurantRepository(context);      
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();     
            
        }
    }
}

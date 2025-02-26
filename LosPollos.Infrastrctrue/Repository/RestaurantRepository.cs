using LosPollos.Application.Specefications;
using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using LosPollos.Infrastructrue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit.Cryptography;
using System.Linq.Expressions;


namespace LosPollos.Infrastructrue.Repository
{
    public class RestaurantRepository:Repository<Resturant>,IRestaurantRepository
    {
        private readonly AppDbContext _context;
        public RestaurantRepository(AppDbContext context) :base(context) 
        {
            _context = context;     
            
        }
        public async Task<(IEnumerable<Resturant>, int count)> GetAllMatching(string? searchPhrase,
            int pageNumber,int pageSize,string? sortBy,SortDirection sortDirection)
        {
            IQueryable<Resturant> query = _context.Set<Resturant>();  
            if (!string.IsNullOrEmpty(searchPhrase))
            {
                string lowerSearch = searchPhrase.ToLower();
                query = query.Where(x => x.Description.ToLower().Contains(lowerSearch) || x.Name.ToLower().Contains(lowerSearch));
            }
            Dictionary<string, Expression<Func<Resturant, object>>> sortColumns = new()
                {
                    {nameof(Resturant.Name),x=>x.Name },
                    {nameof(Resturant.Description),x=>x.Description },
                    {nameof(Resturant.Category),x=>x.Category }

                };

            if (!string.IsNullOrEmpty(sortBy))
            {
             
                var selectedColumn = sortColumns[sortBy];
                query =sortDirection ==SortDirection.Ascending? query.OrderBy(selectedColumn)
                    : query.OrderByDescending(selectedColumn);
            }
            var resturantCount = await  query.CountAsync();    
            var restaurantPatch =  await query.Skip(pageSize*(pageNumber-1)).Take(pageSize).ToListAsync();
            return (restaurantPatch, resturantCount);
        }
      
    }
}

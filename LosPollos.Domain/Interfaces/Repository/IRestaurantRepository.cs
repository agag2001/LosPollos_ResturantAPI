using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;

namespace LosPollos.Domain.Interfaces.Repository
{
    public interface IRestaurantRepository : IRepository<Resturant>
    {
        Task<(IEnumerable<Resturant>, int count)> GetAllMatching(string? searchPhrase,
            int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection);
    }
}

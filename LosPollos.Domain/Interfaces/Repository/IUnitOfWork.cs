using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IRestaurantRepository restaurantRepository { get; }
        void Save();
    }
}

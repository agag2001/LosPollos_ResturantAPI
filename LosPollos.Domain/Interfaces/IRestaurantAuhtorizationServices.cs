using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;

namespace LosPollos.Domain.Interfaces
{
    public interface IRestaurantAuhtorizationServices
    {
        bool Authorize(Resturant resutrant, ResourceOperation operation);
    }
}
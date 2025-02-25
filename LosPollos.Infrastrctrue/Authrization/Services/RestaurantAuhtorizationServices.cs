using LosPollos.Application.User;
using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Authrization.Services
{
    public class RestaurantAuhtorizationServices : IRestaurantAuhtorizationServices
    {
        private readonly ILogger<RestaurantAuhtorizationServices> _logger;
        private readonly IUserContext _userContext;

        public RestaurantAuhtorizationServices(ILogger<RestaurantAuhtorizationServices> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        public bool Authorize(Resturant resutrant, ResourceOperation operation)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Authorizing User {email} to Operaiton{operaiton} to Restaurant Name {name}",
                user!.email, operation, resutrant.Name);
            if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            {
                _logger.LogInformation(" Create / Read Resource successful Authorize");
                return true;

            } 
            if (operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                _logger.LogInformation("Admin User Delete resource Succeful Authorized");
                return true;
            }
            if ((operation == ResourceOperation.Update || operation == ResourceOperation.Delete)
                && resutrant.OwnerId == user.id)
            {
                _logger.LogInformation("Owner: {id} Update/ Delete Resouece", user.id);
                return true;
            }
            return false;
        }
    }
}

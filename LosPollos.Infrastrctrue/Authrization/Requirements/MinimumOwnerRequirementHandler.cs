using LosPollos.Application.User;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Authrization.Requirements
{
    public class MinimumOwnerRequirementHandler : AuthorizationHandler<MinimumOwnerRequirement>
    {
        private readonly ILogger<MinimumOwnerRequirementHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork unitOfWork;


        public MinimumOwnerRequirementHandler(ILogger<MinimumOwnerRequirementHandler> logger, IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userContext = userContext;
            this.unitOfWork = unitOfWork;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumOwnerRequirement requirement)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Handle Owner with id :{id}  Authorization Requirment ", user!.id);

            var Restaurants = await unitOfWork.restaurantRepository.GetAllAsync();
            var UserRestaurants = Restaurants.Count(x=>x.OwnerId == user!.id);      



            if (UserRestaurants < requirement.MinimumRestaurantCreated)
            {
                _logger.LogInformation("number of Restaurant ot the user:{email} is less {require}", user.email, requirement.MinimumRestaurantCreated);
                context.Fail();
                
            }
            else
            {
                _logger.LogInformation("Authoriazation is successful the user {email} match the requeirments", user.email);
                context.Succeed(requirement);
                   

            }

        }
    }
}

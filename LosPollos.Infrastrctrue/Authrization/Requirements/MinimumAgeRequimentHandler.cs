using LosPollos.Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Authrization.Requirements
{
    public class MinimumAgeRequimentHandler : AuthorizationHandler<MinimumAgeRequiment>

    {
        private readonly ILogger<MinimumAgeRequimentHandler> logger;            
        private readonly IUserContext userContext;

        public MinimumAgeRequimentHandler(ILogger<MinimumAgeRequimentHandler> logger, IUserContext userContext)
        {
            this.logger = logger;
            this.userContext = userContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequiment requirement)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Handling Minimum Age Requirement  to user {email} - birthDate = {birth}", user!.email, user.birthDate);
          
            var userBirthDate = user.birthDate;
            if(userBirthDate == null)
            {
                logger.LogWarning("User doesn't have birthDay");
                context.Fail();
                return Task.CompletedTask;      

            }
            //logic
            if (userBirthDate.Value.AddYears(requirement.Age) <= DateOnly.FromDateTime(DateTime.Today))
            {
                // success
                logger.LogInformation("the user {email} - birthDate = {birth} is Successed", user!.email, user.birthDate);
                context.Succeed(requirement);
                return Task.CompletedTask;      
            }
            context.Fail();
            return Task.CompletedTask;


        }
    }
}

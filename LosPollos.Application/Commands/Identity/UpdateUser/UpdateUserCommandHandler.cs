using LosPollos.Application.User;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Identity.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserContext userContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userContext = userContext;
            _userManager = userManager;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("update user {UserId} with{@request}",user!.id, request);
            var userDB = await _userManager.FindByEmailAsync(user.email); 
            if(userDB is null)
                throw new NotFoundException(nameof(userDB),user.id);  
            userDB.BirthDate = request.BirthDate;   
            userDB.Nationality  = request.Nationality;      
            await _userManager.UpdateAsync(userDB);     


        }
    }
}

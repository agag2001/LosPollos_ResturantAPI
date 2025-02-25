using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Identity.AssignUserRole
{
    public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
    {
        private readonly ILogger<AssignUserRoleCommandHandler> _logger;
        private readonly UserManager<AppUser>_userManager;  
        private readonly RoleManager<IdentityRole> _roleManager;

        public AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assing Role to User {@Request}", request);
            var user  = await _userManager.FindByEmailAsync(request.Email)
                ??throw new NotFoundException(nameof(AppUser), request.Email);      

            // check if the Role form Request is exist in the database
            var Role = await _roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole),request.RoleName);
            await _userManager.AddToRoleAsync(user, request.RoleName);

          
               

        }
    }
}

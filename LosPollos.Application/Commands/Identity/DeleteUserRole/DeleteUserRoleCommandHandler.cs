using LosPollos.Application.Commands.Identity.AssignUserRole;
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

namespace LosPollos.Application.Commands.Identity.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand>
    {

        private readonly ILogger<DeleteUserRoleCommandHandler> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteUserRoleCommandHandler(ILogger<DeleteUserRoleCommandHandler> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Deleting User role {@user}", request);

            var user  = await _userManager.FindByEmailAsync(request.Email)
                ?? throw  new NotFoundException(nameof(AppUser),request.Email);

            var userInRole = await _userManager.IsInRoleAsync(user, request.RoleName);

            if (!userInRole)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }

            await _userManager.RemoveFromRoleAsync(user, request.RoleName);
        }
    }
}

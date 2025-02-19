using AutoMapper;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using LosPollos.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(ILogger<RegisterCommandHandler> logger, IMapper mapper, UserManager<AppUser> userManager)
        {

            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" Registering New user {@User}", request);
            // check if the user already Exist 
            var userDB = await _userManager.FindByEmailAsync(request.Email);
            if (userDB is not null)
                throw  new UserException("user is already Exist");
            var user = _mapper.Map<AppUser>(request);
            var Created = await _userManager.CreateAsync(user, request.Password);
            if (!Created.Succeeded)
                throw new UserException();


        }
    }
}

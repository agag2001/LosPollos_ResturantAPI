using AutoMapper;
using LosPollos.Application.Commands.Account.Register;
using LosPollos.Application.DTOs;
using LosPollos.Application.Services.Interfaces;
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

namespace LosPollos.Application.Commands.Account.Login
{


    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
    {

          
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IJwtServices _jwt;
        public LoginCommandHandler(ILogger<LoginCommandHandler> logger, IMapper mapper, UserManager<AppUser> userManager, IJwtServices jwt)
        {

            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _jwt = jwt;
        }
        public async  Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userFromDB = await _userManager.FindByEmailAsync(request.Email);
            if(userFromDB is null)
                throw new NotFoundException(request.Email, new Guid().ToString());
            var Exists = await  _userManager.CheckPasswordAsync(userFromDB,request.Password);
            if (!Exists)
                throw new UserException("Can't login User");
            var authResponse = await _jwt.GenerateToken(userFromDB);
            return authResponse;
           
        }
    }
}

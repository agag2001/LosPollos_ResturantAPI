using AutoMapper;
using LosPollos.Application.Commands.Account.ForgotPassword;
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
using System.Web;

namespace LosPollos.Application.Commands.Account.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;

        public ResetPasswordCommandHandler(ILogger<ResetPasswordCommandHandler> logger, IMapper mapper, UserManager<AppUser> userManager, IEmailServices emailServices)
        {

            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _emailServices = emailServices;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new UserException("Bad request ");

            string decodedToken = HttpUtility.UrlDecode(request.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.Password);
            if (!result.Succeeded)
                throw new UserException("please try again");

        }
    }
}

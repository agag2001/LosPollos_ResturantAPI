using AutoMapper;
using LosPollos.Application.Commands.Account.Login;
using LosPollos.Application.Services.Interfaces;
using LosPollos.Domain.Entities;
using LosPollos.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Account.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;

        public ForgotPasswordCommandHandler(ILogger<ForgotPasswordCommandHandler> logger, IMapper mapper, UserManager<AppUser> userManager, IEmailServices emailServices)
        {

            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _emailServices = emailServices;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User with Email{@email} enter the forget Password",request.Email);   
            var user =  await _userManager.FindByEmailAsync(request.Email!);
            if (user == null)
                throw new UserException("bad request");
            var token =  await _userManager.GeneratePasswordResetTokenAsync(user);
            string clientUri = "https://localhost:7047/api/Account/resetPassword";

            var queryPram =  new Dictionary<string, string?>();
            queryPram.Add("token",token);   
            queryPram.Add("email",request.Email);   


            var callback = QueryHelpers.AddQueryString(clientUri, queryPram);
            await _emailServices.SendEmail(request.Email, "reset pasword Test", callback);


        }
    }
}

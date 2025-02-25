using LosPollos.Infrastructrue.Authorization;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.User
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

        }
        public CurrentUser? GetCurrentUser()
        {
            var user = _contextAccessor?.HttpContext?.User;
            if (user == null)
                throw new InvalidOperationException("User is not Present");
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return null;
            var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var userEmail = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
            var userRoles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);

            var userNationality = user.FindFirst(x=>x.Type==AppClaimTypes.Nationality)?.Value;
            var userBirthDateString = user.FindFirst(x => x.Type == AppClaimTypes.BirthDate)?.Value;
            var birthDate =  userBirthDateString == null ?(DateOnly?) null : DateOnly.ParseExact(userBirthDateString,"yyyy-MM-dd");   
            return new CurrentUser(userId, userEmail, userRoles,userNationality,birthDate);



        }

    }
}

using LosPollos.Application.DTOs;
using LosPollos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Services.Interfaces
{
    public  interface IJwtServices
    {
        Task<AuthResponseDTO> GenerateToken(AppUser user);
    }
}

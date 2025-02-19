using AutoMapper;
using LosPollos.Application.Commands.Account.Register;
using LosPollos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommand, AppUser>()
            .ForMember(dest => dest.UserName, option => option.MapFrom(x => x.Email))
            .ForMember(dest => dest.Email, option => option.MapFrom(x => x.Email))
            .ForMember(dest => dest.FullName, option => option.MapFrom(x => x.Name));


        }
    }
}

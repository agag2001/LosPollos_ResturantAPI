using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Domain.Entities
{
    public class AppUser:IdentityUser

    {
        public string FullName { get; set; } = default!;
        public DateOnly? BirthDate { get; set; }        
        public string? Nationality {  get; set; }       

        public ICollection<Resturant> Restaurants { get; set; } = new List<Resturant>();    
        public List<RefreshToken>? RefreshTokens { get; set; }   = new List<RefreshToken>(); 
    }
}

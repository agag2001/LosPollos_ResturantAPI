using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.User
{
    public record CurrentUser(string id , string email, IEnumerable<string> roles) 
    {
        public bool IsInRole(string roleName)
        {
            return roles.Contains(roleName);        
        }
    }
}

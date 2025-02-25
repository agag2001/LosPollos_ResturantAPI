using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Authrization.Requirements
{
    public class MinimumOwnerRequirement:IAuthorizationRequirement
    {
        public MinimumOwnerRequirement(int MinimumRestaurantCreated)
        {
            this.MinimumRestaurantCreated = MinimumRestaurantCreated;       
            
        }
        public int MinimumRestaurantCreated {  get;  }        
    }
}

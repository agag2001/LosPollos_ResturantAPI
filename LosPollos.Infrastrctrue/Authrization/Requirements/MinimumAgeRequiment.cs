using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Authrization.Requirements
{
    public class MinimumAgeRequiment:IAuthorizationRequirement
    {
        public MinimumAgeRequiment(int Age)
        {
            this.Age = Age;     
        }
        public int Age { get; set; }    
    }
}

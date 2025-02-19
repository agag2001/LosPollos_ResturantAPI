using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Domain.Exceptions
{
    public class UserException : Exception
    {

        public UserException(string? message = null):base(string.IsNullOrEmpty(message)?"Failed To Create User":message)

        {
            
        }
    }
}

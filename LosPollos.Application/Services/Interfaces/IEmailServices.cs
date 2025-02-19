using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Services.Interfaces
{
    public  interface IEmailServices
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}

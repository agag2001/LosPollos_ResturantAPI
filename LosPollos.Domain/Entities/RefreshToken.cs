using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Domain.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; }   
        public DateTime ExpiredAt { get; set; } 
        public bool IsExpired =>DateTime.UtcNow>=ExpiredAt;     
        public DateTime CreatedAt { get; set; }     
        public DateTime? RevokedAt { get; set; }
        public  bool IsActive => RevokedAt == null && !IsExpired;    
    }
}

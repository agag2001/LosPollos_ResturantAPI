using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.DTOs
{
    public  class CreateRestaurantDTO
    {
       
        
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? CantactEmail { get; set; }

        [Phone(ErrorMessage ="Invalid Phone Number")]
        public string? CantactPhone { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}

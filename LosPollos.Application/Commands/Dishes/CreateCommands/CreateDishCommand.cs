using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Dishes.CreateCommands
{
    public class CreateDishCommand:IRequest<int>
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }
        public int ResturantId { get; set; }
    }
}

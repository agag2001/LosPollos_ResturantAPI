using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Commands.Dishes.DeleteCommands
{
    public class DeleteDishesCommand:IRequest
    {

        public DeleteDishesCommand(int restaurantId)
        {
            RestaurantId = restaurantId;          
        }
        public int RestaurantId { get; private set; }
    }
}

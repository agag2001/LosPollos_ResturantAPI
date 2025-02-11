using MediatR;

namespace LosPollos.Application.Commands.Restaurants.DeleteCommands
{
    public class DeleteRestaurantCommand:IRequest<bool>
    {
        public DeleteRestaurantCommand(int id)
        {
            Id = id;    
        }
        public int Id { get;  }
    }
}

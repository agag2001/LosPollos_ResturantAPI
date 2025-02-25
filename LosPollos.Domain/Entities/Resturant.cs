
namespace LosPollos.Domain.Entities
{
    public class Resturant:BaseEntity
    {
        
        public string Name { get; set; } = default!;    
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        
        public bool HasDelivery { get; set; }       

        public string? CantactEmail { get; set; }   
        public string? CantactPhone { get; set; }
        public Address? Address { get; set; }

        public List<Dish> Dishes { get; set; } = new();

        public AppUser Owner { get; set; } = default!;
        public string OwnerId {  get; set; }=  default!;        
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace LosPollos.Domain.Entities
{
    public class Dish:BaseEntity
    {
        public string Name { get; set; } = string.Empty;  
        public string Description { get; set; } = string.Empty;  
        public decimal Price { get; set; }    
        public int? KiloCalories { get; set; }      
     
        
        public Resturant Resturant { get; set; }
        [ForeignKey("Resturant")]
        public int ResturantId {  get; set; }       

    }
}
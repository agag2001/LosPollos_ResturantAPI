using LosPollos.Domain.Entities;
using LosPollos.Infrastructrue.Data;
namespace LosPollos.Infrastructrue.Seeders
{
    public class ResturantSeeder : IResturantSeeder
    {
        private readonly AppDbContext _context;
        public ResturantSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Database.CanConnectAsync())
            {
                // if the resturant table is empty seed it
                if (!_context.Resturants.Any())
                {
                    var resturants = GetResturants();
                    _context.Resturants.AddRange(resturants);
                    await _context.SaveChangesAsync();

                }
            }
        }

        private IEnumerable<Resturant> GetResturants()
        {
            List<Resturant> restaurants = [
          new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                CantactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                CantactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
      ];

            return restaurants;
        }
    }
}

using LosPollos.Domain.Constant;
using LosPollos.Domain.Entities;
using LosPollos.Infrastructrue.Data;
using Microsoft.AspNetCore.Identity;
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
                if (!_context.Restaurants.Any())
                {
                    var resturants = GetResturants();
                    _context.Restaurants.AddRange(resturants);
                    await _context.SaveChangesAsync();

                }
                if(!_context.Roles.Any())
                {
                    var roles = GetRoles();     
                    _context.Roles.AddRange(roles);     
                    await _context.SaveChangesAsync();      
                }
            }
        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            IEnumerable<IdentityRole> Roles = new List<IdentityRole>()
            {
                new(UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new(UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()

                },
            };
            return Roles;
            
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
                ContactEmail = "contact@kfc.com",
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
                ContactEmail = "contact@mcdonald.com",
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

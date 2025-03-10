using LosPollos.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Infrastructrue.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        internal DbSet<Resturant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }
        internal DbSet<AppUser> AppUsers { get; set; }      
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Resturant>().OwnsOne(x => x.Address);
            
            modelBuilder.Entity<Resturant>().HasOne(x=>x.Owner).WithMany(x=>x.Restaurants).HasForeignKey(x=>x.OwnerId);
            modelBuilder.Entity<AppUser>().OwnsMany(x => x.RefreshTokens);
        }
    }
}

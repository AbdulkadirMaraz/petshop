using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Concrete
{

    public class PetShopContext : IdentityDbContext<ApplicationUser>
    {  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-DO47NIG;database=PetShop;integrated security=true;TrustServerCertificate=true;");
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
       public DbSet<Category> Categoryies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        
   
    }
}

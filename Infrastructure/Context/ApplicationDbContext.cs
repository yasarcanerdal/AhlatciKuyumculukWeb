using AhlatciKuyumculukWeb.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


// Veritabanında tablo oluşturmak için sınıflarımızı buraya eklemeliyiz
namespace AhlatciKuyumculukWeb.Infrastructure.Context
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }	

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }	
		public DbSet<ToBuy> ToBuys { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }	
	}
}

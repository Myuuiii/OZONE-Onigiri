using Microsoft.EntityFrameworkCore;
using OZONEOnigiri.Models;

namespace OZONEOnigiri.Data
{
	public class OnigiriDbContext : DbContext
	{
		// Db Context data
		private string ConnectionString = "Host=localhost;Username=root;Password=;Database=OZONEOnigiri";

		// Collections
		public DbSet<OnigiriUser> Users { get; set; }
		public DbSet<OnigiriRole> Roles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
		}
	}
}
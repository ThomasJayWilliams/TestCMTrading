using Microsoft.EntityFrameworkCore;
using TestCMTrading.Models;

namespace TestCMTrading
{
	public class DataContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=foodnavigatorcore.db",
				   options =>
				   {
					   options.CommandTimeout(5);
				   });
		}
	}
}

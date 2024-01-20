using DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;
namespace DataLayer.Contexts;
public class DBContext : DbContext
{
	public DBContext(DbContextOptions<DBContext> options) : base(options)
	{

	}

	// User

	public DbSet<User> Users { get; set; }



	#region on model creating
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
		{
			relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}

		base.OnModelCreating(modelBuilder);
	}
	#endregion

}
using DataLayer.Entities.Account;
using DataLayer.Entities.Categories;
using DataLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;
namespace DataLayer.Contexts;
public class DBContext : DbContext
{
	public DBContext(DbContextOptions<DBContext> options) : base(options)
	{

	}

	// User

	public DbSet<User> Users { get; set; }

	// Categories
	public DbSet<Category> Categories { get; set; }

	// Products

	public DbSet<Product> Products { get; set; }
	public DbSet<ProductsCategories> ProductsCategories { get; set; }



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
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;


namespace Project.Infrastructure.Persitence.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		// Definir DbSets para tus entidades
		public DbSet<User> Users { get; set; }
	
		public DbSet<Person> Persons { get; set; }



		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<BaseEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = DateTime.Now;
						entry.Entity.CreatedBy = "system";
						break;

					case EntityState.Modified:
						entry.Entity.ModifiedAt = DateTime.Now;
						entry.Entity.ModifiedBy = "system";
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configuraciones adicionales de modelo si son necesarias
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Person>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.ToTable("Person");

			});

			// Configuraciones adicionales de modelo si son necesarias
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(e => e.UserId);

				entity.ToTable("User");

			});
		}
	}
}

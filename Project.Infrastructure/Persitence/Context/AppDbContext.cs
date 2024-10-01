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
		public DbSet<Rol> Rols { get; set; }
		public DbSet<UserStatus> UsersStatus { get; set; }
		public DbSet<Person> Persons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configuraciones adicionales de modelo si son necesarias
			base.OnModelCreating(modelBuilder);
		}
	}
}

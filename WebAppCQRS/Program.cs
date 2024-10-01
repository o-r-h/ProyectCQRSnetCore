using FluentValidation;
using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Persitence.Context;


var builder = WebApplication.CreateBuilder(args);



// Config SeriLog 
builder.Host.UseSerilog((context, config) =>
{
	config.WriteTo.Console();
});

// Agregar MediatR y FluentValidation
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();

public class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		//services.AddDbContext<AppDbContext>(options =>
		//	options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

		services.AddDbContext<AppDbContext>(options =>
		options.UseMySql(
			Configuration.GetConnectionString("DefaultConnection"),
			ServerVersion.AutoDetect(Configuration.GetConnectionString("MySqlConnectionStrings"))
		));
	}
}
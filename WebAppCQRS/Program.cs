using FluentValidation;
using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Persitence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Project.Application.Services;
using Project.Application.Interfaces.Repositories;
using Project.Infrastructure.Persitence.Repositories;
using Project.Application.Interfaces;
using Project.Application.Commands.User;
using Project.Application.Commands.Person;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR;
using WebAppCQRS.Middleware;
using Microsoft.OpenApi.Models;
using Project.Application.Mapping;
using Project.Application.Queries.Person;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySql(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		new MySqlServerVersion(new Version(9, 0, 1)) // Cambia la versión según tu versión de MySQL
	)
	
);



 
//Add Repositories
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();





// Config SeriLog 
builder.Host.UseSerilog((context, config) =>
{
	config.WriteTo.Console();
});

// Add MediatR y commands
builder.Services.AddMediatR(options =>
{
	options.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
	options.RegisterServicesFromAssembly(typeof(CreatePersonCommandHandler).Assembly);
	options.RegisterServicesFromAssembly(typeof(GetAllPersonQueryHandler).Assembly);
	options.RegisterServicesFromAssembly(typeof(GetListPersonFilteredQueryHandler).Assembly);
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreatePersonCommandValidator).Assembly);
// Pipeline to execute validations before handler
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));





// Add Swagger
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Version = "v1",
		Title = "CQRS-example API",
		Description = "API CQRS using MediatR",

	});
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please insert JWT with Bearer into field",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme
					{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
					},
					new string[] { }
				}
				});
});

// Add services to the container.
builder.Services.AddControllers();

// Add Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//JWT
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

builder.Services.AddSingleton<JwtService>();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS API v1");
		options.RoutePrefix = string.Empty;
	});
}

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseErrorHandlingMiddleware();

app.Run();



using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositroy.Interfacies;
using Talabat.Repositroy.Data;
using Talabat.Repositroy.Repositries;

namespace Talabat.APIS
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container. 

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			//builder.Services.AddScoped<IGenaricInterface<Product>,GenaricRepositroy<Product>>();
			//builder.Services.AddScoped<IGenaricInterface<ProductBrand>,GenaricRepositroy<ProductBrand>>();
			//builder.Services.AddScoped<IGenaricInterface<ProductCategory>,GenaricRepositroy<ProductCategory>>();
			builder.Services.AddScoped(typeof(IGenaricInterface<>),typeof(GenaricRepositroy<>));



			var app = builder.Build();

		    using var Scope = app.Services.CreateScope();
            var service = Scope.ServiceProvider;
            var _dbcontext = service.GetRequiredService<StoreContext>();
			var loggerfactory = service.GetRequiredService<ILoggerFactory>();
            try
			{
				
				await _dbcontext.Database.MigrateAsync();//update database
				await StoreContextSeed.SeedAsync(_dbcontext);//dataseeding 
			}
			catch (Exception ex)
			{
				var logger = loggerfactory.CreateLogger<Program>();
				logger.LogError(ex, "An Erorr Occurred During Migration");
				
			}
           
			


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}

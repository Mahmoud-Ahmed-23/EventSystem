
using EventSystem.Apis.Extensions;
using EventSystem.Apis.Middlewares;
using EventSystem.Core.Application;
using EventSystem.Infastructure.Persistence;
using System.Threading.Tasks;

namespace EventSystem.Apis
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			builder.Services.RegesteredPresestantLayer();
			builder.Services.AddPersistenceServices(builder.Configuration);
			builder.Services.AddApplicationServices();
			builder.Services.AddIdentityServices(builder.Configuration);
			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			await app.InitializerCarCareIdentityContextAsync();

			app.UseMiddleware<ExceptionHandlerMiddleware>();
			app.UseMiddleware<ErrorHandlerMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}

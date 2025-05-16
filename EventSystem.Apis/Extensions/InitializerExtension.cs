using EventSystem.Core.Domain.Contracts.Persistence;

namespace EventSystem.Apis.Extensions
{
	public static class InitializerExtension
	{
		public static async Task<WebApplication> InitializerCarCareIdentityContextAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var storeIdentityContextIntializer = services.GetRequiredService<IEventSystemDbInitializer>();
			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				await storeIdentityContextIntializer.InitializeAsync();
				await storeIdentityContextIntializer.SeedAsync();
			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "an error has been occured during applaying migrations");
			}

			return app;
		}
	}
}

using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Infastructure.Persistence._Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<EventSystemDbContext>((provider, options) =>
			{
				options.UseLazyLoadingProxies()
				.UseSqlServer(configuration.GetConnectionString("EventSystemContext"));
			});

			services.AddScoped<IEventSystemDbInitializer, EventSystemDbInitializer>();
			return services;
		}
	}
}

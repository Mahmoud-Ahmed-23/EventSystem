using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Infastructure.Persistence._Data;
using EventSystem.Infastructure.Persistence._Data.Interceptors;
using EventSystem.Infastructure.Persistence.Repositories;
using EventSystem.Infastructure.Persistence.Repositories.GenericRepo;
using EventSystem.Infastructure.Persistence.UnitOfwork;
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
				.UseSqlServer(configuration.GetConnectionString("EventSystemContext"))
				.AddInterceptors(provider.GetRequiredService<AuditInterceptor>(),
								 provider.GetRequiredService<SettedUserIdInterceptor>());
			});

			services.AddScoped(typeof(AuditInterceptor));
			services.AddScoped(typeof(SettedUserIdInterceptor));


			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
			services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
			services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));

			services.AddScoped<IEventSystemDbInitializer, EventSystemDbInitializer>();
			return services;
		}
	}
}

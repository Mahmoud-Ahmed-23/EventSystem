using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Core.Application.Abstraction.Service.Booking;
using EventSystem.Core.Application.Abstraction.Service.Categories;
using EventSystem.Core.Application.Mapping;
using EventSystem.Core.Application.Service.Auth;
using EventSystem.Core.Application.Service.Booking;
using EventSystem.Core.Application.Service.Categories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));

			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

			services.AddScoped(typeof(IBookService), typeof(BookService));
			services.AddScoped(typeof(Func<IBookService>), (serviceprovider) =>
			{
				return () => serviceprovider.GetRequiredService<IBookService>();

			});

			services.AddScoped(typeof(IAuthService), typeof(AuthService));
			services.AddScoped(typeof(Func<IAuthService>), (serviceprovider) =>
			{
				return () => serviceprovider.GetRequiredService<IAuthService>();

			});

			services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
			services.AddScoped(typeof(Func<ICategoryService>), (serviceprovider) =>
			{
				return () => serviceprovider.GetRequiredService<ICategoryService>();

			});
			return services;
		}

	}
}

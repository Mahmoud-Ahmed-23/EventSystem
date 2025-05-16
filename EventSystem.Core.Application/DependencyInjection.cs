using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Core.Application.Service.Auth;
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


			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			return services;
		}

	}
}

using EventSystem.Apis.Services;
using EventSystem.Core.Application.Abstraction;
using EventSystem.Shared.ErrorModule.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EventSystem.Apis.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection RegesteredPresestantLayer(this IServiceCollection services)
		{

			services.AddHttpContextAccessor();

			services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

			services.AddControllers().ConfigureApiBehaviorOptions(options =>
			{
				options.SuppressModelStateInvalidFilter = false;
				options.InvalidModelStateResponseFactory = (actionContext =>
				{
					var Errors = actionContext.ModelState.Where(e => e.Value!.Errors.Count() > 0)
												.SelectMany(e => e.Value!.Errors).Select(e => e.ErrorMessage);

					return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = Errors });

				});
			}
			).AddApplicationPart(typeof(APIs.Controllers.AssemblyInformation).Assembly);

			services.AddCors(corsOptions =>
			{
				corsOptions.AddPolicy("FrontEnd", policyBuilder =>
				{
					policyBuilder.AllowAnyHeader()
								 .AllowAnyMethod()
								 .AllowAnyOrigin(); // Allow all domains
				});
			});

			return services;
		}
	}
}

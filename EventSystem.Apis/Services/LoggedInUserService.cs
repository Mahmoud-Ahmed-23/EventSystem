using EventSystem.Core.Application.Abstraction;
using System.Security.Claims;

namespace EventSystem.Apis.Services
{
	public class LoggedInUserService : ILoggedInUserService
	{
		private readonly IHttpContextAccessor? _httpcontextAccessor;

		public LoggedInUserService(IHttpContextAccessor? contextAccessor)
		{
			_httpcontextAccessor = contextAccessor;

		}
		public string? UserId =>
				_httpcontextAccessor!.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}

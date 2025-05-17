using EventSystem.Shared.ErrorModule.Errors;
using EventSystem.Shared.ErrorModule.Exceptions;
using EventSystem.Shared.Responses;
using System.Net;
using System.Text.Json;

namespace EventSystem.Apis.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);

				if (httpContext.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
				{
					var response = new Response<string>
					{
						StatusCode = HttpStatusCode.MethodNotAllowed,
						Succeeded = false,
						Message = "HTTP method not allowed on this endpoint",
						Data = null
					};

					httpContext.Response.ContentType = "application/json";
					await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
					{
						PropertyNamingPolicy = JsonNamingPolicy.CamelCase
					}));
				}
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					_logger.LogError(ex, ex.Message);

				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			httpContext.Response.ContentType = "application/json";

			var response = new Response<string>
			{
				Data = null,
				Errors = new List<string>(),
				Succeeded = false
			};

			switch (ex)
			{
				case NotFoundException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					response.Message = ex.Message;
					response.StatusCode = HttpStatusCode.NotFound;
					break;

				case ValidationException validationException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response.Message = ex.Message;
					response.StatusCode = HttpStatusCode.BadRequest;
					response.Errors = validationException.Errors.ToList();
					break;

				case BadRequestException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response.Message = ex.Message;
					response.StatusCode = HttpStatusCode.BadRequest;
					break;

				case UnAuthorizedException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					response.Message = ex.Message;
					response.StatusCode = HttpStatusCode.Unauthorized;
					break;

				default:
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					response.Message = _env.IsDevelopment()
						? $"{ex.Message} | {ex.StackTrace}"
						: "Internal Server Error";
					response.StatusCode = HttpStatusCode.InternalServerError;
					break;
			}

			await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			}));
		}
	}
}

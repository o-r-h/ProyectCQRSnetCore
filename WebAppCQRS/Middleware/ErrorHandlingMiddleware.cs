using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;


namespace WebAppCQRS.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError; 
			var result = string.Empty;

			switch (exception)
			{
				case ValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result = JsonSerializer.Serialize(new
					{
						errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
					});
					break;
				case ApplicationException appException:
					code = HttpStatusCode.BadRequest;
					result = JsonSerializer.Serialize(new { error = appException.Message });
					break;
				case SecurityTokenExpiredException:
					code = HttpStatusCode.Unauthorized;
					result = JsonSerializer.Serialize(new { error = "Token has expired" });
					break;
				case SecurityTokenException:
					code = HttpStatusCode.Unauthorized;
					result = JsonSerializer.Serialize(new { error = "Invalid token" });
					break;
				case UnauthorizedAccessException:
					code = HttpStatusCode.Forbidden;
					result = JsonSerializer.Serialize(new { error = "You do not have permission to access this resource" });
					break;
				case DataMappingException dataMappingException:
					code = HttpStatusCode.InternalServerError;
					result = JsonSerializer.Serialize(new { error = "An error occurred while processing the data", details = dataMappingException.Message });
					break;
				default:
					result = JsonSerializer.Serialize(new { error = $"An error occurred while processing your request. Error Detail: {exception.Message} " });
					break;
			}

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;

			return context.Response.WriteAsync(result);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class ErrorHandlingMiddlewareExtensions
	{
		public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandlingMiddleware>();
		}
	}

	//Extension method used to add the middleware data mappings errors
	public class DataMappingException : Exception
	{
		public DataMappingException(string message) : base(message) { }
		public DataMappingException(string message, Exception innerException) : base(message, innerException) { }
	}
}

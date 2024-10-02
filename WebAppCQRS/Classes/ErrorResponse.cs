using Microsoft.IdentityModel.Tokens;

namespace WebAppCQRS.Classes
{
	public class ErrorResponse
	{
		public string Message { get; set; } =string.Empty;
		public string Details { get; set; } = string.Empty;	
		public IEnumerable<ValidationError> Errors { get; set; } = new List<ValidationError>();

	}
}

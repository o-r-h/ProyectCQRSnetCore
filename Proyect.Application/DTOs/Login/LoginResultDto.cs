using Project.Domain.Entities;

namespace Project.Application.DTOs.Login
{
    public class LoginResultDto
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}

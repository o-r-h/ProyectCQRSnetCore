using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Project.Application.Services
{
	public class PasswordHasher : IPasswordHasher
	{
		public string HashPassword(string password)
		{
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return $"{Convert.ToBase64String(salt)}.{hashed}";
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			var parts = hashedPassword.Split('.');
			var salt = Convert.FromBase64String(parts[0]);
			var storedHash = parts[1];

			string newHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return storedHash == newHash;
		}
	}
}

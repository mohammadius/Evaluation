using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Evaluation.Utilities
{
	public static class PasswordUtility
	{
		public static string HashPassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
			
			const int iterations = 10000;

			var salt = new byte[64];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			var saltString = Convert.ToBase64String(salt);

			var hashed = Hash(password, iterations, salt);

			return $"{hashed}:{saltString}:{iterations}";
		}

		public static bool VerifyPassword(string password, string hashedPassword)
		{
			if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword)) return false;

			var (dbHash, dbSalt, dbIteration) = SplitHash(hashedPassword);
			return string.Equals(Hash(password, dbIteration, dbSalt), dbHash);
		}

		private static string Hash(string password, int iterations, byte[] salt)
		{
			return Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password,
				salt,
				KeyDerivationPrf.HMACSHA1,
				iterations,
				128));
		}

		private static (string, byte[], int) SplitHash(string hash)
		{
			var hashSplit = hash.Split(':');
			return (hashSplit[0], Convert.FromBase64String(hashSplit[1]), Convert.ToInt32(hashSplit[2]));
		}
	}
}

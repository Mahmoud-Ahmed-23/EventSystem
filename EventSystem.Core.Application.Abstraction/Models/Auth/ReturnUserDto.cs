using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Auth
{
	public class ReturnUserDto
	{
		public string Id { get; set; }
		public required string FullName { get; set; }
		public required string Email { get; set; }
		public required string PhoneNumber { get; set; }
		public string Role { get; set; }
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpirationDate { get; set; }
	}
}

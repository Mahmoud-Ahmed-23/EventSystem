using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Auth
{
	public class ReturnUserDto
	{
		public required string FullName { get; set; }
		public required string Email { get; set; }
		public required string PhoneNumber { get; set; }
		public required string Role { get; set; }
	}
}

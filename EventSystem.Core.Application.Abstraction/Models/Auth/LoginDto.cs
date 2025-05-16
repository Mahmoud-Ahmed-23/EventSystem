using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Auth
{
	public class LoginDto
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Auth
{
	public class RefreshDto
	{
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
	}
}

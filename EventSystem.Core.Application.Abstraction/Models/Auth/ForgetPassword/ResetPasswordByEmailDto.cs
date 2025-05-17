using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword
{
	public class ResetPasswordByEmailDto : SendCodeByEmailDto
	{
		public required string NewPassword { get; set; }
	}
}

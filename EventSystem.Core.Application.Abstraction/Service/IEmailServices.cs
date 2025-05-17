using EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Service
{
	public interface IEmailServices
	{
		Task SendEmail(EmailDto emailDto);
	}
}

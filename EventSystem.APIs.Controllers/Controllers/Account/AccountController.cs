using EventSystem.APIs.Controllers.Controllers._Base;
using EventSystem.Core.Application.Abstraction.Models.Auth;
using EventSystem.Core.Application.Abstraction.Service.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.APIs.Controllers.Controllers.Account
{
	public class AccountController(IAuthService _authService) : BaseApiController
	{
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
		{
			var result = await _authService.Login(loginDto);
			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<ActionResult<ReturnUserDto>> Register([FromBody] RegisterDto registerDto)
		{
			var result = await _authService.Register(registerDto);
			return Ok(result);
		}
	}
}

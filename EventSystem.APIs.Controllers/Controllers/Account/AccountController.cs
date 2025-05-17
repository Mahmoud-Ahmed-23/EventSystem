using EventSystem.APIs.Controllers.Controllers._Base;
using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Models.Auth;
using EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword;
using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.APIs.Controllers.Controllers.Account
{
	public class AccountController(IServiceManager _serviceManager) : BaseApiController
	{
		[HttpPost("login")]
		public async Task<ActionResult<ReturnUserDto>> Login([FromBody] LoginDto loginDto)
		{
			var result = await _serviceManager.AuthService.Login(loginDto);
			return NewResult(result);
		}

		[HttpPost("register")]
		public async Task<ActionResult<ReturnUserDto>> Register([FromBody] RegisterDto registerDto)
		{
			var result = await _serviceManager.AuthService.Register(registerDto);
			return NewResult(result);
		}


		[HttpPost("RefreshToken")]

		public async Task<ActionResult<ReturnUserDto>> GetRefreshToken([FromBody] RefreshDto model)
		{
			var result = await _serviceManager.AuthService.GetRefreshTokenAsync(model);
			return NewResult(result);
		}

		[HttpPost("RevokeRefreshToken")]
		public async Task<ActionResult<bool>> RevokeRefreshToken([FromBody] RefreshDto model)
		{
			var result = await _serviceManager.AuthService.RevokeRefreshTokenAsync(model);
			return NewResult(result);

		}


		[HttpPost("SendCodeByEmail")]
		public async Task<ActionResult<SuccessDto>> SendCodeByEmail(SendCodeByEmailDto forgetPasswordDto)
		{
			var result = await _serviceManager.AuthService.SendCodeByEmailasync(forgetPasswordDto);
			return Ok(result);
		}

		[HttpPost("VerfiyCodeEmail")]
		public async Task<ActionResult<SuccessDto>> VerfiyCodeEmail(ResetCodeConfirmationByEmailDto resetCode)
		{
			var result = await _serviceManager.AuthService.VerifyCodeByEmailAsync(resetCode);
			return Ok(result);
		}


		[HttpPut("ResetPasswordEmail")]
		public async Task<ActionResult<ReturnUserDto>> ResetPasswordEmail(ResetPasswordByEmailDto resetPassword)
		{
			var result = await _serviceManager.AuthService.ResetPasswordByEmailAsync(resetPassword);
			return Ok(result);
		}

		[HttpPost("ConfirmEmail")]
		public async Task<ActionResult<SuccessDto>> ConfirmEmail(ConfirmationEmailCodeDto codeDto)
		{
			var result = await _serviceManager.AuthService.ConfirmEmailAsync(codeDto);
			return Ok(result);
		}
	}
}

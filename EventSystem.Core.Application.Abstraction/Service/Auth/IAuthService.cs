using EventSystem.Core.Application.Abstraction.Models.Auth;
using EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword;
using EventSystem.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Service.Auth
{
	public interface IAuthService
	{
		Task<Response<ReturnUserDto>> Login(LoginDto loginDto);
		Task<Response<ReturnUserDto>> Register(RegisterDto registerDto);

		Task<Response<ReturnUserDto>> GetRefreshTokenAsync(RefreshDto refreshDto, CancellationToken cancellationToken = default);

		Task<Response<bool>> RevokeRefreshTokenAsync(RefreshDto refreshDto, CancellationToken cancellationToken = default);

		Task<SuccessDto> SendCodeByEmailasync(SendCodeByEmailDto emailDto);


		Task<SuccessDto> VerifyCodeByEmailAsync(ResetCodeConfirmationByEmailDto resetCodeDto);


		Task<ReturnUserDto> ResetPasswordByEmailAsync(ResetPasswordByEmailDto resetCodeDto);


		Task<SuccessDto> ConfirmEmailAsync(ConfirmationEmailCodeDto codeDto);


	}
}

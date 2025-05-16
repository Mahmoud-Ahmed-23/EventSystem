using EventSystem.Core.Application.Abstraction.Models.Auth;
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
		Task<ReturnUserDto> Login(LoginDto loginDto);
		Task<ReturnUserDto> Register(RegisterDto registerDto);

		Task<ReturnUserDto> GetRefreshTokenAsync(RefreshDto refreshDto, CancellationToken cancellationToken = default);

		Task<bool> RevokeRefreshTokenAsync(RefreshDto refreshDto, CancellationToken cancellationToken = default);

	}
}

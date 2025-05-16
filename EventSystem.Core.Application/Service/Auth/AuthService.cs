using EventSystem.Core.Application.Abstraction.Models.Auth;
using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Core.Domain.Entities.Identity;
using EventSystem.Shared.ErrorModule.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Data;
using UnAuthorizedException = EventSystem.Shared.ErrorModule.Exceptions.UnAuthorizedException;
using ValidationException = EventSystem.Shared.ErrorModule.Exceptions.ValidationException;

namespace EventSystem.Core.Application.Service.Auth
{
	internal class AuthService(UserManager<ApplicationUser> _userManager,
		SignInManager<ApplicationUser> _signInManager,
		RoleManager<IdentityRole> _roleManager) : IAuthService
	{
		public async Task<string> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user is null)
			{
				throw new UnAuthorizedException("Invalid Login");
			}
			var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

			if (!user.EmailConfirmed)
				throw new UnAuthorizedException("Email is not Confirmed");

			if (result.IsLockedOut)
				throw new UnAuthorizedException("Email is Locked Out");

			if (!result.Succeeded)
				throw new UnAuthorizedException("Invalid Login");

			return "Token";
		}

		public async Task<ReturnUserDto> Register(RegisterDto registerDto)
		{
			var user = await _userManager.FindByEmailAsync(registerDto.Email);

			if (user is not null)
			{
				throw new UnAuthorizedException("User already exists");
			}

			var newUser = new ApplicationUser()
			{
				Email = registerDto.Email,
				UserName = registerDto.Email,
				FullName = registerDto.FullName,
				PhoneNumber = registerDto.PhoneNumber
			};

			var result = await _userManager.CreateAsync(newUser, registerDto.Password);

			if (!result.Succeeded)
				throw new ValidationException() { Errors = result.Errors.Select(E => E.Description) };


			var roleResult = await _userManager.AddToRoleAsync(newUser, registerDto.Role);

			if (!roleResult.Succeeded)
				throw new ValidationException() { Errors = roleResult.Errors.Select(E => E.Description) };

			return new ReturnUserDto()
			{
				Email = newUser.Email,
				FullName = newUser.FullName,
				PhoneNumber = newUser.PhoneNumber,
				Role = registerDto.Role
			};
		}
	}
}

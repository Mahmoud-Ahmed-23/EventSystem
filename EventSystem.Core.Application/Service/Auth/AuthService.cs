using EventSystem.Core.Application.Abstraction.Models.Auth;
using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Core.Domain.Entities.Identity;
using EventSystem.Shared.AppSettings;
using EventSystem.Shared.ErrorModule.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UnAuthorizedException = EventSystem.Shared.ErrorModule.Exceptions.UnAuthorizedException;
using ValidationException = EventSystem.Shared.ErrorModule.Exceptions.ValidationException;
using NotFoundException = EventSystem.Shared.ErrorModule.Exceptions.NotFoundException;
using BadRequestException = EventSystem.Shared.ErrorModule.Exceptions.BadRequestException;
using EventSystem.Shared.Responses;
using EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword;
using Microsoft.EntityFrameworkCore;
using EventSystem.Core.Application.Abstraction.Service;

namespace EventSystem.Core.Application.Service.Auth
{
	internal class AuthService(UserManager<ApplicationUser> _userManager,
		SignInManager<ApplicationUser> _signInManager,
		 IOptions<JwtSettings> jwtSettings,
		  IEmailServices emailServices) : ResponseHandler, IAuthService
	{


		private readonly JwtSettings _jwtSettings = jwtSettings.Value;

		public async Task<Response<ReturnUserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user is null)
				return Unauthorized<ReturnUserDto>("Invalid Login");

			var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

			if (!user.EmailConfirmed)
				return Unauthorized<ReturnUserDto>("Email Not Confirmed");

			if (result.IsLockedOut)
				return Unauthorized<ReturnUserDto>("User Locked Out");

			if (!result.Succeeded)
				return Unauthorized<ReturnUserDto>("Invalid Login");

			var response = new ReturnUserDto()
			{
				Email = user.Email!,
				FullName = user.FullName,
				PhoneNumber = user.PhoneNumber!,
				Id = user.Id,
				Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()!,
				Token = await GenerateToken(user),
			};

			await CheckRefreshToken(_userManager, user, response);

			return Success(response);
		}

		public async Task<Response<ReturnUserDto>> Register(RegisterDto registerDto)
		{
			var user = await _userManager.FindByEmailAsync(registerDto.Email);

			if (user is not null)
				return BadRequest<ReturnUserDto>("User Already Exists");

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

			var email = new SendCodeByEmailDto() { Email = user!.Email! };
			await SendCodeByEmailasync(email);

			var roleResult = await _userManager.AddToRoleAsync(newUser, registerDto.Role);

			if (!roleResult.Succeeded)
				throw new ValidationException() { Errors = roleResult.Errors.Select(E => E.Description) };

			return Success(new ReturnUserDto()
			{
				Email = newUser.Email,
				FullName = newUser.FullName,
				PhoneNumber = newUser.PhoneNumber,
				Role = registerDto.Role
			});
		}

		private async Task<string> GenerateToken(ApplicationUser user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.PrimarySid, user.Id),
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.MobilePhone,user.PhoneNumber!),
				new Claim(ClaimTypes.Name, user.FullName)
			}
			.Union(userClaims)
			.Union(roleClaims);


			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				expires: DateTime.Now.AddDays(_jwtSettings.DurationInDays),
				claims: claims,
				signingCredentials: creds
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private RefreshToken GenerateRefreshToken()
		{
			var randomNumber = new byte[32];

			var genrator = new RNGCryptoServiceProvider();

			genrator.GetBytes(randomNumber);

			return new RefreshToken()
			{
				Token = Convert.ToBase64String(randomNumber),
				CreatedOn = DateTime.UtcNow,
				ExpireOn = DateTime.UtcNow.AddDays(_jwtSettings.JWTRefreshTokenExpire)


			};


		}
		private async Task CheckRefreshToken(UserManager<ApplicationUser> _userManager, ApplicationUser? user, ReturnUserDto response)
		{
			if (user!.RefreshTokens.Any(t => t.IsActive))
			{
				var acticetoken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
				response.RefreshToken = acticetoken!.Token;
				response.RefreshTokenExpirationDate = acticetoken.ExpireOn;
			}
			else
			{

				var refreshtoken = GenerateRefreshToken();
				response.RefreshToken = refreshtoken.Token;
				response.RefreshTokenExpirationDate = refreshtoken.ExpireOn;

				user.RefreshTokens.Add(new RefreshToken()
				{
					Token = refreshtoken.Token,
					ExpireOn = refreshtoken.ExpireOn,
				});
				await _userManager.UpdateAsync(user);
			}
		}

		private string? ValidateToken(string token)
		{
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var TokenHandler = new JwtSecurityTokenHandler();

			try
			{
				TokenHandler.ValidateToken(token, new TokenValidationParameters()
				{
					IssuerSigningKey = authKey,
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = false,
					ClockSkew = TimeSpan.Zero,

				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;

				return jwtToken.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value;

			}
			catch
			{
				return null;
			}
		}


		public async Task<Response<ReturnUserDto>> GetRefreshTokenAsync(RefreshDto refreshDto, CancellationToken cancellationToken = default)
		{
			var userId = ValidateToken(refreshDto.Token!);

			if (userId is null) throw new NotFoundException("User id Not Found", nameof(userId));

			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) throw new NotFoundException("User Do Not Exists", nameof(user.Id));

			var UserRefreshToken = user!.RefreshTokens.SingleOrDefault(x => x.Token == refreshDto.RefreshToken && x.IsActive);

			if (UserRefreshToken is null)
				return BadRequest<ReturnUserDto>("Invalid Refresh Token");

			UserRefreshToken.RevokedOn = DateTime.UtcNow;

			var newtoken = await GenerateToken(user);

			var newrefreshtoken = GenerateRefreshToken();

			user.RefreshTokens.Add(new RefreshToken()
			{
				Token = newrefreshtoken.Token,
				ExpireOn = newrefreshtoken.ExpireOn
			});

			await _userManager.UpdateAsync(user);

			return Success(new ReturnUserDto()
			{
				Email = user.Email!,
				FullName = user.FullName,
				PhoneNumber = user.PhoneNumber!,
				RefreshToken = newrefreshtoken.Token,
				RefreshTokenExpirationDate = newrefreshtoken.ExpireOn,
				Token = newtoken,
				Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()!
			});

		}


		public async Task<Response<bool>> RevokeRefreshTokenAsync(RefreshDto refreshDto, CancellationToken cancellationToken = default)
		{
			var userId = ValidateToken(refreshDto.Token!);

			if (userId is null) return Success(false);

			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) return Success(false);

			var UserRefreshToken = user!.RefreshTokens.SingleOrDefault(x => x.Token == refreshDto.RefreshToken && x.IsActive);

			if (UserRefreshToken is null) return Success(false);

			UserRefreshToken.RevokedOn = DateTime.UtcNow;

			await _userManager.UpdateAsync(user);
			return Success(true);
		}


		public async Task<SuccessDto> SendCodeByEmailasync(SendCodeByEmailDto emailDto)
		{
			var user = await _userManager.Users.Where(u => u.Email == emailDto.Email).FirstOrDefaultAsync();

			if (user is null)
				throw new BadRequestException("Invalid Email");

			var ResetCode = RandomNumberGenerator.GetInt32(100_0, 999_9);

			var ResetCodeExpire = DateTime.UtcNow.AddMinutes(15);

			user.EmailConfirmResetCode = ResetCode;
			user.EmailConfirmResetCodeExpiry = ResetCodeExpire;

			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded)
				throw new BadRequestException("Something Went Wrong While Sending Reset Code");

			var Email = new EmailDto()
			{
				To = emailDto.Email,
				Subject = "Reset Code For CarCare Account",
				Body = $"We Have Recived Your Request For Reset Your Account Password, \nYour Reset Code Is ==> [ {ResetCode} ] <== \nNote: This Code Will Be Expired After 15 Minutes!",
			};

			await emailServices.SendEmail(Email);

			var SuccessObj = new SuccessDto()
			{
				Status = "Success",
				Message = "We Have Sent You The Reset Code"
			};

			return SuccessObj;
		}

		public async Task<SuccessDto> VerifyCodeByEmailAsync(ResetCodeConfirmationByEmailDto resetCodeDto)
		{
			var user = await _userManager.Users.Where(u => u.Email == resetCodeDto.Email).FirstOrDefaultAsync();

			if (user is null)
				throw new BadRequestException("Invalid Email");

			if (user.EmailConfirmResetCode != resetCodeDto.ResetCode)
				throw new BadRequestException("The Provided Code Is Invalid");

			if (user.EmailConfirmResetCodeExpiry < DateTime.UtcNow)
				throw new BadRequestException("The Provided Code Has Been Expired");

			var SuccessObj = new SuccessDto()
			{
				Status = "Success",
				Message = "Reset Code Is Verified, Please Proceed To Change Your Password"
			};

			return SuccessObj;
		}

		public async Task<ReturnUserDto> ResetPasswordByEmailAsync(ResetPasswordByEmailDto resetCodeDto)
		{
			var user = await _userManager.Users.Where(u => u.Email == resetCodeDto.Email).FirstOrDefaultAsync();

			if (user is null)
				throw new BadRequestException("Invalid Email");

			var RemovePass = await _userManager.RemovePasswordAsync(user);

			if (!RemovePass.Succeeded)
				throw new BadRequestException("Something Went Wrong While Reseting Your Password");

			var newPass = await _userManager.AddPasswordAsync(user, resetCodeDto.NewPassword);

			if (!newPass.Succeeded)
				throw new BadRequestException("Something Went Wrong While Reseting Your Password");

			var mappedUser = new ReturnUserDto
			{
				FullName = user.FullName!,
				Id = user.Id,
				PhoneNumber = user.PhoneNumber!,
				Email = user.Email!,
				Token = await GenerateToken(user),
			};

			return mappedUser;
		}


		public async Task<SuccessDto> ConfirmEmailAsync(ConfirmationEmailCodeDto codeDto)
		{
			var user = await _userManager.Users.Where(U => U.Email == codeDto.Email).FirstOrDefaultAsync();

			if (user is null)
				throw new BadRequestException("Invalid Email");

			if (user.EmailConfirmResetCode != codeDto.ConfirmationCode)
				throw new BadRequestException("The Provided Code Is Invalid");

			if (user.EmailConfirmResetCodeExpiry < DateTime.UtcNow)
				throw new BadRequestException("The Provided Code Has Been Expired");

			user.EmailConfirmed = true;

			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded)
				throw new BadRequestException("Something Went Wrong While Confirming Email");

			var SuccessObj = new SuccessDto()
			{
				Status = "Success",
				Message = "Email Has Been Confirmed"
			};

			return SuccessObj;
		}

	}
}

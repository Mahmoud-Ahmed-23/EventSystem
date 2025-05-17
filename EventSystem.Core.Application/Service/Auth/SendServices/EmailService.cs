﻿using EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword;
using EventSystem.Core.Application.Abstraction.Service;
using EventSystem.Shared.AppSettings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EventSystem.Core.Application.Service.Auth.SendServices
{
	public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailServices
	{
		private readonly EmailSettings _emailSettings = emailSettings.Value;
		public async Task SendEmail(EmailDto emailDto)
		{

			var Email = new MimeMessage()
			{
				Sender = MailboxAddress.Parse(_emailSettings.Email),
				Subject = emailDto.Subject
			};

			Email.To.Add(MailboxAddress.Parse(emailDto.To));
			Email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email));


			var EmailBody = new BodyBuilder();
			EmailBody.TextBody = emailDto.Body;


			Email.Body = EmailBody.ToMessageBody();


			using var Smtp = new SmtpClient();

			Smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

			await Smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);


			await Smtp.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);


			await Smtp.SendAsync(Email);


			await Smtp.DisconnectAsync(true);
		}
	}
}

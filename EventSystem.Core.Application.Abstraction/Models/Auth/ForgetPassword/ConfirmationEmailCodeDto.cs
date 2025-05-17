﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Auth.ForgetPassword
{
	public class ConfirmationEmailCodeDto
	{
		[Required]
		public required string Email { get; set; }

		[Required]
		public int ConfirmationCode { get; set; }
	}
}

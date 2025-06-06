﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Shared.ErrorModule.Exceptions
{
	public class ValidationException : BadRequestException
	{
		public required IEnumerable<string> Errors { get; set; }
		public ValidationException(string message = "Bad Request") : base(message)
		{


		}
	}
}

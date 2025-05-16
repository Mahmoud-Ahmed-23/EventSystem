using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Shared.ErrorModule.Exceptions
{
	public class UnAuthorizedException : ApplicationException
	{

		public UnAuthorizedException(string message) : base(message)
		{

		}
	}
}

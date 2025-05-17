using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Service.Booking
{
	public interface IBookService
	{
		Task<Response<ReturnBookDto>> CreateBook(ClaimsPrincipal claimsPrincipal, int eventId);
	}
}

using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Core.Application.Abstraction.Wrapper;
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

		Task<Response<ReturnBookDto>> GetBookById(int bookId);

		Task<Response<Pagination<ReturnBookDto>>> GetAllBooksForSpecificUser(ClaimsPrincipal claimsPrincipal, int? eventId, int pageIndex, int pageSize);

		Task<Response<string>> CancelBook(int bookId);
	}
}

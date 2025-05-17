using EventSystem.APIs.Controllers.Controllers._Base;
using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Core.Application.Abstraction.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.APIs.Controllers.Controllers.Booking
{
	[Authorize]
	public class BookController(IServiceManager _serviceManager) : BaseApiController
	{
		[HttpPost("CreateBook")]
		public async Task<ActionResult<ReturnBookDto>> CreateBook([FromQuery] int eventId)
		{
			var result = await _serviceManager.BookService.CreateBook(User, eventId);
			return Ok(result);
		}

		[HttpGet("GetBook/{bookingId}")]
		public async Task<ActionResult<ReturnBookDto>> GetBook([FromRoute] int bookingId)
		{
			var result = await _serviceManager.BookService.GetBookById(bookingId);
			return Ok(result);
		}

		[HttpGet("GetAllBooksForSpecificUser")]
		public async Task<ActionResult<Pagination<ReturnBookDto>>> GetAllBooksForSpecificUser([FromQuery] int? eventId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
		{
			var result = await _serviceManager.BookService.GetAllBooksForSpecificUser(User, eventId, pageIndex, pageSize);
			return Ok(result);
		}

		[HttpDelete("CancelBook/{bookingId}")]
		public async Task<ActionResult<string>> CancelBook([FromRoute] int bookingId)
		{
			var result = await _serviceManager.BookService.CancelBook(bookingId);
			return Ok(result);
		}
	}
}

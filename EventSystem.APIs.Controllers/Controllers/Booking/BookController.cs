using EventSystem.APIs.Controllers.Controllers._Base;
using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Models.Booking;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.APIs.Controllers.Controllers.Booking
{
	public class BookController(IServiceManager _serviceManager) : BaseApiController
	{
		[HttpPost("CreateBooking")]
		public async Task<ActionResult<ReturnBookDto>> CreateBooking([FromRoute] int eventId)
		{
			var result = await _serviceManager.BookService.CreateBook(User, eventId);
			return Ok(result);
		}
	}
}

using AutoMapper;
using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Core.Application.Abstraction.Service.Booking;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Booking;
using EventSystem.Core.Domain.Entities.Identity;
using EventSystem.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EventSystem.Core.Application.Service.Booking
{
	internal class BookService(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager, IMapper _mapper) : ResponseHandler, IBookService
	{
		public async Task<Response<ReturnBookDto>> CreateBook(ClaimsPrincipal claimsPrincipal, int eventId)
		{

			var userId = claimsPrincipal.FindFirst(ClaimTypes.PrimarySid)?.Value;

			if (userId is null)
				return BadRequest<ReturnBookDto>("User Not Found!");

			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return BadRequest<ReturnBookDto>("User Not Found!");


			var bookRepo = _unitOfWork.BookRepository;

			var isBooked = await bookRepo.IsBookedByUser(userId, eventId);

			if (isBooked)
				return BadRequest<ReturnBookDto>("Event Already Booked!");

			var book = new Book()
			{
				EventId = eventId,
				UserId = userId,
				BookingDate = DateTime.UtcNow
			};

			await bookRepo.AddAsync(book);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<ReturnBookDto>("Booking Failed!");

			var mappedBook = _mapper.Map<ReturnBookDto>(book);

			return Success(mappedBook);
		}


	}
}


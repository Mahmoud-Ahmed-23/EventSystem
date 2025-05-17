using AutoMapper;
using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Core.Application.Abstraction.Service.Booking;
using EventSystem.Core.Application.Abstraction.Wrapper;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Booking;
using EventSystem.Core.Domain.Entities.Events;
using EventSystem.Core.Domain.Entities.Identity;
using EventSystem.Core.Domain.Specifications.Books;
using EventSystem.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventSystem.Core.Application.Service.Booking
{
	internal class BookService(IUnitOfWork _unitOfWork,
		UserManager<ApplicationUser> _userManager,
		IMapper _mapper) : ResponseHandler, IBookService
	{


		public async Task<Response<ReturnBookDto>> CreateBook(ClaimsPrincipal claimsPrincipal, int eventId)
		{

			var eventEntity = await _unitOfWork.GetRepository<Event, int>().GetByIdAsync(eventId);

			if (eventEntity is null)
				return NotFound<ReturnBookDto>("Event Not Found");

			var userId = claimsPrincipal.FindFirst(ClaimTypes.PrimarySid)?.Value;

			if (userId is null)
				return Unauthorized<ReturnBookDto>("User Not Found!");

			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return Unauthorized<ReturnBookDto>("User Not Found!");


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

		public async Task<Response<Pagination<ReturnBookDto>>> GetAllBooksForSpecificUser(ClaimsPrincipal claimsPrincipal, int? eventId, int pageIndex, int pageSize)
		{
			var userId = claimsPrincipal.FindFirst(ClaimTypes.PrimarySid)?.Value;

			if (userId is null)
				return Unauthorized<Pagination<ReturnBookDto>>("User Not Found!");

			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
				return Unauthorized<Pagination<ReturnBookDto>>("User Not Found!");

			var bookRepo = _unitOfWork.BookRepository;

			var spec = new BookPagination(userId, eventId, pageSize, pageIndex);

			var books = await bookRepo.GetAllWithSpecAsync(spec);

			if (books is null)
				return NotFound<Pagination<ReturnBookDto>>("No Bookings Found!");

			var mappedBooks = _mapper.Map<List<ReturnBookDto>>(books);

			var countSpec = new BookCountSpec(userId, eventId);

			var count = await _unitOfWork.GetRepository<Book, int>().GetCountAsync(countSpec);

			var response = new Pagination<ReturnBookDto>(pageIndex, pageSize, count) { Data = mappedBooks };

			return Success(response);


		}

		public async Task<Response<ReturnBookDto>> GetBookById(int bookId)
		{
			var bookRepo = _unitOfWork.BookRepository;

			var book = await bookRepo.GetByIdAsync(bookId);

			if (book is null)
				return NotFound<ReturnBookDto>("Booking Not Found!");

			var mappedBook = _mapper.Map<ReturnBookDto>(book);

			return Success(mappedBook);
		}

		public async Task<Response<string>> CancelBook(int bookId)
		{
			var bookRepo = _unitOfWork.BookRepository;

			var book = await bookRepo.GetByIdAsync(bookId);

			if (book is null)
				return NotFound<string>("Booking Not Found!");

			if (book.Event.Date < DateTime.Now)
				return BadRequest<string>("Booking Cancellation Failed! Event Date Passed!");

			bookRepo.Delete(book);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<string>("Booking Cancellation Failed!");

			return Success("Booking Cancelled Successfully!");
		}
	}
}


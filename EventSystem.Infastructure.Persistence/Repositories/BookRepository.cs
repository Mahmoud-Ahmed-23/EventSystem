using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Booking;
using EventSystem.Infastructure.Persistence._Data;
using EventSystem.Infastructure.Persistence.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence.Repositories
{
	internal class BookRepository : GenericRepository<Book, int>, IBookRepository
	{
		private readonly EventSystemDbContext _dbContext;

		public BookRepository(EventSystemDbContext dbContext)
			: base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Book>> GetAllBooksByUserId(string userId)
		{
			var books = await _dbContext.Books
				.Where(x => x.UserId == userId)
				.ToListAsync();

			return books;
		}

		public async Task<bool> IsBookedByUser(string userId, int eventId)
		{
			var isBooked = await _dbContext.Books
				.Where(x => x.UserId == userId && x.EventId == eventId)
				.FirstOrDefaultAsync();

			return isBooked != null;
		}
	}
}

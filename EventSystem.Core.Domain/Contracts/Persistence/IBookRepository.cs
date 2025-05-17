using EventSystem.Core.Domain.Entities.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Persistence
{
	public interface IBookRepository : IGenericRepository<Book, int>
	{
		Task<bool> IsBookedByUser(string userId, int eventId);
	}
}

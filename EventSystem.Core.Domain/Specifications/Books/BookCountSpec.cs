using EventSystem.Core.Domain.Entities.Booking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Specifications.Books
{
	public class BookCountSpec : BaseSpecification<Book, int>
	{
		public BookCountSpec(string userId, int? eventId)
			: base(b => (eventId == null || b.EventId == eventId)
			&& (string.IsNullOrEmpty(userId) || b.UserId == userId)
			)
		{
		}
	}
}

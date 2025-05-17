using EventSystem.Core.Domain.Entities.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Specifications.Books
{
	public class BookPagination : BaseSpecification<Book, int>
	{
		public BookPagination(string userId, int? eventId, int pageSize, int pageIndex)
			: base(b => (eventId == null || b.EventId == eventId)
			&& (string.IsNullOrEmpty(userId) || b.UserId == userId)
			)
		{
			AddIncludes();
			ApplyPagination((pageIndex - 1) * pageSize, pageSize);
		}

		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(E => E.Event);
			Includes.Add(E => E.User);
		}
	}
}

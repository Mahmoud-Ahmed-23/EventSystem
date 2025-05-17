using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Booking
{
	public class ReturnBookDto
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public string UserId { get; set; }
		public DateTime BookingDate { get; set; }
		public string EventName { get; set; }
		public string EventDescription { get; set; }
		public string EventVenue { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		public decimal EventPrice { get; set; }
		public string UserName { get; set; }

	}
}

using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Events;
using EventSystem.Core.Domain.Entities.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Entities.Booking
{
	public class Book : BaseAuditableEntity<int>
	{

		public int EventId { get; set; }

		public required string UserId { get; set; }

		public DateTime BookingDate { get; set; } = DateTime.UtcNow;

		public virtual Event Event { get; set; }

		public virtual ApplicationUser User { get; set; }
	}
}

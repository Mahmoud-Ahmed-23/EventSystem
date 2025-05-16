using EventSystem.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Entities.Booking
{
	public class Event : BaseAuditableEntity<int>
	{

		public string Name { get; set; }
		public string NormalizedName { get; set; }

		public string Description { get; set; }

		public int? CategoryId { get; set; }
		public DateTime Date { get; set; }

		public string Venue { get; set; }


		public decimal Price { get; set; }

		public string? ImageUrl { get; set; }


		public virtual ICollection<Book>? Books { get; set; }
		public virtual Category Category { get; set; }

	}
}

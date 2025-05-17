using EventSystem.Core.Domain.Entities.Booking;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Entities.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public DateTime CreatedAt { get; set; }
		public required string FullName { get; set; }
		public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
		public virtual ICollection<Book> Books { get; set; }

	}
}

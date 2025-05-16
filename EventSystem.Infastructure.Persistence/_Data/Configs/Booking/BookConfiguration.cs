using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Entities.Booking;
using EventSystem.Infastructure.Persistence._Data.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence._Data.Configs.Booking
{
	internal class BookConfiguration : BaseAuditableEntityConfigurations<Book, int>
	{
		public override void Configure(EntityTypeBuilder<Book> builder)
		{
			base.Configure(builder);

			builder.Property(b => b.BookingDate)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.HasOne(b => b.User)
				.WithMany(u => u.Books)
				.HasForeignKey(b => b.UserId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}

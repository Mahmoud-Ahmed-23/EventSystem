using EventSystem.Core.Domain.Entities.Events;
using EventSystem.Infastructure.Persistence._Data.Configs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infastructure.Persistence._Data.Configs.Booking
{
	internal class EventConfiguration : BaseAuditableEntityConfigurations<Event, int>
	{
		public override void Configure(EntityTypeBuilder<Event> builder)
		{
			base.Configure(builder);

			builder.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(200);

			builder.Property(e => e.Description)
				.HasMaxLength(2000);

			builder.Property(e => e.Venue)
				.IsRequired()
				.HasMaxLength(300);

			builder.Property(e => e.Price)
				.HasColumnType("decimal(18,2)");

			builder.Property(e => e.ImageUrl)
				.HasMaxLength(500);

			builder.HasOne(e => e.Category)
				.WithMany(c => c.Events)
				.HasForeignKey(e => e.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(e => e.Books)
				.WithOne(b => b.Event)
				.HasForeignKey(b => b.EventId);
		}
	}
}
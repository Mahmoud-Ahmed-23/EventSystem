using EventSystem.Core.Domain.Entities.Booking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence._Data
{
	public class EventSystemDbContext : IdentityDbContext
	{
		public EventSystemDbContext(DbContextOptions<EventSystemDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformtion).Assembly);
		}
		public DbSet<Event> Events { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Book> Books { get; set; }
	}
}

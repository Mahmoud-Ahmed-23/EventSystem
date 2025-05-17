using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Events;
using EventSystem.Infastructure.Persistence._Data;
using EventSystem.Infastructure.Persistence.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence.Repositories
{
	internal class EventRepository : GenericRepository<Event, int>, IEventRepository
	{
		private readonly EventSystemDbContext _dbContext;

		public EventRepository(EventSystemDbContext dbContext)
			: base(dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<bool> IsEventExists(string eventName)
		{
			var eventExists = await _dbContext.Events
				.AsNoTracking()
				.AnyAsync(e => e.NormalizedName == eventName.ToUpper());
			return eventExists;
		}
	}
}

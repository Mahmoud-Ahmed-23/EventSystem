using EventSystem.Core.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Persistence
{
	public interface IEventRepository : IGenericRepository<Event, int>
	{
		Task<bool> IsEventExists(string eventName);
	}
}

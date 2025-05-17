using EventSystem.Core.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Specifications.Events
{
	public class EventCountSpec : BaseSpecification<Event, int>
	{
		public EventCountSpec(int? categoryId)
			: base(e => (categoryId == null || e.CategoryId == categoryId))
		{
		}
	}
}

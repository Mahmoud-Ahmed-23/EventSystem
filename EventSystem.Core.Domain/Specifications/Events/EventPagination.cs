using EventSystem.Core.Domain.Entities.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Specifications.Events
{
	public class EventPagination : BaseSpecification<Event, int>
	{
		public EventPagination(int? categoryId, int pageSize, int pageIndex)
			: base(e => (categoryId == null || e.CategoryId == categoryId))
		{
			ApplyPagination((pageIndex - 1) * pageSize, pageSize);
		}

		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(E => E.Category);
		}
	}
}

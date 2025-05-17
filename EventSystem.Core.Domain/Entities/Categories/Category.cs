using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Entities.Categories
{
	public class Category : BaseAuditableEntity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Event>? Events { get; set; }
	}
}

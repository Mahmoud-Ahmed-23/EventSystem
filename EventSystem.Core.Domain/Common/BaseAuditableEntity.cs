using EventSystem.Core.Domain.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Common
{
	public class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IBaseAuditableEntity where TKey : IEquatable<TKey>
	{
		public string CreatedBy { get; set; } = null!;

		public DateTime CreatedOn { get; set; }

		public string LastModifiedBy { get; set; } = null!;


		public DateTime LastModifiedOn { get; set; }
	}
}

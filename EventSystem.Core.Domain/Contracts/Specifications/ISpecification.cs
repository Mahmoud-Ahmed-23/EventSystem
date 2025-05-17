﻿using EventSystem.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Specifications
{
	public interface ISpecification<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; }
		public List<Expression<Func<TEntity, object>>> Includes { get; set; }


		public int Skip { get; set; }
		public int Take { get; set; }

		public bool IsPaginationEnabled { get; set; }



	}
}

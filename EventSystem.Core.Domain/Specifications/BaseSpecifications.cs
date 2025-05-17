using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Specifications
{
	public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey>
	   where TEntity : BaseEntity<TKey>
	   where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; }
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }

		private protected virtual void AddIncludes()
		{

		}

		public BaseSpecification()
		{

		}

		public BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
		{
			Criteria = criteria;
		}

		public BaseSpecification(TKey id)
		{
			Criteria = E => E.Id.Equals(id);
		}

		private protected void ApplyPagination(int skip, int take)
		{
			IsPaginationEnabled = true;

			Skip = skip;
			Take = take;
		}

	}
}

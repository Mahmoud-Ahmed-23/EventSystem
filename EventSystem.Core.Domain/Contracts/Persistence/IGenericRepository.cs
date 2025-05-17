using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Persistence
{
	public interface IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Task<TEntity> GetByIdAsync(TKey? id);
		Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec);
		public Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
		Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> Spec, bool WithTraching = false);
		public Task AddAsync(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);


		Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec);
	}
}

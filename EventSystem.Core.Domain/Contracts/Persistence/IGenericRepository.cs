using EventSystem.Core.Domain.Common;
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
		public Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
		public Task AddAsync(TEntity entity);
		public void UpdateAsync(TEntity entity);
		public void DeleteAsync(TEntity entity);
	}
}

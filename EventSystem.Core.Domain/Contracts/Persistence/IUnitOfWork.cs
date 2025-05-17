using EventSystem.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Persistence
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>;

		IBookRepository BookRepository { get; }
		ICategoryRepository categoryRepository { get; }
		IEventRepository EventRepository { get; }

		Task<int> CompleteAsync();
	}
}

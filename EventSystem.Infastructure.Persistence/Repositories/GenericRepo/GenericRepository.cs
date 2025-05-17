using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Infastructure.Persistence._Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence.Repositories.GenericRepo
{
	internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		private readonly EventSystemDbContext _dbContext;

		public GenericRepository(EventSystemDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
		=> WithTracking ? await _dbContext.Set<TEntity>().ToListAsync() : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		public async Task<TEntity?> GetByIdAsync(TKey id)
			=> await _dbContext.Set<TEntity>().FindAsync(id);

		public async Task AddAsync(TEntity entity)
			=> await _dbContext.Set<TEntity>().AddAsync(entity);
		public void Update(TEntity entity)
		=> _dbContext.Set<TEntity>().Update(entity);

		public void Delete(TEntity entity)
			=> _dbContext.Set<TEntity>().Remove(entity);

	}
}

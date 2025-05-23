﻿using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Infastructure.Persistence._Data;
using EventSystem.Infastructure.Persistence.Repositories;
using EventSystem.Infastructure.Persistence.Repositories.GenericRepo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence.UnitOfwork
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly ConcurrentDictionary<string, object> _repositories;
		private readonly EventSystemDbContext _dbContext;
		private readonly Lazy<IBookRepository> _bookRepository;
		private readonly Lazy<ICategoryRepository> _categoryRepository;
		private readonly Lazy<IEventRepository> _eventRepository;



		public UnitOfWork(EventSystemDbContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new();
			_bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_dbContext));
			_categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_dbContext));
			_eventRepository = new Lazy<IEventRepository>(() => new EventRepository(_dbContext));
		}

		public IBookRepository BookRepository => _bookRepository.Value;

		public ICategoryRepository categoryRepository => _categoryRepository.Value;

		public IEventRepository EventRepository => _eventRepository.Value;

		public async Task<int> CompleteAsync()
			=> await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
			=> await _dbContext.DisposeAsync();

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		=> (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
	}
}

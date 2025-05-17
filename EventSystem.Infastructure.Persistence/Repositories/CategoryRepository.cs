using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Categories;
using EventSystem.Infastructure.Persistence._Data;
using EventSystem.Infastructure.Persistence.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence.Repositories
{
	internal class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
	{
		private readonly EventSystemDbContext _dbContext;

		public CategoryRepository(EventSystemDbContext dbContext)
			: base(dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<bool> IsCategoryExist(string name, CancellationToken cancellationToken = default)
		{

			var category = await _dbContext.Categories
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
			return category != null;
		}
	}
}

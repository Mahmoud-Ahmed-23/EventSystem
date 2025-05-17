using EventSystem.Core.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Persistence
{
	public interface ICategoryRepository : IGenericRepository<Category, int>
	{
		Task<bool> IsCategoryExist(string name, CancellationToken cancellationToken = default);
	}
}

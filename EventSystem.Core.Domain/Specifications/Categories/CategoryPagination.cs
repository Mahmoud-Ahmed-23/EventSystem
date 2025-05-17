using EventSystem.Core.Domain.Entities.Categories;

namespace EventSystem.Core.Domain.Specifications.Categories
{
	public class CategoryPagination : BaseSpecification<Category, int>
	{
		public CategoryPagination(int pageSize, int pageIndex)
		{
			ApplyPagination((pageIndex - 1) * pageSize, pageSize);
		}

	}
}

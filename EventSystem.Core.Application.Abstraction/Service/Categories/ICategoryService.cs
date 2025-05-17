using EventSystem.Core.Application.Abstraction.Models.Categories;
using EventSystem.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Service.Categories
{
	public interface ICategoryService
	{
		Task<Response<ReturnCategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken = default);
		Task<Response<string>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken = default);
		Task<Response<string>> DeleteCategory(int id, CancellationToken cancellationToken = default);
		Task<Response<IEnumerable<ReturnCategoryDto>>> GetCategories(CancellationToken cancellationToken = default);
		Task<Response<ReturnCategoryDto>> GetCategoryById(int id, CancellationToken cancellationToken = default);
	}
}

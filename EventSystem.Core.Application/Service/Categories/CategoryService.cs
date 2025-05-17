using AutoMapper;
using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Core.Application.Abstraction.Models.Categories;
using EventSystem.Core.Application.Abstraction.Service.Categories;
using EventSystem.Core.Application.Abstraction.Wrapper;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Categories;
using EventSystem.Core.Domain.Specifications.Categories;
using EventSystem.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Service.Categories
{
	internal class CategoryService(IUnitOfWork _unitOfWork, IMapper _mapper) : ResponseHandler, ICategoryService
	{
		public async Task<Response<ReturnCategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken = default)
		{
			var categoryRepo = _unitOfWork.categoryRepository;

			var categoryIsExist = await categoryRepo.IsCategoryExist(createCategoryDto.Name);

			if (categoryIsExist)
				return BadRequest<ReturnCategoryDto>("Category Already Exist!");

			var category = _mapper.Map<Category>(createCategoryDto);

			await categoryRepo.AddAsync(category);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<ReturnCategoryDto>("Category Creation Failed!");

			var mappedCategory = _mapper.Map<ReturnCategoryDto>(category);

			return Success(mappedCategory);


		}

		public async Task<Response<Pagination<ReturnCategoryDto>>> GetCategories(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
		{
			var categoryRepo = _unitOfWork.GetRepository<Category, int>();

			var spec = new CategoryPagination(pageSize, pageIndex);

			var categories = await categoryRepo.GetAllWithSpecAsync(spec);

			var count = categories.Count();

			if (categories is null)
				return NotFound<Pagination<ReturnCategoryDto>>("Categories Not Found!");

			var mappedCategories = _mapper.Map<List<ReturnCategoryDto>>(categories);

			var response = new Pagination<ReturnCategoryDto>(pageIndex, pageSize, count) { Data = mappedCategories };

			return Success(response);
		}

		public async Task<Response<ReturnCategoryDto>> GetCategoryById(int id, CancellationToken cancellationToken = default)
		{
			var categoryRepo = _unitOfWork.GetRepository<Category, int>();

			var category = await categoryRepo.GetByIdAsync(id);

			if (category is null)
				return NotFound<ReturnCategoryDto>("Category Not Found!");

			var mappedCategory = _mapper.Map<ReturnCategoryDto>(category);

			return Success(mappedCategory);
		}

		public async Task<Response<string>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken = default)
		{
			var categoryRepo = _unitOfWork.GetRepository<Category, int>();

			var category = await categoryRepo.GetByIdAsync(id);

			if (category is null)
				return NotFound<string>("Category Not Found!");

			var updatedCategory = _mapper.Map(updateCategoryDto, category);

			categoryRepo.Update(updatedCategory);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<string>("Category Update Failed!");

			return Success("Category Updated Successfully!");
		}

		public async Task<Response<string>> DeleteCategory(int id, CancellationToken cancellationToken = default)
		{
			var categoryRepo = _unitOfWork.GetRepository<Category, int>();

			var category = await categoryRepo.GetByIdAsync(id);

			if (category is null)
				return NotFound<string>("Category Not Found!");

			categoryRepo.Delete(category);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<string>("Category Deletion Failed!");

			return Success("Category Deleted Successfully!");
		}

		
	}
}

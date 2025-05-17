using EventSystem.APIs.Controllers.Controllers._Base;
using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Models.Categories;
using EventSystem.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSystem.APIs.Controllers.Controllers.Category
{
	[Authorize]
	public class CategoryController(IServiceManager _serviceManager) : BaseApiController
	{
		[Authorize(Roles = "Admin")]
		[HttpPost("CreateCategory")]
		public async Task<ActionResult<Response<ReturnCategoryDto>>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
		{
			var result = await _serviceManager.categoryService.CreateCategory(createCategoryDto);
			return NewResult(result);
		}


		[HttpGet("GetCategories")]
		public async Task<ActionResult<Response<List<ReturnCategoryDto>>>> GetCategories()
		{
			var result = await _serviceManager.categoryService.GetCategories();
			return NewResult(result);
		}


		[HttpGet("GetCategoryById/{id}")]
		public async Task<ActionResult<Response<ReturnCategoryDto>>> GetCategoryById([FromRoute] int id)
		{
			var result = await _serviceManager.categoryService.GetCategoryById(id);
			return NewResult(result);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("UpdateCategory/{id}")]
		public async Task<ActionResult<Response<string>>> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateCategoryDto)
		{
			var result = await _serviceManager.categoryService.UpdateCategory(id, updateCategoryDto);
			return NewResult(result);
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("DeleteCategory/{id}")]
		public async Task<ActionResult<Response<string>>> DeleteCategory([FromRoute] int id)
		{
			var result = await _serviceManager.categoryService.DeleteCategory(id);
			return NewResult(result);
		}
	}
}

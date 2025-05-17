using EventSystem.APIs.Controllers.Controllers._Base;
using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Models.Events;
using EventSystem.Core.Application.Abstraction.Wrapper;
using EventSystem.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.APIs.Controllers.Controllers.Event
{
	[Authorize]
	public class EventController(IServiceManager _serviceManager) : BaseApiController
	{
		[Authorize(Roles = "Admin")]
		[HttpPost("CreateEvent")]
		public async Task<ActionResult<Response<ReturnEventDto>>> CreateEvent([FromBody] CreateEventDto eventDto)
		{
			var result = await _serviceManager.EventService.CreateEvent(eventDto);
			return NewResult(result);
		}

		[HttpGet("GetAllEvents")]
		public async Task<ActionResult<Response<Pagination<ReturnEventDto>>>> GetAllEvents([FromQuery] int? categoryId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
		{
			var result = await _serviceManager.EventService.GetAllEvents(categoryId, pageIndex, pageSize);
			return NewResult(result);
		}

		[HttpGet("GetEventById/{eventId}")]
		public async Task<ActionResult<Response<ReturnEventDto>>> GetEventById([FromRoute] int eventId)
		{
			var result = await _serviceManager.EventService.GetEventById(eventId);
			return NewResult(result);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("UpdateEvent/{eventId}")]
		public async Task<ActionResult<Response<string>>> UpdateEvent([FromRoute] int eventId, [FromBody] UpdateEventDto eventDto)
		{
			var result = await _serviceManager.EventService.UpdateEvent(eventId, eventDto);
			return NewResult(result);
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("DeleteEvent/{eventId}")]
		public async Task<ActionResult<Response<string>>> DeleteEvent([FromRoute] int eventId)
		{
			var result = await _serviceManager.EventService.DeleteEvent(eventId);
			return NewResult(result);
		}
	}
}

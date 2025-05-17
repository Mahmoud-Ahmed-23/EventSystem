using AutoMapper;
using EventSystem.Core.Application.Abstraction.Models.Categories;
using EventSystem.Core.Application.Abstraction.Models.Events;
using EventSystem.Core.Application.Abstraction.Service.Events;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Categories;
using EventSystem.Core.Domain.Entities.Events;
using EventSystem.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Service.Events
{
	internal class EventService(IUnitOfWork _unitOfWork, IMapper _mapper) : ResponseHandler, IEventService
	{
		public async Task<Response<ReturnEventDto>> CreateEvent(CreateEventDto createEventDto)
		{
			var eventRepo = _unitOfWork.EventRepository;

			var isEventExist = await eventRepo.IsEventExists(createEventDto.Name);

			if (isEventExist)
				return BadRequest<ReturnEventDto>("Event Already Exist!");

			var eventEntity = _mapper.Map<Event>(createEventDto);

			await eventRepo.AddAsync(eventEntity);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<ReturnEventDto>("Event Creation Failed!");

			var mappedEvent = _mapper.Map<ReturnEventDto>(eventEntity);

			return Success(mappedEvent);
		}

		public async Task<Response<string>> DeleteEvent(int eventId)
		{
			var eventRepo = _unitOfWork.GetRepository<Event, int>();

			var eventEntity = await eventRepo.GetByIdAsync(eventId);

			if (eventEntity is null)
				return NotFound<string>("Event Not Found!");

			eventRepo.Delete(eventEntity);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<string>("Event Deletion Failed!");

			return Success("Event Deleted Successfully!");
		}

		public async Task<Response<IEnumerable<ReturnEventDto>>> GetAllEvents()
		{
			var eventRepo = _unitOfWork.GetRepository<Event, int>();

			var events = await eventRepo.GetAllAsync();

			if (events is null)
				return NotFound<IEnumerable<ReturnEventDto>>("Events Not Found!");

			var mappedEvents = _mapper.Map<List<ReturnEventDto>>(events);

			return Success<IEnumerable<ReturnEventDto>>(mappedEvents);
		}

		public async Task<Response<ReturnEventDto>> GetEventById(int eventId)
		{
			var eventRepo = _unitOfWork.GetRepository<Event, int>();

			var eventEntity = await eventRepo.GetByIdAsync(eventId);

			if (eventEntity is null)
				return NotFound<ReturnEventDto>("Event Not Found!");

			var mappedEvent = _mapper.Map<ReturnEventDto>(eventEntity);

			return Success(mappedEvent);
		}

		public async Task<Response<string>> UpdateEvent(int eventId, UpdateEventDto updateEventDto)
		{
			var eventRepo = _unitOfWork.GetRepository<Event, int>();

			var eventEntity = await eventRepo.GetByIdAsync(eventId);
			if (eventEntity is null)
				return NotFound<string>("Event Not Found!");

			var updatedEvent = _mapper.Map(updateEventDto, eventEntity);

			eventRepo.Update(updatedEvent);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
				return BadRequest<string>("Event Update Failed!");

			return Success("Event Updated Successfully!");
		}
	}
}

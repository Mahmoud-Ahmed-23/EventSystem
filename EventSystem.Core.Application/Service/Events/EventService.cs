using AutoMapper;
using EventSystem.Core.Application.Abstraction.Models.Events;
using EventSystem.Core.Application.Abstraction.Service.Events;
using EventSystem.Core.Application.Abstraction.Wrapper;
using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Events;
using EventSystem.Core.Domain.Specifications.Events;
using EventSystem.Shared.Responses;

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

		public async Task<Response<Pagination<ReturnEventDto>>> GetAllEvents(int? categoryId, int pageIndex, int pageSize)
		{
			var eventRepo = _unitOfWork.GetRepository<Event, int>();

			var spec = new EventPagination(categoryId, pageSize, pageIndex);

			var events = await eventRepo.GetAllWithSpecAsync(spec);

			if (events is null)
				return NotFound<Pagination<ReturnEventDto>>("There isn't any Event!");

			var countSpec = new EventCountSpec(categoryId);

			var count = await _unitOfWork.GetRepository<Event, int>().GetCountAsync(countSpec);

			var mappedEvents = _mapper.Map<List<ReturnEventDto>>(events);

			var response = new Pagination<ReturnEventDto>(pageIndex, pageSize, count) { Data = mappedEvents };

			return Success(response);
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

using EventSystem.Core.Application.Abstraction.Models.Events;
using EventSystem.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Service.Events
{
	public interface IEventService
	{
		public Task<Response<ReturnEventDto>> CreateEvent(CreateEventDto createEventDto);

		public Task<Response<ReturnEventDto>> GetEventById(int eventId);

		public Task<Response<IEnumerable<ReturnEventDto>>> GetAllEvents();

		public Task<Response<string>> UpdateEvent(int eventId, UpdateEventDto updateEventDto);
		public Task<Response<string>> DeleteEvent(int eventId);

	}
}

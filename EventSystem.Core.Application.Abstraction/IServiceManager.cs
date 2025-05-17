using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Core.Application.Abstraction.Service.Booking;
using EventSystem.Core.Application.Abstraction.Service.Categories;
using EventSystem.Core.Application.Abstraction.Service.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction
{
	public interface IServiceManager
	{
		public IAuthService AuthService { get; }
		public IBookService BookService { get; }
		public ICategoryService categoryService { get; }
		public IEventService EventService { get; }
	}
}

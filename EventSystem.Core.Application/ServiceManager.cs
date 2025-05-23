﻿using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Application.Abstraction.Service.Auth;
using EventSystem.Core.Application.Abstraction.Service.Booking;
using EventSystem.Core.Application.Abstraction.Service.Categories;
using EventSystem.Core.Application.Abstraction.Service.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application
{
	internal class ServiceManager : IServiceManager
	{
		private readonly Lazy<IAuthService> _authService;
		private readonly Lazy<IBookService> _bookService;
		private readonly Lazy<ICategoryService> _categoryService;
		private readonly Lazy<IEventService> _eventService;
		public ServiceManager(Func<IAuthService> authfactory, Func<ICategoryService> categoryfactory, Func<IBookService> bookfactory, Func<IEventService> eventfactory)
		{
			_authService = new Lazy<IAuthService>(authfactory, LazyThreadSafetyMode.ExecutionAndPublication);
			_categoryService = new Lazy<ICategoryService>(categoryfactory, LazyThreadSafetyMode.ExecutionAndPublication);
			_bookService = new Lazy<IBookService>(bookfactory, LazyThreadSafetyMode.ExecutionAndPublication);
			_eventService = new Lazy<IEventService>(eventfactory, LazyThreadSafetyMode.ExecutionAndPublication);

		}

		public IAuthService AuthService => _authService.Value;
		public IBookService BookService => _bookService.Value;
		public ICategoryService categoryService => _categoryService.Value;

		public IEventService EventService => _eventService.Value;
	}

}

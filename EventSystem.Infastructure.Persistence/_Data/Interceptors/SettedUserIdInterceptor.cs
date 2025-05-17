using EventSystem.Core.Application.Abstraction;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Core.Domain.Common;

namespace EventSystem.Infastructure.Persistence._Data.Interceptors
{
	public class SettedUserIdInterceptor : AuditInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public SettedUserIdInterceptor(ILoggedInUserService loggedInUserService) : base(loggedInUserService)
		{
			_loggedInUserService = loggedInUserService;
		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities(eventData.Context);

			return base.SavingChanges(eventData, result);


		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{

			UpdateEntities(eventData.Context);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private new void UpdateEntities(DbContext? context)
		{

			if (context is null) return;

			var Entries = context.ChangeTracker.Entries<BaseEntity<string>>()
								.Where(entry => entry.State is EntityState.Added or EntityState.Modified);

			foreach (var entry in Entries)
			{
				
				if (entry.State is EntityState.Added)
				{

					entry.Entity.Id = _loggedInUserService.UserId;

				}

			}
		}

	}
}

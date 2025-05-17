using EventSystem.Core.Application.Abstraction;
using EventSystem.Core.Domain.Common;
using EventSystem.Core.Domain.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EventSystem.Infastructure.Persistence._Data.Interceptors
{
	public class AuditInterceptor(ILoggedInUserService _loggedInUser) : SaveChangesInterceptor
	{



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

		private protected void UpdateEntities(DbContext? context)
		{

			if (context is null) return;

			var Entries = context.ChangeTracker.Entries<IBaseAuditableEntity>()
								.Where(entry => entry.State is EntityState.Added or EntityState.Modified);

			foreach (var entry in Entries)
			{

				if (entry.State is EntityState.Added)
				{

					entry.Entity.CreatedBy = _loggedInUser.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;

				}

				entry.Entity.LastModifiedBy = _loggedInUser.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;

			}
		}
	}
}

using EventSystem.Core.Domain.Contracts.Persistence;
using EventSystem.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence._Data
{
	internal class EventSystemDbInitializer(EventSystemDbContext _dbContext
		, RoleManager<IdentityRole> _roleManager)
		: IEventSystemDbInitializer
	{
		public async Task InitializeAsync()
		{
			var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
			{
				await _dbContext.Database.MigrateAsync();
			}
		}

		public async Task SeedAsync()
		{
			if (!_dbContext.Roles.Any())
			{
				var roles = new List<IdentityRole>
				{
					new IdentityRole
					{
						Name = "Admin",
						NormalizedName = "ADMIN"
					},
					new IdentityRole
					{
						Name = "User",
						NormalizedName = "USER"
					}
				};

				foreach (var role in roles)
				{
					await _roleManager.CreateAsync(role);
				}
			}
		}
	}
}

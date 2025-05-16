using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Domain.Contracts.Persistence
{
	public interface IEventSystemDbInitializer
	{
		Task SeedAsync();
		Task InitializeAsync();
	}
}

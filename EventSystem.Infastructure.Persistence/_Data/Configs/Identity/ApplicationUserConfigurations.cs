using EventSystem.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence._Data.Configs.Identity
{
	public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(e => e.FullName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.CreatedAt)
				.HasDefaultValueSql("getdate()")
				.ValueGeneratedOnAdd();
		}
	}
}

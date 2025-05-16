﻿using EventSystem.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Infastructure.Persistence._Data.Configs.Base
{
	internal class BaseAuditableEntityConfigurations<TEntity, TKey> : BaseEntityConfigurations<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			base.Configure(builder);

			builder.Property(E => E.CreatedBy).IsRequired();

			builder.Property(E => E.CreatedOn).IsRequired();


			builder.Property(E => E.LastModifiedBy).IsRequired();

			builder.Property(E => E.LastModifiedOn).IsRequired();
		}
	}
}

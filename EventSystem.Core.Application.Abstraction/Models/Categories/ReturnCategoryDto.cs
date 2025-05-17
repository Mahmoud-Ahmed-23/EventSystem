using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Categories
{
	public class ReturnCategoryDto
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }

		public string CreatedBy { get; set; } = null!;

		public DateTime CreatedOn { get; set; }

		public string LastModifiedBy { get; set; } = null!;


		public DateTime LastModifiedOn { get; set; }
	}
}

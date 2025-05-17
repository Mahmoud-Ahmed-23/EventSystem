using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Events
{
	public class ReturnEventDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int? CategoryId { get; set; }

		public DateTime Date { get; set; }

		public string Venue { get; set; }

		public decimal Price { get; set; }

		public string? ImagePath { get; set; }

		public string CategoryName { get; set; }

		public string CategoryDescription { get; set; }

		public string CreatedBy { get; set; } 

		public DateTime CreatedOn { get; set; }

		public string LastModifiedBy { get; set; } 
		public DateTime LastModifiedOn { get; set; }
	}
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Abstraction.Models.Events
{
	public class UpdateEventDto
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int? CategoryId { get; set; }
		public DateTime Date { get; set; }

		public string Venue { get; set; }


		public decimal Price { get; set; }

		public IFormFile? ImagePath { get; set; }
	}
}

using AutoMapper;
using EventSystem.Core.Application.Abstraction.Models.Booking;
using EventSystem.Core.Application.Abstraction.Models.Categories;
using EventSystem.Core.Domain.Entities.Booking;
using EventSystem.Core.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem.Core.Application.Mapping
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Book, ReturnBookDto>()
				.ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
				.ForMember(dest => dest.EventDescription, opt => opt.MapFrom(src => src.Event.Description))
				.ForMember(dest => dest.EventVenue, opt => opt.MapFrom(src => src.Event.Venue))
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Event.Category.Name))
				.ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Event.Category.Description))
				.ForMember(dest => dest.EventPrice, opt => opt.MapFrom(src => src.Event.Price))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));

			CreateMap<CreateCategoryDto, Category>();
			CreateMap<UpdateCategoryDto, Category>();
			CreateMap<Category, ReturnCategoryDto>();
		}
	}
}

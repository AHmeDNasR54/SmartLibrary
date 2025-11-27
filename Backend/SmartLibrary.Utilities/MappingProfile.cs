using AutoMapper;
using SmartLibrary.Models;
using SmartLibrary.Models.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartLibrary.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add more maps as you go
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Book, BookDto>().ForMember(dest => dest.Image, opt => opt.Ignore()).ReverseMap();

            CreateMap<Borrow, BorrowDto>().ReverseMap();

            CreateMap<ApplicationUser,RegisterDto>().ReverseMap();
         
        }
    }
}

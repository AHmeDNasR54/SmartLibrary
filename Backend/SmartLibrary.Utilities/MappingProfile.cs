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
            //CreateMap<Book, BookDto>().ReverseMap();
            //        CreateMap<Book, BookGetDto>()
            //.ForMember(dest => dest.Image,
            //    opt => opt.MapFrom(src =>
            //        string.IsNullOrEmpty(src.ImageUrl)
            //            ? null
            //            : $"{_baseUrl}/uploads/books/{src.ImageUrl}"
            //    ));
            CreateMap<Book, BookGetDto>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Borrow, BorrowDto>().ReverseMap();
            CreateMap<CreateBorrowDto, Borrow>();


            //CreateMap<ApplicationUser,RegisterDto>().ReverseMap();
            CreateMap<RegisterDto, ApplicationUser>()
    .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ReverseMap();  // هيسمح بالمابنج العكسي من ApplicationUser لـ RegisterDto


        }
    }
}

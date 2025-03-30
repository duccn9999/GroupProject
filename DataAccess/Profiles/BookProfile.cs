using AutoMapper;
using DataAccess.DTOs.Books;
using DataAccess.Models;
namespace DataAccess.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDTO, Book>();
            CreateMap<UpdateBookDTO, Book>();
        }
    }
}

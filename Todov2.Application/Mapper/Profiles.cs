using Application.DTO.Todo;
using AutoMapper;
using TodoV2.Domain.Entities;

namespace Application.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<TodoDtoGet, Todo>().ReverseMap();
            CreateMap<TodoDtoPost, Todo>().ReverseMap();
        }
        
    }
}
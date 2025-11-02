using AutoMapper;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Task, TaskDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using TaskManagementSystem.API.DTOs;

namespace TaskManagementSystem.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        }
    }
}

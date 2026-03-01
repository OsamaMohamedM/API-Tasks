using AdvancedQuerying.Models.DTOs;
using AdvancedQuerying.Models.Entities;
using AutoMapper;

namespace AdvancedQuerying.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskEntity, TaskDto>();
    }
}

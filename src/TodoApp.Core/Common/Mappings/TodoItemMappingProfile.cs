using AutoMapper;
using TodoApp.Core.Dtos.Todos;
using TodoApp.Core.Entities;
using TodoApp.Core.Enums;

namespace TodoApp.Core.Common.Mappings;

public class TodoItemMappingProfile : Profile
{
    public TodoItemMappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => Enumeration.GetById<StatusEnum>(s.StatusId)))
            .ForMember(d => d.Priority, o => o.MapFrom(s => Enumeration.GetById<PriorityEnum>(s.PriorityId)));

        CreateMap<TodoEditDto, TodoItem>();
    }
}
using AutoMapper;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.Mapping.Task;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<DAL.Entities.Task, TaskDto>();
        CreateMap<TaskCreateDto, DAL.Entities.Task>();
        CreateMap<TaskUpdateDto, DAL.Entities.Task>();
    }
}
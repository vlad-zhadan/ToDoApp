using AutoMapper;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.Mapping.Task;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<DAL.Entities.ToDoTask, TaskDto>();
        CreateMap<TaskCreateDto, DAL.Entities.ToDoTask>();
        CreateMap<TaskUpdateDto, DAL.Entities.ToDoTask>();
    }
}
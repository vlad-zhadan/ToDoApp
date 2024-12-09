using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.DTO.Task;
using ToDoApp.BLL.MediatR.Task.Create;
using ToDoApp.BLL.MediatR.Task.Delete;
using ToDoApp.BLL.MediatR.Task.GetAll;
using ToDoApp.BLL.MediatR.Task.GetById;
using ToDoApp.BLL.MediatR.Task.Update;
using ToDoApp.BLL.MediatR.Task.UpdateDueDate;
using ToDoApp.BLL.MediatR.Task.UpdateStatus;
using ToDoApp.WebApi.Controllers.Base;

namespace ToDoApp.WebApi.Controllers.Task;

public class TaskController : BaseController
{
    public TaskController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return  HandleResult(await _mediator.Send(new GetAllTasksQuery()));
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return  HandleResult(await _mediator.Send(new GetTaskByIdQuery(id)));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto newTask)
    {
        return  HandleResult(await _mediator.Send(new CreateTaskCommand(newTask)));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] TaskUpdateDto updatedTask)
    {
        return  HandleResult(await _mediator.Send(new UpdateTaskCommand(updatedTask)));
    }
    
    [HttpPatch("status")]
    public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskUpdateStatusDto updatedTaskStatus)
    {
        return  HandleResult(await _mediator.Send(new UpdateTaskStatusCommand(updatedTaskStatus)));
    }
    
    [HttpPatch("due-date")]
    public async Task<IActionResult> UpdateTaskDueDate([FromBody] TaskUpdateDueDateDto updatedTaskDueDate)
    {
        return  HandleResult(await _mediator.Send(new UpdateTaskDueDateCommand(updatedTaskDueDate)));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult>DeleteTask(int id)
    {
        return  HandleResult(await _mediator.Send(new DeleteTaskCommand(id)));
    }
    
}
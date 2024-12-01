using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.DTO.Task;

public class TaskDto
{
    public int TaskId { get; set; }
    
    public string? Name{ get; set; }
    
    public string? Description{ get; set; }

    public Status Status{ get; set; }
    
    public DateTime DueDate{ get; set; }
} 
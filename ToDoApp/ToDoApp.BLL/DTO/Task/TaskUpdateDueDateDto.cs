using System.ComponentModel.DataAnnotations;
using ToDoApp.BLL.Attributes.General;
using ToDoApp.BLL.Attributes.Task;
using ToDoApp.BLL.Constants.General;

namespace ToDoApp.BLL.DTO.Task;

public class TaskUpdateDueDateDto
{
    [Required]
    [GreaterOrEqualThan(GeneralConstants.MinValueForId,ErrorMessage = "Id is invalid")]
    public int Id { get; set; }
    
    [Required]
    [ValidDateForTask(ErrorMessage = "Due date should be within the allowed range.")]
    public DateTime DueDate{ get; set; }
}
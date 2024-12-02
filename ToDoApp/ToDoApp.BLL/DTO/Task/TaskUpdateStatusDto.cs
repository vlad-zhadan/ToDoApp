using System.ComponentModel.DataAnnotations;
using ToDoApp.BLL.Attributes.General;
using ToDoApp.BLL.Attributes.Task;
using ToDoApp.BLL.Constants.General;
using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.DTO.Task;

public class TaskUpdateStatusDto
{
    [Required]
    [GreaterOrEqualThan(GeneralConstants.MinValueForId,ErrorMessage = "Id is invalid")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Status is required")]
    [ValidStatus(ErrorMessage = "Invalid status")]
    public Status Status{ get; set; }
}
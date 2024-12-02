using System.ComponentModel.DataAnnotations;
using ToDoApp.BLL.Attributes.General;
using ToDoApp.BLL.Attributes.Task;
using ToDoApp.BLL.Constants.General;
using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.DTO.Task;

public class TaskUpdateDto
{
    [Required]
    [GreaterOrEqualThan(GeneralConstants.MinValueForId,ErrorMessage = "Id is invalid")]
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [StringLength(70, ErrorMessage = "Max Name Length is 70")]
    public string Name{ get; set; }
    
    [StringLength(200, ErrorMessage = "Max Description Length is 200")]
    public string? Description{ get; set; }

    [Required(ErrorMessage = "Status is required")]
    [ValidStatus(ErrorMessage = "Invalid status")]
    public Status Status{ get; set; }
    
    [ValidDateForTask(ErrorMessage = "Due date should be within the allowed range.")]
    public DateTime? DueDate{ get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.DAL.Enums;

namespace ToDoApp.DAL.Entities;

[Table("Tasks")]
public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }
    
    [Required]
    [MaxLength(70)]
    public string? Name{ get; set; }
    
    [MaxLength(200)]
    public string? Description{ get; set; }
    
    [Required]
    public Status Status{ get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate{ get; set; }
}
using System.ComponentModel.DataAnnotations;
using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.Attributes.Task;

public class ValidStatusAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return false;
        }
        
        return Enum.IsDefined(typeof(Status), value);
    }
}
using System.ComponentModel.DataAnnotations;
using ToDoApp.BLL.Constants;

namespace ToDoApp.BLL.Attributes.Task;

public class ValidDateForTaskAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }
        
        if (value is DateTime dateTime)
        {
            var maxDate = DateTime.Now.Date.AddYears(TaskConstants.MaxNumberOfYearsForTaskFromToday);
            var minDate = DateTime.Now.Date.AddYears(-TaskConstants.MinNumberOfYearsForTaskFromToday);
            return (dateTime.Date >= maxDate && dateTime.Date <= minDate);
        }
        
        return false;
    }
}
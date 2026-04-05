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
            dateTime = dateTime.ToUniversalTime();

            var today = DateTime.UtcNow.Date;

            var previousMonday = today.AddDays((int)DayOfWeek.Monday - (int)today.DayOfWeek);

            var minDate = previousMonday;
            var maxDate = today.AddDays(TaskConstants.NumberOfDaysAfterTodayYouCanPlanFor);

            return dateTime.Date >= minDate && dateTime.Date <= maxDate;
        }
    
        return false;
    }
}
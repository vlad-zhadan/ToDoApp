using ToDoApp.BLL.Attributes.Task;
using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.Tests;

public class ValidStatusAttributeTests
{
    [Fact]
    public void IsValid_ReturnsTrue_ForKnownStatus()
    {
        var attribute = new ValidStatusAttribute();

        var isValid = attribute.IsValid(Status.Done);

        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForUnknownStatusValue()
    {
        var attribute = new ValidStatusAttribute();

        var isValid = attribute.IsValid((Status)999);

        Assert.False(isValid);
    }
}

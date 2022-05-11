using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.Service.Validation;

namespace ToDo.Service.Tests.Validation;

[TestClass]
public class TaskValidatorResultTests
{
    [TestMethod]
    public void Success_IsSuccess()
    {
        Assert.IsNotNull(TaskValidatorResult.Success);
        Assert.IsTrue(TaskValidatorResult.Success.IsSuccess);
    }
}
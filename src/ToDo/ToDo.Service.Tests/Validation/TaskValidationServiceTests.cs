using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.Core;
using ToDo.Service.Validation;

namespace ToDo.Service.Tests.Validation
{
    [TestClass]
    public class TaskValidationServiceTests
    {
        [TestMethod]
        public void Validate_FulfilledTask_Success()
        {
            var taskValidationService = new TaskValidationService();
            var task = new TodoTask
            {
                Name = "To do"
            };

            var validationResult = taskValidationService.Validate(task);

            Assert.IsNotNull(validationResult);
            Assert.AreEqual(0, validationResult.Count);
        }

        [TestMethod]
        public void Validate_EmptyTask_Fail()
        {
            var taskValidationService = new TaskValidationService();
            var task = new TodoTask();

            var validationResult = taskValidationService.Validate(task);

            Assert.IsNotNull(validationResult);
            Assert.AreEqual(1, validationResult.Count);
        }
    }
}
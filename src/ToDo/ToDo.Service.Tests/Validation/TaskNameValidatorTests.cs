using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.Core;
using ToDo.Service.Validation;

namespace ToDo.Service.Tests.Validation
{
    [TestClass]
    public class TaskNameValidatorTests
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("\t")]
        public void Validate_EmptyName_Invalid(string taskName)
        {
            var taskNameValidator = new TaskNameValidator();
            var emptyNameTask = new TodoTask { Name = taskName };

            var validationResult = taskNameValidator.Validate(emptyNameTask);

            Assert.IsNotNull(validationResult);
            Assert.IsFalse(validationResult.IsSuccess);
            Assert.AreEqual("The field 'Name' should be filled", validationResult.Error);
        }

        [TestMethod]
        public void Validate_NormalName_Invalid()
        {
            var taskNameValidator = new TaskNameValidator();
            var filledNameTask = new TodoTask { Name = "To do something" };

            var validationResult = taskNameValidator.Validate(filledNameTask);

            Assert.AreSame(TaskValidatorResult.Success, validationResult);
        }

        [TestMethod]
        public void Validate_NullValue_Invalid()
        {
            var taskNameValidator = new TaskNameValidator();
            
            var validationResult = taskNameValidator.Validate(null);

            Assert.IsNotNull(validationResult);
            Assert.IsFalse(validationResult.IsSuccess);
            Assert.AreEqual("Task can not be empty", validationResult.Error);
        }
    }
}
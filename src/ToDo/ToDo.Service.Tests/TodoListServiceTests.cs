using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDo.Core;
using ToDo.Core.Extensions;
using ToDo.Data;
using ToDo.Service.Validation;

namespace ToDo.Service.Tests
{
    [TestClass]
    public class TodoListServiceTests
    {
        [TestMethod]
        public async Task GetAllTasksAsync_ReturnMockedList_Success()
        {
            ICollection<TodoTask> mockList = new[] { new TodoTask() };
            var repository = new Mock<IRepository<TodoTask>>();
            repository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(mockList));
            var validation = new Mock<ITaskValidationService>();

            var todoListService = new TodoListService(repository.Object, validation.Object);

            var allTasks = await todoListService.GetAllTasksAsync();

            Assert.AreSame(mockList, allTasks);
        }

        [TestMethod]
        public async Task AddTaskAsync_VerifiedTask_Success()
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask();
            repository.Setup(x => x.AddAsync(targetTask));
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(Array.Empty<TaskValidatorResult>());

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.AddTaskAsync(targetTask);

            validation.Verify(x => x.Validate(targetTask), Times.Once);
            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.AddAsync(targetTask), Times.Once);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Category B")]
        public async Task AddTaskAsync_TaskHasHighPriority_CategoryWasSetToC(string category)
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask
            {
                Priority = 1,
                Category = category.IsNullOrEmpty() 
                    ? null 
                    : new Taxonomy { Name = category }
            };
            repository.Setup(x => x.AddAsync(targetTask));
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(Array.Empty<TaskValidatorResult>());

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.AddTaskAsync(targetTask);

            Assert.IsNotNull(targetTask.Category);
            Assert.AreSame(TaxonomyRepository.CategoryC, targetTask.Category);
            validation.Verify(x => x.Validate(targetTask), Times.Once);
            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.AddAsync(targetTask), Times.Once);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("Category B")]
        public async Task AddTaskAsync_TaskHasNotHighPriority_CategoryWasTheSameAsInput(string category)
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var taskCategory = category.IsNullOrEmpty()
                ? null
                : new Taxonomy { Name = category };
            var targetTask = new TodoTask
            {
                Priority = 2,
                Category = taskCategory
            };
            repository.Setup(x => x.AddAsync(targetTask));
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(Array.Empty<TaskValidatorResult>());

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.AddTaskAsync(targetTask);
            
            //Category was not changed
            Assert.AreSame(taskCategory, targetTask.Category);
            validation.Verify(x => x.Validate(targetTask), Times.Once);
            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.AddAsync(targetTask), Times.Once);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddTaskAsync_FailedTask_TaskNotSaved()
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask();
            repository.Setup(x => x.AddAsync(It.IsAny<TodoTask>()));
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(new[] { new TaskValidatorResult("An error") });

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.AddTaskAsync(targetTask);

            validation.Verify(x => x.Validate(targetTask), Times.Once);
            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.AddAsync(It.IsAny<TodoTask>()), Times.Never);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task UpdateTaskAsync_VerifiedTask_Success()
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask();
            repository.Setup(x => x.UpdateAsync(targetTask));
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(Array.Empty<TaskValidatorResult>());

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.UpdateTaskAsync(targetTask);

            validation.Verify(x => x.Validate(targetTask), Times.Once);
            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.UpdateAsync(targetTask), Times.Once);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task UpdateTaskAsync_FailedTask_TaskNotSaved()
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask();
            repository.Setup(x => x.AddAsync(It.IsAny<TodoTask>()));
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(new[] { new TaskValidatorResult("An error") });

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.UpdateTaskAsync(targetTask);

            validation.Verify(x => x.Validate(targetTask), Times.Once);
            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.UpdateAsync(It.IsAny<TodoTask>()), Times.Never);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task Delete_CompletedTask_TaskDeleted()
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask { Id = Guid.NewGuid(), Status = TodoTaskStatus.Completed };
            repository.Setup(x => x.DeleteAsync(targetTask));
            repository.Setup(x => x.GetByIdAsync(targetTask.Id)).Returns(Task.FromResult(targetTask)!);
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(Array.Empty<TaskValidatorResult>());

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.DeleteTaskAsync(targetTask.Id);

            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.GetByIdAsync(targetTask.Id), Times.Once);
            repository.Verify(x => x.DeleteAsync(targetTask), Times.Once);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        [DataRow(TodoTaskStatus.InProgress)]
        [DataRow(TodoTaskStatus.NotStarted)]
        public async Task Delete_CompletedTask_TaskStillExist(TodoTaskStatus taskStatus)
        {
            var repository = new Mock<IRepository<TodoTask>>();
            var targetTask = new TodoTask { Status = taskStatus, Id = Guid.NewGuid() };
            repository.Setup(x => x.DeleteAsync(targetTask));
            repository.Setup(x => x.GetByIdAsync(targetTask.Id)).Returns(Task.FromResult(targetTask)!);
            var validation = new Mock<ITaskValidationService>();
            validation.Setup(x => x.Validate(targetTask)).Returns(Array.Empty<TaskValidatorResult>());

            var todoListService = new TodoListService(repository.Object, validation.Object);

            await todoListService.DeleteTaskAsync(targetTask.Id);

            validation.VerifyNoOtherCalls();
            repository.Verify(x => x.GetByIdAsync(targetTask.Id), Times.Once);
            repository.VerifyNoOtherCalls();
        }
    }
}
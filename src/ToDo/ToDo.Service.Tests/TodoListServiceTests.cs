using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDo.Core;
using ToDo.Data;

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
            
            var todoListService = new TodoListService(repository.Object);

            var allTasks = await todoListService.GetAllTasksAsync();

            Assert.AreSame(mockList, allTasks);
        }
    }
}
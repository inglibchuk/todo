using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDo.Data.Tests
{
    [TestClass]
    public class JustInMemoryRepositoryTests
    {
        [TestMethod]
        public async Task GetAllAsync_FirstRequest_EmptyCollection()
        {
            var repository = new JustInMemoryRepository<object>();

            var result = await repository.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
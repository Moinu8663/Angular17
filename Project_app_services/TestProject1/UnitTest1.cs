using ContectUs.Controllers;
using ContectUs.Model;
using ContectUs.Repository;
using ContectUs.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestProject1
{
    public class Tests
    {
        
        private Mock<ContectContext> mockContext;
        private Repo repo;
        private Mock<IRepo> mockRepo;
        private Service service;
        private Mock<IService> mockService;
        private ContectUsController controller;
        [SetUp]
        public void Setup()
        {
            // Set up mock ContectContext using an in-memory database
            var options = new DbContextOptionsBuilder<ContectContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            mockContext = new Mock<ContectContext>(options);
            repo = new Repo(mockContext.Object);

            mockRepo = new Mock<IRepo>();
            service = new Service(mockRepo.Object);

            mockService = new Mock<IService>();
            controller = new ContectUsController(mockService.Object);
        }
        // test case for controller.
        [Test]
        public void Get_ReturnsOkWithContects()
        {
            // Arrange
            var expectedContects = new List<Contect> 
            { new Contect { Id = 1, Name = "John Doe", Message = "Hello" } };
            mockService.Setup(s => s.GetAll()).Returns(expectedContects);

            // Act
            IActionResult result = controller.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedContects, okResult.Value);
        }
        [Test]
        public void Post_ReturnsCreated()
        {
            // Arrange
            var contect = new Contect { Id = 1, Name = "Jane Smith", Message = "Hi there" };

            // Act
            IActionResult result = controller.Post(contect);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(201, objectResult.StatusCode);
            Assert.AreEqual("New contect Added", objectResult.Value);
            mockService.Verify(s => s.AddContect(It.IsAny<Contect>()), Times.Once);
        }
        // test case for repo.
        [Test]
        public void AddContect_AddsContectToContext()
        {
            // Arrange
            var contect = new Contect { Id = 1, Name = "John Doe", Message = "Hello" };

            // Act
            repo.AddContect(contect);

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            mockContext.Verify(c => c.contects.Add(It.IsAny<Contect>()), Times.Once);
        }

        [Test]
        public void GetAll_ReturnsAllContects()
        {
            // Arrange
            var expectedContects = new List<Contect>
            {
                new Contect { Id = 1, Name = "John Doe", Message = "Hello" },
                new Contect { Id = 2, Name = "Jane Smith", Message = "Hi there" }
            };
            var mockSet = new Mock<DbSet<Contect>>();
            mockSet.As<IQueryable<Contect>>().Setup(m => m.Provider).Returns(expectedContects.AsQueryable().Provider);
            mockSet.As<IQueryable<Contect>>().Setup(m => m.Expression).Returns(expectedContects.AsQueryable().Expression);
            mockSet.As<IQueryable<Contect>>().Setup(m => m.ElementType).Returns(expectedContects.AsQueryable().ElementType);
            mockSet.As<IQueryable<Contect>>().Setup(m => m.GetEnumerator()).Returns(expectedContects.AsQueryable().GetEnumerator());
            mockContext.Setup(c => c.contects).Returns(mockSet.Object);

            // Act
            var result = repo.GetAll();

            // Assert
            Assert.AreEqual(expectedContects.Count, result.Count);
            Assert.AreEqual(expectedContects, result);
        }

        [Test]
        public void AddContect_CallsRepoAddContect()
        {
            // Arrange
            var contect = new Contect { Id = 1, Name = "John Doe", Message = "Hello" };

            // Act
            service.AddContect(contect);

            // Assert
            mockRepo.Verify(r => r.AddContect(It.IsAny<Contect>()), Times.Once);
        }

        [Test]
        public void GetAll_ReturnsAllContectsFromRepo()
        {
            // Arrange
            var expectedContects = new List<Contect>
            {
                new Contect { Id = 1, Name = "John Doe", Message = "Hello" },
                new Contect { Id = 2, Name = "Jane Smith", Message = "Hi there" }
            };
            mockRepo.Setup(r => r.GetAll()).Returns(expectedContects);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.AreEqual(expectedContects.Count, result.Count);
            Assert.AreEqual(expectedContects, result);
        }
    }
}
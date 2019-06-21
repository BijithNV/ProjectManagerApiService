using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagerApiService.API.Controllers;
using ProjectManagerBusinessLayer;
using ProjectManagerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManager.API.Tests
{
    public class ProjectControllerTest
    {
        private readonly Mock<IProjectMangerBL<Project>> projectBL;
        private readonly ProjectController projectController;

        public ProjectControllerTest()
        {
            projectBL = new Mock<IProjectMangerBL<Project>>();
            projectController = new ProjectController(projectBL.Object);
        }

        [Fact]
        public void GetAll()
        {
            // Arrange
            projectBL.Setup(m => m.RetrieveAllData()).Returns(GetTestProjects());
            // Act
            var result = projectController.Get();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Search()
        {
            // Arrange
            projectBL.Setup(m => m.SearchByKey("Test")).Returns(GetTestProjects());
            // Act
            var result = projectController.Search("Test");
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Post()
        {
            // Arrange
            var project = GetTestProjects().First();
            projectBL.Setup(m => m.CreateNew(project));
            // Act
            var result = projectController.Post(project) as OkResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Put()
        {
            // Arrange
            var project = GetTestProjects().First();
            projectBL.Setup(m => m.Update(project));
            // Act
            var result = projectController.Put(project) as OkResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete()
        {
            // Arrange
            projectBL.Setup(m => m.Delete(It.IsAny<int>()));
            // Act
            var result = projectController.Delete(1) as OkResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ErrorResponse()
        {
            // Arrange
            projectBL.Setup(m => m.Delete(It.IsAny<int>()));
            // Act
            var result = projectController.Delete(0) as BadRequestResult;
            // Assert
            Assert.NotNull(result);
        }

        private IEnumerable<Project> GetTestProjects()
        {
            return new[]
            {
                new Project{ ProjectName="Test project", Priority=10, StartDate = DateTime.Now.Date,  EndDate = DateTime.Now.AddDays(10).Date },
                new Project{ ProjectName="Test project1", Priority=15, StartDate = DateTime.Now.Date,  EndDate = DateTime.Now.AddDays(5).Date }
            };
        }
    }
}

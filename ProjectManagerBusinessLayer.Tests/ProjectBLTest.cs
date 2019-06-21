
using ProjectManagerEntities;
using System;
using System.Linq;
using Xunit;

namespace ProjectManagerBusinessLayer.Tests
{
    public class ProjectBLTest : IClassFixture<DbContextFixture>
    {
        private readonly ProjectBL projectBL;
        public DbContextFixture _dbfixture;
        public ProjectBLTest(DbContextFixture dbFixture)
        {
            _dbfixture = dbFixture;
            projectBL = new ProjectBL(_dbfixture.dbContext);
        }

        [Fact]
        public void GetAll()
        {
            var projects = projectBL.RetrieveAllData();

            Assert.NotNull(projects);
        }

        [Fact]
        public void SearchProjects()
        {
            var projects = projectBL.SearchByKey("Test project");

            Assert.NotNull(projects);
        }

        [Fact]
        public void Add()
        {
            var id = projectBL.CreateNew(new Project
            {
                ProjectName = "Test project2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            });

            Assert.True(id > 0);
        }

        [Fact]
        public void Update()
        {
            var project = projectBL.SearchByKey("Test project").First();
            project.Priority = 10;

            project = projectBL.Update(project);

            Assert.True(project.Priority == 10);
        }

        [Fact]
        public void Delete()
        {
            var id = projectBL.CreateNew(new Project
            {
                ProjectName = "Test project",
                ManagerId = 1
            });

            projectBL.Delete(id);

            var project = projectBL.RetrieveAllData().FirstOrDefault(m => m.ProjectId == id);

            Assert.Null(project);
        }
    }
}

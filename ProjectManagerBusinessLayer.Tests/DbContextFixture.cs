using Microsoft.EntityFrameworkCore;
using ProjectManagerDAL;
using ProjectManagerEntities;
using System;

namespace ProjectManagerBusinessLayer.Tests
{
    public class DbContextFixture : IDisposable
    {
        public IProjectManagerContext dbContext;
        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ProjectManagerContext>()
                .UseInMemoryDatabase("BijithFinalSBA")
                .Options;

            dbContext = new ProjectManagerContext(options);

            dbContext.Tasks.Add(new Task { TaskName = "Test task", Priority = 10, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.AddDays(10).Date });
            dbContext.Users.Add(new User { EmployeeId = "1234", FirstName = "Test user", LastName = "Test" });
            dbContext.Projects.Add(new Project { ProjectName = "Test project", Priority = 10, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.AddDays(10).Date, ManagerId = 1 });           

            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext = null;
        }
    }
}


using ProjectManagerEntities;
using System;
using Xunit;

namespace ProjectManagerBusinessLayer.Tests
{
    public class TaskBLTest : IClassFixture<DbContextFixture>
    {
        private readonly TaskBL taskBL;
        public DbContextFixture _dbfixture;
        public TaskBLTest(DbContextFixture dbFixture)
        {
            _dbfixture = dbFixture;
            taskBL = new TaskBL(_dbfixture.dbContext);
        }

        [Fact]
        public void GetAll()
        {
            var tasks = taskBL.RetriveAllTasks();

            Assert.NotNull(tasks);
        }

        [Fact]
        public void GetParentTasks()
        {
            var taskLookups = taskBL.RetriveAllParentTasks();

            Assert.NotNull(taskLookups);
        }

        [Fact]
        public void GetTaskByProjectId()
        {
            var taskLookups = taskBL.RetriveTasksByProjectId(1);

            Assert.NotNull(taskLookups);
        }

        [Fact]
        public void GetById()
        {
            var task = taskBL.SearchTaskById(1);

            Assert.NotNull(task);
        }

        [Fact]
        public void Add()
        {
            var id = taskBL.CreateNewTask(new Task
            {
                TaskName = "Test task2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ProjectId = 1
            });

            Assert.True(id > 0);
        }

        [Fact]
        public void Update()
        {
            var newTitle = "Test task1";

            var task = GetTestTask();
            task.TaskName = newTitle;

            taskBL.UpdateTask(task);

            task = taskBL.SearchTaskById(1);

            Assert.True(task.TaskName == newTitle);
        }

        [Fact]
        public void End()
        {
            taskBL.CloseTask(1);

            var task = taskBL.SearchTaskById(1);

            Assert.True(task.IsCompleted);
        }

        private Task GetTestTask()
        {
            return new Task
            {
                TaskId = 1,
                TaskName = "Test task",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using ProjectManagerDAL;
using ProjectManagerEntities;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagerBusinessLayer
{
    public class TaskBL : ITaskBL
    {
        private readonly IProjectManagerContext _dataContext;

        public TaskBL(IProjectManagerContext context)
        {
            _dataContext = context;
        }

        public IEnumerable<Task> RetriveAllTasks()
        {
            return _dataContext.Tasks.ToList();
        }

        public IEnumerable<Task> RetriveAllParentTasks()
        {
            var parentTaskList= _dataContext.Tasks
                .Where(row => row.IsParentTask)
                .ToList();
            return parentTaskList;
        }

        public IEnumerable<Task> RetriveTasksByProjectId(int projectId)
        {
            return _dataContext.Tasks.Include(row => row.Parent)
                        .Where(row => row.ProjectId == projectId)
                        .ToList();
        }

        public Task SearchTaskById(int taskId)
        {
            var task = _dataContext.Tasks.Include(row => row.User).Include(row => row.Project)
                .Include(row => row.Parent)
                .FirstOrDefault(row => row.TaskId == taskId);

            if (task.Project != null)
            {
                task.Project.Tasks = null;
            }
            return task;
        }

        public int CreateNewTask(Task taskDetails)
        {
            _dataContext.Tasks.Add(taskDetails);
            _dataContext.SaveChanges();
            return taskDetails.TaskId;
        }

        public void UpdateTask(Task taskDetails)
        {
            var task = _dataContext.Tasks.FirstOrDefault(m => m.TaskId == taskDetails.TaskId);

            task.TaskName = taskDetails.TaskName;
            task.Priority = taskDetails.Priority;
            task.StartDate = taskDetails.StartDate;
            task.EndDate = taskDetails.EndDate;
            task.ParentId = taskDetails.ParentId;
            task.UserId = taskDetails.UserId;
            task.ProjectId = taskDetails.ProjectId;

            _dataContext.SaveChanges();
        }

        public void CloseTask(int taskId)
        {
            var task = _dataContext.Tasks.FirstOrDefault(row => row.TaskId == taskId);
            task.IsCompleted = true;

            _dataContext.SaveChanges();
        }
    }
}

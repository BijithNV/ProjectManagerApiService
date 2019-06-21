using System.Collections.Generic;
using ProjectManagerEntities;

namespace ProjectManagerBusinessLayer
{
    public interface ITaskBL
    {
        int CreateNewTask(Task taskDetails);
        void CloseTask(int taskId);
        IEnumerable<Task> RetriveAllTasks();
        IEnumerable<Task> RetriveAllParentTasks();
        IEnumerable<Task> RetriveTasksByProjectId(int projectId);
        Task SearchTaskById(int taskId);
        void UpdateTask(Task item);
    }
}
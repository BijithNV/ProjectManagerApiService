using Microsoft.EntityFrameworkCore;
using ProjectManagerDAL;
using ProjectManagerEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerBusinessLayer
{
    public class ProjectBL : IProjectMangerBL<Project>
    {
        private readonly IProjectManagerContext _dataContext;

        public ProjectBL(IProjectManagerContext context)
        {
            _dataContext = context;
        }

        public int CreateNew(Project objDetails)
        {
            _dataContext.Projects.Add(objDetails);
            _dataContext.SaveChanges();
            return objDetails.ProjectId;
        }

        public void Delete(int Id)
        {
            var project = _dataContext.Projects.FirstOrDefault(row => row.ProjectId == Id);
            _dataContext.Projects.Remove(project);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Project> RetrieveAllData()
        {
            var allprojectDetails = _dataContext.Projects.Include(row => row.Tasks).Include(row => row.Manager)
                .Select(row => new Project
                {
                    ProjectId = row.ProjectId,
                    ProjectName = row.ProjectName,
                    StartDate = row.StartDate,
                    EndDate = row.EndDate,
                    ManagerId = row.ManagerId,
                    Manager = row.Manager,
                    Priority = row.Priority,
                    Tasks = row.Tasks.Select(task => new Task
                    {
                        TaskId = task.TaskId,
                        IsCompleted = task.IsCompleted
                    }).ToList()
                })
                .ToList();
            return allprojectDetails;
        }

        public IEnumerable<Project> SearchByKey(string searchText)
        {
            var filteredProjects = _dataContext.Projects
                                .Where(row =>
                                    string.IsNullOrEmpty(searchText)
                                    || row.ProjectName.Contains(searchText))
                                .ToList();
            //.ToList();
            return filteredProjects;
        }

        public Project Update(Project objDetails)
        {
            _dataContext.Projects.Update(objDetails);
            _dataContext.SaveChanges();
            return objDetails;
        }

        public Project GetById(int projectId)
        {
            var projectdetails = _dataContext.Projects.FirstOrDefault(row => row.ProjectId == projectId);
            return projectdetails;
        }

        //public IEnumerable<Project> RetriveAllProjects()
        //{
        //    var allprojectDetails = _dataContext.Projects.Include(row => row.Tasks).Include(row => row.Manager)
        //        .Select(row => new Project
        //        {
        //            ProjectId = row.ProjectId,
        //            ProjectName = row.ProjectName,
        //            StartDate = row.StartDate,
        //            EndDate = row.EndDate,
        //            ManagerId = row.ManagerId,
        //            Manager = row.Manager,
        //            Priority = row.Priority,
        //            Tasks = row.Tasks.Select(task => new Task
        //            {
        //                TaskId = task.TaskId,
        //                IsCompleted = task.IsCompleted
        //            }).ToList()
        //        })
        //        .ToList();
        //    return allprojectDetails;
        //}

        //public IEnumerable<Project> SearchProjects(string searchText)
        //{
        //    var filteredProjects = _dataContext.Projects
        //                        .Where(row =>
        //                            string.IsNullOrEmpty(searchText)
        //                            || row.ProjectName.Contains(searchText))
        //                        .ToList();
        //    //.ToList();
        //    return filteredProjects;
        //}


        //public int CreateNewProject(Project projectDetails)
        //{
        //    _dataContext.Projects.Add(projectDetails);
        //    _dataContext.SaveChanges();
        //    return projectDetails.ProjectId;
        //}

        //public Project UpdateProject(Project projectDetails)
        //{
        //    _dataContext.Projects.Update(projectDetails);
        //    _dataContext.SaveChanges();
        //    return projectDetails;
        //}

        //public void DeleteProject(int projectId)
        //{
        //    var project = _dataContext.Projects.FirstOrDefault(row => row.ProjectId == projectId);
        //    _dataContext.Projects.Remove(project);
        //    _dataContext.SaveChanges();
        //}
    }
}

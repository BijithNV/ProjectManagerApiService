using Microsoft.EntityFrameworkCore;
using ProjectManagerEntities;

namespace ProjectManagerDAL
{
    public interface IProjectManagerContext 
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
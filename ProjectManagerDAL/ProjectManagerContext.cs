using Microsoft.EntityFrameworkCore;
using ProjectManagerEntities;

namespace ProjectManagerDAL
{
    public class ProjectManagerContext : DbContext, IProjectManagerContext
    {
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options)
            : base(options)
        {            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }        

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

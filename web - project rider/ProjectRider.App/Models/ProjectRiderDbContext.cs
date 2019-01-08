namespace ProjectRider.App.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ProjectRiderDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public ProjectRiderDbContext(DbContextOptions<ProjectRiderDbContext> options) : base(options)
        {
            this.InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            this.Database.EnsureCreated();
        }
    }
}
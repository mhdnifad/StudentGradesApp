using Microsoft.EntityFrameworkCore;
using StudentGrades.Models;

namespace StudentGrades.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // optional: configure table names to match "mst_student" / "mst_subject"
            modelBuilder.Entity<Student>().ToTable("mst_student");
            modelBuilder.Entity<Subject>().ToTable("mst_subject");
        }
    }
}

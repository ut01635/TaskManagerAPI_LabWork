using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ONE to ONE
            modelBuilder.Entity<User>()
                .HasOne(a => a.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(a => a.UserId);

            //ONE to Many
            modelBuilder.Entity<User>()
                .HasMany(t => t.Tasks)
                .WithOne(u => u.User)
                .HasForeignKey(t => t.AssigneeId);
                
            base.OnModelCreating(modelBuilder);
        }
    }

}

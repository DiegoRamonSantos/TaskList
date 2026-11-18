using Microsoft.EntityFrameworkCore;
using Tasklist.Models;

namespace Tasklist.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("TaskItems");

            entity.HasKey(task => task.Id);

            entity.Property(task => task.Title)
                .IsRequired()
                .HasMaxLength(140);

            entity.Property(task => task.Description)
                .HasMaxLength(1000);

            entity.Property(task => task.IsCompleted)
                .HasDefaultValue(false);

            entity.Property(task => task.Priority)
                .HasConversion<int>();

            entity.Property(task => task.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasIndex(task => task.CreatedAt);
            entity.HasIndex(task => task.DueDate);
            entity.HasIndex(task => task.Priority);
            entity.HasIndex(task => task.IsCompleted);
        });
    }
}

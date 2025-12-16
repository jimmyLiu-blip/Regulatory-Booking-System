using Microsoft.EntityFrameworkCore;
using RFScheduling.Domain;

namespace RFScheduling.Infrastructure
{
    public class AppDbContext : DbContext
    {
        // 「外面會把連線設定、Provider（SQL Server）、其他 EF 設定，包成 options 丟進來」，把設定交給 EF Core 的 DbContext 爸爸去初始化
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Project> Projects => Set<Project>();

        public DbSet<Schedule> Schedules => Set<Schedule>();

        public DbSet<Resource> Resources => Set<Resource>();

        public DbSet<Regulation> Regulations => Set<Regulation>();

        public DbSet<TestItem>  TestItems => Set<TestItem>();

        public DbSet<TestLog> TestLogs => Set<TestLog>();

        public DbSet<EstimateHistory> EstimateHistories => Set<EstimateHistory>();

        public DbSet<ProjectTestRecord> ProjectTestRecords => Set<ProjectTestRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // (A) 主檔：Role ↔ User (1:N)
            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // (B) 主檔：Regulation/TestItem ↔ Project (1:N)
            modelBuilder.Entity<Project>()
                .HasOne<Regulation>()
                .WithMany()
                .HasForeignKey(p => p.RegulationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<TestItem>()
                .WithMany()
                .HasForeignKey(p => p.TestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // (C) User ↔ Project (CreatedBy / ModifiedBy) (1:N, 多個 FK 指向同一張表)
            modelBuilder.Entity<Project>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // (D) Project ↔ ProjectTestRecord (1 ↔ 1)
            modelBuilder.Entity<ProjectTestRecord>()
                .HasOne<Project>()
                .WithOne()
                .HasForeignKey<ProjectTestRecord>(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTestRecord>()
                .HasIndex(pt => pt.ProjectId)
                .IsUnique();

            // (E) Project ↔ Schedule(1 ↔ N)
            modelBuilder.Entity<Schedule>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // (F) Resource ↔ Schedule
            modelBuilder.Entity<Schedule>()
                .HasOne<Resource>()
                .WithMany()
                .HasForeignKey(s => s.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            // (G) User ↔ Schedule (CreatedBy / ModifiedBy / EngineerId) (1:N, 多個 FK 指向同一張表) 
            modelBuilder.Entity<Schedule>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

            // (H) User ↔ EstimateHistory 
            modelBuilder.Entity<EstimateHistory>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(eh => eh.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // (I) User ↔ TestLog
            modelBuilder.Entity<TestLog>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // (J) Schedule ↔ TestLog
            modelBuilder.Entity<TestLog>()
                .HasOne<Schedule>()
                .WithMany()
                .HasForeignKey(t => t.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            // (K) Schedule ↔ EstimateHistory 
            modelBuilder.Entity<EstimateHistory>()
                .HasOne<Schedule>()
                .WithMany()
                .HasForeignKey(eh => eh.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

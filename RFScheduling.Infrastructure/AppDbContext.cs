using Microsoft.EntityFrameworkCore;
using RFScheduling.Domain;
using System.Configuration;

namespace RFScheduling.Infrastructure
{
    public class AppDbContext : DbContext
    {
        // 「外面會把連線設定、Provider（SQL Server）、其他 EF 設定，包成 options 丟進來」，把設定交給 EF Core 的 DbContext 爸爸去初始化
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        // 給 WinForms / 測試 / 直接 new 用
        public AppDbContext()
        {
        }

        // 給App.config連線
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    ?.ConnectionString;

                optionsBuilder.UseSqlServer (connectionString);
            }
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

        public DbSet<ActualTestRecord> ActualTestRecords => Set<ActualTestRecord>();

        public DbSet<ProjectTestItem> ProjectTestItems => Set<ProjectTestItem>();

        public DbSet<ProjectRegulation> ProjectRegulations => Set<ProjectRegulation>();

        public DbSet<ScheduleEngineer> ScheduleEngineers => Set<ScheduleEngineer>();

        public DbSet<ResourceEngineer> ResourceEngineers => Set<ResourceEngineer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // (A) 主檔：Role ↔ User (1:N)
            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(u => u.RoleId)
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

            // (D) ProjectTestItem ↔ ActualTestRecord (1 ↔ 1)
            modelBuilder.Entity<ActualTestRecord>()
                .HasOne<ProjectTestItem>()
                .WithOne()
                .HasForeignKey<ActualTestRecord>(pt => pt.ProjectTestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActualTestRecord>()
                .HasIndex(pt => pt.ProjectTestItemId)
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

            // (L) ProjectTestItem ↔ Schedule (1:N)
            modelBuilder.Entity<Schedule>()
                .HasOne<ProjectTestItem>()
                .WithMany()
                .HasForeignKey(s => s.ProjectTestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // (M) ResourceEngineer
            modelBuilder.Entity<ResourceEngineer>()
                .HasOne<Resource>()
                .WithMany()
                .HasForeignKey(x => x.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResourceEngineer>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

            // (N) ScheduleEngineer
            modelBuilder.Entity<ScheduleEngineer>()
                .HasOne<Schedule>()
                .WithMany()
                .HasForeignKey(x => x.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScheduleEngineer>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduleEngineer>()
                .HasIndex(x => new { x.ScheduleId, x.EngineerId })
                .IsUnique();

            // (O) ProjectTestItem
            modelBuilder.Entity<ProjectTestItem>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTestItem>()
                .HasOne<TestItem>()
                .WithMany()
                .HasForeignKey(x => x.TestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTestItem>()
                .HasCheckConstraint(
                    "CK_ProjectTestItem_Other",
                    "([TestItemId] IS NOT NULL AND [OtherTestItemText] IS NULL) OR " +
                    "([TestItemId] IS NULL AND [OtherTestItemText] IS NOT NULL)"
                );

            // (P) ProjectRegulation
            modelBuilder.Entity<ProjectRegulation>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectRegulation>()
                .HasOne<Regulation>()
                .WithMany()
                .HasForeignKey(x => x.RegulationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectRegulation>()
                .HasCheckConstraint(
                    "CK_ProjectRegulation_Other",
                    "([RegulationId] IS NOT NULL AND [OtherRegulationText] IS NULL) OR " +
                    "([RegulationId] IS NULL AND [OtherRegulationText] IS NOT NULL)"
    );

        }
    }
}

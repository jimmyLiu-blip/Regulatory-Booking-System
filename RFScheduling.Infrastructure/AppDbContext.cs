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

        public DbSet<Resource> Resources => Set<Resource>();

        public DbSet<ResourceEngineer> ResourceEngineers => Set<ResourceEngineer>();

        public DbSet<Project> Projects => Set<Project>();

        public DbSet<ProjectRegulation> ProjectRegulations => Set<ProjectRegulation>();

        public DbSet<ProjectTestItem> ProjectTestItems => Set<ProjectTestItem>();

        public DbSet<Schedule> Schedules => Set<Schedule>();

        public DbSet<ScheduleEngineer> ScheduleEngineers => Set<ScheduleEngineer>();

        public DbSet<TestLog> TestLogs => Set<TestLog>();

        public DbSet<ActualTestRecord> ActualTestRecords => Set<ActualTestRecord>();

        public DbSet<EstimateHistory> EstimateHistories => Set<EstimateHistory>();

        public DbSet<Regulation> Regulations => Set<Regulation>();

        public DbSet<TestItem>  TestItems => Set<TestItem>();

        public DbSet<ReviewRecord> ReviewRecords => Set<ReviewRecord>();

        public DbSet<ProgressReport> ProgressReports => Set<ProgressReport>();

        public DbSet<RegulationTestItem> RegulationTestItems => Set<RegulationTestItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // (A) 主檔：Role ↔ User (1:N)
            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // (B) User ↔ Project (CreatedBy / ModifiedBy) (1:N)
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

            // (C) Project / TestItem (1) → ProjectTestItem (N)
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

            // (D) Project / Regulation (1) → ProjectRegulation (N)
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

            // (E) Project / Resource / ProjectTestItem / User(1) → Schedule (N)
            modelBuilder.Entity<Schedule>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne<Resource>()
                .WithMany()
                .HasForeignKey(s => s.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .HasOne<ProjectTestItem>()
                .WithMany()
                .HasForeignKey(s => s.ProjectTestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // (F) Resource / User (Engineer) → ResourceEngineer (N)
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

            // (G) Schedule / User (Engineer) (1) → ScheduleEngineer (N)
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

            // (H) Schedule / User (1) → TestLog (N)
            modelBuilder.Entity<TestLog>()
                .HasOne<Schedule>()
                .WithMany()
                .HasForeignKey(t => t.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TestLog>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // (I) Schedule / User (1) → EstimateHistory (N)
            modelBuilder.Entity<EstimateHistory>()
                .HasOne<Schedule>()
                .WithMany()
                .HasForeignKey(eh => eh.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateHistory>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(eh => eh.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // (J) ProjectTestItem ↔ ActualTestRecord (1 ↔ 1)
            modelBuilder.Entity<ActualTestRecord>()
                .HasOne<ProjectTestItem>()
                .WithOne()
                .HasForeignKey<ActualTestRecord>(pt => pt.ProjectTestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActualTestRecord>()
                .HasIndex(pt => pt.ProjectTestItemId)
                .IsUnique();

            // (K) Schedule / User (1) → ProgressReport (N)
            modelBuilder.Entity<ProgressReport>()
                .HasOne<Schedule>()
                .WithMany()
                .HasForeignKey(p => p.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProgressReport>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.ReportedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // (L) Project / ProjectTestItem / User (Reviewer) (1) → ReviewRecord (N)
            modelBuilder.Entity<ReviewRecord>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReviewRecord>()
                .HasOne<ProjectTestItem>()
                .WithMany()
                .HasForeignKey(r => r.ProjectTestItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReviewRecord>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.ReviewedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // (M) Regulation / TestItem (1) → RegulationTestItem (多)
            modelBuilder.Entity<RegulationTestItem>()
                .HasOne<Regulation>()
                .WithMany()
                .HasForeignKey(r => r.RegulationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegulationTestItem>()
                .HasOne<TestItem>()
                .WithMany()
                .HasForeignKey(r => r.TestItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

# 📘 RF 測試排程系統 - 系統設計文件 v1.0（實作修正版）

## 📋 文件資訊
- **版本**：v1.0（根據實際 Model 修正）
- **日期**：2025-12-18
- **狀態**：可直接作為實作藍本
- **技術棧**：WinForms + DevExpress v25.1 + EF Core 9.0 + SQL Server
- **🔮 標註說明**：標有 🔮 的項目為未來擴展保留點

---

## ⚠️ 重要修正說明

### 與原始 SD 的差異
1. ✅ **Project 主鍵改為 INT**（原 SD 使用 ProjectKey + ProjectCode 分離設計）
2. ✅ **移除 Schedule.EngineerId**（透過 ScheduleEngineer 管理）
3. ✅ **所有 FK 型別統一為 INT**（與實際 Model 一致）
4. ⚠️ **ScheduleEngineer.IsActive 保留**（可用於歷史追蹤）

---

## 🎯 設計目標與範圍

### v1.0 核心目標
1. 實現完整的案件生命週期：開案 → 派案 → 測試 → 送審 → 審查 → 完成
2. 支援 A/B 兩區獨立運作
3. 支援多場地同步測試（同一測項在不同場地）
4. 自動記錄測試實際時間（開始/暫停/續測/自動補時）
5. 提供預估 vs 實際的差異資料
6. 實現通知機制（Email）

### v1.0 不包含
- 🔮 權限代理機制（保留至 v1.1）
- 🔮 CSV 批次開案（保留至 v1.1）
- 🔮 甘特圖分析（保留至 v1.2）
- 🔮 逾期預警（保留至 v1.2）

---

## 2️⃣ 資料庫設計（已對齊實際 Model）

### 2.1 實體關係圖

```
User ───┐
        │
        ├─ Role (1:1)
        │
        ├─ Project (1:N, CreatedBy/ModifiedBy)
        │     │
        │     ├─ ProjectRegulation (1:N)
        │     │
        │     ├─ ProjectTestItem (1:N) ─── ActualTestRecord (1:1)
        │     │           │
        │     └─ Schedule (1:N)
        │               │
        │               ├─ ScheduleEngineer (N:M with User) ⭐
        │               ├─ TestLog (1:N)
        │               ├─ ProgressReport (1:N)
        │               └─ EstimateHistory (1:N)
        │
        ├─ Resource (1:N)
        │     │
        │     ├─ ResourceEngineer (N:M with User)
        │     └─ Schedule (1:N)
        │
        └─ ReviewRecord (N:1)
```

---

### 2.2 核心資料表設計（✅ 已對齊實際 Model）

#### 2.2.1 User
```sql
CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    RoleId INT NOT NULL,
    Area NVARCHAR(50), -- A區/B區，僅現場主管需要
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_User_Role FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

CREATE INDEX IX_User_RoleId ON [User](RoleId);
CREATE INDEX IX_User_Area ON [User](Area);
```

#### 2.2.2 Role
```sql
CREATE TABLE Role (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO Role (RoleName, Description) VALUES
('Manager', '排程主管'),
('FieldManager', '現場主管'),
('Engineer', '工程師'),
('Reviewer', '審查人員');
```

#### 2.2.3 Resource
```sql
CREATE TABLE Resource (
    ResourceId INT PRIMARY KEY IDENTITY,
    ResourceName NVARCHAR(100) NOT NULL,
    Area NVARCHAR(50) NOT NULL, -- A區/B區
    ResourceType NVARCHAR(50) NOT NULL, -- SAC/FAC/Conducted/Normal
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_Resource_Area ON Resource(Area);
CREATE INDEX IX_Resource_Type ON Resource(ResourceType);
```

#### 2.2.4 ResourceEngineer
```sql
CREATE TABLE ResourceEngineer (
    ResourceEngineerId INT PRIMARY KEY IDENTITY,
    ResourceId INT NOT NULL,
    EngineerId INT NOT NULL, -- ✅ 修正：改為 INT
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ResourceEngineer_Resource FOREIGN KEY (ResourceId) REFERENCES Resource(ResourceId),
    CONSTRAINT FK_ResourceEngineer_Engineer FOREIGN KEY (EngineerId) REFERENCES [User](UserId)
);

CREATE INDEX IX_ResourceEngineer_ResourceId ON ResourceEngineer(ResourceId);
CREATE INDEX IX_ResourceEngineer_EngineerId ON ResourceEngineer(EngineerId);
```

#### 2.2.5 Regulation
```sql
CREATE TABLE Regulation (
    RegulationId INT PRIMARY KEY IDENTITY,
    RegulationCode NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO Regulation (RegulationCode) VALUES
('FCC'), ('NCC'), ('CE'), ('TELEC'), ('IC'), ('Other');
```

#### 2.2.6 TestItem
```sql
CREATE TABLE TestItem (
    TestItemId INT PRIMARY KEY IDENTITY,
    TestItemName NVARCHAR(100) NOT NULL,
    TestItemType NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO TestItem (TestItemName, TestItemType) VALUES
('WWAN_Conducted', 'Conducted'),
('WIFI_Conducted', 'Conducted'),
('WWAN_Radiated', 'Radiated'),
('WIFI_Radiated', 'Radiated'),
('Adaptivity', 'Normal'),
('Receiver Blocking', 'Normal'),
('DFS', 'Normal'),
('Other', 'Other');
```

#### 2.2.7 Project ⭐ 關鍵修正
```sql
CREATE TABLE Project (
    ProjectId INT PRIMARY KEY IDENTITY, -- ✅ 主鍵改為 INT
    ProjectCode NVARCHAR(50) NOT NULL,  -- ✅ 業務編號（唯一）
    ProjectName NVARCHAR(200) NOT NULL,
    Priority NVARCHAR(20) NOT NULL DEFAULT 'P2',
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    Notes NVARCHAR(MAX),
    CreatedBy INT NOT NULL, -- ✅ 改為 INT
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy INT,         -- ✅ 改為 INT
    ModifiedDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Project_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserId),
    CONSTRAINT FK_Project_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES [User](UserId)
);

-- ✅ 業務編號唯一約束
CREATE UNIQUE INDEX UQ_Project_ProjectCode ON Project(ProjectCode) WHERE IsDeleted = 0;
CREATE INDEX IX_Project_Status ON Project(Status);
```

**狀態說明（由系統計算）**：
- `Pending` - 所有測項待派案
- `Scheduled` - 至少一個測項已排程
- `InProgress` - 至少一個測項測試中
- `UnderReview` - 所有測項待審查
- `Completed` - 所有測項已完成
- `Returned` - 至少一個測項被退回

#### 2.2.8 ProjectRegulation
```sql
CREATE TABLE ProjectRegulation (
    ProjectRegulationId INT PRIMARY KEY IDENTITY,
    ProjectId INT NOT NULL, -- ✅ 改為 INT
    RegulationId INT NULL,
    OtherRegulationText NVARCHAR(100),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ProjectRegulation_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectRegulation_Regulation FOREIGN KEY (RegulationId) REFERENCES Regulation(RegulationId)
);

CREATE INDEX IX_ProjectRegulation_ProjectId ON ProjectRegulation(ProjectId);
```

#### 2.2.9 ProjectTestItem ⭐ 主狀態機
```sql
CREATE TABLE ProjectTestItem (
    ProjectTestItemId INT PRIMARY KEY IDENTITY,
    ProjectId INT NOT NULL, -- ✅ 改為 INT
    TestItemId INT NULL,
    OtherTestItemText NVARCHAR(100),
    TestItemType NVARCHAR(50),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ProjectTestItem_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectTestItem_TestItem FOREIGN KEY (TestItemId) REFERENCES TestItem(TestItemId)
);

CREATE INDEX IX_ProjectTestItem_ProjectId ON ProjectTestItem(ProjectId);
CREATE INDEX IX_ProjectTestItem_Status ON ProjectTestItem(Status);
```

**狀態流轉（真正的主狀態機）**：
- `Pending` → `Scheduled` → `InProgress` → `UnderReview` → `Completed`
- `UnderReview` → `Returned` → `Scheduled`（退回重測）

#### 2.2.10 Schedule ⭐ 關鍵修正
```sql
CREATE TABLE Schedule (
    ScheduleId INT PRIMARY KEY IDENTITY,
    ProjectId INT NOT NULL,           -- ✅ 改為 INT
    ProjectTestItemId INT NOT NULL,
    ResourceId INT NOT NULL,
    ScheduleType NVARCHAR(50) NOT NULL DEFAULT 'Case',
    EstimatedStart DATETIME,
    EstimatedEnd DATETIME,
    OriginalEstimatedStart DATETIME,
    OriginalEstimatedEnd DATETIME,
    Status NVARCHAR(50) NOT NULL DEFAULT 'InQueue',
    Notes NVARCHAR(MAX),
    CreatedBy INT NOT NULL,           -- ✅ 改為 INT
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy INT,                   -- ✅ 改為 INT
    ModifiedDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0,
    -- ❌ 已移除 EngineerId（透過 ScheduleEngineer 管理）
    CONSTRAINT FK_Schedule_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_Schedule_ProjectTestItem FOREIGN KEY (ProjectTestItemId) REFERENCES ProjectTestItem(ProjectTestItemId),
    CONSTRAINT FK_Schedule_Resource FOREIGN KEY (ResourceId) REFERENCES Resource(ResourceId),
    CONSTRAINT FK_Schedule_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserId),
    CONSTRAINT FK_Schedule_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_Schedule_ProjectTestItemId ON Schedule(ProjectTestItemId);
CREATE INDEX IX_Schedule_ResourceId ON Schedule(ResourceId);
CREATE INDEX IX_Schedule_Status ON Schedule(Status);
```

**狀態說明（場地層級）**：
- `InQueue` - 待派案
- `Scheduled` - 已排程
- `InProgress` - 測試中
- `Paused` - 暫停中
- `Completed` - 該場地完成 ⚠️ 不等於測項完成
- `Review` - 送審

#### 2.2.11 ScheduleEngineer ⭐ 多工程師管理
```sql
CREATE TABLE ScheduleEngineer (
    ScheduleEngineerId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    EngineerId INT NOT NULL,          -- ✅ 改為 INT
    AssignedDate DATETIME NOT NULL DEFAULT GETDATE(),
    AssignedBy INT NOT NULL,          -- ✅ 改為 INT
    IsActive BIT NOT NULL DEFAULT 1,  -- 🔮 保留：未來可追蹤指派歷史
    CONSTRAINT FK_ScheduleEngineer_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId) ON DELETE CASCADE,
    CONSTRAINT FK_ScheduleEngineer_Engineer FOREIGN KEY (EngineerId) REFERENCES [User](UserId),
    CONSTRAINT FK_ScheduleEngineer_AssignedBy FOREIGN KEY (AssignedBy) REFERENCES [User](UserId),
    CONSTRAINT UQ_ScheduleEngineer UNIQUE (ScheduleId, EngineerId)
);

CREATE INDEX IX_ScheduleEngineer_ScheduleId ON ScheduleEngineer(ScheduleId);
CREATE INDEX IX_ScheduleEngineer_EngineerId ON ScheduleEngineer(EngineerId);
```

**🔮 IsActive 欄位說明**：
- v1.0：可保留（用於軟刪除）
- v1.1：若需追蹤「誰曾經被指派過」，此欄位很有用
- 建議：先保留，未來視需求決定是否使用

#### 2.2.12 TestLog
```sql
CREATE TABLE TestLog (
    TestLogId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    ActionType NVARCHAR(50) NOT NULL,
    ActionTime DATETIME NOT NULL DEFAULT GETDATE(),
    UserId INT NOT NULL,              -- ✅ 改為 INT
    Notes NVARCHAR(500),
    IsModifiable BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TestLog_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    CONSTRAINT FK_TestLog_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

CREATE INDEX IX_TestLog_ScheduleId ON TestLog(ScheduleId);
CREATE INDEX IX_TestLog_ActionTime ON TestLog(ActionTime);
```

#### 2.2.13 ActualTestRecord
```sql
CREATE TABLE ActualTestRecord (
    ActualTestRecordId INT PRIMARY KEY IDENTITY,
    ProjectTestItemId INT NOT NULL UNIQUE,
    ActualStart DATETIME,
    ActualEnd DATETIME,
    TotalDuration INT,
    PauseCount INT DEFAULT 0,
    LastCalculatedDate DATETIME,
    CONSTRAINT FK_ActualTestRecord_ProjectTestItem FOREIGN KEY (ProjectTestItemId) REFERENCES ProjectTestItem(ProjectTestItemId)
);

CREATE INDEX IX_ActualTestRecord_ProjectTestItemId ON ActualTestRecord(ProjectTestItemId);
```

#### 2.2.14 ProgressReport
```sql
CREATE TABLE ProgressReport (
    ProgressReportId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    ReportStatus NVARCHAR(50) NOT NULL,
    ReportMessage NVARCHAR(MAX) NOT NULL,
    ReportedBy INT NOT NULL,          -- ✅ 改為 INT
    ReportedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ProgressReport_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    CONSTRAINT FK_ProgressReport_User FOREIGN KEY (ReportedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_ProgressReport_ScheduleId ON ProgressReport(ScheduleId);
```

#### 2.2.15 EstimateHistory
```sql
CREATE TABLE EstimateHistory (
    EstimateHistoryId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    OldStart DATETIME,
    OldEnd DATETIME,
    NewStart DATETIME,
    NewEnd DATETIME,
    Reason NVARCHAR(500),
    ModifiedBy INT,                   -- ✅ 改為 INT
    ModifiedDate DATETIME,
    CONSTRAINT FK_EstimateHistory_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    CONSTRAINT FK_EstimateHistory_User FOREIGN KEY (ModifiedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_EstimateHistory_ScheduleId ON EstimateHistory(ScheduleId);
```

#### 2.2.16 ReviewRecord ⭐ 含審查輪次
```sql
CREATE TABLE ReviewRecord (
    ReviewRecordId INT PRIMARY KEY IDENTITY,
    ProjectId INT NOT NULL,           -- ✅ 改為 INT
    ProjectTestItemId INT NOT NULL,
    ReviewRound INT NOT NULL DEFAULT 1,
    ReviewResult NVARCHAR(50) NOT NULL,
    ReviewComment NVARCHAR(MAX),
    ReviewedBy INT NOT NULL,          -- ✅ 改為 INT
    ReviewedDate DATETIME NOT NULL DEFAULT GETDATE(),
    SubmittedAt DATETIME NOT NULL,
    CONSTRAINT FK_ReviewRecord_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_ReviewRecord_ProjectTestItem FOREIGN KEY (ProjectTestItemId) REFERENCES ProjectTestItem(ProjectTestItemId),
    CONSTRAINT FK_ReviewRecord_User FOREIGN KEY (ReviewedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_ReviewRecord_ProjectTestItemId ON ReviewRecord(ProjectTestItemId);
```

---

## 3️⃣ AppDbContext Fluent API 配置

### 3.1 修正後的完整配置

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // ===== User & Role =====
    modelBuilder.Entity<User>(entity =>
    {
        entity.ToTable("User");
        entity.HasKey(e => e.UserId);
        
        entity.HasOne<Role>()
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasIndex(e => e.RoleId);
        entity.HasIndex(e => e.Area);
    });

    // ===== Project =====
    modelBuilder.Entity<Project>(entity =>
    {
        entity.ToTable("Project");
        entity.HasKey(e => e.ProjectId);
        
        // ProjectCode 唯一約束
        entity.HasIndex(e => e.ProjectCode)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");
        
        // CreatedBy FK
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ModifiedBy FK
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.ModifiedBy)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // ===== ProjectRegulation =====
    modelBuilder.Entity<ProjectRegulation>(entity =>
    {
        entity.ToTable("ProjectRegulation");
        
        entity.HasOne<Project>()
            .WithMany()
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne<Regulation>()
            .WithMany()
            .HasForeignKey(e => e.RegulationId)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // ===== ProjectTestItem =====
    modelBuilder.Entity<ProjectTestItem>(entity =>
    {
        entity.ToTable("ProjectTestItem");
        
        entity.HasOne<Project>()
            .WithMany()
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne<TestItem>()
            .WithMany()
            .HasForeignKey(e => e.TestItemId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasIndex(e => e.ProjectId);
        entity.HasIndex(e => e.Status);
    });

    // ===== Schedule ⭐ 關鍵修正 =====
    modelBuilder.Entity<Schedule>(entity =>
    {
        entity.ToTable("Schedule");
        entity.HasKey(e => e.ScheduleId);
        
        // Project FK
        entity.HasOne<Project>()
            .WithMany()
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ProjectTestItem FK
        entity.HasOne<ProjectTestItem>()
            .WithMany()
            .HasForeignKey(s => s.ProjectTestItemId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Resource FK
        entity.HasOne<Resource>()
            .WithMany()
            .HasForeignKey(s => s.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // CreatedBy FK
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ModifiedBy FK
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.ModifiedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        // ❌ 已移除 EngineerId FK
        
        entity.HasIndex(e => e.ProjectTestItemId);
        entity.HasIndex(e => e.ResourceId);
        entity.HasIndex(e => e.Status);
    });

    // ===== ScheduleEngineer ⭐ 多工程師 =====
    modelBuilder.Entity<ScheduleEngineer>(entity =>
    {
        entity.ToTable("ScheduleEngineer");
        
        entity.HasOne<Schedule>()
            .WithMany()
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.EngineerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.AssignedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        // 唯一約束：同一排程不能重複指派同一工程師
        entity.HasIndex(e => new { e.ScheduleId, e.EngineerId })
            .IsUnique();
    });

    // ===== ResourceEngineer =====
    modelBuilder.Entity<ResourceEngineer>(entity =>
    {
        entity.ToTable("ResourceEngineer");
        
        entity.HasOne<Resource>()
            .WithMany()
            .HasForeignKey(e => e.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.EngineerId)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // ===== TestLog =====
    modelBuilder.Entity<TestLog>(entity =>
    {
        entity.ToTable("TestLog");
        
        entity.HasOne<Schedule>()
            .WithMany()
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // ===== ActualTestRecord =====
    modelBuilder.Entity<ActualTestRecord>(entity =>
    {
        entity.ToTable("ActualTestRecord");
        
        entity.HasOne<ProjectTestItem>()
            .WithOne()
            .HasForeignKey<ActualTestRecord>(e => e.ProjectTestItemId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasIndex(e => e.ProjectTestItemId)
            .IsUnique();
    });

    // ===== ProgressReport =====
    modelBuilder.Entity<ProgressReport>(entity =>
    {
        entity.ToTable("ProgressReport");
        
        entity.HasOne<Schedule>()
            .WithMany()
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.ReportedBy)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // ===== EstimateHistory =====
    modelBuilder.Entity<EstimateHistory>(entity =>
    {
        entity.ToTable("EstimateHistory");
        
        entity.HasOne<Schedule>()
            .WithMany()
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.ModifiedBy)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // ===== ReviewRecord =====
    modelBuilder.Entity<ReviewRecord>(entity =>
    {
        entity.ToTable("ReviewRecord");
        
        entity.HasOne<Project>()
            .WithMany()
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<ProjectTestItem>()
            .WithMany()
            .HasForeignKey(e => e.ProjectTestItemId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.ReviewedBy)
            .OnDelete(DeleteBehavior.Restrict);
    });
}
```

---

## 🔮 未來可能需要修改的地方

### 4.1 權限系統擴展（v1.1）

#### 新增表：RolePermission
```sql
-- 🔮 v1.1 新增
CREATE TABLE RolePermission (
    RolePermissionId INT PRIMARY KEY IDENTITY,
    RoleId INT NOT NULL,
    PermissionCode NVARCHAR(50) NOT NULL, -- CREATE_SCHEDULE, START_TEST, etc.
    IsGranted BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_RolePermission_Role FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

CREATE UNIQUE INDEX UQ_RolePermission ON RolePermission(RoleId, PermissionCode);
```

#### 新增表：UserResourceAccess（資料層級權限）
```sql
-- 🔮 v1.1 新增（若需要更細緻的權限控制）
CREATE TABLE UserResourceAccess (
    UserResourceAccessId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    ResourceId INT NOT NULL,
    AccessLevel NVARCHAR(20) NOT NULL, -- View, Edit, Assign
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_UserResourceAccess_User FOREIGN KEY (UserId) REFERENCES [User](UserId),
    CONSTRAINT FK_UserResourceAccess_Resource FOREIGN KEY (ResourceId) REFERENCES Resource(ResourceId)
);

CREATE UNIQUE INDEX UQ_UserResourceAccess ON UserResourceAccess(UserId, ResourceId);
```

**影響範圍**：
- ⚠️ ScheduleService 所有查詢方法需加權限過濾
- ⚠️ ResourceService.GetResourcesByUser 需改為動態權限查詢

---

### 4.2 權限代理機制（v1.2）

#### 新增表：Delegation
```sql
-- 🔮 v1.2 新增（您需求書第 3 點）
CREATE TABLE Delegation (
    DelegationId INT PRIMARY KEY IDENTITY,
    GrantorUserId INT NOT NULL,    -- 排程主管
    DelegateUserId INT NOT NULL,   -- 現場主管
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    IsApproved BIT NOT NULL DEFAULT 0,
    ApprovedDate DATETIME,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Delegation_Grantor FOREIGN KEY (GrantorUserId) REFERENCES [User](UserId),
    CONSTRAINT FK_Delegation_Delegate FOREIGN KEY (DelegateUserId) REFERENCES [User](UserId)
);
```

**影響範圍**：
- ⚠️⚠️ Service 層每個方法需檢查「當前使用者是否為代理人」
- ⚠️⚠️ 所有操作記錄需增加「IsDelegated」欄位

**修改示例**：
```csharp
// 原本
public void CreateProject(CreateProjectDto dto, int userId)
{
    var project = new Project { CreatedBy = userId };
}

// v1.2 需改為
public void CreateProject(CreateProjectDto dto, int userId, int? delegatedBy = null)
{
    var project = new Project 
    { 
        CreatedBy = delegatedBy ?? userId, // 若為代理操作，記錄原使用者
        ActualOperatorId = userId           // 🔮 新增欄位：實際操作者
    };
}
```

---

### 4.3 通知日誌（v1.2）

#### 新增表：NotificationLog
```sql
-- 🔮 v1.2 新增（追蹤所有通知）
CREATE TABLE NotificationLog (
    NotificationLogId INT PRIMARY KEY IDENTITY,
    RelatedEntityType NVARCHAR(50) NOT NULL,  -- Schedule, Project, ReviewRecord
    RelatedEntityId INT NOT NULL,
    NotificationType NVARCHAR(50) NOT NULL,   -- ScheduleAssigned, ReviewNeeded
    Recipients NVARCHAR(MAX) NOT NULL,        -- JSON 格式的收件人清單
    Status NVARCHAR(20) NOT NULL,             -- Pending, Sent, Failed
    SentDate DATETIME,
    ErrorMessage NVARCHAR(MAX),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_NotificationLog_Type ON NotificationLog(NotificationType);
CREATE INDEX IX_NotificationLog_Status ON NotificationLog(Status);
```

**影響範圍**：
- ✅ NotificationService 每次發送通知都需記錄
- ⚠️ 可加入重試機制

---

### 4.4 附件管理（v1.3）

#### 新增表：ProjectAttachment
```sql
-- 🔮 v1.3 新增（案件附件）
CREATE TABLE ProjectAttachment (
    AttachmentId INT PRIMARY KEY IDENTITY,
    ProjectId INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FileSize BIGINT NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    ContentType NVARCHAR(100),
    UploadedBy INT NOT NULL,
    UploadedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_ProjectAttachment_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectAttachment_User FOREIGN KEY (UploadedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_ProjectAttachment_ProjectId ON ProjectAttachment(ProjectId);
```

---

### 4.5 ScheduleEngineer.IsActive 的未來用途

**目前狀態**：您的 Model 有此欄位，但未在 SD 業務邏輯中使用

**未來可能的用途**：

#### 選項 1：軟刪除（推薦）
```csharp
// 取消工程師指派時
public void RemoveEngineer(int scheduleId, int engineerId)
{
    var se = _context.ScheduleEngineers
        .First(x => x.ScheduleId == scheduleId && x.EngineerId == engineerId);
    
    se.IsActive = false; // 軟刪除
    // 而非 _context.ScheduleEngineers.Remove(se);
}
```

#### 選項 2：歷史追蹤
```csharp
// 查詢「誰曾經被指派過這個排程」
var history = _context.ScheduleEngineers
    .Where(x => x.ScheduleId == scheduleId)
    .OrderBy(x => x.AssignedDate)
    .ToList();
// IsActive = true → 目前指派中
// IsActive = false → 已取消指派（但保留歷史）
```

**建議**：
- v1.0：保留欄位，但不使用（刪除時直接 `Remove()`）
- v1.1：若需要「誰曾經參與過」的報表，啟用此欄位

---

## 5️⃣ Service 層修改指引（未來擴展）

### 5.1 若加入權限系統（v1.1）

#### 影響的 Service 方法

| Service | 方法 | 需要加什麼 |
|---------|------|------------|
| **ScheduleService** | CreateSchedule | 檢查「是否有該 Resource 的派案權限」 |
| **ScheduleService** | AssignSchedule | 檢查「是否有該 Resource 的派案權限」 |
| **TestService** | RecordTestAction | 檢查「是否為該 Schedule 的指派工程師」 |
| **ProjectService** | CreateProject | 檢查「是否有開案權限」 |
| **ReviewService** | ProcessReview | 檢查「是否為 Reviewer 角色」 |

#### 建議做法：使用 Attribute + AOP

```csharp
// 定義權限 Attribute
[AttributeUsage(AttributeTargets.Method)]
public class RequirePermissionAttribute : Attribute
{
    public string Permission { get; }
    public RequirePermissionAttribute(string permission) 
    { 
        Permission = permission; 
    }
}

// 使用範例
[RequirePermission("CREATE_SCHEDULE")]
public async Task<Result<int>> CreateSchedule(CreateScheduleDto dto, int userId)
{
    // 不用寫權限檢查，由 AOP 攔截器處理
}

// AOP 攔截器（使用 Castle.DynamicProxy 或類似框架）
public class PermissionInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var attr = invocation.Method.GetCustomAttribute<RequirePermissionAttribute>();
        if (attr != null)
        {
            if (!CurrentUser.HasPermission(attr.Permission))
                throw new UnauthorizedAccessException($"缺少權限: {attr.Permission}");
        }
        invocation.Proceed();
    }
}
```

**優點**：
- ✅ 不用改動每個方法內部
- ✅ 集中管理權限邏輯
- ✅ 易於測試

---

### 5.2 若加入權限代理（v1.2）

#### 需要修改的地方

**所有 CreatedBy / ModifiedBy 欄位都需要額外記錄「實際操作者」**

```csharp
// 🔮 v1.2 需要在各表增加
ALTER TABLE Project ADD ActualOperatorId INT;
ALTER TABLE Schedule ADD ActualOperatorId INT;
ALTER TABLE TestLog ADD ActualOperatorId INT;

// Service 方法簽名需改為
public void CreateProject(CreateProjectDto dto, int userId, int? delegatedBy = null)
{
    var project = new Project
    {
        CreatedBy = delegatedBy ?? userId,     // 代理人 or 本人
        ActualOperatorId = userId               // 實際操作者
    };
}
```

**影響評估**：
- ⚠️⚠️ **所有** Service 方法簽名都要改
- ⚠️⚠️ UI 層需傳遞「當前是否為代理狀態」
- ⚠️ 資料庫需要增加多個欄位

**建議**：
- v1.0 不實作
- v1.2 若真的需要，建立單獨的 `OperationContext` 類別統一管理

```csharp
public class OperationContext
{
    public int UserId { get; set; }
    public int? DelegatedBy { get; set; }
    public bool IsDelegated => DelegatedBy.HasValue;
}

// Service 改為
public void CreateProject(CreateProjectDto dto, OperationContext context)
{
    // ...
}
```

---

## 6️⃣ UI 層未來修改點

### 6.1 若加入權限系統（v1.1）

#### MainForm 需要增加

```csharp
public partial class MainForm : Form
{
    private void MainForm_Load(object sender, EventArgs e)
    {
        // 🔮 v1.1 需要加入
        LoadUserPermissions();
        ApplyPermissionsToUI();
    }

    private void LoadUserPermissions()
    {
        var permissions = _authService.GetUserPermissions(CurrentUserId);
        // 儲存到 Session 或 Context
    }

    private void ApplyPermissionsToUI()
    {
        // 根據權限控制按鈕顯隱
        btnOpenCalendar.Enabled = CurrentUser.HasPermission("VIEW_CALENDAR");
        btnCreateProject.Enabled = CurrentUser.HasPermission("CREATE_PROJECT");
    }
}
```

#### CalendarWindow 需要增加

```csharp
private void schedulerControl_AppointmentInserted(object sender, PersistentObjectsEventArgs e)
{
    // 🔮 v1.1 需要加入權限檢查
    if (!CurrentUser.HasPermission("CREATE_SCHEDULE"))
    {
        MessageBox.Show("您沒有派案權限");
        e.Cancel = true;
        return;
    }

    // 原本的派案邏輯
}
```

---

### 6.2 若加入權限代理（v1.2）

#### MainForm 需要增加代理狀態提示

```csharp
public partial class MainForm : Form
{
    private void MainForm_Load(object sender, EventArgs e)
    {
        // 🔮 v1.2 需要加入
        CheckDelegationStatus();
    }

    private void CheckDelegationStatus()
    {
        var delegation = _delegationService.GetActiveDelegation(CurrentUserId);
        if (delegation != null)
        {
            lblDelegationStatus.Text = $"⚠️ 您正在代理 {delegation.GrantorName} 的權限";
            lblDelegationStatus.Visible = true;
        }
    }
}
```

---

## 7️⃣ 資料庫 Migration 策略

### 7.1 v1.0 → v1.1（權限系統）

```sql
-- Migration: 20250101_AddPermissionTables

-- 1. 新增 RolePermission
CREATE TABLE RolePermission (
    RolePermissionId INT PRIMARY KEY IDENTITY,
    RoleId INT NOT NULL,
    PermissionCode NVARCHAR(50) NOT NULL,
    IsGranted BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_RolePermission_Role FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

-- 2. 新增預設權限
INSERT INTO RolePermission (RoleId, PermissionCode) VALUES
(1, 'CREATE_PROJECT'),   -- Manager
(1, 'VIEW_ALL_AREAS'),   -- Manager
(2, 'CREATE_SCHEDULE'),  -- FieldManager
(3, 'START_TEST');       -- Engineer

-- 3. 新增 UserResourceAccess（若需要）
-- ...
```

### 7.2 v1.1 → v1.2（權限代理）

```sql
-- Migration: 20250201_AddDelegation

-- 1. 新增 Delegation 表
CREATE TABLE Delegation (
    -- ...（前面已列出）
);

-- 2. 各表增加 ActualOperatorId
ALTER TABLE Project ADD ActualOperatorId INT;
ALTER TABLE Schedule ADD ActualOperatorId INT;
ALTER TABLE TestLog ADD ActualOperatorId INT;

-- 3. 設定預設值（現有資料）
UPDATE Project SET ActualOperatorId = CreatedBy WHERE ActualOperatorId IS NULL;
UPDATE Schedule SET ActualOperatorId = CreatedBy WHERE ActualOperatorId IS NULL;
UPDATE TestLog SET ActualOperatorId = UserId WHERE ActualOperatorId IS NULL;
```

---

## 8️⃣ 開發檢查清單（含未來擴展）

### Phase 1: v1.0 基礎版（8 週）
- [x] 資料庫設計完成
- [ ] EF Core DbContext 完成
- [ ] 核心 Service 實作
- [ ] UI 基本功能
- [ ] 完整流程測試

### Phase 2: v1.1 權限系統（2-3 週）
- [ ] 新增 RolePermission 表
- [ ] 實作權限檢查邏輯（AOP）
- [ ] UI 按鈕權限控制
- [ ] 資料層級權限過濾（若需要）

### Phase 3: v1.2 權限代理（3-4 週）
- [ ] 新增 Delegation 表
- [ ] 修改所有 Service 簽名
- [ ] UI 增加代理狀態提示
- [ ] 操作記錄增加代理資訊

### Phase 4: v1.3 進階功能（按需求）
- [ ] 附件管理
- [ ] 通知重試機制
- [ ] 甘特圖分析
- [ ] CSV 批次開案

---

## 9️⃣ 關鍵設計決策記錄

### 9.1 為什麼不在 Schedule 直接加 EngineerId？

**原因**：
- 需要支援「多工程師同時測試」
- 需要支援「未指派工程師」的情況
- ScheduleEngineer 提供更彈性的擴展性（如記錄指派時間、指派人）

### 9.2 為什麼 Project.ProjectId 改為 INT？

**原因**：
- 簡化關聯表設計（所有 FK 統一為 INT）
- ProjectCode 作為業務編號已足夠
- INT 主鍵效能更好

### 9.3 ScheduleEngineer.IsActive 要不要刪除？

**結論**：**保留**

**理由**：
- 刪除代價小，但未來加回來代價大
- 可用於軟刪除（避免誤刪歷史）
- 可用於追蹤「誰曾經參與過」

**建議**：
- v1.0：保留但不使用
- v1.1：視需求決定是否啟用

---

## 🔟 總結

### ✅ 已對齊實際 Model 的修正

1. Project.ProjectId 改為 INT
2. 移除 Schedule.EngineerId
3. 所有 FK 型別統一為 INT
4. 保留 ScheduleEngineer.IsActive

### 🔮 未來需要修改的地方（優先順序）

| 功能 | 版本 | 影響範圍 | 預估工作量 |
|------|------|----------|------------|
| **權限系統（RBAC）** | v1.1 | Service 層 + UI 按鈕 | 2-3 週 |
| **權限代理** | v1.2 | 所有 Service + 資料表 | 3-4 週 |
| **通知日誌** | v1.2 | NotificationService | 1 週 |
| **附件管理** | v1.3 | 新增功能 | 1-2 週 |

### 📌 立即可用

這份 SD 文件：
- ✅ 完全對齊您的 Model 設計
- ✅ 可直接作為 v1.0 實作藍本
- ✅ 標註了所有未來擴展點
- ✅ 提供了清晰的 Migration 策略

**建議開發順序**：
1. 先完成 v1.0（8 週）
2. 觀察實際使用情況
3. 再決定是否需要 v1.1/v1.2 的權限功能

如有任何疑問，請隨時提出！
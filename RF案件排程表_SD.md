# 📘 RF 測試排程系統 - 系統設計文件 v1.0

## 📋 文件資訊
- **版本**：v1.0（最小可行版本）
- **日期**：2025-12-18
- **狀態**：可直接作為實作藍本
- **技術棧**：WinForms + DevExpress v25.1 + EF Core 9.0 + SQL Server

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
- 權限代理機制（保留至 v1.1）
- CSV 批次開案（保留至 v1.1）
- 甘特圖分析（保留至 v1.2）
- 逾期預警（保留至 v1.2）

---

## 1️⃣ 系統架構設計

### 1.1 三層架構

```
┌─────────────────────────────────────────────────────────┐
│                  Presentation Layer                     │
│  ┌──────────┐  ┌──────────────┐  ┌──────────────────┐  │
│  │MainForm  │  │CalendarWindow│  │ProjectMgmtWindow │  │
│  │  (MDI)   │  │              │  │                  │  │
│  └──────────┘  └──────────────┘  └──────────────────┘  │
│       │               │                    │            │
│       └───────────────┴────────────────────┘            │
│                       ↓                                 │
│              ServiceEventBus (事件總線)                 │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│                Business Logic Layer                     │
│  ┌──────────────┐  ┌───────────────┐  ┌─────────────┐  │
│  │ProjectService│  │ScheduleService│  │TestService  │  │
│  ├──────────────┤  ├───────────────┤  ├─────────────┤  │
│  │ReviewService │  │ResourceService│  │NotifyService│  │
│  └──────────────┘  └───────────────┘  └─────────────┘  │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│                 Data Access Layer                       │
│           Entity Framework Core 9.0                     │
│              (Fluent API 配置)                          │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│                SQL Server Database                      │
└─────────────────────────────────────────────────────────┘
```

### 1.2 關鍵設計原則

1. **狀態機由 Service 層統一管理**：UI 不直接修改狀態
2. **事件驅動同步**：視窗間透過 ServiceEventBus 同步
3. **測項為主狀態單位**：Project.Status 為衍生計算欄位
4. **非同步通知**：Email 發送不阻塞主流程

---

## 2️⃣ 資料庫設計

### 2.1 實體關係圖（ER Diagram）

```
User ──┐
       │
       ├─ Project (1:N) ──┬─ ProjectRegulation (1:N)
       │                  │
       │                  ├─ ProjectTestItem (1:N) ──┬─ ActualTestRecord (1:1)
       │                  │                          │
       │                  └─ Schedule (1:N) ─────────┘
       │                           │
       │                           ├─ ScheduleEngineer (1:N)
       │                           │
       │                           ├─ TestLog (1:N)
       │                           │
       │                           ├─ ProgressReport (1:N)
       │                           │
       │                           └─ EstimateHistory (1:N)
       │
       ├─ Resource (1:N) ──┬─ ResourceEngineer (N:M with User)
       │                   │
       │                   └─ Schedule (1:N)
       │
       └─ ReviewRecord (N:1 with Project/ProjectTestItem)
```

### 2.2 核心資料表設計

#### 2.2.1 User（使用者）
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

INSERT INTO User (UserName, Email, RoleId ) VALUES
('Jimmy', 'abc@gmail.com', 3),


CREATE INDEX IX_User_RoleId ON [User](RoleId);
CREATE INDEX IX_User_Area ON [User](Area);


```

#### 2.2.2 Role（角色）
```sql
CREATE TABLE Role (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL, -- Manager/FieldManager/Engineer/Reviewer
    Description NVARCHAR(200),
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- 初始資料
INSERT INTO Role (RoleName, Description) VALUES
('Manager', '排程主管'),
('FieldManager', '現場主管'),
('Engineer', '工程師'),
('Reviewer', '審查人員');
```

#### 2.2.3 Resource（場地）
```sql
CREATE TABLE Resource (
    ResourceId INT PRIMARY KEY IDENTITY,
    ResourceName NVARCHAR(100) NOT NULL, -- SAC1, Conducted A, etc.
    Area NVARCHAR(50) NOT NULL, -- A區/B區
    ResourceType NVARCHAR(50) NOT NULL, -- SAC/FAC/Conducted/Normal
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
-- 初始資料
INSERT INTO Resource (ResourceName, Area, ResourceType) VALUES
('SAC1', 'A區', 'Radiated'),
('SAC2', 'A區', 'Radiated'),
('SAC3', 'A區', 'Radiated'),
('FAC1', 'A區', 'Radiated'),
('Conducted_1', 'A區', 'Conducted'),
('Conducted_2', 'A區', 'Conducted'),
('Conducted_3', 'A區', 'Conducted'),
('Conducted_4', 'A區', 'Conducted'),
('Conducted_5', 'A區', 'Normal'),
('Conducted_6', 'A區', 'Normal'),
('SACC', 'B區', 'Radiated'),
('SACD', 'B區', 'Radiated'),
('SACG', 'B區', 'Radiated'),
('FACA', 'B區', 'Radiated'),
('Conducted_A', 'B區', 'Conducted'),
('Conducted_B', 'B區', 'Conducted'),
('Conducted_C', 'B區', 'Conducted'),
('Conducted_D', 'B區', 'Conducted'),
('Conducted_E', 'B區', 'Normal'),
('Conducted_F', 'B區', 'Normal'),


CREATE INDEX IX_Resource_Area ON Resource(Area);
CREATE INDEX IX_Resource_Type ON Resource(ResourceType);
```

#### 2.2.4 ResourceEngineer（場地工程師關聯）
```sql
CREATE TABLE ResourceEngineer (
    ResourceEngineerId INT PRIMARY KEY IDENTITY,
    ResourceId INT NOT NULL,
    EngineerId NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ResourceEngineer_Resource FOREIGN KEY (ResourceId) REFERENCES Resource(ResourceId),
    CONSTRAINT FK_ResourceEngineer_Engineer FOREIGN KEY (EngineerId) REFERENCES [User](UserId)
);

CREATE INDEX IX_ResourceEngineer_ResourceId ON ResourceEngineer(ResourceId);
CREATE INDEX IX_ResourceEngineer_EngineerId ON ResourceEngineer(EngineerId);
```

#### 2.2.5 Regulation（法規）
```sql
CREATE TABLE Regulation (
    RegulationId INT PRIMARY KEY IDENTITY,
    RegulationCode NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- 初始資料
INSERT INTO Regulation (RegulationCode, RegulationName) VALUES
('FCC'),
('FCC'),
('NCC'),
('NCC'),
('CE'),
('CE'),
('TELEC'),
('TELEC'),
('IC'),
('IC'),
('Other')
```

#### 2.2.6 TestItem（測試項目）
```sql
CREATE TABLE TestItem (
    TestItemId INT PRIMARY KEY IDENTITY,
    TestItemName NVARCHAR(100) NOT NULL,
    TestItemType NVARCHAR(50) NOT NULL, -- Conducted/Radiated/Normal
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- 初始資料
INSERT INTO TestItem (TestItemName, TestItemType) VALUES
('WWAN_Conducted'),
('WIFI_Conducted'),
('WWAN_Radiated'),
('WIFI_Radiated'),
('Adaptivity')
('Recevier Blocking')
('DFS')
('Other')
```

#### 2.2.7 Project（案件）⭐ 修正重點
```sql
CREATE TABLE Project (
    ProjectKey INT IDENTITY(1,1) PRIMARY KEY,
    ProjectCode NVARCHAR(50) UNIQUE NOT NULL,   -- 業務用
    ProjectName NVARCHAR(200) NOT NULL,
    Priority NVARCHAR(20) NOT NULL DEFAULT 'P2', -- P0/P1/P2/P3
    Status NVARCHAR(50) NOT NULL, -- 衍生計算欄位，由測項狀態決定
    Notes NVARCHAR(MAX),
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Project_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserId)
);

-- 狀態說明（由系統計算，不由 UI 直接操作）：
-- 'Pending'      - 所有測項都是待派案
-- 'Scheduled'    - 至少一個測項已排程
-- 'InProgress'   - 至少一個測項測試中
-- 'UnderReview'  - 所有測項都在待審查/審查中
-- 'Completed'    - 所有測項都已完成
-- 'Returned'     - 至少一個測項被退回

CREATE INDEX IX_Project_Status ON Project(Status);
CREATE INDEX IX_Project_CreatedBy ON Project(CreatedBy);
```

#### 2.2.8 ProjectRegulation（案件法規）
```sql
CREATE TABLE ProjectRegulation (
    ProjectRegulationId INT PRIMARY KEY IDENTITY,
    ProjectId NVARCHAR(50) NOT NULL,
    RegulationId INT NULL, -- NULL 表示 OTHER
    OtherRegulationText NVARCHAR(100), -- 當 RegulationId 為 NULL 時填寫
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ProjectRegulation_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectRegulation_Regulation FOREIGN KEY (RegulationId) REFERENCES Regulation(RegulationId),
    CONSTRAINT CK_ProjectRegulation_Other CHECK (
        (RegulationId IS NOT NULL AND OtherRegulationText IS NULL) OR
        (RegulationId IS NULL AND OtherRegulationText IS NOT NULL)
    )
);

CREATE INDEX IX_ProjectRegulation_ProjectId ON ProjectRegulation(ProjectId);
```

#### 2.2.9 ProjectTestItem（案件測項）⭐ 主狀態機
```sql
CREATE TABLE ProjectTestItem (
    ProjectTestItemId INT PRIMARY KEY IDENTITY,
    ProjectId NVARCHAR(50) NOT NULL,
    TestItemId INT NULL, -- NULL 表示 OTHER
    OtherTestItemText NVARCHAR(100),
    TestItemType NVARCHAR(50), -- 用於區分測試類型
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending', -- 主狀態機
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ProjectTestItem_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectTestItem_TestItem FOREIGN KEY (TestItemId) REFERENCES TestItem(TestItemId),
    CONSTRAINT CK_ProjectTestItem_Other CHECK (
        (TestItemId IS NOT NULL AND OtherTestItemText IS NULL) OR
        (TestItemId IS NULL AND OtherTestItemText IS NOT NULL)
    )
);

-- 狀態流轉（這是真正的主狀態機）：
-- 'Pending'      - 待派案
-- 'Scheduled'    - 已排程（至少一個 Schedule 存在）
-- 'InProgress'   - 測試中（至少一個 Schedule 在測試中）
-- 'UnderReview'  - 待審查（所有 Schedule 都已完成且送審）
-- 'Completed'    - 已完成（Reviewer 審查通過）
-- 'Returned'     - 退回修正（Reviewer 退回）

CREATE INDEX IX_ProjectTestItem_ProjectId ON ProjectTestItem(ProjectId);
CREATE INDEX IX_ProjectTestItem_Status ON ProjectTestItem(Status);
```

#### 2.2.10 Schedule（排程/Appointment）⭐ 關鍵表
```sql
CREATE TABLE Schedule (
    ScheduleId INT PRIMARY KEY IDENTITY,
    ProjectId NVARCHAR(50) NOT NULL,
    ProjectTestItemId INT NOT NULL, -- ⭐ 指向測項（多場地同步的關鍵）
    ResourceId INT NOT NULL,
    ScheduleType NVARCHAR(50) NOT NULL DEFAULT 'Case', -- Case/Maintenance/Blocking/NonProjectEvent
    EstimatedStart DATETIME, -- 可為 NULL（未派案時）
    EstimatedEnd DATETIME,
    OriginalEstimatedStart DATETIME, -- 原始預估（第一次設定時寫入）
    OriginalEstimatedEnd DATETIME,
    Status NVARCHAR(50) NOT NULL DEFAULT 'InQueue', -- 排程狀態（非測項狀態）
    Notes NVARCHAR(MAX),
    CreatedBy NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(50),
    ModifiedDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Schedule_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_Schedule_ProjectTestItem FOREIGN KEY (ProjectTestItemId) REFERENCES ProjectTestItem(ProjectTestItemId),
    CONSTRAINT FK_Schedule_Resource FOREIGN KEY (ResourceId) REFERENCES Resource(ResourceId),
    CONSTRAINT FK_Schedule_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [User](UserId)
);

-- Schedule.Status 說明（與 ProjectTestItem.Status 不同）：
-- 'InQueue'      - 待派案（未設定場地/時間）
-- 'Scheduled'    - 已排程（已設定場地/時間，尚未開始）
-- 'InProgress'   - 測試中（該場地正在測試）
-- 'Paused'       - 暫停中（該場地暫停）
-- 'Completed'    - 該場地測試完成（⚠️ 不等於測項完成）
-- 'Review'       - 該場地送審

CREATE INDEX IX_Schedule_ProjectTestItemId ON Schedule(ProjectTestItemId);
CREATE INDEX IX_Schedule_ResourceId ON Schedule(ResourceId);
CREATE INDEX IX_Schedule_Status ON Schedule(Status);
CREATE INDEX IX_Schedule_EstimatedStart ON Schedule(EstimatedStart);
```

#### 2.2.11 ScheduleEngineer（排程工程師）
```sql
CREATE TABLE ScheduleEngineer (
    ScheduleEngineerId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    EngineerId NVARCHAR(50) NOT NULL,
    AssignedDate DATETIME NOT NULL DEFAULT GETDATE(),
    AssignedBy NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_ScheduleEngineer_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId) ON DELETE CASCADE,
    CONSTRAINT FK_ScheduleEngineer_Engineer FOREIGN KEY (EngineerId) REFERENCES [User](UserId),
    CONSTRAINT UQ_ScheduleEngineer UNIQUE (ScheduleId, EngineerId)
);

CREATE INDEX IX_ScheduleEngineer_ScheduleId ON ScheduleEngineer(ScheduleId);
CREATE INDEX IX_ScheduleEngineer_EngineerId ON ScheduleEngineer(EngineerId);
```

#### 2.2.12 TestLog（測試操作記錄）
```sql
CREATE TABLE TestLog (
    TestLogId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    ActionType NVARCHAR(50) NOT NULL, -- Start/Pause/Resume/End/AutoFill
    ActionTime DATETIME NOT NULL DEFAULT GETDATE(),
    UserId NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(500),
    IsModifiable BIT NOT NULL DEFAULT 1, -- 7天後鎖定
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TestLog_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    CONSTRAINT FK_TestLog_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

CREATE INDEX IX_TestLog_ScheduleId ON TestLog(ScheduleId);
CREATE INDEX IX_TestLog_ActionTime ON TestLog(ActionTime);
```

#### 2.2.13 ActualTestRecord（實際測試時間彙總）⭐ 重要
```sql
CREATE TABLE ActualTestRecord (
    ActualTestRecordId INT PRIMARY KEY IDENTITY,
    ProjectTestItemId INT NOT NULL UNIQUE, -- 每個測項只有一筆
    ActualStart DATETIME, -- 該測項最早開始時間（跨所有場地）
    ActualEnd DATETIME, -- 該測項最晚結束時間（跨所有場地）
    TotalDuration INT, -- 累計工時（分鐘）
    PauseCount INT DEFAULT 0, -- 暫停次數
    LastCalculatedDate DATETIME,
    CONSTRAINT FK_ActualTestRecord_ProjectTestItem FOREIGN KEY (ProjectTestItemId) REFERENCES ProjectTestItem(ProjectTestItemId)
);

CREATE INDEX IX_ActualTestRecord_ProjectTestItemId ON ActualTestRecord(ProjectTestItemId);
```

#### 2.2.14 ProgressReport（進度回報）
```sql
CREATE TABLE ProgressReport (
    ProgressReportId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    ReportStatus NVARCHAR(50) NOT NULL, -- InProgress/Completed/Fail
    ReportMessage NVARCHAR(MAX) NOT NULL,
    ReportedBy NVARCHAR(50) NOT NULL,
    ReportedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ProgressReport_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    CONSTRAINT FK_ProgressReport_User FOREIGN KEY (ReportedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_ProgressReport_ScheduleId ON ProgressReport(ScheduleId);
CREATE INDEX IX_ProgressReport_ReportedDate ON ProgressReport(ReportedDate);
```

#### 2.2.15 EstimateHistory（預估時間調整記錄）
```sql
CREATE TABLE EstimateHistory (
    EstimateHistoryId INT PRIMARY KEY IDENTITY,
    ScheduleId INT NOT NULL,
    OldStart DATETIME,
    OldEnd DATETIME,
    NewStart DATETIME,
    NewEnd DATETIME,
    Reason NVARCHAR(500),
    ModifiedBy NVARCHAR(50) NOT NULL,
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_EstimateHistory_Schedule FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    CONSTRAINT FK_EstimateHistory_User FOREIGN KEY (ModifiedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_EstimateHistory_ScheduleId ON EstimateHistory(ScheduleId);
```

#### 2.2.16 ReviewRecord（審查記錄）⭐ 修正重點
```sql
CREATE TABLE ReviewRecord (
    ReviewRecordId INT PRIMARY KEY IDENTITY,
    ProjectId NVARCHAR(50) NOT NULL,
    ProjectTestItemId INT NOT NULL,
    ReviewRound INT NOT NULL DEFAULT 1, -- ⭐ 新增：第幾次送審
    ReviewResult NVARCHAR(50) NOT NULL, -- Approved/Returned
    ReviewComment NVARCHAR(MAX),
    ReviewedBy NVARCHAR(50) NOT NULL,
    ReviewedDate DATETIME NOT NULL DEFAULT GETDATE(),
    SubmittedAt DATETIME NOT NULL, -- ⭐ 新增：送審時間快照
    CONSTRAINT FK_ReviewRecord_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
    CONSTRAINT FK_ReviewRecord_ProjectTestItem FOREIGN KEY (ProjectTestItemId) REFERENCES ProjectTestItem(ProjectTestItemId),
    CONSTRAINT FK_ReviewRecord_User FOREIGN KEY (ReviewedBy) REFERENCES [User](UserId)
);

CREATE INDEX IX_ReviewRecord_ProjectTestItemId ON ReviewRecord(ProjectTestItemId);
CREATE INDEX IX_ReviewRecord_ReviewedDate ON ReviewRecord(ReviewedDate);
```

---

## 3️⃣ 核心業務邏輯設計

### 3.1 狀態機設計⭐ 重要修正

#### 3.1.1 ProjectTestItem 主狀態機（真正的流程控制）

```csharp
public enum ProjectTestItemStatus
{
    Pending,      // 待派案
    Scheduled,    // 已排程
    InProgress,   // 測試中
    UnderReview,  // 待審查
    Completed,    // 已完成
    Returned      // 退回修正
}

// 允許的狀態轉換
public static class ProjectTestItemStatusTransitions
{
    public static readonly Dictionary> Allowed = new()
    {
        { ProjectTestItemStatus.Pending, new() { ProjectTestItemStatus.Scheduled } },
        { ProjectTestItemStatus.Scheduled, new() { ProjectTestItemStatus.InProgress } },
        { ProjectTestItemStatus.InProgress, new() { ProjectTestItemStatus.UnderReview } },
        { ProjectTestItemStatus.UnderReview, new() { ProjectTestItemStatus.Completed, ProjectTestItemStatus.Returned } },
        { ProjectTestItemStatus.Returned, new() { ProjectTestItemStatus.Scheduled } } // 退回後重新派案
    };
}
```

#### 3.1.2 Schedule 狀態機（場地層級）

```csharp
public enum ScheduleStatus
{
    InQueue,      // 待派案
    Scheduled,    // 已排程
    InProgress,   // 測試中
    Paused,       // 暫停中
    Completed,    // 該場地完成
    Review        // 送審
}

public static class ScheduleStatusTransitions
{
    public static readonly Dictionary> Allowed = new()
    {
        { ScheduleStatus.InQueue, new() { ScheduleStatus.Scheduled } },
        { ScheduleStatus.Scheduled, new() { ScheduleStatus.InProgress } },
        { ScheduleStatus.InProgress, new() { ScheduleStatus.Paused, ScheduleStatus.Completed } },
        { ScheduleStatus.Paused, new() { ScheduleStatus.InProgress, ScheduleStatus.Completed } },
        { ScheduleStatus.Completed, new() { ScheduleStatus.Review } }
    };
}
```

#### 3.1.3 Project 狀態計算邏輯（衍生欄位）

```csharp
public class ProjectService
{
    /// 
    /// 計算專案整體狀態（由測項狀態彙總）
    /// ⚠️ 此方法不應由 UI 直接調用修改狀態
    /// 
    public string CalculateProjectStatus(string projectId)
    {
        var testItems = _context.ProjectTestItems
            .Where(pti => pti.ProjectId == projectId && !pti.IsDeleted)
            .Select(pti => pti.Status)
            .ToList();

        if (!testItems.Any()) return "Pending";

        // 所有測項都是待派案
        if (testItems.All(s => s == "Pending"))
            return "Pending";

        // 至少一個測項被退回
        if (testItems.Any(s => s == "Returned"))
            return "Returned";

        // 所有測項都已完成
        if (testItems.All(s => s == "Completed"))
            return "Completed";

        // 所有測項都在待審查/審查中
        if (testItems.All(s => s == "UnderReview" || s == "Completed"))
            return "UnderReview";

        // 至少一個測項在測試中
        if (testItems.Any(s => s == "InProgress"))
            return "InProgress";

        // 至少一個測項已排程
        if (testItems.Any(s => s == "Scheduled"))
            return "Scheduled";

        return "Pending";
    }

    /// 
    /// 更新專案狀態（在測項狀態變更後自動調用）
    /// 
    public void UpdateProjectStatus(string projectId)
    {
        var project = _context.Projects.Find(projectId);
        if (project == null) return;

        var newStatus = CalculateProjectStatus(projectId);
        
        if (project.Status != newStatus)
        {
            project.Status = newStatus;
            project.ModifiedDate = DateTime.Now;
            _context.SaveChanges();

            // 觸發事件通知 UI
            ServiceEventBus.Instance.RaiseProjectStatusChanged(project);
        }
    }
}
```

### 3.2 多場地同步測試時間計算⭐ 核心邏輯

```csharp
public class TestService
{
    /// 
    /// 計算測項實際時間（跨所有場地）
    /// 觸發時機：任何 Schedule 的 TestLog 新增後
    /// 
    public void CalculateActualTimeForTestItem(int projectTestItemId)
    {
        // 取得該測項的所有 Schedule
        var scheduleIds = _context.Schedules
            .Where(s => s.ProjectTestItemId == projectTestItemId && !s.IsDeleted)
            .Select(s => s.ScheduleId)
            .ToList();

        if (!scheduleIds.Any()) return;

        // 取得所有相關的 TestLog（按時間排序）
        var logs = _context.TestLogs
            .Where(tl => scheduleIds.Contains(tl.ScheduleId))
            .OrderBy(tl => tl.ActionTime)
            .ToList();

        if (!logs.Any()) return;

        // 計算實際開始時間（最早的 Start）
        var actualStart = logs
            .Where(l => l.ActionType == "Start")
            .Select(l => (DateTime?)l.ActionTime)
            .Min();

        // 計算實際結束時間（最晚的 End）
        var actualEnd = logs
            .Where(l => l.ActionType == "End" || l.ActionType == "AutoFill")
            .Select(l => (DateTime?)l.ActionTime)
            .Max();

        // 計算總工時（每個場地的 Start~Pause/End 累計）
        int totalMinutes = 0;
        int pauseCount = 0;
        
        // 按 ScheduleId 分組計算
        foreach (var scheduleId in scheduleIds)
        {
            var scheduleLogs = logs.Where(l => l.ScheduleId == scheduleId).ToList();
            DateTime? lastStart = null;

            foreach (var log in scheduleLogs)
            {
                if (log.ActionType == "Start" || log.ActionType == "Resume")
                {
                    lastStart = log.ActionTime;
                }
                else if ((log.ActionType == "Pause" || log.ActionType == "End" || log.ActionType == "AutoFill") 
                         && lastStart.HasValue)
                {
                    totalMinutes += (int)(log.ActionTime - lastStart.Value).TotalMinutes;
                    
                    if (log.ActionType == "Pause")
                        pauseCount++;
                    
                    lastStart = null;
                }
            }
        }

        // 更新或插入 ActualTestRecord
        var record = _context.ActualTestRecords
            .FirstOrDefault(r => r.ProjectTestItemId == projectTestItemId);

        if (record == null)
        {
            record = new ActualTestRecord
            {
                ProjectTestItemId = projectTestItemId
            };
            _context.ActualTestRecords.Add(record);
        }

        record.ActualStart = actualStart;
        record.ActualEnd = actualEnd;
        record.TotalDuration = totalMinutes;
        record.PauseCount = pauseCount;
        record.LastCalculatedDate = DateTime.Now;

        _context.SaveChanges();

        // 觸發事件
        ServiceEventBus.Instance.RaiseActualTimeUpdated(projectTestItemId);
    }
}
```

### 3.3 測試操作邏輯

```csharp
public class TestService
{
    /// 
    /// 記錄測試操作（Start/Pause/Resume/End）
    /// 
    public async Task RecordTestAction(int scheduleId, string actionType, string userId)
    {
        var schedule = await _context.Schedules.FindAsync(scheduleId);
        if (schedule == null)
            return Result.Fail("排程不存在");

        // 狀態機檢查
        var currentStatus = Enum.Parse(schedule.Status);
        var isValidTransition = ValidateActionForStatus(currentStatus, actionType);
        
        if (!isValidTransition)
            return Result.Fail($"當前狀態 {currentStatus} 不允許執行 {actionType}");

        // 檢查是否有未完成的測試
        if (actionType == "Start")
        {
            var hasUnfinished = await HasUnfinishedTest(userId);
            if (hasUnfinished)
                return Result.Fail("您有未完成的測試，請先處理");
        }

        // 記錄 TestLog
        var testLog = new TestLog
        {
            ScheduleId = scheduleId,
            ActionType = actionType,
            ActionTime = DateTime.Now,
            UserId = userId,
            IsModifiable = true
        };
        _context.TestLogs.Add(testLog);

        // 更新 Schedule 狀態
        schedule.Status = GetNewStatusForAction(actionType);
        schedule.ModifiedBy = userId;
        schedule.ModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        // 計算實際時間
        CalculateActualTimeForTestItem(schedule.ProjectTestItemId);

        // 更新測項狀態
        await UpdateTestItemStatus(schedule.ProjectTestItemId);

        // 觸發事件
        ServiceEventBus.Instance.RaiseTestActionRecorded(scheduleId, actionType);

        return Result.Success();
    }

    /// 
    /// 檢查工程師是否有未完成的測試
    /// 
    public async Task HasUnfinishedTest(string engineerId)
    {
        var lastLog = await _context.TestLogs
            .Where(tl => tl.UserId == engineerId)
            .OrderByDescending(tl => tl.ActionTime)
            .FirstOrDefaultAsync();

        return lastLog != null && 
               (lastLog.ActionType == "Start" || lastLog.ActionType == "Resume");
    }

    /// 
    /// 自動補時
    /// 
    public async Task AutoFillEndTime(int scheduleId, DateTime suggestedEndTime, string userId, string reason)
    {
        var lastLog = await _context.TestLogs
            .Where(tl => tl.ScheduleId == scheduleId)
            .OrderByDescending(tl => tl.ActionTime)
            .FirstOrDefaultAsync();

        if (lastLog == null || lastLog.ActionType == "End")
            return Result.Fail("沒有需要補時的記錄");

        // 檢查是否在 7 天內
        if ((DateTime.Now - lastLog.ActionTime).TotalDays > 7)
            return Result.Fail("超過 7 天的記錄無法修改");

        var autoFillLog = new TestLog
        {
            ScheduleId = scheduleId,
            ActionType = "AutoFill",
            ActionTime = suggestedEndTime,
            UserId = userId,
            Notes = $"自動補時（原始最後操作：{lastLog.ActionTime:yyyy-MM-dd HH:mm}）。原因：{reason}",
            IsModifiable = true
        };
        _context.TestLogs.Add(autoFillLog);

        await _context.SaveChangesAsync();

        // 重新計算實際時間
        var schedule = await _context.Schedules.FindAsync(scheduleId);
        CalculateActualTimeForTestItem(schedule.ProjectTestItemId);

        return Result.Success();
    }

    /// 
    /// 更新測項狀態（基於所有 Schedule 的狀態）
    /// 
    private async Task UpdateTestItemStatus(int projectTestItemId)
    {
        var schedules = await _context.Schedules
            .Where(s => s.ProjectTestItemId == projectTestItemId && !s.IsDeleted)
            .ToListAsync();

        if (!schedules.Any()) return;

        var testItem = await _context.ProjectTestItems.FindAsync(projectTestItemId);
        if (testItem == null) return;

        var scheduleStatuses = schedules.Select(s => s.Status).ToList();

        // 至少一個在測試中 → 測項為測試中
        if (scheduleStatuses.Any(s => s == "InProgress" || s == "Paused"))
        {
            testItem.Status = "InProgress";
        }
        // 所有場地都完成 → 測項為待審查
        else if (scheduleStatuses.All(s => s == "Completed" || s == "Review"))
        {
            testItem.Status = "UnderReview";
        }
        // 至少一個已排程 → 測項為已排程
        else if (scheduleStatuses.Any(s => s == "Scheduled"))
        {
            testItem.Status = "Scheduled";
        }

        await _context.SaveChangesAsync();

        // 更新專案狀態
        _projectService.UpdateProjectStatus(testItem.ProjectId);
    }

    /// 
    /// 驗證動作是否符合當前狀態
    /// 
    private bool ValidateActionForStatus(ScheduleStatus status, string actionType)
    {
        return (status, actionType) switch
        {
            (ScheduleStatus.Scheduled, "Start") => true,
            (ScheduleStatus.InProgress, "Pause") => true,
            (ScheduleStatus.InProgress, "End") => true,
            (ScheduleStatus.Paused, "Resume") => true,
            (ScheduleStatus.Paused, "End") => true,
            _ => false
        };
    }

    /// 
    /// 取得執行動作後的新狀態
    /// 
    private string GetNewStatusForAction(string actionType)
    {
        return actionType switch
        {
            "Start" => "InProgress",
            "Resume" => "InProgress",
            "Pause" => "Paused",
            "End" => "Completed",
            _ => throw new ArgumentException($"Unknown action type: {actionType}")
        };
    }
}
```

### 3.4 派案邏輯⭐ 兩個入口統一處理

```csharp
public class ScheduleService
{
    /// 
    /// 建立排程（兩個入口共用）
    /// 入口 A：日曆拖曳（有場地、時間）
    /// 入口 B：未派案區（無場地、時間）
    /// 
    public async Task<Result> CreateSchedule(CreateScheduleDto dto, string createdBy)
    {
        // 驗證測項是否存在
        var testItem = await _context.ProjectTestItems.FindAsync(dto.ProjectTestItemId);
        if (testItem == null)
            return Result.Fail("測項不存在");

        var schedule = new Schedule
        {
            ProjectId = testItem.ProjectId,
            ProjectTestItemId = dto.ProjectTestItemId,
            ResourceId = dto.ResourceId,
            ScheduleType = dto.ScheduleType ?? "Case",
            EstimatedStart = dto.EstimatedStart,
            EstimatedEnd = dto.EstimatedEnd,
            OriginalEstimatedStart = dto.EstimatedStart, // 第一次設定時同時寫入原始預估
            OriginalEstimatedEnd = dto.EstimatedEnd,
            Status = dto.EstimatedStart.HasValue ? "Scheduled" : "InQueue",
            Notes = dto.Notes,
            CreatedBy = createdBy,
            CreatedDate = DateTime.Now
        };

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();

        // 指派工程師
        if (dto.EngineerIds != null && dto.EngineerIds.Any())
        {
            foreach (var engineerId in dto.EngineerIds)
            {
                _context.ScheduleEngineers.Add(new ScheduleEngineer
                {
                    ScheduleId = schedule.ScheduleId,
                    EngineerId = engineerId,
                    AssignedBy = createdBy,
                    AssignedDate = DateTime.Now
                });
            }
            await _context.SaveChangesAsync();
        }

        // 更新測項狀態
        if (schedule.Status == "Scheduled")
        {
            testItem.Status = "Scheduled";
            await _context.SaveChangesAsync();
            _projectService.UpdateProjectStatus(testItem.ProjectId);
        }

        // 發送通知（非同步，不阻塞主流程）
        if (schedule.Status == "Scheduled")
        {
            _ = Task.Run(() => _notificationService.NotifyScheduleAssigned(schedule.ScheduleId));
        }

        // 觸發事件
        ServiceEventBus.Instance.RaiseScheduleCreated(schedule);

        return Result.Success(schedule.ScheduleId);
    }

    /// 
    /// 指派場地和時間（將未派案轉為已排程）
    /// 
    public async Task AssignSchedule(int scheduleId, int resourceId, DateTime start, DateTime end, string modifiedBy)
    {
        var schedule = await _context.Schedules.FindAsync(scheduleId);
        if (schedule == null)
            return Result.Fail("排程不存在");

        if (schedule.Status != "InQueue")
            return Result.Fail("只能指派待派案的排程");

        // 記錄預估時間調整
        if (schedule.EstimatedStart.HasValue)
        {
            _context.EstimateHistories.Add(new EstimateHistory
            {
                ScheduleId = scheduleId,
                OldStart = schedule.EstimatedStart,
                OldEnd = schedule.EstimatedEnd,
                NewStart = start,
                NewEnd = end,
                Reason = "從未派案區指派場地和時間",
                ModifiedBy = modifiedBy,
                ModifiedDate = DateTime.Now
            });
        }

        schedule.ResourceId = resourceId;
        schedule.EstimatedStart = start;
        schedule.EstimatedEnd = end;
        schedule.Status = "Scheduled";
        schedule.ModifiedBy = modifiedBy;
        schedule.ModifiedDate = DateTime.Now;

        // 第一次設定時寫入原始預估
        if (!schedule.OriginalEstimatedStart.HasValue)
        {
            schedule.OriginalEstimatedStart = start;
            schedule.OriginalEstimatedEnd = end;
        }

        await _context.SaveChangesAsync();

        // 更新測項狀態
        var testItem = await _context.ProjectTestItems.FindAsync(schedule.ProjectTestItemId);
        testItem.Status = "Scheduled";
        await _context.SaveChangesAsync();
        _projectService.UpdateProjectStatus(testItem.ProjectId);

        // 發送通知
        _ = Task.Run(() => _notificationService.NotifyScheduleAssigned(scheduleId));

        // 觸發事件
        ServiceEventBus.Instance.RaiseScheduleUpdated(schedule);

        return Result.Success();
    }

    /// 
    /// 修改預估時間（不覆蓋原始預估）
    /// 
    public async Task UpdateEstimatedTime(int scheduleId, DateTime newStart, DateTime newEnd, string reason, string modifiedBy)
    {
        var schedule = await _context.Schedules.FindAsync(scheduleId);
        if (schedule == null)
            return Result.Fail("排程不存在");

        // 記錄調整歷史
        _context.EstimateHistories.Add(new EstimateHistory
        {
            ScheduleId = scheduleId,
            OldStart = schedule.EstimatedStart,
            OldEnd = schedule.EstimatedEnd,
            NewStart = newStart,
            NewEnd = newEnd,
            Reason = reason,
            ModifiedBy = modifiedBy,
            ModifiedDate = DateTime.Now
        });

        // 更新預估時間（不覆蓋原始）
        schedule.EstimatedStart = newStart;
        schedule.EstimatedEnd = newEnd;
        schedule.ModifiedBy = modifiedBy;
        schedule.ModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        // 觸發事件
        ServiceEventBus.Instance.RaiseScheduleUpdated(schedule);

        return Result.Success();
    }
}
```

### 3.5 審查邏輯⭐ 包含審查輪次

```csharp
public class ReviewService
{
    /// 
    /// 處理審查（通過/退回）
    /// 
    public async Task ProcessReview(int projectTestItemId, bool approved, string comment, string reviewedBy)
    {
        var testItem = await _context.ProjectTestItems.FindAsync(projectTestItemId);
        if (testItem == null)
            return Result.Fail("測項不存在");

        if (testItem.Status != "UnderReview")
            return Result.Fail("只能審查待審查狀態的測項");

        // 計算審查輪次
        var reviewRound = await _context.ReviewRecords
            .Where(r => r.ProjectTestItemId == projectTestItemId)
            .CountAsync() + 1;

        // 記錄審查結果
        var reviewRecord = new ReviewRecord
        {
            ProjectId = testItem.ProjectId,
            ProjectTestItemId = projectTestItemId,
            ReviewRound = reviewRound,
            ReviewResult = approved ? "Approved" : "Returned",
            ReviewComment = comment,
            ReviewedBy = reviewedBy,
            ReviewedDate = DateTime.Now,
            SubmittedAt = DateTime.Now // 送審時間快照
        };
        _context.ReviewRecords.Add(reviewRecord);

        // 更新測項狀態
        testItem.Status = approved ? "Completed" : "Returned";
        await _context.SaveChangesAsync();

        // 更新專案狀態
        _projectService.UpdateProjectStatus(testItem.ProjectId);

        // 發送通知（非同步）
        _ = Task.Run(() => _notificationService.NotifyReviewCompleted(
            testItem.ProjectId,
            approved ? "審查通過" : "退回修正"
        ));

        // 觸發事件
        ServiceEventBus.Instance.RaiseReviewCompleted(projectTestItemId, approved);

        return Result.Success();
    }

    /// 
    /// 取得待審查清單
    /// 
    public async Task<List> GetPendingReviews()
    {
        var items = await _context.ProjectTestItems
            .Where(pti => pti.Status == "UnderReview")
            .Include(pti => pti.Project)
            .Include(pti => pti.TestItem)
            .Select(pti => new ReviewItemDto
            {
                ProjectTestItemId = pti.ProjectTestItemId,
                ProjectId = pti.ProjectId,
                ProjectName = pti.Project.ProjectName,
                TestItemName = pti.TestItem != null ? pti.TestItem.TestItemName : pti.OtherTestItemText,
                Priority = pti.Project.Priority,
                SubmittedDate = pti.Schedules
                    .SelectMany(s => s.TestLogs)
                    .Where(tl => tl.ActionType == "End")
                    .Max(tl => (DateTime?)tl.ActionTime)
            })
            .ToListAsync();

        return items;
    }
}
```

---

## 4️⃣ UI 設計與同步機制

### 4.1 ServiceEventBus（事件總線）

```csharp
/// 
/// 單例事件總線，用於視窗間通訊
/// 
public class ServiceEventBus
{
    private static readonly Lazy _instance = 
        new Lazy(() => new ServiceEventBus());
    
    public static ServiceEventBus Instance => _instance.Value;

    private ServiceEventBus() { }

    // 專案事件
    public event EventHandler ProjectCreated;
    public event EventHandler ProjectUpdated;
    public event EventHandler ProjectStatusChanged;

    // 排程事件
    public event EventHandler ScheduleCreated;
    public event EventHandler ScheduleUpdated;
    public event EventHandler ScheduleDeleted;

    // 測試事件
    public event EventHandler TestActionRecorded;
    public event EventHandler ActualTimeUpdated;

    // 審查事件
    public event EventHandler ReviewCompleted;

    // 觸發方法
    public void RaiseProjectCreated(Project project) => 
        ProjectCreated?.Invoke(this, new ProjectEventArgs { Project = project });

    public void RaiseProjectStatusChanged(Project project) => 
        ProjectStatusChanged?.Invoke(this, new ProjectEventArgs { Project = project });

    public void RaiseScheduleCreated(Schedule schedule) => 
        ScheduleCreated?.Invoke(this, new ScheduleEventArgs { Schedule = schedule });

    public void RaiseScheduleUpdated(Schedule schedule) => 
        ScheduleUpdated?.Invoke(this, new ScheduleEventArgs { Schedule = schedule });

    public void RaiseTestActionRecorded(int scheduleId, string actionType) => 
        TestActionRecorded?.Invoke(this, new TestActionEventArgs { ScheduleId = scheduleId, ActionType = actionType });

    public void RaiseActualTimeUpdated(int projectTestItemId) => 
        ActualTimeUpdated?.Invoke(this, new ActualTimeEventArgs { ProjectTestItemId = projectTestItemId });

    public void RaiseReviewCompleted(int projectTestItemId, bool approved) => 
        ReviewCompleted?.Invoke(this, new ReviewEventArgs { ProjectTestItemId = projectTestItemId, Approved = approved });
}
```

### 4.2 Calendar Window 設計

```csharp
public partial class CalendarWindow : DevExpress.XtraEditors.XtraForm
{
    private readonly ScheduleService _scheduleService;
    private readonly ResourceService _resourceService;
    private readonly string _currentUserId;
    private readonly string _currentUserRole;

    public CalendarWindow(string userId, string userRole)
    {
        InitializeComponent();
        _currentUserId = userId;
        _currentUserRole = userRole;

        // 訂閱事件
        ServiceEventBus.Instance.ScheduleCreated += OnScheduleCreated;
        ServiceEventBus.Instance.ScheduleUpdated += OnScheduleUpdated;
        ServiceEventBus.Instance.ScheduleDeleted += OnScheduleDeleted;
        ServiceEventBus.Instance.TestActionRecorded += OnTestActionRecorded;
    }

    private void CalendarWindow_Load(object sender, EventArgs e)
    {
        // 載入場地（依權限過濾）
        LoadResources();
        
        // 載入排程
        LoadSchedules();
        
        // 載入未派案區
        LoadUnassignedSchedules();
    }

    private void LoadResources()
    {
        var resources = _resourceService.GetResourcesByUser(_currentUserId);
        
        schedulerControl.ResourceDataSource = resources.Select(r => new
        {
            Id = r.ResourceId,
            Name = r.ResourceName,
            Color = GetColorForResourceType(r.ResourceType)
        }).ToList();
    }

    private void LoadSchedules()
    {
        var schedules = _scheduleService.GetSchedulesByUser(_currentUserId);
        
        schedulerControl.AppointmentDataSource = schedules.Select(s => new
        {
            Id = s.ScheduleId,
            Subject = $"{s.ProjectId} - {s.ProjectName}",
            StartTime = s.EstimatedStart,
            EndTime = s.EstimatedEnd,
            ResourceId = s.ResourceId,
            Status = s.Status,
            Color = GetColorForStatus(s.Status)
        }).ToList();
    }

    private void LoadUnassignedSchedules()
    {
        var unassigned = _scheduleService.GetUnassignedSchedules(_currentUserId);
        gridControlUnassigned.DataSource = unassigned;
    }

    // 事件處理
    private void OnScheduleCreated(object sender, ScheduleEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnScheduleCreated(sender, e)));
            return;
        }

        // 判斷是否為未派案
        if (e.Schedule.Status == "InQueue")
            LoadUnassignedSchedules();
        else
            LoadSchedules();
    }

    private void OnScheduleUpdated(object sender, ScheduleEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnScheduleUpdated(sender, e)));
            return;
        }

        LoadSchedules();
        LoadUnassignedSchedules();
    }

    // Scheduler 拖曳建立
    private void schedulerControl_AppointmentInserted(object sender, PersistentObjectsEventArgs e)
    {
        var appointment = e.Objects[0] as Appointment;
        if (appointment == null) return;

        // 彈出對話框選擇案件、測項
        using (var dialog = new CreateScheduleDialog())
        {
            dialog.ResourceId = (int)appointment.ResourceId;
            dialog.EstimatedStart = appointment.Start;
            dialog.EstimatedEnd = appointment.End;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var dto = new CreateScheduleDto
                {
                    ProjectTestItemId = dialog.SelectedTestItemId,
                    ResourceId = dialog.ResourceId,
                    EstimatedStart = dialog.EstimatedStart,
                    EstimatedEnd = dialog.EstimatedEnd,
                    EngineerIds = dialog.SelectedEngineerIds,
                    Notes = dialog.Notes
                };

                _scheduleService.CreateSchedule(dto, _currentUserId);
            }
        }
    }

    // 指派未派案排程
    private void btnAssignSchedule_Click(object sender, EventArgs e)
    {
        var schedule = gridViewUnassigned.GetFocusedRow() as ScheduleDto;
        if (schedule == null) return;

        using (var dialog = new AssignScheduleDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _scheduleService.AssignSchedule(
                    schedule.ScheduleId,
                    dialog.SelectedResourceId,
                    dialog.EstimatedStart,
                    dialog.EstimatedEnd,
                    _currentUserId
                );
            }
        }
    }
}
```

### 4.3 ProjectMgmt Window 設計

```csharp
public partial class ProjectMgmtWindow : DevExpress.XtraEditors.XtraForm
{
    private readonly ProjectService _projectService;

    public ProjectMgmtWindow()
    {
        InitializeComponent();

        // 訂閱事件
        ServiceEventBus.Instance.ProjectCreated += OnProjectCreated;
        ServiceEventBus.Instance.ProjectUpdated += OnProjectUpdated;
        ServiceEventBus.Instance.ProjectStatusChanged += OnProjectStatusChanged;
    }

    private void ProjectMgmtWindow_Load(object sender, EventArgs e)
    {
        LoadProjects();
    }

    private void LoadProjects()
    {
        var projects = _projectService.GetAllProjects();
        gridControlProjects.DataSource = projects;
    }

    private void btnNewProject_Click(object sender, EventArgs e)
    {
        using (var dialog = new ProjectEditDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Service 會自動觸發 ProjectCreated 事件
                var dto = new CreateProjectDto
                {
                    ProjectId = dialog.ProjectId,
                    ProjectName = dialog.ProjectName,
                    Priority = dialog.Priority,
                    Notes = dialog.Notes,
                    RegulationIds = dialog.SelectedRegulationIds,
                    TestItemIds = dialog.SelectedTestItemIds
                };

                _projectService.CreateProject(dto, CurrentUserId);
            }
        }
    }

    private void gridViewProjects_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        var project = gridViewProjects.GetFocusedRow() as ProjectDto;
        if (project == null) return;

        // 顯示右側詳細資訊
        LoadProjectDetails(project.ProjectId);
    }

    private void LoadProjectDetails(string projectId)
    {
        var details = _projectService.GetProjectDetails(projectId);
        
        // 顯示法規
        gridControlRegulations.DataSource = details.Regulations;
        
        // 顯示測項
        gridControlTestItems.DataSource = details.TestItems;
    }

    // 事件處理
    private void OnProjectCreated(object sender, ProjectEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnProjectCreated(sender, e)));
            return;
        }

        LoadProjects();
    }

    private void OnProjectStatusChanged(object sender, ProjectEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnProjectStatusChanged(sender, e)));
            return;
        }

        // 刷新特定專案的狀態
        gridViewProjects.RefreshRow(gridViewProjects.FocusedRowHandle);
    }
}
```

---

## 5️⃣ 通知機制設計⭐ 非同步處理

```csharp
public interface IEmailService
{
    Task SendEmailAsync(List recipients, string subject, string body);
}

public class NotificationService
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;

    public NotificationService(AppDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    /// 
    /// 派案通知（非同步執行，不阻塞主流程）
    /// 
    public async Task NotifyScheduleAssigned(int scheduleId)
    {
        try
        {
            var schedule = await _context.Schedules
                .Include(s => s.Project)
                .Include(s => s.Resource)
                .Include(s => s.ProjectTestItem)
                    .ThenInclude(pti => pti.TestItem)
                .Include(s => s.ScheduleEngineers)
                    .ThenInclude(se => se.Engineer)
                .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);

            if (schedule == null) return;

            List recipients = new();

            // 判斷通知對象
            if (schedule.ScheduleEngineers.Any())
            {
                // 有指定工程師 → 只通知被指派的人
                recipients = schedule.ScheduleEngineers
                    .Select(se => se.Engineer.Email)
                    .ToList();
            }
            else
            {
                // 未指定工程師 → 通知該場地所有工程師
                recipients = await _context.ResourceEngineers
                    .Where(re => re.ResourceId == schedule.ResourceId && re.IsActive)
                    .Select(re => re.Engineer.Email)
                    .ToListAsync();
            }

            if (!recipients.Any()) return;

            var testItemName = schedule.ProjectTestItem.TestItem != null
                ? schedule.ProjectTestItem.TestItem.TestItemName
                : schedule.ProjectTestItem.OtherTestItemText;

            var subject = $"【新派案通知】{schedule.Project.ProjectName}";
            var body = $@"
您好，

您有新的測試任務已派案：

案件編號：{schedule.ProjectId}
案件名稱：{schedule.Project.ProjectName}
測試項目：{testItemName}
測試場地：{schedule.Resource.ResourceName}
預估時間：{schedule.EstimatedStart:yyyy-MM-dd HH:mm} ~ {schedule.EstimatedEnd:yyyy-MM-dd HH:mm}
優先級：{schedule.Project.Priority}

請登入系統查看詳細資訊並開始測試。

此為系統自動發送郵件，請勿直接回覆。
";

            await _emailService.SendEmailAsync(recipients, subject, body);

            // 記錄通知（可選）
            await LogNotification(scheduleId, "ScheduleAssigned", string.Join(",", recipients));
        }
        catch (Exception ex)
        {
            // 通知失敗不應影響主流程
            // 記錄錯誤日誌
            Console.WriteLine($"通知發送失敗：{ex.Message}");
        }
    }

    /// 
    /// 送審通知
    /// 
    public async Task NotifyReviewNeeded(int projectTestItemId)
    {
        try
        {
            var testItem = await _context.ProjectTestItems
                .Include(pti => pti.Project)
                .Include(pti => pti.TestItem)
                .FirstOrDefaultAsync(pti => pti.ProjectTestItemId == projectTestItemId);

            if (testItem == null) return;

            // 取得所有 Reviewer
            var reviewers = await _context.Users
                .Where(u => u.Role.RoleName == "Reviewer" && u.IsActive)
                .Select(u => u.Email)
                .ToListAsync();

            if (!reviewers.Any()) return;

            var testItemName = testItem.TestItem != null
                ? testItem.TestItem.TestItemName
                : testItem.OtherTestItemText;

            var subject = $"【待審查案件】{testItem.Project.ProjectName}";
            var body = $@"
您好，

以下案件已完成測試，等待您的審查：

案件編號：{testItem.ProjectId}
案件名稱：{testItem.Project.ProjectName}
測試項目：{testItemName}
優先級：{testItem.Project.Priority}

請登入系統進行審查。

此為系統自動發送郵件，請勿直接回覆。
";

            await _emailService.SendEmailAsync(reviewers, subject, body);
            await LogNotification(projectTestItemId, "ReviewNeeded", string.Join(",", reviewers));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"通知發送失敗：{ex.Message}");
        }
    }

    /// 
    /// 審查結果通知
    /// 
    public async Task NotifyReviewCompleted(string projectId, string result)
    {
        try
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null) return;

            // 通知排程主管 + 該案件的現場主管
            var managers = await _context.Users
                .Where(u => (u.Role.RoleName == "Manager" || u.Role.RoleName == "FieldManager") && u.IsActive)
                .Select(u => u.Email)
                .ToListAsync();

            if (!managers.Any()) return;

            var subject = $"【審查結果】{project.ProjectName} - {result}";
            var body = $@"
您好，

案件審查結果通知：

案件編號：{project.ProjectId}
案件名稱：{project.ProjectName}
審查結果：{result}

請登入系統查看詳細資訊。

此為系統自動發送郵件，請勿直接回覆。
";

            await _emailService.SendEmailAsync(managers, subject, body);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"通知發送失敗：{ex.Message}");
        }
    }

    /// 
    /// 記錄通知（用於稽核）
    /// 
    private async Task LogNotification(int relatedId, string notificationType, string recipients)
    {
        // 可選：建立 NotificationLog 資料表記錄所有通知
        // 這裡簡化處理
    }
}
```

---

## 6️⃣ Entity Framework Core 配置

### 6.1 DbContext 設計

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    // DbSet
    public DbSet Users { get; set; }
    public DbSet Roles { get; set; }
    public DbSet Resources { get; set; }
    public DbSet ResourceEngineers { get; set; }
    public DbSet Regulations { get; set; }
    public DbSet TestItems { get; set; }
    public DbSet Projects { get; set; }
    public DbSet ProjectRegulations { get; set; }
    public DbSet ProjectTestItems { get; set; }
    public DbSet Schedules { get; set; }
    public DbSet ScheduleEngineers { get; set; }
    public DbSet TestLogs { get; set; }
    public DbSet ActualTestRecords { get; set; }
    public DbSet ProgressReports { get; set; }
    public DbSet EstimateHistories { get; set; }
    public DbSet ReviewRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User 配置
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.RoleId);
            entity.HasIndex(e => e.Area);
        });

        // Project 配置
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("Project");
            entity.HasKey(e => e.ProjectId);
            entity.Property(e => e.ProjectId).HasMaxLength(50);
            entity.Property(e => e.ProjectName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Status);
        });

        // ProjectRegulation 配置
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("ProjectRegulation");
            entity.HasKey(e => e.ProjectRegulationId);
            
            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ProjectTestItem 配置⭐
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("ProjectTestItem");
            entity.HasKey(e => e.ProjectTestItemId);
            
            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.ProjectId);
            entity.HasIndex(e => e.Status);
        });

        // Schedule 配置⭐
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("Schedule");
            entity.HasKey(e => e.ScheduleId);
            
            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ProjectTestItemId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ResourceId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(e => e.ProjectTestItemId);
            entity.HasIndex(e => e.ResourceId);
            entity.HasIndex(e => e.Status);
        });

        // ScheduleEngineer 配置
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("ScheduleEngineer");
            entity.HasKey(e => e.ScheduleEngineerId);
            
            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.ScheduleId, e.EngineerId }).IsUnique();
        });

        // ResourceEngineer 配置
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("ResourceEngineer");
            entity.HasKey(e => e.ResourceEngineerId);
            entity.HasIndex(e => e.ResourceId);
            entity.HasIndex(e => e.EngineerId);
        });

        // TestLog 配置
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("TestLog");
            entity.HasKey(e => e.TestLogId);
            entity.HasIndex(e => e.ScheduleId);
            entity.HasIndex(e => e.ActionTime);
        });

        // ActualTestRecord 配置⭐
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("ActualTestRecord");
            entity.HasKey(e => e.ActualTestRecordId);
            
            entity.HasOne()
                .WithMany()
                .HasForeignKey(e => e.ProjectTestItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.ProjectTestItemId).IsUnique();
        });

        // ReviewRecord 配置⭐
        modelBuilder.Entity(entity =>
        {
            entity.ToTable("ReviewRecord");
            entity.HasKey(e => e.ReviewRecordId);
            entity.HasIndex(e => e.ProjectTestItemId);
            entity.HasIndex(e => e.ReviewedDate);
        });
    }
}
```

---

## 7️⃣ 開發檢查清單（MVP v1.0）

### Phase 1: 資料庫與基礎設施（第 1-2 週）
- [ ] 建立 SQL Server 資料庫
- [ ] 執行所有資料表建立 SQL
- [ ] 建立測試資料（User, Role, Resource, Regulation, TestItem）
- [ ] 配置 EF Core DbContext
- [ ] 測試資料庫連線與基本 CRUD

### Phase 2: 核心業務邏輯（第 3-4 週）
- [ ] 實作 ProjectService（CreateProject, GetProjectDetails, UpdateProjectStatus）
- [ ] 實作 ScheduleService（CreateSchedule, AssignSchedule, UpdateEstimatedTime）
- [ ] 實作 TestService（RecordTestAction, CalculateActualTime, AutoFillEndTime）
- [ ] 實作 ReviewService（ProcessReview, GetPendingReviews）
- [ ] 實作 ResourceService（GetResourcesByUser）
- [ ] 實作 NotificationService（NotifyScheduleAssigned, NotifyReviewNeeded）
- [ ] 實作 ServiceEventBus（所有事件定義與觸發）
- [ ] 單元測試核心邏輯

### Phase 3: UI 開發（第 5-6 週）
- [ ] MainForm（MDI 容器 + Ribbon + 權限控制）
- [ ] CalendarWindow（SchedulerControl + 場地過濾 + 拖曳建立排程）
- [ ] 未派案區（GridControl + 指派功能）
- [ ] ProjectMgmtWindow（Grid + 新增/編輯案件）
- [ ] ProjectEditDialog（案件編輯對話框 + 法規/測項多選）
- [ ] CreateScheduleDialog（排程建立對話框）
- [ ] AssignScheduleDialog（指派場地/時間對話框）
- [ ] TestActionPanel（測試操作按鈕 + 狀態顯示）
- [ ] ReviewListWindow（Reviewer 待審查清單）

### Phase 4: 整合測試（第 7 週）
- [ ] 測試完整流程：開案 → 派案 → 測試 → 送審 → 審查
- [ ] 測試多場地同步測試（同一測項在不同場地）
- [ ] 測試狀態機轉換（所有狀態流轉）
- [ ] 測試自動補時功能
- [ ] 測試通知機制（Email 發送）
- [ ] 測試視窗同步（EventBus）
- [ ] 測試權限控制（場地權限、角色權限）

### Phase 5: 部署與交付（第 8 週）
- [ ] 建立生產環境資料庫
- [ ] 部署應用程式
- [ ] 使用者培訓
- [ ] 撰寫使用手冊
- [ ] 收集反饋並修正 Bug

---

## 8️⃣ 關鍵設計決策說明

### 8.1 為什麼 ProjectTestItem 是主狀態機？

**理由**：
- 案件（Project）可以包含多個測項
- 每個測項有自己的生命週期（待派案 → 測試 → 審查 → 完成）
- Project.Status 是衍生計算欄位，避免與測項狀態矛盾

**實務例子**：
```
案件 A 包含：
  - 測項 1（Conducted）：已完成
  - 測項 2（Radiated）：測試中
  → Project.Status 應該是「測試中」而非「已完成」
```

### 8.2 為什麼 Schedule.Completed ≠ ProjectTestItem.Completed？

**理由**：
- Schedule 是「場地層級」的狀態
- ProjectTestItem 是「測項層級」的狀態
- 同一測項可能在多個場地測試，所有場地都完成才算測項完成

**實務例子**：
```
測項 A 在兩個場地測試：
  - Schedule 1（SAC1）：Completed（該場地完成）
  - Schedule 2（SAC2）：InProgress（該場地還在測）
  → ProjectTestItem.Status = InProgress（測項還沒完成）
```

### 8.3 為什麼需要 ReviewRound 和 SubmittedAt？

**理由**：
- 測項可能被退回後重新送審
- 需要追溯「第幾次送審」的歷史
- 避免審查記錄混淆

**實務例子**：
```
測項 A 的審查歷史：
  - Round 1：2025-01-10 送審 → 2025-01-11 退回
  - Round 2：2025-01-15 重新送審 → 2025-01-16 通過
  → 可清楚追溯每次審查的時間線
```

### 8.4 為什麼通知要非同步執行？

**理由**：
- SMTP 發送可能很慢（網路延遲）
- Email 服務可能暫時不可用
- 通知失敗不應影響主流程（派案、測試、審查）

**實作方式**：
```csharp
// ❌ 錯誤做法（同步，會阻塞）
_notificationService.NotifyScheduleAssigned(scheduleId);
await _context.SaveChangesAsync();

// ✅ 正確做法（非同步，不阻塞）
await _context.SaveChangesAsync();
_ = Task.Run(() => _notificationService.NotifyScheduleAssigned(scheduleId));
```

---

## 9️⃣ 潛在風險與建議

### 9.1 效能風險

**問題**：多場地同步測試時，每次 TestLog 新增都會觸發 `CalculateActualTime`，可能造成效能瓶頸。

**建議**：
- v1.0 先不優化，觀察實際使用情況
- v1.1 可考慮批次計算或快取機制

### 9.2 並發風險

**問題**：多位工程師同時操作同一排程時，可能產生並發衝突。

**建議**：
- v1.0 使用資料庫交易（EF Core 預設）
- v1.1 可考慮樂觀鎖（Optimistic Concurrency）

### 9.3 Email 發送失敗

**問題**：SMTP 服務不穩定，通知可能失敗。

**建議**：
- 記錄通知失敗日誌
- v1.1 可考慮訊息佇列（Message Queue）重試機制

---

## 🔟 附錄

### 附錄 A：資料表大小預估

假設：
- 每年 500 個案件
- 每個案件平均 3 個測項
- 每個測項平均 2 個場地
- 每個場地平均 10 筆 TestLog

**5 年資料量預估**：
- Project：2,500 筆
- ProjectTestItem：7,500 筆
- Schedule：15,000 筆
- TestLog：150,000 筆

**結論**：資料量屬於中小型，SQL Server 標準配置可應對。

### 附錄 B：DevExpress SchedulerControl 設定重點

```csharp
// 設定資源（場地）
schedulerControl.Storage.Resources.DataSource = resources;
schedulerControl.Storage.Resources.Mappings.Id = "Id";
schedulerControl.Storage.Resources.Mappings.Caption = "Name";

// 設定 Appointment（排程）
schedulerControl.Storage.Appointments.DataSource = schedules;
schedulerControl.Storage.Appointments.Mappings.AppointmentId = "Id";
schedulerControl.Storage.Appointments.Mappings.Start = "StartTime";
schedulerControl.Storage.Appointments.Mappings.End = "EndTime";
schedulerControl.Storage.Appointments.Mappings.Subject = "Subject";
schedulerControl.Storage.Appointments.Mappings.ResourceId = "ResourceId";
schedulerControl.Storage.Appointments.Mappings.Status = "Status";

// 設定視圖
schedulerControl.ActiveViewType = SchedulerViewType.Month;
schedulerControl.Views.DayView.Enabled = true;
schedulerControl.Views.WeekView.Enabled = true;
schedulerControl.Views.MonthView.Enabled = true;
```

### 附錄 C：狀態顏色對照表

| 狀態 | 顏色代碼 | DevExpress Color |
|------|----------|------------------|
| 待派案 | #3498DB | Blue |
| 已排程 | #F1C40F | Yellow |
| 測試中 | #2ECC71 | Green |
| 暫停中 | #E67E22 | Orange |
| 逾期 | #E74C3C | Red |
| 待審查 | #9B59B6 | Purple |
| 已完成 | #95A5A6 | Gray |

---

## 📌 總結

本文件提供了 RF 測試排程系統 v1.0 的完整設計，包含：

1. ✅ 修正後的資料庫設計（ProjectTestItem 為主狀態機）
2. ✅ 清晰的狀態機邏輯（Schedule ≠ ProjectTestItem ≠ Project）
3. ✅ 多場地同步測試的時間計算
4. ✅ 非同步通知機制
5. ✅ UI 同步架構（ServiceEventBus）
6. ✅ 審查輪次追蹤（ReviewRound + SubmittedAt）

**本文件可直接作為實作藍本使用。**

如有任何疑問或需要調整，請隨時提出！
                
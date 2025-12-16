  # 📙 RF案件排程系統 — 系統設計文件 (SD v4.0)

---

## 2. 資料庫設計

### 2.1 核心資料表設計

#### 2.1.1 User (使用者)

```sql
CREATE TABLE [dbo].[User] (
    [UserId]                INT IDENTITY(1,1) NOT NULL,
    [Account]               NVARCHAR(50)   NOT NULL,  -- 顯示帳號
    [PasswordHash]          NVARCHAR(255)  NULL,      -- Local 才使用
    [DisplayName]           NVARCHAR(100)  NOT NULL,
    [Email]                 NVARCHAR(255)  NOT NULL,  -- 唯一識別(Local/AD)
    [RoleId]                INT            NOT NULL,  -- FK → Role
    
    [WeeklyAvailableHours]  DECIMAL(5,2)   NOT NULL DEFAULT 37.5,
    [IsActive]              BIT            NOT NULL DEFAULT 1,  -- 啟用/停用

    -- AD 支援欄位
    [AuthType]              NVARCHAR(20)   NOT NULL DEFAULT 'Local',  -- Local/AD
    [ADAccount]             NVARCHAR(100)  NULL,
    [ADDomain]              NVARCHAR(100)  NULL,

    -- 登入紀錄欄位
    [LastLoginDate]         DATETIME       NULL,
    [LastLoginIP]           NVARCHAR(50)   NULL,

    -- 審計欄位
    [CreatedByUserId]       INT            NULL,
    [CreatedDate]           DATETIME       NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]      INT            NULL,
    [ModifiedDate]          DATETIME       NULL,
    [RowVersion]            ROWVERSION     NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId]),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId]),
    CONSTRAINT [UQ_User_Account] UNIQUE ([Account]),
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email])  -- Email 唯一識別
);

-- Email唯一索引(活躍用戶)
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_Email] 
    ON [User]([Email]) WHERE [IsActive] = 1;

-- Account唯一索引(活躍用戶)
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_Account] 
    ON [User]([Account]) WHERE [IsActive] = 1;
```

---

#### 2.1.2 Project (案件)

```sql
CREATE TABLE [dbo].[Project] (
    [ProjectId]         INT             IDENTITY(1,1) NOT NULL,
    [ProjectName]       NVARCHAR(200)   NOT NULL,
    [Customer]          NVARCHAR(200)   NULL,
    [Priority]          NVARCHAR(20)    NOT NULL DEFAULT 'Medium',
    [Status]            NVARCHAR(20)    NOT NULL DEFAULT 'Draft',
    [StartDate]         DATE            NULL,
    [EndDate]           DATE            NULL,
    [Note]              NVARCHAR(1000)  NULL,
    
    -- 審計欄位
    [CreatedByUserId]   INT             NOT NULL,
    [CreatedDate]       DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]  INT             NULL,
    [ModifiedDate]      DATETIME        NULL,
    
    -- Soft Delete
    [IsDeleted]         BIT             NOT NULL DEFAULT 0,
    [DeletedByUserId]   INT             NULL,
    [DeletedDate]       DATETIME        NULL,
    [RowVersion]        ROWVERSION      NOT NULL,
    
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ProjectId]),
    CONSTRAINT [FK_Project_CreatedBy] FOREIGN KEY ([CreatedByUserId]) 
        REFERENCES [User]([UserId]),
    CONSTRAINT [UQ_Project_Name] UNIQUE ([ProjectName]) WHERE [IsDeleted] = 0,
    CONSTRAINT [CK_Project_Priority] CHECK ([Priority] IN ('High', 'Medium', 'Low')),
    CONSTRAINT [CK_Project_Status] CHECK ([Status] IN ('Draft', 'Active', 'Completed', 'OnHold', 'Delayed'))
);
```
---

#### 2.1.3 Regulation (法規)

```sql
CREATE TABLE [dbo].[Regulation] (
    [RegulationId]          INT             IDENTITY(1,1) NOT NULL,
    [ProjectId]             INT             NOT NULL,
    [RegulationName]        NVARCHAR(100)   NOT NULL,
    [StartDate]             DATE            NOT NULL,
    [EndDate]               DATE            NOT NULL,
    [Status]                NVARCHAR(20)    NOT NULL DEFAULT 'NotStarted',
    [ManualStatusOverride]  BIT             NOT NULL DEFAULT 0,  -- 手動狀態標記
    [Note]                  NVARCHAR(500)   NULL,
    
    -- 審計欄位
    [CreatedByUserId]       INT             NOT NULL,
    [CreatedDate]           DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]      INT             NULL,
    [ModifiedDate]          DATETIME        NULL,
    
    -- Soft Delete
    [IsDeleted]             BIT             NOT NULL DEFAULT 0,
    [DeletedByUserId]       INT             NULL,
    [DeletedDate]           DATETIME        NULL,
    
    CONSTRAINT [PK_Regulation] PRIMARY KEY CLUSTERED ([RegulationId]),
    CONSTRAINT [FK_Regulation_Project] FOREIGN KEY ([ProjectId]) 
        REFERENCES [Project]([ProjectId]),
    CONSTRAINT [CK_Regulation_Status] CHECK ([Status] IN ('NotStarted', 'InProgress', 'Completed', 'Delayed', 'OnHold'))
);
```
---

#### 2.1.4 TestItem (測試項目)

```sql
CREATE TABLE [dbo].[TestItem] (
    [TestItemId]            INT             IDENTITY(1,1) NOT NULL,
    [RegulationId]          INT             NOT NULL,
    [TestItemName]          NVARCHAR(200)   NOT NULL,
    [TestType]              NVARCHAR(100)   NOT NULL,
    [TestLocation]          NVARCHAR(100)   NOT NULL,
    [EstimatedHours]        DECIMAL(10,2)   NOT NULL,
    [Status]                NVARCHAR(20)    NOT NULL DEFAULT 'NotStarted',
    [ManualStatusOverride]  BIT             NOT NULL DEFAULT 0,  -- 手動狀態標記
    [ManagerNote]           NVARCHAR(500)   NULL,
    
    -- 審計欄位
    [CreatedByUserId]       INT             NOT NULL,
    [CreatedDate]           DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]      INT             NULL,
    [ModifiedDate]          DATETIME        NULL,
    
    -- Soft Delete
    [IsDeleted]             BIT             NOT NULL DEFAULT 0,
    [DeletedByUserId]       INT             NULL,
    [DeletedDate]           DATETIME        NULL,
    [RowVersion]            ROWVERSION      NOT NULL,
    
    CONSTRAINT [PK_TestItem] PRIMARY KEY CLUSTERED ([TestItemId]),
    CONSTRAINT [FK_TestItem_Regulation] FOREIGN KEY ([RegulationId]) 
        REFERENCES [Regulation]([RegulationId]),
    CONSTRAINT [CK_TestItem_Status] CHECK ([Status] IN ('NotStarted', 'InProgress', 'Completed', 'Delayed', 'OnHold'))
);
```

---

#### 2.1.5 TestItemRevision (補測版本) **[v4.0 新增]**

```sql
CREATE TABLE [dbo].[TestItemRevision] (
    [RevisionId]         INT IDENTITY(1,1) NOT NULL,
    [TestItemId]         INT NOT NULL,
    [RevisionNumber]     INT NOT NULL,  -- 1, 2, 3, 4...
    [RevisionType]       NVARCHAR(20) NOT NULL DEFAULT 'Command', 
                         -- Command(客訴補測) / Retest(重測) / Fix(修正) / Others(其他)
    [EstimatedHours]     DECIMAL(10,2) NOT NULL,  -- 主管預估補測工時
    [Reason]             NVARCHAR(200) NOT NULL,  -- 補測原因（Command內容/重測原因）
    [Description]        NVARCHAR(500) NULL,      -- 詳細說明
    
    -- 審計欄位
    [CreatedByUserId]    INT NOT NULL,
    [CreatedDate]        DATETIME NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]   INT NULL,
    [ModifiedDate]       DATETIME NULL,
    
    -- Soft Delete
    [IsDeleted]          BIT NOT NULL DEFAULT 0,
    [DeletedByUserId]    INT NULL,
    [DeletedDate]        DATETIME NULL,
    
    CONSTRAINT [PK_TestItemRevision] PRIMARY KEY CLUSTERED ([RevisionId]),
    CONSTRAINT [FK_TestItemRevision_TestItem] FOREIGN KEY ([TestItemId]) 
        REFERENCES [TestItem]([TestItemId]),
    CONSTRAINT [UQ_TestItemRevision] UNIQUE ([TestItemId], [RevisionNumber]) 
        WHERE [IsDeleted] = 0
);

-- 索引優化
CREATE NONCLUSTERED INDEX [IX_TestItemRevision_TestItemId] 
    ON [TestItemRevision]([TestItemId]) 
    WHERE [IsDeleted] = 0;
```

---

#### 2.1.6 TestItemEngineer (工程師分配) **[v4.0 更新]**

```sql
CREATE TABLE [dbo].[TestItemEngineer] (
    [TestItemEngineerId]      INT             IDENTITY(1,1) NOT NULL,
    [TestItemId]        INT             NOT NULL,
    [EngineerUserId]    INT             NOT NULL,
    [RoleType]          NVARCHAR(20)    NOT NULL,  -- Main1/Main2/Main3/Support
    [AssignedHours]     DECIMAL(10,2)   NOT NULL,  -- 分配工時
    
    -- 審計欄位
    [CreatedByUserId]    INT NOT NULL,
    [CreatedDate]        DATETIME NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]   INT NULL,
    [ModifiedDate]       DATETIME NULL,

    -- Soft Delete
    [IsDeleted]         BIT             NOT NULL DEFAULT 0,
    [DeletedByUserId]   INT             NULL,
    [DeletedDate]       DATETIME        NULL,
    
    CONSTRAINT [PK_TestItemEngineer] PRIMARY KEY CLUSTERED ([TestItemEngineerId]),
    CONSTRAINT [FK_TIE_TestItem] FOREIGN KEY ([TestItemId]) 
        REFERENCES [TestItem]([TestItemId]),
    CONSTRAINT [FK_TIE_Engineer] FOREIGN KEY ([EngineerUserId]) 
        REFERENCES [User]([UserId]),
    CONSTRAINT [FK_TIE_AssignedBy] FOREIGN KEY ([AssignedByUserId]) 
        REFERENCES [User]([UserId]),
    CONSTRAINT [CK_TIE_RoleType] CHECK ([RoleType] IN ('Main1', 'Main2', 'Main3', 'Support'))
);

-- 唯一約束: 同一測項不可重複分配同一工程師
CREATE UNIQUE NONCLUSTERED INDEX [UX_TestItemEngineer] 
    ON [TestItemEngineer]([TestItemId], [EngineerUserId]) 
    WHERE [IsDeleted] = 0;
```
---

#### 2.1.7 WorkLog (工時記錄)

```sql
CREATE TABLE [dbo].[WorkLog] (
    [WorkLogId]             INT             IDENTITY(1,1) NOT NULL,
    [TestItemId]            INT             NOT NULL,
    [RevisionId]            INT             NULL,  -- NULL = v1 (原始版本)
    [EngineerUserId]        INT             NOT NULL,
    [WorkDate]              DATE            NOT NULL,
    [ActualHours]           DECIMAL(10,2)   NOT NULL,
    [Status]                NVARCHAR(20)    NOT NULL,
    [Comment]               NVARCHAR(500)   NULL,
    [DelayReasonId]         INT             NULL,
    -- 審計欄位
    [CreatedByUserId]       INT             NOT NULL,
    [CreatedDate]           DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]      INT             NULL,
    [ModifiedDate]          DATETIME        NULL,
    [ModificationReason]    NVARCHAR(500)   NULL,
    
    -- Soft Delete (保留稽核軌跡)
    [IsDeleted]             BIT             NOT NULL DEFAULT 0,
    [DeletedByUserId]       INT             NULL,
    [DeletedDate]           DATETIME        NULL,
    
    CONSTRAINT [PK_WorkLog] PRIMARY KEY CLUSTERED ([WorkLogId]),
    CONSTRAINT [FK_WorkLog_TestItem] FOREIGN KEY ([TestItemId]) 
        REFERENCES [TestItem]([TestItemId]),
    CONSTRAINT [FK_WorkLog_Revision] FOREIGN KEY ([RevisionId]) 
        REFERENCES [TestItemRevision]([RevisionId]),
        ⭐ 新增：
    CONSTRAINT [FK_WorkLog_Engineer] FOREIGN KEY ([EngineerUserId])
        REFERENCES [User]([UserId]),
    -- ⭐ 新增：延遲原因 FK
    CONSTRAINT [FK_WorkLog_DelayReason] FOREIGN KEY ([DelayReasonId])
        REFERENCES [DelayReason]([DelayReasonId]),
    CONSTRAINT [CK_WorkLog_Status] CHECK ([Status] IN ('InProgress', 'Completed', 'Delayed')),
    CONSTRAINT [CK_WorkLog_ActualHours] CHECK ([ActualHours] > 0 AND [ActualHours] <= 12)
        -- ⭐ 新增：只有 Delay 時才允許 DelayReasonId
    CONSTRAINT [CK_WorkLog_DelayReason_Status] 
        CHECK (
            ([Status] <> 'Delayed' AND [DelayReasonId] IS NULL)
            OR
            ([Status] = 'Delayed' AND [DelayReasonId] IS NOT NULL)
        )
);

-- 避免同日重複回報
CREATE UNIQUE NONCLUSTERED INDEX [UX_WorkLog_UniqueDate] 
    ON [WorkLog]([TestItemId], [EngineerUserId], [WorkDate], [RevisionId]) 
    WHERE [IsDeleted] = 0;
```
---

#### 2.1.8 Role
```sql
CREATE TABLE [dbo].[Role] (
    [RoleId]            INT IDENTITY(1,1) NOT NULL,
    [RoleName]          NVARCHAR(50)  NOT NULL,
    [Description]       NVARCHAR(200) NULL,
    [IsActive]          BIT           NOT NULL DEFAULT 1,

    [CreatedByUserId]   INT           NULL,
    [CreatedDate]       DATETIME      NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]  INT           NULL,
    [ModifiedDate]      DATETIME      NULL,

    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId])
);
```

---

#### 2.1.9 DelayReason (延遲原因)
```sql
CREATE TABLE [dbo].[DelayReason] (
    [DelayReasonId]     INT             IDENTITY(1,1) NOT NULL,
    [ReasonText]        NVARCHAR(200)   NOT NULL,
    [ReasonType]        NVARCHAR(50)    NOT NULL,
    [IsActive]          BIT             NOT NULL DEFAULT 1,  -- 啟用/停用
    
    -- 審計欄位
    [CreatedByUserId]   INT             NOT NULL,
    [CreatedDate]       DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]  INT             NULL,
    [ModifiedDate]      DATETIME        NULL,
    
    CONSTRAINT [PK_DelayReason] PRIMARY KEY CLUSTERED ([DelayReasonId]),
    CONSTRAINT [CK_DelayReason_Type] CHECK ([ReasonType] IN ('Equipment', 'Customer', 'Engineer', 'Location', 'Other')),
    CONSTRAINT [UQ_DelayReason_Text] UNIQUE ([ReasonText])
);
```

**重要說明:**
- DelayReason 使用 **IsActive** 機制,不使用 IsDeleted
- 已使用的 DelayReason 不可刪除,僅能停用(IsActive = false)
- 停用後不再顯示於下拉選單,但歷史資料仍可查詢

---

#### 2.1.10 IAM 權限體系資料表

##### Permission (權限)

```sql
CREATE TABLE [dbo].[Permission] (
    [PermissionId]      INT             IDENTITY(1,1) NOT NULL,
    [PermissionCode]    NVARCHAR(100)   NOT NULL,  -- PROJECT_CREATE, WORKLOG_VIEW_ALL
    [PermissionName]    NVARCHAR(100)   NOT NULL,  -- 給 UI 顯示的「中文名稱」
    [Category]          NVARCHAR(50)    NOT NULL,  -- Project/TestItem/WorkLog/User/Report
    [Description]       NVARCHAR(200)   NULL,      -- 權限補充說明
    [IsActive]          BIT             NOT NULL DEFAULT 1,
    
    -- 審計欄位
    [CreatedByUserId]   INT             NOT NULL,
    [CreatedDate]       DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]  INT             NULL,
    [ModifiedDate]      DATETIME        NULL,
    
    CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED ([PermissionId]),
    CONSTRAINT [UQ_Permission_Code] UNIQUE ([PermissionCode])
);
```

##### PermissionGroup (權限群組)

```sql
CREATE TABLE [dbo].[PermissionGroup] (
    [GroupId]           INT             IDENTITY(1,1) NOT NULL,
    [GroupName]         NVARCHAR(50)    NOT NULL,  -- Engineer/Manager/Admin
    [Description]       NVARCHAR(200)   NULL,
    [IsActive]          BIT             NOT NULL DEFAULT 1,  -- 啟用/停用
    
    -- 審計欄位
    [CreatedByUserId]   INT             NOT NULL,
    [CreatedDate]       DATETIME        NOT NULL DEFAULT GETDATE(),
    [ModifiedByUserId]  INT             NULL,
    [ModifiedDate]      DATETIME        NULL,
    
    CONSTRAINT [PK_PermissionGroup] PRIMARY KEY CLUSTERED ([GroupId]),
    CONSTRAINT [UQ_PermissionGroup_Name] UNIQUE ([GroupName])
);
```

**重要說明:**
- PermissionGroup 使用 **IsActive** 機制
- 系統預設群組(Engineer/Manager/Admin)不可停用
- 停用後該群組不再可指派給新用戶

##### PermissionGroupMapping (群組權限對應)

```sql
CREATE TABLE [dbo].[PermissionGroupMapping] (
    [MappingId]         INT             IDENTITY(1,1) NOT NULL,
    [GroupId]           INT             NOT NULL,
    [PermissionId]      INT             NOT NULL,
    
    -- 審計欄位
    [CreatedByUserId]   INT             NOT NULL,
    [CreatedDate]       DATETIME        NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT [PK_PermissionGroupMapping] PRIMARY KEY CLUSTERED ([MappingId]),
    CONSTRAINT [FK_PGM_Group] FOREIGN KEY ([GroupId]) 
        REFERENCES [PermissionGroup]([GroupId]),
    CONSTRAINT [FK_PGM_Permission] FOREIGN KEY ([PermissionId]) 
        REFERENCES [Permission]([PermissionId]),
    CONSTRAINT [UQ_PGM] UNIQUE ([GroupId], [PermissionId])
);
```

##### UserGroup (使用者群組)

```sql
CREATE TABLE [dbo].[UserGroup] (
    [UserGroupId]       INT             IDENTITY(1,1) NOT NULL,
    [UserId]            INT             NOT NULL,
    [GroupId]           INT             NOT NULL,
    [AssignedDate]      DATETIME        NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED ([UserGroupId]),
    CONSTRAINT [FK_UG_User] FOREIGN KEY ([UserId]) 
        REFERENCES [User]([UserId]),
    CONSTRAINT [FK_UG_Group] FOREIGN KEY ([GroupId]) 
        REFERENCES [PermissionGroup]([GroupId]),
    CONSTRAINT [UQ_UserGroup] UNIQUE ([UserId], [GroupId])
);
```

##### UserPermission (使用者個別權限)

```sql
CREATE TABLE [dbo].[UserPermission] (
    [UserPermissionId]  INT             IDENTITY(1,1) NOT NULL,
    [UserId]            INT             NOT NULL,
    [PermissionId]      INT             NOT NULL,
    [GrantedByUserId]   INT             NOT NULL,
    [GrantedDate]       DATETIME        NOT NULL DEFAULT GETDATE(),
    [ExpireDate]        DATETIME        NULL,  -- NULL表示永久
    [IsActive]          BIT             NOT NULL DEFAULT 1,
    
    CONSTRAINT [PK_UserPermission] PRIMARY KEY CLUSTERED ([UserPermissionId]),
    CONSTRAINT [FK_UP_User] FOREIGN KEY ([UserId]) 
        REFERENCES [User]([UserId]),
    CONSTRAINT [FK_UP_Permission] FOREIGN KEY ([PermissionId]) 
        REFERENCES [Permission]([PermissionId]),
    CONSTRAINT [FK_UP_GrantedBy] FOREIGN KEY ([GrantedByUserId]) 
        REFERENCES [User]([UserId])
);
```

---

#### 2.1.11 AuditLog（稽核日誌）
```sql
CREATE TABLE [dbo].[AuditLog] (
    [AuditLogId]    BIGINT          IDENTITY(1,1) NOT NULL,
    [TableName]     NVARCHAR(50)    NOT NULL,      -- 被操作的資料表名稱 (例：Project, TestItem, WorkLog)
    [RecordId]      INT             NOT NULL,      -- 被操作紀錄的主鍵值 (例：TestItemId)
    [Action]        NVARCHAR(20)    NOT NULL,      -- Create / Update / Delete / StatusChange / PasswordReset
    [OldValue]      NVARCHAR(MAX)   NULL,          -- JSON：變更前的欄位值
    [NewValue]      NVARCHAR(MAX)   NULL,          -- JSON：變更後的欄位值
    [UserId]        INT             NOT NULL,      -- 執行操作的使用者
    [ModifiedDate]  DATETIME        NOT NULL DEFAULT GETDATE(), -- 操作時間
    [Reason]        NVARCHAR(500)   NULL,          -- 覆寫、刪除等需要額外說明時填寫
    
    CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED ([AuditLogId]),
    CONSTRAINT [FK_AuditLog_User] FOREIGN KEY ([UserId]) 
        REFERENCES [User]([UserId])
);
```

---

#### 2.1.12 PasswordReset（密碼重置 Token）
```sql
CREATE TABLE [dbo].[PasswordReset] (
    [PasswordResetId ]      INT              IDENTITY(1,1) NOT NULL,
    [UserId]                INT              NOT NULL,           -- 要重置密碼的使用者
    [Token]                 UNIQUEIDENTIFIER NOT NULL,           -- Guid Token，給重置連結用
    [ExpireAt]              DATETIME         NOT NULL,           -- 過期時間
    [IsUsed]                BIT              NOT NULL DEFAULT 0, -- 是否已使用
    [CreatedDate]           DATETIME         NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT [PK_PasswordReset] PRIMARY KEY CLUSTERED ([PasswordResetId]),
    CONSTRAINT [FK_PasswordReset_User] FOREIGN KEY ([UserId]) 
        REFERENCES [User]([UserId]),
    CONSTRAINT [UQ_PasswordReset_Token] UNIQUE ([Token])
);
```

---

#### 2.1.13 SystemSetting（系統設定）
```sql
CREATE TABLE [dbo].[SystemSetting] (
    [SettingId]         INT             IDENTITY(1,1) NOT NULL,
    [SettingKey]        NVARCHAR(100)   NOT NULL,      -- 例：JwtExpiryMinutes、MaxWeeklyHours、AD_Domain
    [SettingValue]      NVARCHAR(500)   NOT NULL,      -- 字串值，由應用程式自行轉型
    [Description]       NVARCHAR(200)   NULL,          -- 給管理者看的說明
    
    [ModifiedByUserId]  INT             NULL,          -- 最後修改者（可為 NULL 表示系統初始）
    [ModifiedDate]      DATETIME        NULL,
    
    CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED ([SettingId]),
    CONSTRAINT [UQ_SystemSetting_Key] UNIQUE ([SettingKey]),
    CONSTRAINT [FK_SystemSetting_ModifiedBy] FOREIGN KEY ([ModifiedByUserId])
        REFERENCES [User]([UserId])
);
```

---


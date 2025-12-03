namespace RF_Schedule
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DevExpress.XtraScheduler.TimeRuler timeRuler10 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler11 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler12 = new DevExpress.XtraScheduler.TimeRuler();
            splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            treeList1 = new DevExpress.XtraTreeList.TreeList();
            ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            btnNewProject = new DevExpress.XtraBars.BarButtonItem();
            btnImportCsv = new DevExpress.XtraBars.BarButtonItem();
            btnViewWeek = new DevExpress.XtraBars.BarButtonItem();
            btnViewMonth = new DevExpress.XtraBars.BarButtonItem();
            btnViewAllResources = new DevExpress.XtraBars.BarButtonItem();
            btnViewMyResources = new DevExpress.XtraBars.BarButtonItem();
            btnAssign = new DevExpress.XtraBars.BarButtonItem();
            btnAdjustTime = new DevExpress.XtraBars.BarButtonItem();
            btnCopySchedule = new DevExpress.XtraBars.BarButtonItem();
            btnRefreshScheduler = new DevExpress.XtraBars.BarButtonItem();
            btnShowEstimateHistory = new DevExpress.XtraBars.BarButtonItem();
            barCurrentUser = new DevExpress.XtraBars.BarStaticItem();
            btnLogout = new DevExpress.XtraBars.BarButtonItem();
            btnReviewHistory = new DevExpress.XtraBars.BarButtonItem();
            btnReviewQueue = new DevExpress.XtraBars.BarButtonItem();
            btnEngineerDailyReport = new DevExpress.XtraBars.BarButtonItem();
            btnEngineerReportList = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            repositoryItemDuration1 = new DevExpress.XtraScheduler.UI.RepositoryItemDuration();
            repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            schedulerControl = new DevExpress.XtraScheduler.SchedulerControl();
            schedulerStorage = new DevExpress.XtraScheduler.SchedulerDataStorage(components);
            calendarToolsRibbonPageCategory1 = new DevExpress.XtraScheduler.UI.CalendarToolsRibbonPageCategory();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel1).BeginInit();
            splitContainerControl.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel2).BeginInit();
            splitContainerControl.Panel2.SuspendLayout();
            splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)treeList1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDuration1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)schedulerControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)schedulerStorage).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl
            // 
            splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerControl.Location = new System.Drawing.Point(0, 237);
            splitContainerControl.Margin = new System.Windows.Forms.Padding(4);
            splitContainerControl.Name = "splitContainerControl";
            splitContainerControl.Padding = new System.Windows.Forms.Padding(9);
            // 
            // splitContainerControl.Panel1
            // 
            splitContainerControl.Panel1.Controls.Add(treeList1);
            splitContainerControl.Panel1.MinSize = 250;
            splitContainerControl.Panel1.Text = "Panel1";
            // 
            // splitContainerControl.Panel2
            // 
            splitContainerControl.Panel2.Controls.Add(schedulerControl);
            splitContainerControl.Panel2.MinSize = 600;
            splitContainerControl.Panel2.Text = "Panel2";
            splitContainerControl.Size = new System.Drawing.Size(1858, 687);
            splitContainerControl.SplitterPosition = 478;
            splitContainerControl.TabIndex = 0;
            splitContainerControl.Text = "splitContainerControl1";
            // 
            // treeList1
            // 
            treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            treeList1.Location = new System.Drawing.Point(0, 0);
            treeList1.MenuManager = ribbonControl;
            treeList1.Name = "treeList1";
            treeList1.Size = new System.Drawing.Size(478, 669);
            treeList1.TabIndex = 0;
            // 
            // ribbonControl
            // 
            ribbonControl.ApplicationButtonText = null;
            ribbonControl.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(45);
            ribbonControl.ExpandCollapseItem.Id = 0;
            ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl.ExpandCollapseItem, btnNewProject, btnImportCsv, btnViewWeek, btnViewMonth, btnViewAllResources, btnViewMyResources, btnAssign, btnAdjustTime, btnCopySchedule, btnRefreshScheduler, btnShowEstimateHistory, barCurrentUser, btnLogout, btnReviewHistory, btnReviewQueue, btnEngineerDailyReport, btnEngineerReportList });
            ribbonControl.Location = new System.Drawing.Point(0, 0);
            ribbonControl.Margin = new System.Windows.Forms.Padding(4);
            ribbonControl.MaxItemId = 124;
            ribbonControl.Name = "ribbonControl";
            ribbonControl.OptionsMenuMinWidth = 495;
            ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1, ribbonPage2, ribbonPage3, ribbonPage4, ribbonPage5 });
            ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemDuration1, repositoryItemSpinEdit1 });
            ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl.ShowToolbarCustomizeItem = false;
            ribbonControl.Size = new System.Drawing.Size(1858, 237);
            ribbonControl.Toolbar.ShowCustomizeItem = false;
            ribbonControl.Click += ribbonControl_Click;
            // 
            // btnNewProject
            // 
            btnNewProject.Caption = "新增專案";
            btnNewProject.Id = 104;
            btnNewProject.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnNewProject.ImageOptions.Image");
            btnNewProject.Name = "btnNewProject";
            // 
            // btnImportCsv
            // 
            btnImportCsv.Caption = "匯入 CSV";
            btnImportCsv.Id = 105;
            btnImportCsv.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnImportCsv.ImageOptions.Image");
            btnImportCsv.Name = "btnImportCsv";
            // 
            // btnViewWeek
            // 
            btnViewWeek.Caption = "週視圖";
            btnViewWeek.Id = 106;
            btnViewWeek.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewWeek.ImageOptions.Image");
            btnViewWeek.Name = "btnViewWeek";
            // 
            // btnViewMonth
            // 
            btnViewMonth.Caption = "月視圖";
            btnViewMonth.Id = 107;
            btnViewMonth.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewMonth.ImageOptions.Image");
            btnViewMonth.Name = "btnViewMonth";
            // 
            // btnViewAllResources
            // 
            btnViewAllResources.Caption = "全部場地";
            btnViewAllResources.Id = 108;
            btnViewAllResources.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewAllResources.ImageOptions.Image");
            btnViewAllResources.Name = "btnViewAllResources";
            // 
            // btnViewMyResources
            // 
            btnViewMyResources.Caption = "我的場地";
            btnViewMyResources.Id = 109;
            btnViewMyResources.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewMyResources.ImageOptions.Image");
            btnViewMyResources.Name = "btnViewMyResources";
            // 
            // btnAssign
            // 
            btnAssign.Caption = "派案";
            btnAssign.Id = 110;
            btnAssign.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnAssign.ImageOptions.Image");
            btnAssign.Name = "btnAssign";
            // 
            // btnAdjustTime
            // 
            btnAdjustTime.Caption = "調整時間";
            btnAdjustTime.Id = 111;
            btnAdjustTime.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnAdjustTime.ImageOptions.Image");
            btnAdjustTime.Name = "btnAdjustTime";
            // 
            // btnCopySchedule
            // 
            btnCopySchedule.Caption = "複製排程";
            btnCopySchedule.Id = 112;
            btnCopySchedule.Name = "btnCopySchedule";
            // 
            // btnRefreshScheduler
            // 
            btnRefreshScheduler.Caption = "重新整理";
            btnRefreshScheduler.Id = 113;
            btnRefreshScheduler.Name = "btnRefreshScheduler";
            // 
            // btnShowEstimateHistory
            // 
            btnShowEstimateHistory.Caption = "顯示預估變動紀錄";
            btnShowEstimateHistory.Id = 114;
            btnShowEstimateHistory.Name = "btnShowEstimateHistory";
            // 
            // barCurrentUser
            // 
            barCurrentUser.Caption = "目前登入者：";
            barCurrentUser.Id = 116;
            barCurrentUser.Name = "barCurrentUser";
            // 
            // btnLogout
            // 
            btnLogout.Caption = "登出";
            btnLogout.Id = 117;
            btnLogout.Name = "btnLogout";
            // 
            // btnReviewHistory
            // 
            btnReviewHistory.Caption = "已審查紀錄";
            btnReviewHistory.Id = 120;
            btnReviewHistory.Name = "btnReviewHistory";
            // 
            // btnReviewQueue
            // 
            btnReviewQueue.Caption = "待審查列表";
            btnReviewQueue.Id = 121;
            btnReviewQueue.Name = "btnReviewQueue";
            // 
            // btnEngineerDailyReport
            // 
            btnEngineerDailyReport.Caption = "今日回報";
            btnEngineerDailyReport.Id = 122;
            btnEngineerDailyReport.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnEngineerDailyReport.ImageOptions.Image");
            btnEngineerDailyReport.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnEngineerDailyReport.ImageOptions.LargeImage");
            btnEngineerDailyReport.Name = "btnEngineerDailyReport";
            // 
            // btnEngineerReportList
            // 
            btnEngineerReportList.Caption = "回報列表";
            btnEngineerReportList.Id = 123;
            btnEngineerReportList.Name = "btnEngineerReportList";
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup4 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "案件管理";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(btnNewProject);
            ribbonPageGroup1.ItemLinks.Add(btnImportCsv);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "專案";
            // 
            // ribbonPageGroup4
            // 
            ribbonPageGroup4.ItemLinks.Add(btnViewWeek);
            ribbonPageGroup4.ItemLinks.Add(btnViewMonth);
            ribbonPageGroup4.ItemLinks.Add(btnViewAllResources);
            ribbonPageGroup4.ItemLinks.Add(btnViewMyResources);
            ribbonPageGroup4.Name = "ribbonPageGroup4";
            ribbonPageGroup4.Text = "View Options";
            // 
            // ribbonPage2
            // 
            ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup2, ribbonPageGroup5 });
            ribbonPage2.Name = "ribbonPage2";
            ribbonPage2.Text = "排程";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(btnAssign);
            ribbonPageGroup2.ItemLinks.Add(btnAdjustTime);
            ribbonPageGroup2.ItemLinks.Add(btnCopySchedule);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "案件操作";
            // 
            // ribbonPageGroup5
            // 
            ribbonPageGroup5.ItemLinks.Add(btnShowEstimateHistory);
            ribbonPageGroup5.ItemLinks.Add(btnRefreshScheduler);
            ribbonPageGroup5.Name = "ribbonPageGroup5";
            ribbonPageGroup5.Text = "Tools";
            // 
            // ribbonPage3
            // 
            ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup3 });
            ribbonPage3.Name = "ribbonPage3";
            ribbonPage3.Text = "使用者";
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.ItemLinks.Add(barCurrentUser);
            ribbonPageGroup3.ItemLinks.Add(btnLogout);
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            ribbonPageGroup3.Text = "帳號";
            // 
            // ribbonPage4
            // 
            ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup7, ribbonPageGroup6 });
            ribbonPage4.Name = "ribbonPage4";
            ribbonPage4.Text = "工程師回報";
            // 
            // ribbonPageGroup7
            // 
            ribbonPageGroup7.ItemLinks.Add(btnEngineerDailyReport);
            ribbonPageGroup7.Name = "ribbonPageGroup7";
            ribbonPageGroup7.Text = "今日回報";
            // 
            // ribbonPageGroup6
            // 
            ribbonPageGroup6.ItemLinks.Add(btnEngineerReportList);
            ribbonPageGroup6.Name = "ribbonPageGroup6";
            ribbonPageGroup6.Text = "回報列表";
            // 
            // ribbonPage5
            // 
            ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup8, ribbonPageGroup9 });
            ribbonPage5.Name = "ribbonPage5";
            ribbonPage5.Text = "審查管理";
            // 
            // ribbonPageGroup8
            // 
            ribbonPageGroup8.ItemLinks.Add(btnReviewQueue);
            ribbonPageGroup8.Name = "ribbonPageGroup8";
            ribbonPageGroup8.Text = "待審查案件";
            // 
            // ribbonPageGroup9
            // 
            ribbonPageGroup9.ItemLinks.Add(btnReviewHistory);
            ribbonPageGroup9.Name = "ribbonPageGroup9";
            ribbonPageGroup9.Text = "已審查紀錄";
            // 
            // repositoryItemDuration1
            // 
            repositoryItemDuration1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            repositoryItemDuration1.AutoHeight = false;
            repositoryItemDuration1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemDuration1.Name = "repositoryItemDuration1";
            repositoryItemDuration1.ShowEmptyItem = true;
            repositoryItemDuration1.ValidateOnEnterKey = true;
            // 
            // repositoryItemSpinEdit1
            // 
            repositoryItemSpinEdit1.AutoHeight = false;
            repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemSpinEdit1.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            repositoryItemSpinEdit1.MaxValue = new decimal(new int[] { 200, 0, 0, 0 });
            repositoryItemSpinEdit1.MinValue = new decimal(new int[] { 10, 0, 0, 0 });
            repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // schedulerControl
            // 
            schedulerControl.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            schedulerControl.DataStorage = schedulerStorage;
            schedulerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            schedulerControl.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            schedulerControl.Location = new System.Drawing.Point(0, 0);
            schedulerControl.Margin = new System.Windows.Forms.Padding(4);
            schedulerControl.Name = "schedulerControl";
            schedulerControl.Size = new System.Drawing.Size(1347, 669);
            schedulerControl.Start = new System.DateTime(2025, 11, 30, 0, 0, 0, 0);
            schedulerControl.TabIndex = 0;
            schedulerControl.Text = "schedulerControl1";
            schedulerControl.Views.DayView.TimeRulers.Add(timeRuler10);
            schedulerControl.Views.FullWeekView.Enabled = true;
            schedulerControl.Views.FullWeekView.TimeRulers.Add(timeRuler11);
            schedulerControl.Views.MonthView.ResourcesPerPage = 3;
            schedulerControl.Views.MonthView.WeekCount = 4;
            schedulerControl.Views.WeekView.Enabled = false;
            schedulerControl.Views.WorkWeekView.TimeRulers.Add(timeRuler12);
            schedulerControl.Views.YearView.UseOptimizedScrolling = false;
            schedulerControl.Click += schedulerControl_Click;
            // 
            // schedulerStorage
            // 
            // 
            // 
            // 
            schedulerStorage.Appointments.Labels.CreateNewLabel(0, "None", "&None", System.Drawing.SystemColors.Window);
            schedulerStorage.Appointments.Labels.CreateNewLabel(1, "Important", "&Important", System.Drawing.Color.FromArgb(255, 194, 190));
            schedulerStorage.Appointments.Labels.CreateNewLabel(2, "Business", "&Business", System.Drawing.Color.FromArgb(168, 213, 255));
            schedulerStorage.Appointments.Labels.CreateNewLabel(3, "Personal", "&Personal", System.Drawing.Color.FromArgb(193, 244, 156));
            schedulerStorage.Appointments.Labels.CreateNewLabel(4, "Vacation", "&Vacation", System.Drawing.Color.FromArgb(243, 228, 199));
            schedulerStorage.Appointments.Labels.CreateNewLabel(5, "Must Attend", "Must &Attend", System.Drawing.Color.FromArgb(244, 206, 147));
            schedulerStorage.Appointments.Labels.CreateNewLabel(6, "Travel Required", "&Travel Required", System.Drawing.Color.FromArgb(199, 244, 255));
            schedulerStorage.Appointments.Labels.CreateNewLabel(7, "Needs Preparation", "&Needs Preparation", System.Drawing.Color.FromArgb(207, 219, 152));
            schedulerStorage.Appointments.Labels.CreateNewLabel(8, "Birthday", "&Birthday", System.Drawing.Color.FromArgb(224, 207, 233));
            schedulerStorage.Appointments.Labels.CreateNewLabel(9, "Anniversary", "&Anniversary", System.Drawing.Color.FromArgb(141, 233, 223));
            schedulerStorage.Appointments.Labels.CreateNewLabel(10, "Phone Call", "Phone &Call", System.Drawing.Color.FromArgb(255, 247, 165));
            // 
            // calendarToolsRibbonPageCategory1
            // 
            calendarToolsRibbonPageCategory1.Name = "calendarToolsRibbonPageCategory1";
            calendarToolsRibbonPageCategory1.Visible = false;
            // 
            // Form1
            // 
            AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1858, 924);
            Controls.Add(splitContainerControl);
            Controls.Add(ribbonControl);
            IconOptions.LargeImage = (System.Drawing.Image)resources.GetObject("Form1.IconOptions.LargeImage");
            Name = "Form1";
            Ribbon = ribbonControl;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel1).EndInit();
            splitContainerControl.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel2).EndInit();
            splitContainerControl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl).EndInit();
            splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)treeList1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDuration1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)schedulerControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)schedulerStorage).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerStorage;
        private DevExpress.XtraScheduler.UI.RepositoryItemDuration repositoryItemDuration1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraScheduler.UI.CalendarToolsRibbonPageCategory calendarToolsRibbonPageCategory1;
        private DevExpress.XtraScheduler.SchedulerControl schedulerControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem btnNewProject;
        private DevExpress.XtraBars.BarButtonItem btnImportCsv;
        private DevExpress.XtraBars.BarButtonItem btnViewWeek;
        private DevExpress.XtraBars.BarButtonItem btnViewMonth;
        private DevExpress.XtraBars.BarButtonItem btnViewAllResources;
        private DevExpress.XtraBars.BarButtonItem btnViewMyResources;
        private DevExpress.XtraBars.BarButtonItem btnAssign;
        private DevExpress.XtraBars.BarButtonItem btnAdjustTime;
        private DevExpress.XtraBars.BarButtonItem btnCopySchedule;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.BarButtonItem btnRefreshScheduler;
        private DevExpress.XtraBars.BarButtonItem btnShowEstimateHistory;
        private DevExpress.XtraBars.BarStaticItem barCurrentUser;
        private DevExpress.XtraBars.BarButtonItem btnLogout;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup7;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup8;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup9;
        private DevExpress.XtraBars.BarButtonItem btnReviewHistory;
        private DevExpress.XtraBars.BarButtonItem btnReviewQueue;
        private DevExpress.XtraBars.BarButtonItem btnEngineerDailyReport;
        private DevExpress.XtraBars.BarButtonItem btnEngineerReportList;
        private DevExpress.XtraTreeList.TreeList treeList1;
    }
}

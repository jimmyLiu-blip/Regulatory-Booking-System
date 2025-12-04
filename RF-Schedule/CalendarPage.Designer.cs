namespace RF_Schedule
{
    partial class CalendarPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarPage));
            schedulerControl1 = new DevExpress.XtraScheduler.SchedulerControl();
            schedulerDataStorage1 = new DevExpress.XtraScheduler.SchedulerDataStorage(components);
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            btnViewDay = new DevExpress.XtraBars.BarButtonItem();
            btnViewWeek = new DevExpress.XtraBars.BarButtonItem();
            btnViewMonth = new DevExpress.XtraBars.BarButtonItem();
            btnPrevDay = new DevExpress.XtraBars.BarButtonItem();
            btnNextDay = new DevExpress.XtraBars.BarButtonItem();
            btnToday = new DevExpress.XtraBars.BarButtonItem();
            btnFilterAreaA = new DevExpress.XtraBars.BarButtonItem();
            btnFilterAreaB = new DevExpress.XtraBars.BarButtonItem();
            btnFilterAllResources = new DevExpress.XtraBars.BarButtonItem();
            btnFilterMyResources = new DevExpress.XtraBars.BarButtonItem();
            btnViewTimeLine = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)schedulerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)schedulerDataStorage1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // schedulerControl1
            // 
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            schedulerControl1.DataStorage = schedulerDataStorage1;
            schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            schedulerControl1.Location = new System.Drawing.Point(2, 2);
            schedulerControl1.Name = "schedulerControl1";
            schedulerControl1.Size = new System.Drawing.Size(1777, 921);
            schedulerControl1.Start = new System.DateTime(2025, 11, 30, 0, 0, 0, 0);
            schedulerControl1.TabIndex = 0;
            schedulerControl1.Text = "schedulerControl1";
            schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1);
            schedulerControl1.Views.FullWeekView.Enabled = true;
            schedulerControl1.Views.FullWeekView.TimeRulers.Add(timeRuler2);
            schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler3);
            schedulerControl1.Views.YearView.UseOptimizedScrolling = false;
            schedulerControl1.Click += schedulerControl1_Click;
            // 
            // schedulerDataStorage1
            // 
            // 
            // 
            // 
            schedulerDataStorage1.AppointmentDependencies.AutoReload = false;
            // 
            // 
            // 
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(0, "None", "&None", System.Drawing.SystemColors.Window);
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(1, "Important", "&Important", System.Drawing.Color.FromArgb(255, 194, 190));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(2, "Business", "&Business", System.Drawing.Color.FromArgb(168, 213, 255));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(3, "Personal", "&Personal", System.Drawing.Color.FromArgb(193, 244, 156));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(4, "Vacation", "&Vacation", System.Drawing.Color.FromArgb(243, 228, 199));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(5, "Must Attend", "Must &Attend", System.Drawing.Color.FromArgb(244, 206, 147));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(6, "Travel Required", "&Travel Required", System.Drawing.Color.FromArgb(199, 244, 255));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(7, "Needs Preparation", "&Needs Preparation", System.Drawing.Color.FromArgb(207, 219, 152));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(8, "Birthday", "&Birthday", System.Drawing.Color.FromArgb(224, 207, 233));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(9, "Anniversary", "&Anniversary", System.Drawing.Color.FromArgb(141, 233, 223));
            schedulerDataStorage1.Appointments.Labels.CreateNewLabel(10, "Phone Call", "Phone &Call", System.Drawing.Color.FromArgb(255, 247, 165));
            // 
            // ribbonControl1
            // 
            ribbonControl1.CommandLayout = DevExpress.XtraBars.Ribbon.CommandLayout.Simplified;
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, btnViewDay, btnViewWeek, btnViewMonth, btnPrevDay, btnNextDay, btnToday, btnFilterAreaA, btnFilterAreaB, btnFilterAllResources, btnFilterMyResources, btnViewTimeLine });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 13;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            ribbonControl1.ShowToolbarCustomizeItem = false;
            ribbonControl1.Size = new System.Drawing.Size(1781, 86);
            ribbonControl1.Toolbar.ShowCustomizeItem = false;
            // 
            // btnViewDay
            // 
            btnViewDay.Hint = "切換到日視圖";
            btnViewDay.Id = 1;
            btnViewDay.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewDay.ImageOptions.Image");
            btnViewDay.Name = "btnViewDay";
            btnViewDay.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnViewDay.ItemClick += btnViewDay_ItemClick;
            // 
            // btnViewWeek
            // 
            btnViewWeek.Hint = "顯示週視圖";
            btnViewWeek.Id = 2;
            btnViewWeek.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewWeek.ImageOptions.Image");
            btnViewWeek.Name = "btnViewWeek";
            btnViewWeek.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnViewWeek.ItemClick += btnViewWeek_ItemClick;
            // 
            // btnViewMonth
            // 
            btnViewMonth.Hint = "顯示月視圖";
            btnViewMonth.Id = 3;
            btnViewMonth.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewMonth.ImageOptions.Image");
            btnViewMonth.Name = "btnViewMonth";
            btnViewMonth.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnViewMonth.ItemClick += btnViewMonth_ItemClick;
            // 
            // btnPrevDay
            // 
            btnPrevDay.Hint = "上一個";
            btnPrevDay.Id = 4;
            btnPrevDay.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnPrevDay.ImageOptions.Image");
            btnPrevDay.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnPrevDay.ImageOptions.LargeImage");
            btnPrevDay.Name = "btnPrevDay";
            btnPrevDay.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnPrevDay.ItemClick += btnPrevDay_ItemClick;
            // 
            // btnNextDay
            // 
            btnNextDay.Hint = "下一個";
            btnNextDay.Id = 5;
            btnNextDay.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnNextDay.ImageOptions.Image");
            btnNextDay.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnNextDay.ImageOptions.LargeImage");
            btnNextDay.Name = "btnNextDay";
            btnNextDay.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnNextDay.ItemClick += btnNextDay_ItemClick;
            // 
            // btnToday
            // 
            btnToday.Hint = "回到今天";
            btnToday.Id = 6;
            btnToday.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnToday.ImageOptions.Image");
            btnToday.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnToday.ImageOptions.LargeImage");
            btnToday.Name = "btnToday";
            btnToday.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnToday.ItemClick += btnToday_ItemClick;
            // 
            // btnFilterAreaA
            // 
            btnFilterAreaA.Hint = "A區";
            btnFilterAreaA.Id = 7;
            btnFilterAreaA.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnFilterAreaA.ImageOptions.Image");
            btnFilterAreaA.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnFilterAreaA.ImageOptions.LargeImage");
            btnFilterAreaA.Name = "btnFilterAreaA";
            btnFilterAreaA.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // btnFilterAreaB
            // 
            btnFilterAreaB.Hint = "B區";
            btnFilterAreaB.Id = 8;
            btnFilterAreaB.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnFilterAreaB.ImageOptions.Image");
            btnFilterAreaB.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnFilterAreaB.ImageOptions.LargeImage");
            btnFilterAreaB.Name = "btnFilterAreaB";
            btnFilterAreaB.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // btnFilterAllResources
            // 
            btnFilterAllResources.Hint = "全部場地";
            btnFilterAllResources.Id = 9;
            btnFilterAllResources.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnFilterAllResources.ImageOptions.Image");
            btnFilterAllResources.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnFilterAllResources.ImageOptions.LargeImage");
            btnFilterAllResources.Name = "btnFilterAllResources";
            btnFilterAllResources.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // btnFilterMyResources
            // 
            btnFilterMyResources.Hint = "自己場地";
            btnFilterMyResources.Id = 10;
            btnFilterMyResources.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnFilterMyResources.ImageOptions.Image");
            btnFilterMyResources.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnFilterMyResources.ImageOptions.LargeImage");
            btnFilterMyResources.Name = "btnFilterMyResources";
            btnFilterMyResources.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // btnViewTimeLine
            // 
            btnViewTimeLine.Hint = "TimelineView";
            btnViewTimeLine.Id = 12;
            btnViewTimeLine.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnViewTimeLine.ImageOptions.Image");
            btnViewTimeLine.Name = "btnViewTimeLine";
            btnViewTimeLine.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            btnViewTimeLine.ItemClick += btnViewTimeLine_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2, ribbonPageGroup3 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "calendarRibbonPage1";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            ribbonPageGroup1.ItemLinks.Add(btnViewDay);
            ribbonPageGroup1.ItemLinks.Add(btnViewWeek);
            ribbonPageGroup1.ItemLinks.Add(btnViewMonth);
            ribbonPageGroup1.ItemLinks.Add(btnViewTimeLine);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(btnPrevDay);
            ribbonPageGroup2.ItemLinks.Add(btnNextDay);
            ribbonPageGroup2.ItemLinks.Add(btnToday);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.ItemLinks.Add(btnFilterAreaA);
            ribbonPageGroup3.ItemLinks.Add(btnFilterAreaB);
            ribbonPageGroup3.ItemLinks.Add(btnFilterAllResources);
            ribbonPageGroup3.ItemLinks.Add(btnFilterMyResources);
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(schedulerControl1);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 86);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1781, 925);
            panelControl1.TabIndex = 1;
            // 
            // CalendarPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Controls.Add(ribbonControl1);
            Name = "CalendarPage";
            Size = new System.Drawing.Size(1781, 1011);
            Load += CalendarPage_Load_1;
            ((System.ComponentModel.ISupportInitialize)schedulerControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)schedulerDataStorage1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraScheduler.SchedulerControl schedulerControl1;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerDataStorage1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnViewDay;
        private DevExpress.XtraBars.BarButtonItem btnViewWeek;
        private DevExpress.XtraBars.BarButtonItem btnViewMonth;
        private DevExpress.XtraBars.BarButtonItem btnPrevDay;
        private DevExpress.XtraBars.BarButtonItem btnNextDay;
        private DevExpress.XtraBars.BarButtonItem btnToday;
        private DevExpress.XtraBars.BarButtonItem btnFilterAreaA;
        private DevExpress.XtraBars.BarButtonItem btnFilterAreaB;
        private DevExpress.XtraBars.BarButtonItem btnFilterAllResources;
        private DevExpress.XtraBars.BarButtonItem btnFilterMyResources;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.BarButtonItem btnViewTimeLine;
    }
}

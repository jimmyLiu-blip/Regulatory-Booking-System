namespace RF_Schedule
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            btnOpenCalendar = new DevExpress.XtraBars.BarButtonItem();
            btnOpenProject = new DevExpress.XtraBars.BarButtonItem();
            rpSchedule = new DevExpress.XtraBars.Ribbon.RibbonPage();
            pgScheduleManagement = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            repositoryItemDuration1 = new DevExpress.XtraScheduler.UI.RepositoryItemDuration();
            repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            calendarToolsRibbonPageCategory1 = new DevExpress.XtraScheduler.UI.CalendarToolsRibbonPageCategory();
            btnEngineerReportList = new DevExpress.XtraBars.BarButtonItem();
            documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(components);
            tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(components);
            dateNavigator1 = new DevExpress.XtraScheduler.DateNavigator();
            ((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDuration1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)documentManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabbedView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigator1.CalendarTimeProperties).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl
            // 
            ribbonControl.AllowMinimizeRibbon = false;
            ribbonControl.AllowMinimizeRibbonOnDoubleClick = false;
            ribbonControl.ApplicationButtonText = null;
            ribbonControl.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(45);
            ribbonControl.ExpandCollapseItem.Id = 0;
            ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl.ExpandCollapseItem, btnOpenCalendar, btnOpenProject });
            ribbonControl.Location = new System.Drawing.Point(0, 0);
            ribbonControl.Margin = new System.Windows.Forms.Padding(4);
            ribbonControl.MaxItemId = 129;
            ribbonControl.Name = "ribbonControl";
            ribbonControl.OptionsMenuMinWidth = 495;
            ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { rpSchedule });
            ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemDuration1, repositoryItemSpinEdit1 });
            ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            ribbonControl.ShowToolbarCustomizeItem = false;
            ribbonControl.Size = new System.Drawing.Size(1858, 237);
            ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // btnOpenCalendar
            // 
            btnOpenCalendar.Caption = "排程日曆";
            btnOpenCalendar.Id = 127;
            btnOpenCalendar.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnOpenCalendar.ImageOptions.Image");
            btnOpenCalendar.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnOpenCalendar.ImageOptions.LargeImage");
            btnOpenCalendar.Name = "btnOpenCalendar";
            btnOpenCalendar.ItemClick += btnOpenCalendar_ItemClick_1;
            // 
            // btnOpenProject
            // 
            btnOpenProject.Caption = "案件管理";
            btnOpenProject.Id = 128;
            btnOpenProject.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnOpenProject.ImageOptions.Image");
            btnOpenProject.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnOpenProject.ImageOptions.LargeImage");
            btnOpenProject.Name = "btnOpenProject";
            btnOpenProject.ItemClick += btnOpenProject_ItemClick;
            // 
            // rpSchedule
            // 
            rpSchedule.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { pgScheduleManagement });
            rpSchedule.Name = "rpSchedule";
            rpSchedule.Text = "排程";
            // 
            // pgScheduleManagement
            // 
            pgScheduleManagement.ItemLinks.Add(btnOpenCalendar);
            pgScheduleManagement.ItemLinks.Add(btnOpenProject);
            pgScheduleManagement.Name = "pgScheduleManagement";
            pgScheduleManagement.Text = "排程管理";
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
            // calendarToolsRibbonPageCategory1
            // 
            calendarToolsRibbonPageCategory1.Name = "calendarToolsRibbonPageCategory1";
            calendarToolsRibbonPageCategory1.Visible = false;
            // 
            // btnEngineerReportList
            // 
            btnEngineerReportList.Caption = "回報列表";
            btnEngineerReportList.Id = 123;
            btnEngineerReportList.Name = "btnEngineerReportList";
            // 
            // documentManager1
            // 
            documentManager1.MdiParent = this;
            documentManager1.MenuManager = ribbonControl;
            documentManager1.View = tabbedView1;
            documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { tabbedView1 });
            // 
            // dateNavigator1
            // 
            dateNavigator1.CalendarAppearance.DayCellSpecial.FontStyleDelta = System.Drawing.FontStyle.Bold;
            dateNavigator1.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            dateNavigator1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateNavigator1.FirstDayOfWeek = System.DayOfWeek.Sunday;
            dateNavigator1.Location = new System.Drawing.Point(958, 346);
            dateNavigator1.Name = "dateNavigator1";
            dateNavigator1.Size = new System.Drawing.Size(8, 8);
            dateNavigator1.TabIndex = 2;
            // 
            // MainForm
            // 
            AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1858, 924);
            Controls.Add(dateNavigator1);
            Controls.Add(ribbonControl);
            IconOptions.LargeImage = (System.Drawing.Image)resources.GetObject("MainForm.IconOptions.LargeImage");
            IsMdiContainer = true;
            Name = "MainForm";
            Ribbon = ribbonControl;
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDuration1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)documentManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabbedView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigator1.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateNavigator1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraScheduler.UI.RepositoryItemDuration repositoryItemDuration1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraScheduler.UI.CalendarToolsRibbonPageCategory calendarToolsRibbonPageCategory1;
        private DevExpress.XtraBars.BarButtonItem btnEngineerReportList;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpSchedule;
        private DevExpress.XtraBars.BarButtonItem btnOpenCalendar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup pgScheduleManagement;
        private DevExpress.XtraBars.BarButtonItem btnOpenProject;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraScheduler.DateNavigator dateNavigator1;
    }
}

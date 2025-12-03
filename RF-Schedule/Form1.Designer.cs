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
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
            splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            schedulerControl = new DevExpress.XtraScheduler.SchedulerControl();
            schedulerStorage = new DevExpress.XtraScheduler.SchedulerDataStorage(components);
            ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            calendarToolsRibbonPageCategory1 = new DevExpress.XtraScheduler.UI.CalendarToolsRibbonPageCategory();
            appointmentRibbonPage1 = new DevExpress.XtraScheduler.UI.AppointmentRibbonPage();
            actionsRibbonPageGroup1 = new DevExpress.XtraScheduler.UI.ActionsRibbonPageGroup();
            optionsRibbonPageGroup1 = new DevExpress.XtraScheduler.UI.OptionsRibbonPageGroup();
            repositoryItemDuration1 = new DevExpress.XtraScheduler.UI.RepositoryItemDuration();
            repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel2).BeginInit();
            splitContainerControl.Panel2.SuspendLayout();
            splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)schedulerControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)schedulerStorage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDuration1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
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
            splitContainerControl.Panel1.Text = "Panel1";
            // 
            // splitContainerControl.Panel2
            // 
            splitContainerControl.Panel2.Controls.Add(schedulerControl);
            splitContainerControl.Panel2.Text = "Panel2";
            splitContainerControl.Size = new System.Drawing.Size(1858, 687);
            splitContainerControl.SplitterPosition = 524;
            splitContainerControl.TabIndex = 0;
            splitContainerControl.Text = "splitContainerControl1";
            // 
            // schedulerControl
            // 
            schedulerControl.DataStorage = schedulerStorage;
            schedulerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            schedulerControl.Location = new System.Drawing.Point(0, 0);
            schedulerControl.Margin = new System.Windows.Forms.Padding(4);
            schedulerControl.Name = "schedulerControl";
            schedulerControl.Size = new System.Drawing.Size(1301, 669);
            schedulerControl.Start = new System.DateTime(2025, 12, 2, 0, 0, 0, 0);
            schedulerControl.TabIndex = 0;
            schedulerControl.Text = "schedulerControl1";
            schedulerControl.Views.DayView.TimeRulers.Add(timeRuler1);
            schedulerControl.Views.FullWeekView.Enabled = true;
            schedulerControl.Views.FullWeekView.TimeRulers.Add(timeRuler2);
            schedulerControl.Views.WeekView.Enabled = false;
            schedulerControl.Views.WorkWeekView.TimeRulers.Add(timeRuler3);
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
            // ribbonControl
            // 
            ribbonControl.ApplicationButtonText = null;
            ribbonControl.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(45);
            ribbonControl.ExpandCollapseItem.Id = 0;
            ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl.ExpandCollapseItem });
            ribbonControl.Location = new System.Drawing.Point(0, 0);
            ribbonControl.Margin = new System.Windows.Forms.Padding(4);
            ribbonControl.MaxItemId = 104;
            ribbonControl.Name = "ribbonControl";
            ribbonControl.OptionsMenuMinWidth = 495;
            ribbonControl.PageCategories.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageCategory[] { calendarToolsRibbonPageCategory1 });
            ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemDuration1, repositoryItemSpinEdit1 });
            ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            ribbonControl.Size = new System.Drawing.Size(1858, 237);
            // 
            // calendarToolsRibbonPageCategory1
            // 
            calendarToolsRibbonPageCategory1.Name = "calendarToolsRibbonPageCategory1";
            calendarToolsRibbonPageCategory1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { appointmentRibbonPage1 });
            calendarToolsRibbonPageCategory1.Visible = false;
            // 
            // appointmentRibbonPage1
            // 
            appointmentRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { actionsRibbonPageGroup1, optionsRibbonPageGroup1 });
            appointmentRibbonPage1.Name = "appointmentRibbonPage1";
            appointmentRibbonPage1.Visible = false;
            // 
            // actionsRibbonPageGroup1
            // 
            actionsRibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            actionsRibbonPageGroup1.Name = "actionsRibbonPageGroup1";
            actionsRibbonPageGroup1.Text = "";
            // 
            // optionsRibbonPageGroup1
            // 
            optionsRibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            optionsRibbonPageGroup1.Name = "optionsRibbonPageGroup1";
            optionsRibbonPageGroup1.Text = "";
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
            // Form1
            // 
            AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1858, 924);
            Controls.Add(splitContainerControl);
            Controls.Add(ribbonControl);
            Name = "Form1";
            Ribbon = ribbonControl;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl.Panel2).EndInit();
            splitContainerControl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl).EndInit();
            splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)schedulerControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)schedulerStorage).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemDuration1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).EndInit();
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
        private DevExpress.XtraScheduler.UI.AppointmentRibbonPage appointmentRibbonPage1;
        private DevExpress.XtraScheduler.UI.ActionsRibbonPageGroup actionsRibbonPageGroup1;
        private DevExpress.XtraScheduler.UI.OptionsRibbonPageGroup optionsRibbonPageGroup1;
        private DevExpress.XtraScheduler.SchedulerControl schedulerControl;
    }
}

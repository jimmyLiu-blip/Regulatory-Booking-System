namespace RF_Schedule
{
    partial class ProjectPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectPage));
            panelMain = new DevExpress.XtraEditors.PanelControl();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            txtCreatedDate = new DevExpress.XtraEditors.TextEdit();
            txtPriority = new DevExpress.XtraEditors.TextEdit();
            txtStatus = new DevExpress.XtraEditors.TextEdit();
            txtProjectName = new DevExpress.XtraEditors.TextEdit();
            lblCreatedDate = new DevExpress.XtraEditors.LabelControl();
            lblPriority = new DevExpress.XtraEditors.LabelControl();
            lblStatus = new DevExpress.XtraEditors.LabelControl();
            lblProjectName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelMain).BeginInit();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCreatedDate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPriority.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtStatus.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProjectName.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(splitContainerControl1);
            panelMain.Controls.Add(ribbonControl1);
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.Location = new System.Drawing.Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new System.Drawing.Size(1772, 1015);
            panelMain.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            splitContainerControl1.Location = new System.Drawing.Point(2, 191);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(gridControl1);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(txtCreatedDate);
            splitContainerControl1.Panel2.Controls.Add(txtPriority);
            splitContainerControl1.Panel2.Controls.Add(txtStatus);
            splitContainerControl1.Panel2.Controls.Add(txtProjectName);
            splitContainerControl1.Panel2.Controls.Add(lblCreatedDate);
            splitContainerControl1.Panel2.Controls.Add(lblPriority);
            splitContainerControl1.Panel2.Controls.Add(lblStatus);
            splitContainerControl1.Panel2.Controls.Add(lblProjectName);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Panel2.Paint += splitContainerControl1_Panel2_Paint;
            splitContainerControl1.Size = new System.Drawing.Size(1768, 822);
            splitContainerControl1.SplitterPosition = 450;
            splitContainerControl1.TabIndex = 1;
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl1.Location = new System.Drawing.Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.MenuManager = ribbonControl1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(1303, 822);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            gridControl1.Click += gridControl1_Click_2;
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            // 
            // ribbonControl1
            // 
            ribbonControl1.AllowMinimizeRibbon = false;
            ribbonControl1.AllowMinimizeRibbonOnDoubleClick = false;
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, barButtonItem1, barButtonItem2, barButtonItem3 });
            ribbonControl1.Location = new System.Drawing.Point(2, 2);
            ribbonControl1.MaxItemId = 4;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            ribbonControl1.ShowToolbarCustomizeItem = false;
            ribbonControl1.Size = new System.Drawing.Size(1768, 189);
            ribbonControl1.Toolbar.ShowCustomizeItem = false;
            ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barButtonItem1
            // 
            barButtonItem1.Caption = "新增案件";
            barButtonItem1.Id = 1;
            barButtonItem1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem1.ImageOptions.Image");
            barButtonItem1.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem1.ImageOptions.LargeImage");
            barButtonItem1.Name = "barButtonItem1";
            barButtonItem1.ItemClick += barButtonItem1_ItemClick;
            // 
            // barButtonItem2
            // 
            barButtonItem2.Caption = "編輯案件";
            barButtonItem2.Id = 2;
            barButtonItem2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem2.ImageOptions.Image");
            barButtonItem2.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem2.ImageOptions.LargeImage");
            barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            barButtonItem3.Caption = "刪除案件";
            barButtonItem3.Id = 3;
            barButtonItem3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem3.ImageOptions.Image");
            barButtonItem3.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem3.ImageOptions.LargeImage");
            barButtonItem3.Name = "barButtonItem3";
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(barButtonItem1);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem2);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem3);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "案件管理";
            // 
            // txtCreatedDate
            // 
            txtCreatedDate.Location = new System.Drawing.Point(151, 206);
            txtCreatedDate.MenuManager = ribbonControl1;
            txtCreatedDate.Name = "txtCreatedDate";
            txtCreatedDate.Properties.ReadOnly = true;
            txtCreatedDate.Size = new System.Drawing.Size(225, 28);
            txtCreatedDate.TabIndex = 7;
            // 
            // txtPriority
            // 
            txtPriority.Location = new System.Drawing.Point(151, 153);
            txtPriority.MenuManager = ribbonControl1;
            txtPriority.Name = "txtPriority";
            txtPriority.Properties.ReadOnly = true;
            txtPriority.Size = new System.Drawing.Size(225, 28);
            txtPriority.TabIndex = 6;
            // 
            // txtStatus
            // 
            txtStatus.Location = new System.Drawing.Point(151, 106);
            txtStatus.MenuManager = ribbonControl1;
            txtStatus.Name = "txtStatus";
            txtStatus.Properties.ReadOnly = true;
            txtStatus.Size = new System.Drawing.Size(225, 28);
            txtStatus.TabIndex = 5;
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new System.Drawing.Point(151, 63);
            txtProjectName.MenuManager = ribbonControl1;
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Properties.ReadOnly = true;
            txtProjectName.Size = new System.Drawing.Size(225, 28);
            txtProjectName.TabIndex = 4;
            // 
            // lblCreatedDate
            // 
            lblCreatedDate.Location = new System.Drawing.Point(28, 209);
            lblCreatedDate.Name = "lblCreatedDate";
            lblCreatedDate.Size = new System.Drawing.Size(108, 22);
            lblCreatedDate.TabIndex = 3;
            lblCreatedDate.Text = "CreatedDate :";
            // 
            // lblPriority
            // 
            lblPriority.Location = new System.Drawing.Point(69, 156);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new System.Drawing.Size(67, 22);
            lblPriority.TabIndex = 2;
            lblPriority.Text = "Priority :";
            // 
            // lblStatus
            // 
            lblStatus.Location = new System.Drawing.Point(75, 109);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(61, 22);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Status :";
            // 
            // lblProjectName
            // 
            lblProjectName.Location = new System.Drawing.Point(19, 66);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new System.Drawing.Size(117, 22);
            lblProjectName.TabIndex = 0;
            lblProjectName.Text = "Project Name :";
            // 
            // ProjectPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelMain);
            Name = "ProjectPage";
            Size = new System.Drawing.Size(1772, 1015);
            ((System.ComponentModel.ISupportInitialize)panelMain).EndInit();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            splitContainerControl1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCreatedDate.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPriority.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtStatus.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProjectName.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelMain;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.TextEdit txtCreatedDate;
        private DevExpress.XtraEditors.TextEdit txtPriority;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private DevExpress.XtraEditors.TextEdit txtProjectName;
        private DevExpress.XtraEditors.LabelControl lblCreatedDate;
        private DevExpress.XtraEditors.LabelControl lblPriority;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.LabelControl lblProjectName;
    }
}

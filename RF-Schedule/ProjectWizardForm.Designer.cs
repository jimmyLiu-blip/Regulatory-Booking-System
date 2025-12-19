namespace RF_Schedule
{
    partial class ProjectWizardForm
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
            wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            wizardPageProjectBasic = new DevExpress.XtraWizard.WizardPage();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            txtNotes = new DevExpress.XtraEditors.TextEdit();
            txtPriority = new DevExpress.XtraEditors.TextEdit();
            txtProjectName = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            wizardPageRegulation = new DevExpress.XtraWizard.WizardPage();
            layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            gridRegulation = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            wizardPageTestItem = new DevExpress.XtraWizard.WizardPage();
            layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            gridTestItem = new DevExpress.XtraGrid.GridControl();
            gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
            wizardControl1.SuspendLayout();
            wizardPageProjectBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtNotes.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPriority.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtProjectName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            wizardPageRegulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl2).BeginInit();
            layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRegulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            wizardPageTestItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layoutControl3).BeginInit();
            layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridTestItem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            SuspendLayout();
            // 
            // wizardControl1
            // 
            wizardControl1.Controls.Add(wizardPageProjectBasic);
            wizardControl1.Controls.Add(wizardPageRegulation);
            wizardControl1.Controls.Add(wizardPageTestItem);
            wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            wizardControl1.ImageOptions.ImageWidth = 308;
            wizardControl1.Margin = new System.Windows.Forms.Padding(5);
            wizardControl1.MinimumSize = new System.Drawing.Size(167, 169);
            wizardControl1.Name = "wizardControl1";
            wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] { wizardPageProjectBasic, wizardPageRegulation, wizardPageTestItem });
            wizardControl1.Size = new System.Drawing.Size(1128, 731);
            wizardControl1.Text = "新增案件精靈";
            wizardControl1.Click += wizardControl1_Click;
            // 
            // wizardPageProjectBasic
            // 
            wizardPageProjectBasic.Controls.Add(layoutControl1);
            wizardPageProjectBasic.Name = "wizardPageProjectBasic";
            wizardPageProjectBasic.Size = new System.Drawing.Size(1080, 518);
            wizardPageProjectBasic.PageValidating += wizardPageProjectBasic_PageValidating;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(txtNotes);
            layoutControl1.Controls.Add(txtPriority);
            layoutControl1.Controls.Add(txtProjectName);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Margin = new System.Windows.Forms.Padding(5);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1080, 518);
            layoutControl1.TabIndex = 7;
            layoutControl1.Text = "layoutControl1";
            // 
            // txtNotes
            // 
            txtNotes.EditValue = "";
            txtNotes.Location = new System.Drawing.Point(159, 86);
            txtNotes.Margin = new System.Windows.Forms.Padding(5);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new System.Drawing.Size(903, 28);
            txtNotes.StyleController = layoutControl1;
            txtNotes.TabIndex = 1;
            txtNotes.EditValueChanged += txtNotes_EditValueChanged;
            // 
            // txtPriority
            // 
            txtPriority.EditValue = "";
            txtPriority.Location = new System.Drawing.Point(159, 52);
            txtPriority.Margin = new System.Windows.Forms.Padding(5);
            txtPriority.Name = "txtPriority";
            txtPriority.Size = new System.Drawing.Size(903, 28);
            txtPriority.StyleController = layoutControl1;
            txtPriority.TabIndex = 1;
            // 
            // txtProjectName
            // 
            txtProjectName.EditValue = "";
            txtProjectName.Location = new System.Drawing.Point(159, 18);
            txtProjectName.Margin = new System.Windows.Forms.Padding(5);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new System.Drawing.Size(903, 28);
            txtProjectName.StyleController = layoutControl1;
            txtProjectName.TabIndex = 4;
            txtProjectName.EditValueChanged += txtProjectName_EditValueChanged;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, emptySpaceItem1, layoutControlItem2, layoutControlItem3 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1080, 518);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtProjectName;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(1050, 34);
            layoutControlItem1.Text = "Project Name：";
            layoutControlItem1.TextSize = new System.Drawing.Size(123, 22);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new System.Drawing.Point(0, 102);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(1050, 386);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtPriority;
            layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(1050, 34);
            layoutControlItem2.Text = "Priority：";
            layoutControlItem2.TextSize = new System.Drawing.Size(123, 22);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txtNotes;
            layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(1050, 34);
            layoutControlItem3.Text = "Notes：";
            layoutControlItem3.TextSize = new System.Drawing.Size(123, 22);
            // 
            // wizardPageRegulation
            // 
            wizardPageRegulation.Controls.Add(layoutControl2);
            wizardPageRegulation.Name = "wizardPageRegulation";
            wizardPageRegulation.Size = new System.Drawing.Size(1080, 518);
            wizardPageRegulation.Text = "選擇法規";
            wizardPageRegulation.PageInit += wizardPageRegulation_PageInit;
            // 
            // layoutControl2
            // 
            layoutControl2.Controls.Add(gridRegulation);
            layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl2.Location = new System.Drawing.Point(0, 0);
            layoutControl2.Name = "layoutControl2";
            layoutControl2.Root = layoutControlGroup1;
            layoutControl2.Size = new System.Drawing.Size(1080, 518);
            layoutControl2.TabIndex = 1;
            layoutControl2.Text = "layoutControl2";
            // 
            // gridRegulation
            // 
            gridRegulation.Location = new System.Drawing.Point(18, 18);
            gridRegulation.MainView = gridView1;
            gridRegulation.Name = "gridRegulation";
            gridRegulation.Size = new System.Drawing.Size(1044, 482);
            gridRegulation.TabIndex = 0;
            gridRegulation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridRegulation;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4 });
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(1080, 518);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = gridRegulation;
            layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(1050, 488);
            layoutControlItem4.TextVisible = false;
            // 
            // wizardPageTestItem
            // 
            wizardPageTestItem.Controls.Add(layoutControl3);
            wizardPageTestItem.Name = "wizardPageTestItem";
            wizardPageTestItem.Size = new System.Drawing.Size(1080, 518);
            wizardPageTestItem.Text = "選取測項";
            wizardPageTestItem.PageInit += wizardPageTestItem_PageInit;
            // 
            // layoutControl3
            // 
            layoutControl3.Controls.Add(gridTestItem);
            layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl3.Location = new System.Drawing.Point(0, 0);
            layoutControl3.Name = "layoutControl3";
            layoutControl3.Root = layoutControlGroup2;
            layoutControl3.Size = new System.Drawing.Size(1080, 518);
            layoutControl3.TabIndex = 0;
            layoutControl3.Text = "layoutControl3";
            // 
            // gridTestItem
            // 
            gridTestItem.Location = new System.Drawing.Point(18, 18);
            gridTestItem.MainView = gridView2;
            gridTestItem.Name = "gridTestItem";
            gridTestItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1 });
            gridTestItem.Size = new System.Drawing.Size(1044, 482);
            gridTestItem.TabIndex = 4;
            gridTestItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
            // 
            // gridView2
            // 
            gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colIsSelected });
            gridView2.GridControl = gridTestItem;
            gridView2.Name = "gridView2";
            gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            gridView2.OptionsLayout.Columns.AddNewColumns = false;
            // 
            // colIsSelected
            // 
            colIsSelected.ColumnEdit = repositoryItemCheckEdit1;
            colIsSelected.FieldName = "IsSelected";
            colIsSelected.MinWidth = 30;
            colIsSelected.Name = "colIsSelected";
            colIsSelected.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            colIsSelected.Visible = true;
            colIsSelected.VisibleIndex = 0;
            colIsSelected.Width = 112;
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup2.GroupBordersVisible = false;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem5 });
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Size = new System.Drawing.Size(1080, 518);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = gridTestItem;
            layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(1050, 488);
            layoutControlItem5.TextVisible = false;
            // 
            // ProjectWizardForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1128, 731);
            Controls.Add(wizardControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProjectWizardForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ProjectWizardForm";
            ((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
            wizardControl1.ResumeLayout(false);
            wizardPageProjectBasic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtNotes.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPriority.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtProjectName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            wizardPageRegulation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl2).EndInit();
            layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridRegulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            wizardPageTestItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layoutControl3).EndInit();
            layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridTestItem).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraWizard.WizardPage wizardPageProjectBasic;
        private DevExpress.XtraEditors.TextEdit txtNotes;
        private DevExpress.XtraEditors.TextEdit txtPriority;
        private DevExpress.XtraEditors.TextEdit txtProjectName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraWizard.WizardPage wizardPageRegulation;
        private DevExpress.XtraGrid.GridControl gridRegulation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraWizard.WizardPage wizardPageTestItem;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraGrid.GridControl gridTestItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}
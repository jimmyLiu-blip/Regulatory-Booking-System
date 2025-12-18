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
            SuspendLayout();
            // 
            // wizardControl1
            // 
            wizardControl1.Controls.Add(wizardPageProjectBasic);
            wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            wizardControl1.ImageOptions.ImageWidth = 308;
            wizardControl1.Margin = new System.Windows.Forms.Padding(5);
            wizardControl1.MinimumSize = new System.Drawing.Size(167, 169);
            wizardControl1.Name = "wizardControl1";
            wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] { wizardPageProjectBasic });
            wizardControl1.Size = new System.Drawing.Size(1128, 731);
            wizardControl1.Text = "新增案件精靈";
            // 
            // wizardPageProjectBasic
            // 
            wizardPageProjectBasic.Controls.Add(layoutControl1);
            wizardPageProjectBasic.Name = "wizardPageProjectBasic";
            wizardPageProjectBasic.Size = new System.Drawing.Size(1080, 518);
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
            txtNotes.Location = new System.Drawing.Point(141, 86);
            txtNotes.Margin = new System.Windows.Forms.Padding(5);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new System.Drawing.Size(921, 28);
            txtNotes.StyleController = layoutControl1;
            txtNotes.TabIndex = 1;
            // 
            // txtPriority
            // 
            txtPriority.EditValue = "";
            txtPriority.Location = new System.Drawing.Point(141, 52);
            txtPriority.Margin = new System.Windows.Forms.Padding(5);
            txtPriority.Name = "txtPriority";
            txtPriority.Size = new System.Drawing.Size(921, 28);
            txtPriority.StyleController = layoutControl1;
            txtPriority.TabIndex = 1;
            // 
            // txtProjectName
            // 
            txtProjectName.EditValue = "";
            txtProjectName.Location = new System.Drawing.Point(141, 18);
            txtProjectName.Margin = new System.Windows.Forms.Padding(5);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new System.Drawing.Size(921, 28);
            txtProjectName.StyleController = layoutControl1;
            txtProjectName.TabIndex = 4;
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
            layoutControlItem1.Text = "Project Name";
            layoutControlItem1.TextSize = new System.Drawing.Size(105, 22);
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
            layoutControlItem2.Text = "Priority";
            layoutControlItem2.TextSize = new System.Drawing.Size(105, 22);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txtNotes;
            layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(1050, 34);
            layoutControlItem3.Text = "Notes";
            layoutControlItem3.TextSize = new System.Drawing.Size(105, 22);
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
    }
}
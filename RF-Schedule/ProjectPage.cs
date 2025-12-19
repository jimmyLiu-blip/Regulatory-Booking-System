using System;
using DevExpress.Data.ODataLinq.Helpers;
using RFScheduling.Infrastructure;
using System.Linq;
using RFScheduling.Domain;

namespace RF_Schedule
{
    public partial class ProjectPage : DevExpress.XtraEditors.XtraUserControl
    {
        public ProjectPage()
        {
            InitializeComponent();
            this.Load += ProjectPage_Load;
        }

        private void ProjectPage_Load(object? sender, EventArgs e)
        {
            LoadProjects();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }



        private void gridControl1_Click_1(object sender, EventArgs e)
        {

        }

        private void LoadProjects()
        {
            using var database = new AppDbContext();

            // 再正常讀資料給 Grid
            var projects = database.Projects
                 .Where(p => !p.IsDeleted)   // 軟刪除先擋掉
                 .OrderByDescending(p => p.CreatedDate)
                 .ToList();

            gridControl1.DataSource = projects;

        }

        private void gridControl1_Click_2(object sender, EventArgs e)
        {

        }

        private void splitContainerControl1_Panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        // 綁定左邊的案件到右邊的詳細清單
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var project = gridView1.GetFocusedRow() as Project;
            if (project == null) return;

            txtProjectName.Text = project.ProjectName;
            txtStatus.Text = project.Status;
            txtPriority.Text = project.Priority;
            txtCreatedDate.Text = project.CreatedDate.ToString("yyyy-MM-dd HH:mm");
        }

        // 點新增案件按鈕，會自動新增
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wizard = new ProjectWizardForm();

            wizard.ShowDialog();
        }
    }
}

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

            // 隱藏不該給使用者看的欄位
            gridView1.Columns["ProjectId"].Visible = false;
            gridView1.Columns["RegulationId"].Visible = false;
            gridView1.Columns["TestItemId"].Visible = false;
            gridView1.Columns["CreatedBy"].Visible = false;
            gridView1.Columns["ModifiedBy"].Visible = false;
            gridView1.Columns["IsDeleted"].Visible = false;
        }

        private void gridControl1_Click_2(object sender, EventArgs e)
        {

        }
    }
}

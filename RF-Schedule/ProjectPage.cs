using RFScheduling.Domain;
using System;
using System.Collections.Generic;

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
            LoadFakeProjects();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void LoadFakeProjects()
        {
            var list = new List<Project>
            {
                new Project
                {
                    ProjectId = 1,
                    ProjectName = "FCC Project A",
                    Priority = "高",
                    Status = "Scheduled",
                    CreatedDate = DateTime.Now.AddDays(-3)
                },
                new Project
                {
                    ProjectId = 2,
                    ProjectName = "CE Project B",
                    Priority = "中",
                    Status = "Testing",
                    CreatedDate = DateTime.Now.AddDays(-1)
                }
            };

            gridControl1.MainView = gridView1;   // 🔑 關鍵
            gridControl1.DataSource = list;

            gridView1.PopulateColumns();         // 🔑 強制產生欄位
        }
    }
}

using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace RF_Schedule
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpenCalendar_ItemClick_1(object sender, EventArgs e)
        {
            OpenPage(new CalendarPage(), "排程日曆");
        }

        private void btnOpenProject_ItemClick(object sender, EventArgs e)
        {
            OpenPage(new ProjectPage(), "案件管理");
        }

        private void OpenPage(XtraUserControl page, string title)
        {
            // 檢查是否已有同名頁籤
            foreach (var doc in tabbedView1.Documents)
            {
                if (doc.Caption == title)
                {
                    tabbedView1.Controller.Activate(doc);
                    return;
                }
            }

            // 建立 MD IForm 包住 UserControl（DocumentManager 的做法）
            XtraForm form = new XtraForm();
            form.Text = title;
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.ShowInTaskbar = false;

            page.Dock = DockStyle.Fill;
            form.Controls.Add(page);
            form.Show();
        }

    }
}
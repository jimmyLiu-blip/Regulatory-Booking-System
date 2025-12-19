using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.XtraEditors;
using RFScheduling.Infrastructure;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RF_Schedule
{
    public partial class ProjectWizardForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly ProjectWizardState _state = new ProjectWizardState();

        public ProjectWizardForm()
        {
            InitializeComponent();
        }

        private void txtProjectName_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNotes_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void wizardControl1_Click(object sender, EventArgs e)
        {

        }

        // Wizard 第一頁中的 Project Name 驗證
        private void wizardPageProjectBasic_PageValidating(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectName.Text))
            {
                XtraMessageBox.Show(
                    "ProjectName為必填項目",
                    "資料不完整",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Valid = false;
                return;
            }

            // 驗證通過 => 存進 Wizard State
            _state.ProjectName = txtProjectName.Text;
            _state.Priority = txtPriority.Text;
            _state.Notes = txtNotes.Text;
        }

        private void wizardPageRegulation_PageInit(object sender, EventArgs e)
        {
            using var db = new AppDbContext();

            var regulations = db.Regulations
                .Where(r => r.IsActive && !r.IsDeleted)
                .OrderBy(r => r.RegulationCode)
                .ToList();

            gridRegulation.DataSource = regulations;
        }

        private void wizardPageTestItem_PageInit(object sender, EventArgs e)
        {
            using var db = new AppDbContext();



        }

    }
}
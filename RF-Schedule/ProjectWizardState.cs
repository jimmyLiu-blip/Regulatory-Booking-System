using System.Collections.Generic;

namespace RF_Schedule
{
    public class ProjectWizardState
    {
        // 暫存Wizard資料

        public string ProjectName { get; set; } = string.Empty;

        public string Priority {  get; set; } = string.Empty;

        public string Notes {  get; set; } = string.Empty;

        public List<int> SelectedRegulationIds { get; set; } = new();

        public List<int> SelectedTestItemIds { get; set; } = new();

        public List<string> OtherTestItems { get; set; } = new();

    }
}

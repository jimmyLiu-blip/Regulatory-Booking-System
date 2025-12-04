using DevExpress.XtraBars;
using DevExpress.XtraScheduler;
using System;
using System.Linq;

namespace RF_Schedule
{
    public partial class CalendarPage : DevExpress.XtraEditors.XtraUserControl
    {
        // null = 全部, "A" = 只顯示 A 區, "B" = 只顯示 B 區
        private string? _currentGroupFilter = null;

        public CalendarPage()
        {
            InitializeComponent();

            // 在這裡訂閱 FilterResource 事件（很重要，要在資料初始化前）
            schedulerDataStorage1.FilterResource += schedulerDataStorage1_FilterResource;
        }


        private void btnViewDay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Day;
        }

        private void btnViewWeek_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Week;
        }

        private void btnViewMonth_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
        }

        private void btnToday_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.Start = DateTime.Today;
        }

        private void btnPrevDay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPreviousPeriod();
        }

        private void schedulerControl1_Click(object sender, EventArgs e)
        {

        }

        private void GoToPreviousPeriod()
        {
            switch (schedulerControl1.ActiveViewType)
            {
                case SchedulerViewType.Day:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(-1);
                    break;
                case SchedulerViewType.Week:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(-7);
                    break;
                case SchedulerViewType.Month:
                    var currentStart = schedulerControl1.Start;
                    // 改用 ActiveView 的實際顯示日期
                    var monthView = schedulerControl1.ActiveView as DevExpress.XtraScheduler.MonthView;
                    if (monthView != null) // 確認是不是使用月視圖
                    {
                        var visibleInterval = monthView.GetVisibleIntervals()[0];
                        var displayedMonth = visibleInterval.Start.AddDays(15); // 取中間日期確保在正確月份
                        var targetMonth = new DateTime(displayedMonth.Year, displayedMonth.Month, 1).AddMonths(-1);
                        schedulerControl1.Start = targetMonth;
                    }
                    else
                    {
                        var firstDayOfMonth = new DateTime(currentStart.Year, currentStart.Month, 1);
                        schedulerControl1.Start = firstDayOfMonth.AddMonths(-1);
                    }
                    break;
                default:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(-1);
                    break;
            }
        }

        private void GoToNextPeriod()
        {
            switch (schedulerControl1.ActiveViewType)
            {
                case SchedulerViewType.Day:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(1);
                    break;
                case SchedulerViewType.Week:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(7);
                    break;
                case SchedulerViewType.Month:
                    var currentStart = schedulerControl1.Start;

                    var monthView = schedulerControl1.ActiveView as DevExpress.XtraScheduler.MonthView;
                    if (monthView != null)
                    {
                        var visibleInterval = monthView.GetVisibleIntervals()[0];
                        var displayedMonth = visibleInterval.Start.AddDays(15);
                        var targetMonth = new DateTime(displayedMonth.Year, displayedMonth.Month, 1).AddMonths(1);
                        schedulerControl1.Start = targetMonth;
                    }
                    else
                    {
                        var firstDayOfMonth = new DateTime(currentStart.Year, currentStart.Month, 1);
                        schedulerControl1.Start = firstDayOfMonth.AddMonths(1);
                    }
                    break;
                default:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(1);
                    break;
            }
        }


        private void btnNextDay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToNextPeriod();
        }


        private void InitResources()  // 假資料會刪除
        {
            schedulerDataStorage1.Resources.CustomFieldMappings.Add(
            new DevExpress.XtraScheduler.ResourceCustomFieldMapping("Group", "Group")
            );
            var storage = schedulerDataStorage1;

            // ============================
            // A 區場地
            // ============================
            AddResource(storage, 101, "SAC1", "A");
            AddResource(storage, 102, "SAC2", "A");
            AddResource(storage, 103, "SAC3", "A");
            AddResource(storage, 104, "FAC1", "A");

            AddResource(storage, 111, "Conducted 1", "A");
            AddResource(storage, 112, "Conducted 2", "A");
            AddResource(storage, 113, "Conducted 3", "A");
            AddResource(storage, 114, "Conducted 4", "A");
            AddResource(storage, 115, "Conducted 5", "A");
            AddResource(storage, 116, "Conducted 6", "A");

            // ============================
            // B 區場地
            // ============================
            AddResource(storage, 201, "SAC C", "B");
            AddResource(storage, 202, "SAC D", "B");
            AddResource(storage, 203, "SAC G", "B");
            AddResource(storage, 204, "FAC A", "B");

            AddResource(storage, 211, "Conducted A", "B");
            AddResource(storage, 212, "Conducted B", "B");
            AddResource(storage, 213, "Conducted C", "B");
            AddResource(storage, 214, "Conducted D", "B");
            AddResource(storage, 215, "Conducted E", "B");
            AddResource(storage, 216, "Conducted F", "B");
        }


        private void AddResource(SchedulerDataStorage storage, int id, string caption, string group)
        {
            var res = storage.CreateResource(id);  // 每個場地唯一 ID
            res.Caption = caption;                 // 顯示名稱
            res.CustomFields["Group"] = group;     // A or B
            storage.Resources.Add(res);            // 加進 Scheduler
        }

        private void CalendarPage_Load_1(object sender, EventArgs e)
        {
            InitResources();
        }

        private void btnViewTimeLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            schedulerControl1.ActiveViewType = SchedulerViewType.Timeline;
        }

        private void btnFilterAreaA_ItemClick(object sender, ItemClickEventArgs e)
        {
            _currentGroupFilter = "A";                       // 只看 A 區
            schedulerControl1.ActiveView.LayoutChanged();    // 通知 Scheduler 重新套用 FilterResource
        }

        private void btnFilterAreaB_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            _currentGroupFilter = "B";
            schedulerControl1.ActiveView.LayoutChanged();
        }

        private void btnFilterAllResources_ItemClick(object sender, ItemClickEventArgs e)
        {
            _currentGroupFilter = null;                      // 清掉 Filter = 顯示全部場地
            schedulerControl1.ActiveView.LayoutChanged();
        }

        private void schedulerDataStorage1_FilterResource(object sender, PersistentObjectCancelEventArgs e)
        {
            // 沒有指定 Filter → 顯示全部資源
            if (string.IsNullOrEmpty(_currentGroupFilter))
                return;

            Resource res = (Resource)e.Object;
            var group = res.CustomFields["Group"]?.ToString();

            // 如果這個 Resource 的 Group 不是目前選的，就把它隱藏
            if (!string.Equals(group, _currentGroupFilter, StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;   // 取消顯示這個資源
            }
        }


    }
}

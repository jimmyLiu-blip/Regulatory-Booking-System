using DevExpress.XtraBars;
using DevExpress.XtraScheduler;
using System;
using System.Linq;

namespace RF_Schedule
{
    public partial class CalendarPage : DevExpress.XtraEditors.XtraUserControl
    {
        // ================================
        // 【Ⅰ】排程畫面狀態（顯示哪一區）
        // ================================
        // null = 全部場地
        // "A" = A 區
        // "B" = B 區
        private string? _currentGroupFilter = null;



        public CalendarPage()
        {
            InitializeComponent();

            // 【重要】先註冊 Resource 過濾事件
            schedulerDataStorage1.FilterResource += schedulerDataStorage1_FilterResource;
        }



        // =======================================
        // 【Ⅱ】UI：視圖切換（日 / 週 / 月 / 時間軸）
        // =======================================
        private void btnViewDay_ItemClick(object sender, ItemClickEventArgs e)
            => schedulerControl1.ActiveViewType = SchedulerViewType.Day;

        private void btnViewWeek_ItemClick(object sender, ItemClickEventArgs e)
            => schedulerControl1.ActiveViewType = SchedulerViewType.Week;

        private void btnViewMonth_ItemClick(object sender, ItemClickEventArgs e)
            => schedulerControl1.ActiveViewType = SchedulerViewType.Month;

        private void btnViewTimeLine_ItemClick(object sender, ItemClickEventArgs e)
            => schedulerControl1.ActiveViewType = SchedulerViewType.Timeline;



        // =======================================
        // 【Ⅲ】UI：日期切換（前一天 / 下一天 / 今天）
        // =======================================
        private void btnToday_ItemClick(object sender, ItemClickEventArgs e)
            => schedulerControl1.Start = DateTime.Today;

        private void btnPrevDay_ItemClick(object sender, ItemClickEventArgs e)
            => GoToPreviousPeriod();

        private void btnNextDay_ItemClick(object sender, ItemClickEventArgs e)
            => GoToNextPeriod();



        // =======================================================
        // 【Ⅳ】日期切換邏輯（依目前視圖 Day/Week/Month 推移）
        // =======================================================
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
                    MoveMonth(-1);
                    break;

                default:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(-7);
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
                    MoveMonth(1);
                    break;

                default:
                    schedulerControl1.Start = schedulerControl1.Start.AddDays(7);
                    break;
            }
        }


        // 獨立方法（簡化 Month 切換邏輯）
        private void MoveMonth(int offset)
        {
            var monthView = schedulerControl1.ActiveView as MonthView;

            if (monthView != null)
            {
                var visibleInterval = monthView.GetVisibleIntervals()[0];
                var midDate = visibleInterval.Start.AddDays(15);
                var targetMonth = new DateTime(midDate.Year, midDate.Month, 1).AddMonths(offset);
                schedulerControl1.Start = targetMonth;
            }
            else
            {
                var currentStart = schedulerControl1.Start;
                var first = new DateTime(currentStart.Year, currentStart.Month, 1);
                schedulerControl1.Start = first.AddMonths(offset);
            }
        }



        // ============================================
        // 【Ⅴ】場地篩選（A 區 / B 區 / 全部）
        // ============================================
        private void btnFilterAreaA_ItemClick(object sender, ItemClickEventArgs e)
        {
            _currentGroupFilter = "A";
            schedulerControl1.ActiveView.LayoutChanged();
        }

        private void btnFilterAreaB_ItemClick(object sender, ItemClickEventArgs e)
        {
            _currentGroupFilter = "B";
            schedulerControl1.ActiveView.LayoutChanged();
        }

        private void btnFilterAllResources_ItemClick(object sender, ItemClickEventArgs e)
        {
            _currentGroupFilter = null; // 顯示全部
            schedulerControl1.ActiveView.LayoutChanged();
        }



        // ===========================================================
        // 【Ⅵ】FilterResource 事件：真正決定哪些 Resource 顯示
        // ===========================================================
        private void schedulerDataStorage1_FilterResource(object sender, PersistentObjectCancelEventArgs e)
        {
            // 沒有篩選 → 顯示全部
            if (string.IsNullOrEmpty(_currentGroupFilter))
                return;

            Resource res = (Resource)e.Object;
            string? group = res.CustomFields["Group"]?.ToString();

            // 如果這個場地不屬於目前要顯示的 Group → 隱藏
            if (!string.Equals(group, _currentGroupFilter, StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
            }
        }



        // ================================================================
        // 【Ⅶ】初始化場地（假資料）★ 未來改 DB → 這段全部會刪除 ★
        // ================================================================
        private void CalendarPage_Load_1(object sender, EventArgs e)
        {
            InitResources();  // TODO: 這整段未來接資料庫後會刪除
        }

        private void InitResources()  // TODO: 假資料會刪除，正式版資料從 DB 載入
        {
            schedulerDataStorage1.Resources.CustomFieldMappings.Add(
                new ResourceCustomFieldMapping("Group", "Group")
            );

            var storage = schedulerDataStorage1;

            // ============ A 區（假資料）============
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

            // ============ B 區（假資料）============
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
            var res = storage.CreateResource(id);
            res.Caption = caption;
            res.CustomFields["Group"] = group; // A or B
            storage.Resources.Add(res);
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

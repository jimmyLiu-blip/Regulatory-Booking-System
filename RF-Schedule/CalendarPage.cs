using DevExpress.XtraScheduler;
using System;

namespace RF_Schedule
{
    public partial class CalendarPage : DevExpress.XtraEditors.XtraUserControl
    {
        public CalendarPage()
        {
            InitializeComponent();
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
                    var firstDayOfMonth = new DateTime(currentStart.Year, currentStart.Month, 1);
                    schedulerControl1.Start = firstDayOfMonth.AddMonths(-1); 
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
                    var firstDayOfMonth = new DateTime(currentStart.Year, currentStart.Month, 1);
                    schedulerControl1.Start = firstDayOfMonth.AddMonths(1); 
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
    }
}

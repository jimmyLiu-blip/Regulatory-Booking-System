using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraScheduler;


namespace RF_Schedule
{
    public partial class Form1 : RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
            schedulerControl.Start = System.DateTime.Now;
        }

        private void schedulerControl_Click(object sender, EventArgs e)
        {

        }
    }
}
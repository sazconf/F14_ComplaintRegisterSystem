using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace F14_ComplaintRegisterSystem
{
    public partial class FrmReportViewer : Form
    {
        public string SelectedCategory { get; set; }
        public FrmReportViewer()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void FrmReportViewer_Load(object sender, EventArgs e)
        {
            ComplaintReport rpt = new ComplaintReport();

            crystalReportViewer1.ReportSource = rpt;

            crystalReportViewer1.Refresh();
        }
    }
}

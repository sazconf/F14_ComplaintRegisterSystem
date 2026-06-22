using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            try
            {
                DataTable dt = new DataTable();

                string query;

                if (SelectedCategory == "All Categories")
                {
                    query = @"
                        SELECT
                            complaint_id,
                            user_id,
                            Citizen,
                            Category,
                            Status,
                            title,
                            description,
                            report_date,
                            rejection_reason
                        FROM vw_Complaints";
                }
                else
                {
                    query = @"
                        SELECT
                            complaint_id,
                            user_id,
                            Citizen,
                            Category,
                            Status,
                            title,
                            description,
                            report_date,
                            rejection_reason
                        FROM vw_Complaints
                        WHERE Category = @Category";
                }

                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    using (SqlCommand cmd =
                        new SqlCommand(query, conn))
                    {
                        if (SelectedCategory != "All Categories")
                        {
                            cmd.Parameters.AddWithValue(
                                "@Category",
                                SelectedCategory);
                        }

                        SqlDataAdapter da =
                            new SqlDataAdapter(cmd);

                        da.Fill(dt);
                    }
                }

                ComplaintReport4 rpt =
                    new ComplaintReport4();

                rpt.SetDataSource(dt);

                crystalReportViewer1.ReportSource = rpt;

                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Report Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
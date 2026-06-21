using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace F14_ComplaintRegisterSystem
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategoryChart();
            LoadStatusChart();

            MessageBox.Show(
                "Dashboard refreshed successfully.",
                "Dashboard",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void LoadCategoryChart()
        {
            try
            {
                chartCategory.Series.Clear();
                chartCategory.Titles.Clear();

                chartCategory.Titles.Add("Complaints by Category");

                Series series = new Series("Complaints");
                series.ChartType = SeriesChartType.Column;

                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    string query = @"
                SELECT
                    c.category_name,
                    COUNT(*) AS TotalComplaints
                FROM complaints cp
                INNER JOIN categories c
                    ON cp.category_id = c.category_id
                GROUP BY c.category_name
                ORDER BY TotalComplaints DESC";

                    SqlCommand cmd =
                        new SqlCommand(query, conn);

                    SqlDataReader reader =
                        cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        series.Points.AddXY(
                            reader["category_name"].ToString(),
                            Convert.ToInt32(
                                reader["TotalComplaints"]));
                    }
                }

                chartCategory.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void LoadStatusChart()
        {
            try
            {
                chartStatus.Series.Clear();
                chartStatus.Titles.Clear();

                chartStatus.Titles.Add("Complaints by Status");

                Series series = new Series("Status");
                series.ChartType = SeriesChartType.Pie;

                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    string query = @"
                SELECT
                    s.status_name,
                    COUNT(*) AS TotalComplaints
                FROM complaints c
                INNER JOIN status s
                    ON c.status_id = s.status_id
                GROUP BY s.status_name";

                    SqlCommand cmd =
                        new SqlCommand(query, conn);

                    SqlDataReader reader =
                        cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int pointIndex = series.Points.AddXY(
                            reader["status_name"].ToString(),
                            Convert.ToInt32(
                                reader["TotalComplaints"]));

                        series.Points[pointIndex].LegendText =
                            reader["status_name"].ToString();

                        series.Points[pointIndex].Label =
                            "#PERCENT{P0}";
                    }
                }

                chartStatus.Series.Add(series);

                chartStatus.Legends.Clear();
                chartStatus.Legends.Add(new Legend());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            LoadCategoryChart();
            LoadStatusChart();

        }
    }
}

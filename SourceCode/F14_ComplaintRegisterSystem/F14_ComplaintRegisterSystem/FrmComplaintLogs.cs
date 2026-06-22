using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F14_ComplaintRegisterSystem
{
    public partial class FrmComplaintLogs : Form
    {
        public FrmComplaintLogs()
        {
            InitializeComponent();
        }


        private void LoadLogs()
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    string query =
                        @"SELECT
                    log_id,
                    complaint_id,
                    action_type,
                    old_status_id,
                    new_status_id,
                    action_date
                  FROM ComplaintLogs
                  ORDER BY action_date DESC";

                    SqlDataAdapter da =
                        new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dgvLogs.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FrmComplaintLogs_Load(
    object sender,
    EventArgs e)
        {
            LoadLogs();
        }

        private void btnRefresh_Click(
    object sender,
    EventArgs e)
        {
            LoadLogs();
        }
    }
}

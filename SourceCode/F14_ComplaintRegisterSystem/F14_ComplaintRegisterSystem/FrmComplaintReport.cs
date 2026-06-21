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

namespace F14_ComplaintRegisterSystem
{
    public partial class FrmComplaintReport : Form
    {


        public FrmComplaintReport()
        {




            InitializeComponent();
        }

        private void FrmComplaintReport_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategory.Items.Clear();

                cmbCategory.Items.Add("All Categories");

                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    string query =
                        "SELECT category_name " +
                        "FROM categories " +
                        "ORDER BY category_name";

                    SqlCommand cmd =
                        new SqlCommand(query, conn);

                    SqlDataReader reader =
                        cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cmbCategory.Items.Add(
                            reader["category_name"].ToString());
                    }
                }

                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading categories: " +
                    ex.Message);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            FrmReportViewer frm = new FrmReportViewer();

            frm.SelectedCategory = cmbCategory.Text;

            frm.ShowDialog();
        }
    }
}

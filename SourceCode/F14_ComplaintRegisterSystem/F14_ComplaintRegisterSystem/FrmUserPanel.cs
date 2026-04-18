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
    public partial class FrmUserPanel : Form
    {
        string connStr = @"Data Source=SAZZAD_LAPTOP\SQLSERVERDEV;Initial Catalog=ComplaintDB;Integrated Security=True";
        public FrmUserPanel()
        {
            InitializeComponent();

            LoadUsers();
            LoadCategories();
        }

        private void LoadUsers()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT user_id, full_name FROM dbo.users WHERE role='Citizen'", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            cmbUser.DataSource = dt;
            cmbUser.DisplayMember = "full_name";
            cmbUser.ValueMember = "user_id";

            conn.Close();
        }

        private void LoadCategories()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT category_id, category_name FROM dbo.categories", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";

            conn.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Enter complaint title.");
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Enter description.");
                return;
            }

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO dbo.complaints
        (
            user_id,
            category_id,
            status_id,
            title,
            description,
            report_date,
            rejection_reason
        )
        VALUES
        (
            @user_id,
            @category_id,
            1,
            @title,
            @description,
            @report_date,
            ''
        )", conn);

            cmd.Parameters.AddWithValue("@user_id", cmbUser.SelectedValue);
            cmd.Parameters.AddWithValue("@category_id", cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
            cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@report_date", dtpDate.Value.Date);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Complaint submitted successfully.");

            txtTitle.Clear();
            txtDescription.Clear();
        }
    }
}

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
    public partial class FrmComplaint : Form
    {
        string connStr = @"Data Source=SAZZAD_LAPTOP\SQLSERVERDEV;Initial Catalog=ComplaintDB;Integrated Security=True";
        public FrmComplaint()
        {
            InitializeComponent();

            LoadUsers();
            LoadCategories();
            LoadStatus();

        }

        private void LoadUsers()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT user_id, full_name FROM dbo.users WHERE role='Citizen'", conn);
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

            SqlCommand cmd = new SqlCommand("SELECT category_id, category_name FROM dbo.categories", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";

            conn.Close();
        }

        private void LoadStatus()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT status_id, status_name FROM dbo.status", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = "status_name";
            cmbStatus.ValueMember = "status_id";

            conn.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "" || txtDescription.Text == "")
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO dbo.complaints
        (user_id, category_id, status_id, title, description, report_date, rejection_reason)
        VALUES
        (@user_id, @category_id, @status_id, @title, @description, @report_date, @rejection_reason)", conn);

            cmd.Parameters.AddWithValue("@user_id", cmbUser.SelectedValue);
            cmd.Parameters.AddWithValue("@category_id", cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@status_id", cmbStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@report_date", dtpDate.Value.Date);
            cmd.Parameters.AddWithValue("@rejection_reason", txtRejectReason.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Complaint inserted successfully.");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            c.complaint_id,
            u.full_name AS Citizen,
            cat.category_name AS Category,
            s.status_name AS Status,
            c.title,
            c.description,
            c.report_date,
            c.rejection_reason
        FROM dbo.complaints c
        INNER JOIN dbo.users u ON c.user_id = u.user_id
        INNER JOIN dbo.categories cat ON c.category_id = cat.category_id
        INNER JOIN dbo.status s ON c.status_id = s.status_id", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            dgvComplaints.DataSource = dt;

            conn.Close();
        }
    }
}

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

        private void dgvComplaints_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvComplaints.Rows[e.RowIndex];

                txtComplaintID.Text = row.Cells["complaint_id"].Value.ToString();
                txtTitle.Text = row.Cells["title"].Value.ToString();
                txtDescription.Text = row.Cells["description"].Value.ToString();
                txtRejectReason.Text = row.Cells["rejection_reason"].Value.ToString();

                dtpDate.Value = Convert.ToDateTime(row.Cells["report_date"].Value);

                cmbUser.Text = row.Cells["Citizen"].Value.ToString();
                cmbCategory.Text = row.Cells["Category"].Value.ToString();
                cmbStatus.Text = row.Cells["Status"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtComplaintID.Text == "")
            {
                MessageBox.Show("Please select a complaint first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to update this record?",
                "Confirm Update",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                @"UPDATE dbo.complaints
          SET user_id = @user_id,
              category_id = @category_id,
              status_id = @status_id,
              title = @title,
              description = @description,
              report_date = @report_date,
              rejection_reason = @rejection_reason
          WHERE complaint_id = @complaint_id", conn);

            cmd.Parameters.AddWithValue("@user_id", cmbUser.SelectedValue);
            cmd.Parameters.AddWithValue("@category_id", cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@status_id", cmbStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@report_date", dtpDate.Value.Date);
            cmd.Parameters.AddWithValue("@rejection_reason", txtRejectReason.Text);
            cmd.Parameters.AddWithValue("@complaint_id", txtComplaintID.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Complaint updated successfully.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtComplaintID.Text == "")
            {
                MessageBox.Show("Please select a complaint first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM dbo.complaints WHERE complaint_id = @complaint_id", conn);

            cmd.Parameters.AddWithValue("@complaint_id", txtComplaintID.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Complaint deleted successfully.");
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
        INNER JOIN dbo.status s ON c.status_id = s.status_id
        WHERE c.title LIKE @search
           OR u.full_name LIKE @search", conn);

            cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            dgvComplaints.DataSource = dt;

            conn.Close();
        }
    }
}

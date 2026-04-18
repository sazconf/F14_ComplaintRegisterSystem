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
        
        public FrmUserPanel()
        {
            InitializeComponent();

            LoadUsers();
            LoadCategories();
        }

        private void LoadUsers()
        {
            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
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
            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
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

            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
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
            btnView.PerformClick();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(@"
        SELECT
            c.complaint_id,
            c.title,
            c.description,
            cat.category_name AS Category,
            s.status_name AS Status,
            c.report_date,
            c.rejection_reason
        FROM dbo.complaints c
        INNER JOIN dbo.categories cat ON c.category_id = cat.category_id
        INNER JOIN dbo.status s ON c.status_id = s.status_id
        WHERE c.user_id = @user_id", conn);

            cmd.Parameters.AddWithValue("@user_id", cmbUser.SelectedValue);

            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);

            dgvMyComplaints.DataSource = dt;

            dgvMyComplaints.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvMyComplaints.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvMyComplaints.ReadOnly = true;

            conn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtDescription.Clear();

            if (cmbUser.Items.Count > 0)
                cmbUser.SelectedIndex = 0;

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            dtpDate.Value = DateTime.Now;

            dgvMyComplaints.DataSource = null;

            txtTitle.Focus();
        }
    }
}

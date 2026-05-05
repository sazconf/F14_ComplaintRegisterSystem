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
            LoadCategories();
        }

        // Load Categories
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT category_id, category_name FROM dbo.categories", conn);

                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);

                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "category_name";
                cmbCategory.ValueMember = "category_id";
            }
        }

        // FORM LOAD
        private void FrmUserPanel_Load(object sender, EventArgs e)
        {
            lblUserWelcome.Text = "Welcome, " + Session.UserName;

            // Disable manual date change
            dtpDate.Value = DateTime.Now;
            dtpDate.Enabled = false;
        }

        // SUBMIT COMPLAINT
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

            // Prevent very short complaints
            if (txtDescription.Text.Length < 10)
            {
                MessageBox.Show("Description must be at least 10 characters.");
                return;
            }

            if (txtTitle.Text.Length < 5)
            {
                MessageBox.Show("Title too short.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
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

                cmd.Parameters.AddWithValue("@user_id", Session.UserID);
                cmd.Parameters.AddWithValue("@category_id", cmbCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@report_date", DateTime.Now);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Complaint submitted successfully.");

            txtTitle.Clear();
            txtDescription.Clear();

            btnView.PerformClick();
        }

        // VIEW MY COMPLAINTS
        private void btnView_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
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

                cmd.Parameters.AddWithValue("@user_id", Session.UserID);

                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);

                dgvMyComplaints.DataSource = dt;

                dgvMyComplaints.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvMyComplaints.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvMyComplaints.ReadOnly = true;
            }
        }

        // CLEAR FORM
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtDescription.Clear();

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            dtpDate.Value = DateTime.Now;

            dgvMyComplaints.DataSource = null;

            txtTitle.Focus();
        }

        // BACK BUTTON
        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmConnection frm = new FrmConnection();
            frm.Show();
            this.Close();
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, digits, space, and backspace
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
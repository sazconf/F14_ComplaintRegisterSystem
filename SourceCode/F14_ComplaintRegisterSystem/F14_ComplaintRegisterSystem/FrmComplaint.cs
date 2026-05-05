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
        public FrmComplaint()
        {
            InitializeComponent();

            LoadUsers();
            LoadCategories();
            LoadStatus();
            LoadTotalRecords();

            // Lock date
            dtpDate.Enabled = true;



            //DTP Validation
            dtpDate.MinDate = new DateTime(2010, 1, 1);
            dtpDate.MaxDate = new DateTime(2050, 12, 31);

        }

        // LOAD USERS
        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT user_id, full_name FROM dbo.users WHERE role='Citizen'", conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                cmbUser.DataSource = dt;
                cmbUser.DisplayMember = "full_name";
                cmbUser.ValueMember = "user_id";
            }
        }

        // LOAD CATEGORIES
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT category_id, category_name FROM dbo.categories", conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "category_name";
                cmbCategory.ValueMember = "category_id";
            }
        }

        // LOAD STATUS
        private void LoadStatus()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT status_id, status_name FROM dbo.status", conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                cmbStatus.DataSource = dt;
                cmbStatus.DisplayMember = "status_name";
                cmbStatus.ValueMember = "status_id";
            }
        }

        // STATUS CHANGE EVENT
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRejectReason.Enabled = true; // ALWAYS enabled

            if (cmbStatus.SelectedValue != null && cmbStatus.SelectedValue.ToString() != "5")
            {
                txtRejectReason.Clear();
            }
        }

        // INSERT
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (cmbUser.SelectedIndex == -1 ||
                cmbCategory.SelectedIndex == -1 ||
                txtTitle.Text.Trim() == "" ||
                txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Fill all required fields.");
                return;
            }

            if (cmbStatus.Text == "Rejected")
            {
                if (txtRejectReason.Text.Trim() == "" || txtRejectReason.Text.Length < 5)
                {
                    MessageBox.Show("Enter valid rejection reason.");
                    return;
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO dbo.complaints
                        (user_id, category_id, status_id, title, description, report_date, rejection_reason)
                        VALUES
                        (@user_id, @category_id, @status_id, @title, @description, @report_date, @rejection_reason)", conn);

                    cmd.Parameters.AddWithValue("@user_id", cmbUser.SelectedValue);
                    cmd.Parameters.AddWithValue("@category_id", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@status_id", cmbStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@report_date", dtpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@rejection_reason", txtRejectReason.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                LoadTotalRecords();
                MessageBox.Show("Complaint inserted successfully.");
                btnShow.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // SHOW
        private void btnShow_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
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

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                dgvComplaints.DataSource = dt;

                dgvComplaints.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComplaints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvComplaints.ReadOnly = true;
            }
        }

        // GRID CLICK
        private void dgvComplaints_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvComplaints.Rows[e.RowIndex];

                txtComplaintID.Text = row.Cells["complaint_id"].Value.ToString();
                txtTitle.Text = row.Cells["title"].Value.ToString();
                txtDescription.Text = row.Cells["description"].Value.ToString();
                txtRejectReason.Text = row.Cells["rejection_reason"].Value.ToString();

                cmbUser.Text = row.Cells["Citizen"].Value.ToString();
                cmbCategory.Text = row.Cells["Category"].Value.ToString();
                cmbStatus.Text = row.Cells["Status"].Value.ToString();
            }
        }

        // UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtComplaintID.Text == "")
            {
                MessageBox.Show("Select a complaint first.");
                return;
            }

            if (cmbStatus.Text == "Rejected" &&
                (txtRejectReason.Text.Trim() == "" || txtRejectReason.Text.Length < 5))
            {
                MessageBox.Show("Enter valid rejection reason.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Update this complaint?", "Confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        @"UPDATE dbo.complaints
                          SET user_id=@user_id,
                              category_id=@category_id,
                              status_id=@status_id,
                              title=@title,
                              description=@description,
                              report_date=@report_date,
                              rejection_reason=@rejection_reason
                          WHERE complaint_id=@id", conn);

                    cmd.Parameters.AddWithValue("@user_id", cmbUser.SelectedValue);
                    cmd.Parameters.AddWithValue("@category_id", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@status_id", cmbStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@report_date", dtpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@rejection_reason", txtRejectReason.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", txtComplaintID.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadTotalRecords();
                MessageBox.Show("Updated successfully.");
                btnShow.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtComplaintID.Text == "")
            {
                MessageBox.Show("Select a complaint first.");
                return;
            }

            if (MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM dbo.complaints WHERE complaint_id=@id", conn);

                cmd.Parameters.AddWithValue("@id", txtComplaintID.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Deleted successfully.");
            btnShow.PerformClick();
        }

        // SEARCH
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                MessageBox.Show("Enter search keyword.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
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
                    WHERE c.title LIKE @search OR u.full_name LIKE @search", conn);

                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                dgvComplaints.DataSource = dt;
            }
        }

        // TOTAL RECORDS
        private void LoadTotalRecords()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.complaints", conn);
                lblTotal.Text = "Total Records: " + cmd.ExecuteScalar().ToString();
            }
        }

        // CLEAR
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtComplaintID.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
            txtRejectReason.Clear();
            txtSearch.Clear();

            cmbUser.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;

            txtTitle.Focus();
        }

        // BACK
        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmConnection frm = new FrmConnection();
            frm.Show();
            this.Close();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

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
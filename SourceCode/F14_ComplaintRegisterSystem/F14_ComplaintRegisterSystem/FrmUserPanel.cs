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
                MessageBox.Show(
                    "Enter complaint description.");
                return;
            }

            if (txtTitle.Text.Length < 5)
            {
                MessageBox.Show(
                    "Title must be at least 5 characters.");
                return;
            }

            if (txtDescription.Text.Length < 10)
            {
                MessageBox.Show(
                    "Description must be at least 10 characters.");
                return;
            }

            try
            {
                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "sp_InsertComplaint",
                        conn);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@user_id",
                        Session.UserID);

                    cmd.Parameters.AddWithValue(
                        "@category_id",
                        cmbCategory.SelectedValue);

                    cmd.Parameters.AddWithValue(
                        "@status_id",
                        1);

                    cmd.Parameters.AddWithValue(
                        "@title",
                        txtTitle.Text.Trim());

                    cmd.Parameters.AddWithValue(
                        "@description",
                        txtDescription.Text.Trim());

                    cmd.Parameters.AddWithValue(
                        "@report_date",
                        dtpDate.Value.Date);

                    cmd.Parameters.AddWithValue(
                        "@rejection_reason",
                        "");

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Complaint submitted successfully.");

                    btnView.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message);
            }
        }

        // VIEW MY COMPLAINTS
        private void btnView_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn =
                new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "sp_ViewUserComplaints",
                    conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@user_id",
                    Session.UserID);

                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                bindingSource1.DataSource = dt;
                dgvMyComplaints.DataSource =
                    bindingSource1;

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

        private void dgvMyComplaints_CellClick(
    object sender,
    DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dgvMyComplaints.Rows[e.RowIndex];

                txtTitle.Text =
                    row.Cells["title"].Value.ToString();

                txtDescription.Text =
                    row.Cells["description"].Value.ToString();

                cmbCategory.Text =
                    row.Cells["Category"].Value.ToString();

                dtpDate.Value =
                    Convert.ToDateTime(
                        row.Cells["report_date"].Value);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                MessageBox.Show("Enter search keyword.");
                return;
            }

            using (SqlConnection conn =
                new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "sp_SearchUserComplaints",
                    conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@user_id",
                    Session.UserID);

                cmd.Parameters.AddWithValue(
                    "@search",
                    txtSearch.Text.Trim());

                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                bindingSource1.DataSource = dt;
                dgvMyComplaints.DataSource =
                    bindingSource1;
            }
        }

        private void btnUnsafeSearch_Click(
    object sender,
    EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                MessageBox.Show("Enter search keyword.");
                return;
            }

            using (SqlConnection conn =
                new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();

                string query =
                    "SELECT * FROM vw_Complaints " +
                    "WHERE user_id = " +
                    Session.UserID +
                    " AND title LIKE '%" +
                    txtSearch.Text.Trim() +
                    "%'";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                bindingSource1.DataSource = dt;
                dgvMyComplaints.DataSource =
                    bindingSource1;
            }
        }
    }
}
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

            bindingSource1.CurrentChanged +=
            bindingSource1_CurrentChanged;

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
        //Binding Navigator Current Changed Event

        private void bindingSource1_CurrentChanged(
    object sender,
    EventArgs e)
        {
            if (bindingSource1.Current == null)
                return;

            DataRowView row =
                (DataRowView)bindingSource1.Current;

            txtComplaintID.Text =
                row["complaint_id"].ToString();

            txtTitle.Text =
                row["title"].ToString();

            txtDescription.Text =
                row["description"].ToString();

            txtRejectReason.Text =
                row["rejection_reason"].ToString();

            cmbUser.Text =
                row["Citizen"].ToString();

            cmbCategory.Text =
                row["Category"].ToString();

            cmbStatus.Text =
                row["Status"].ToString();

            DateTime reportDate;

            if (DateTime.TryParse(
                row["report_date"].ToString(),
                out reportDate))
            {
                dtpDate.Value = reportDate;
            }
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

            // Rejection validation
            if (cmbStatus.Text == "Rejected")
            {
                if (txtRejectReason.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Enter valid rejection reason.");
                    return;
                }
            }

            try
            {
                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "sp_InsertComplaint", conn);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@user_id",
                        cmbUser.SelectedValue);

                    cmd.Parameters.AddWithValue(
                        "@category_id",
                        cmbCategory.SelectedValue);

                    cmd.Parameters.AddWithValue(
                        "@status_id",
                        cmbStatus.SelectedValue);

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
                        txtRejectReason.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                LoadTotalRecords();

                MessageBox.Show(
                    "Complaint inserted successfully.");

                btnShow.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // SHOW
        private void btnShow_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn =
                new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM vw_Complaints",
                    conn);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                bindingSource1.DataSource = dt;
                dgvComplaints.DataSource = bindingSource1;

                dgvComplaints.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvComplaints.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

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
                MessageBox.Show(
                    "Select a complaint first.");
                return;
            }

            // Rejection validation
            if (cmbStatus.Text == "Rejected")
            {
                if (txtRejectReason.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Enter valid rejection reason.");
                    return;
                }
            }

            DialogResult result = MessageBox.Show(
                "Update this complaint?",
                "Confirm",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "sp_UpdateComplaint", conn);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@complaint_id",
                        txtComplaintID.Text);

                    cmd.Parameters.AddWithValue(
                        "@user_id",
                        cmbUser.SelectedValue);

                    cmd.Parameters.AddWithValue(
                        "@category_id",
                        cmbCategory.SelectedValue);

                    cmd.Parameters.AddWithValue(
                        "@status_id",
                        cmbStatus.SelectedValue);

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
                        txtRejectReason.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                LoadTotalRecords();

                MessageBox.Show(
                    "Updated successfully.");

                btnShow.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            DialogResult result = MessageBox.Show(
                "Delete this record?",
                "Confirm",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn =
                    new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "sp_DeleteComplaint", conn);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@complaint_id",
                        txtComplaintID.Text);

                    cmd.ExecuteNonQuery();
                }

                LoadTotalRecords();

                MessageBox.Show(
                    "Deleted successfully.");

                btnShow.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // SEARCH
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
                    "sp_SearchComplaint",
                    conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@search",
                    txtSearch.Text.Trim());

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                bindingSource1.DataSource = dt;
                dgvComplaints.DataSource = bindingSource1;

                dgvComplaints.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvComplaints.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvComplaints.ReadOnly = true;
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

        private void btnUnsafeSearch_Click(object sender, EventArgs e)
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
                    "WHERE title LIKE '%" +
                    txtSearch.Text.Trim() +
                    "%'";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                bindingSource1.DataSource = dt;
                dgvComplaints.DataSource = bindingSource1;
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            FrmImportComplaints frm = new FrmImportComplaints();
            frm.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FrmComplaintReport frm =
                new FrmComplaintReport();

            frm.ShowDialog();
        }



        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.ShowDialog();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            bool anyVisible = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f != this && f.Visible)
                {
                    anyVisible = true;
                    break;
                }
            }
            if (!anyVisible)
            {
                Application.Exit();
            }
        }
    }
}
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
    }
}

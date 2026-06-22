using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F14_ComplaintRegisterSystem
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // 1. Empty validation
            if (name == "" || email == "" || password == "" || confirmPassword == "")
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            // 2. Name validation (only letters + space)
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Name can contain only letters.");
                return;
            }

            // 3. Email validation
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            // 4. Password length
            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.");
                return;
            }

            // 5. Password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
            conn.Open();

            // 6. Duplicate email check
            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM dbo.users WHERE email=@email", conn);

            checkCmd.Parameters.AddWithValue("@email", email);

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Email already exists.");
                conn.Close();
                return;
            }

            // 7. Insert user
            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO dbo.users (full_name, email, password, role)
          VALUES (@name, @email, @password, 'Citizen')", conn);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Registration successful!");

            txtName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void btnGoLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmConnection frm = new FrmConnection();
            frm.Show();
            this.Close();
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

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
using static System.Collections.Specialized.BitVector32;


namespace F14_ComplaintRegisterSystem
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter email and password.");
                return;
            }

            // Add email format check
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT user_id, full_name FROM dbo.users WHERE email=@email AND password=@password",
                conn);

            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Session.UserID = (int)dr["user_id"];
                Session.UserName = dr["full_name"].ToString();

                MessageBox.Show("Login successful!");

                FrmUserPanel frm = new FrmUserPanel();
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }

            conn.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmRegister frm = new FrmRegister();
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

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
    public partial class FrmAdminLogin : Form
    {
        public FrmAdminLogin()
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

            SqlConnection conn = new SqlConnection(DBHelper.ConnStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                @"SELECT user_id, full_name 
          FROM users 
          WHERE email=@email 
          AND password=@password 
          AND role='Admin'", conn);

            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Session.UserID = (int)dr["user_id"];
                Session.UserName = dr["full_name"].ToString();

                MessageBox.Show("Admin login successful!");

                FrmComplaint frm = new FrmComplaint(); // Admin panel
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid admin credentials.");
            }

            conn.Close();
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

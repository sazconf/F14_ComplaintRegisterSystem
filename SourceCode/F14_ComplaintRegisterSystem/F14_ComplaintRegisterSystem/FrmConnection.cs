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
    public partial class FrmConnection : Form
    {
        
        public FrmConnection()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();

                    lblStatus.Text = "Status: Connected";
                    
                    

                    MessageBox.Show("Database connected successfully.");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Failed";
                

                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }

        

        private void btnUserPanel_Click(object sender, EventArgs e)
        {
            FrmUserPanel frm = new FrmUserPanel();
            frm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.Show();
            this.Hide();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            FrmRegister frm = new FrmRegister();
            frm.Show();
            this.Hide();
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            FrmAdminLogin frm = new FrmAdminLogin();
            frm.Show();
            this.Hide();
        }

        private void FrmConnection_Load(object sender, EventArgs e)
        {

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

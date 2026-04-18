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
        string connStr = @"Data Source=SAZZAD_LAPTOP\SQLSERVERDEV;Initial Catalog=ComplaintDB;Integrated Security=True";

        public FrmConnection()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    lblStatus.Text = "Status: Connected";
                    btnEnter.Enabled = true;
                    btnUserPanel.Enabled = true;

                    MessageBox.Show("Database connected successfully.");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Status: Failed";
                btnEnter.Enabled = false;

                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            FrmComplaint frm = new FrmComplaint();
            frm.Show();
            this.Hide();
        }
    }
}

using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace F14_ComplaintRegisterSystem
{
    public partial class FrmImportComplaints : Form
    {
        private DataTable excelData = new DataTable();
        public FrmImportComplaints()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter =
                "Excel Files|*.xlsx;*.xls";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (excelData == null || excelData.Rows.Count == 0)
            {
                MessageBox.Show("No data to import. Please preview first.");
                return;
            }

            // 1. Validate required headers
            string[] requiredHeaders = { "email", "full_name", "category_name", "status_name", "title", "description", "report_date", "rejection_reason" };
            List<string> missingHeaders = new List<string>();

            foreach (var header in requiredHeaders)
            {
                if (!excelData.Columns.Contains(header))
                {
                    missingHeaders.Add(header);
                }
            }

            if (missingHeaders.Count > 0)
            {
                MessageBox.Show("Missing headers:\n" + string.Join("\n", missingHeaders), "Import Error");
                return;
            }

            // Fetch categories and status from DB to validate
            Dictionary<string, int> categoryMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, int> statusMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            try
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT category_id, category_name FROM dbo.categories", conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            categoryMap[dr["category_name"].ToString()] = (int)dr["category_id"];
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT status_id, status_name FROM dbo.status", conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            statusMap[dr["status_name"].ToString()] = (int)dr["status_id"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lookup data: " + ex.Message);
                return;
            }

            // 2. Validate all rows
            List<string> errors = new List<string>();

            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DataRow row = excelData.Rows[i];
                int rowNum = i + 1; // 1-based for error reporting

                string email = row["email"]?.ToString().Trim();
                string fullName = row["full_name"]?.ToString().Trim();
                string categoryName = row["category_name"]?.ToString().Trim();
                string statusName = row["status_name"]?.ToString().Trim();
                string title = row["title"]?.ToString().Trim();
                string description = row["description"]?.ToString().Trim();
                string reportDateStr = row["report_date"]?.ToString().Trim();

                if (string.IsNullOrEmpty(email)) errors.Add($"Row {rowNum}: email is required.");
                if (string.IsNullOrEmpty(fullName)) errors.Add($"Row {rowNum}: full_name is required.");
                if (string.IsNullOrEmpty(title)) errors.Add($"Row {rowNum}: title is required.");
                if (string.IsNullOrEmpty(description)) errors.Add($"Row {rowNum}: description is required.");

                if (string.IsNullOrEmpty(categoryName) || !categoryMap.ContainsKey(categoryName))
                {
                    errors.Add($"Row {rowNum}: category_name '{categoryName}' does not exist.");
                }
                if (string.IsNullOrEmpty(statusName) || !statusMap.ContainsKey(statusName))
                {
                    errors.Add($"Row {rowNum}: status_name '{statusName}' does not exist.");
                }

                DateTime parsedDate;
                if (!DateTime.TryParse(reportDateStr, out parsedDate))
                {
                    errors.Add($"Row {rowNum}: report_date '{reportDateStr}' is not a valid date.");
                }
            }

            if (errors.Count > 0)
            {
                MessageBox.Show("Validation errors found:\n" + string.Join("\n", errors), "Import Validation Failed");
                return;
            }

            // 3. Process Import
            using (SqlConnection conn = new SqlConnection(DBHelper.ConnStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    for (int i = 0; i < excelData.Rows.Count; i++)
                    {
                        DataRow row = excelData.Rows[i];

                        string email = row["email"].ToString().Trim();
                        string fullName = row["full_name"].ToString().Trim();
                        string categoryName = row["category_name"].ToString().Trim();
                        string statusName = row["status_name"].ToString().Trim();
                        string title = row["title"].ToString().Trim();
                        string description = row["description"].ToString().Trim();
                        DateTime reportDate = Convert.ToDateTime(row["report_date"]);
                        string rejectionReason = row["rejection_reason"]?.ToString().Trim() ?? "";

                        int categoryId = categoryMap[categoryName];
                        int statusId = statusMap[statusName];

                        // Handle user
                        int userId = 0;
                        using (SqlCommand cmdUser = new SqlCommand("SELECT user_id FROM dbo.users WHERE email = @email", conn, transaction))
                        {
                            cmdUser.Parameters.AddWithValue("@email", email);
                            object result = cmdUser.ExecuteScalar();
                            if (result != null)
                            {
                                userId = (int)result;
                            }
                        }

                        if (userId == 0)
                        {
                            using (SqlCommand cmdInsertUser = new SqlCommand(
                                "INSERT INTO dbo.users (full_name, email, password, role) OUTPUT INSERTED.user_id VALUES (@fullName, @email, '12345678', 'Citizen')", conn, transaction))
                            {
                                cmdInsertUser.Parameters.AddWithValue("@fullName", fullName);
                                cmdInsertUser.Parameters.AddWithValue("@email", email);
                                userId = (int)cmdInsertUser.ExecuteScalar();
                            }
                        }

                        // Insert Complaint
                        using (SqlCommand cmdComplaint = new SqlCommand("sp_InsertComplaint", conn, transaction))
                        {
                            cmdComplaint.CommandType = CommandType.StoredProcedure;
                            cmdComplaint.Parameters.AddWithValue("@user_id", userId);
                            cmdComplaint.Parameters.AddWithValue("@category_id", categoryId);
                            cmdComplaint.Parameters.AddWithValue("@status_id", statusId);
                            cmdComplaint.Parameters.AddWithValue("@title", title);
                            cmdComplaint.Parameters.AddWithValue("@description", description);
                            cmdComplaint.Parameters.AddWithValue("@report_date", reportDate);
                            cmdComplaint.Parameters.AddWithValue("@rejection_reason", rejectionReason);

                            cmdComplaint.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show($"Import Successful. {excelData.Rows.Count} records imported.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please select an Excel file first.");
                return;
            }

            try
            {
                System.Text.Encoding.RegisterProvider(
                    System.Text.CodePagesEncodingProvider.Instance);

                using (var stream =
                    File.Open(txtFilePath.Text,
                    FileMode.Open,
                    FileAccess.Read))
                {
                    using (var reader =
                        ExcelReaderFactory.CreateReader(stream))
                    {
                        var result =
                            reader.AsDataSet(
                                new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable =
                                        (_) => new ExcelDataTableConfiguration()
                                        {
                                            UseHeaderRow = true
                                        }
                                });

                        excelData =
                            result.Tables[0];

                        dgvPreview.DataSource =
                            excelData;

                        dgvPreview.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }

                MessageBox.Show(
                    "Preview loaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message);
            }
        }
    }
}

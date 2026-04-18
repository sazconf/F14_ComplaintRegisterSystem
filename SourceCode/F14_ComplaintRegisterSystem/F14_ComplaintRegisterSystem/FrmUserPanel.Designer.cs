namespace F14_ComplaintRegisterSystem
{
    partial class FrmUserPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvMyComplaints = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.txtNewEmail = new System.Windows.Forms.TextBox();
            this.txtNewPhone = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyComplaints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Citizen Name:";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(173, 87);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(333, 24);
            this.cmbUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Complaint Title: ";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(173, 127);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(330, 22);
            this.txtTitle.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(173, 176);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(330, 106);
            this.txtDescription.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Catagory:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(173, 309);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(330, 24);
            this.cmbCategory.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 351);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Report Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(173, 351);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(330, 22);
            this.dtpDate.TabIndex = 9;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(53, 385);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(127, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit Complaint";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(200, 385);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(138, 23);
            this.btnView.TabIndex = 11;
            this.btnView.Text = "View My Complaints";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(359, 384);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvMyComplaints
            // 
            this.dgvMyComplaints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyComplaints.Location = new System.Drawing.Point(50, 423);
            this.dgvMyComplaints.Name = "dgvMyComplaints";
            this.dgvMyComplaints.RowHeadersWidth = 51;
            this.dgvMyComplaints.RowTemplate.Height = 24;
            this.dgvMyComplaints.Size = new System.Drawing.Size(993, 161);
            this.dgvMyComplaints.TabIndex = 13;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(50, 609);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(94, 16);
            this.lblInfo.TabIndex = 14;
            this.lblInfo.Text = "Status : Ready";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 15;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(530, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(10, 347);
            this.dataGridView1.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(215, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(219, 29);
            this.label6.TabIndex = 17;
            this.label6.Text = "Submit Complaint";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(654, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(276, 29);
            this.label7.TabIndex = 18;
            this.label7.Text = "New User Registration";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(641, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(644, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 20;
            this.label9.Text = "Email:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(644, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "Phone:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(641, 223);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 16);
            this.label11.TabIndex = 22;
            this.label11.Text = "Password:";
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(724, 90);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(206, 22);
            this.txtNewName.TabIndex = 23;
            // 
            // txtNewEmail
            // 
            this.txtNewEmail.Location = new System.Drawing.Point(724, 127);
            this.txtNewEmail.Name = "txtNewEmail";
            this.txtNewEmail.Size = new System.Drawing.Size(206, 22);
            this.txtNewEmail.TabIndex = 24;
            // 
            // txtNewPhone
            // 
            this.txtNewPhone.Location = new System.Drawing.Point(724, 172);
            this.txtNewPhone.Name = "txtNewPhone";
            this.txtNewPhone.Size = new System.Drawing.Size(206, 22);
            this.txtNewPhone.TabIndex = 25;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(724, 223);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(206, 22);
            this.txtNewPassword.TabIndex = 26;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(636, 276);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 27;
            this.btnAddUser.Text = "Register";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // FrmUserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 653);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtNewPhone);
            this.Controls.Add(this.txtNewEmail);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvMyComplaints);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbUser);
            this.Controls.Add(this.label1);
            this.Name = "FrmUserPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Complaint Panel";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyComplaints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvMyComplaints;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.TextBox txtNewEmail;
        private System.Windows.Forms.TextBox txtNewPhone;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Button btnAddUser;
    }
}
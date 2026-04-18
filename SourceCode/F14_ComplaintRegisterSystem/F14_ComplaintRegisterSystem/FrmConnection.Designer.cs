namespace F14_ComplaintRegisterSystem
{
    partial class FrmConnection
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnUserPanel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(253, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Complaint Register System";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(39, 96);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(139, 16);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status: Not Connected";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(30, 70);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(159, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Connect";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Enabled = false;
            this.btnEnter.Location = new System.Drawing.Point(258, 185);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(113, 23);
            this.btnEnter.TabIndex = 3;
            this.btnEnter.Text = "Admin Panel";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnUserPanel
            // 
            this.btnUserPanel.Enabled = false;
            this.btnUserPanel.Location = new System.Drawing.Point(397, 185);
            this.btnUserPanel.Name = "btnUserPanel";
            this.btnUserPanel.Size = new System.Drawing.Size(128, 23);
            this.btnUserPanel.TabIndex = 4;
            this.btnUserPanel.Text = "User Panel";
            this.btnUserPanel.UseVisualStyleBackColor = true;
            // 
            // FrmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUserPanel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Name = "FrmConnection";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnUserPanel;
    }
}


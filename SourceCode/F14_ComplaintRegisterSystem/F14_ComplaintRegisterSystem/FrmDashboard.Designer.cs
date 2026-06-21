namespace F14_ComplaintRegisterSystem
{
    partial class FrmDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // chartCategory
            // 
            chartArea3.Name = "ChartArea1";
            this.chartCategory.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartCategory.Legends.Add(legend3);
            this.chartCategory.Location = new System.Drawing.Point(44, 68);
            this.chartCategory.Name = "chartCategory";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartCategory.Series.Add(series3);
            this.chartCategory.Size = new System.Drawing.Size(399, 300);
            this.chartCategory.TabIndex = 0;
            this.chartCategory.Text = "chart1";
            // 
            // chartStatus
            // 
            chartArea4.Name = "ChartArea1";
            this.chartStatus.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartStatus.Legends.Add(legend4);
            this.chartStatus.Location = new System.Drawing.Point(469, 68);
            this.chartStatus.Name = "chartStatus";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartStatus.Series.Add(series4);
            this.chartStatus.Size = new System.Drawing.Size(422, 300);
            this.chartStatus.TabIndex = 1;
            this.chartStatus.Text = "chart1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(44, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 531);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chartStatus);
            this.Controls.Add(this.chartCategory);
            this.Name = "FrmDashboard";
            this.Text = "FrmDashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartCategory;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatus;
        private System.Windows.Forms.Button btnRefresh;
    }
}
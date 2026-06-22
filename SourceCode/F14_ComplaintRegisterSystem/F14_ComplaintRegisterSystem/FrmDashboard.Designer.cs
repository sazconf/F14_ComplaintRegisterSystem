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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // chartCategory
            // 
            chartArea1.Name = "ChartArea1";
            this.chartCategory.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartCategory.Legends.Add(legend1);
            this.chartCategory.Location = new System.Drawing.Point(44, 115);
            this.chartCategory.Name = "chartCategory";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCategory.Series.Add(series1);
            this.chartCategory.Size = new System.Drawing.Size(399, 300);
            this.chartCategory.TabIndex = 0;
            this.chartCategory.Text = "chart1";
            // 
            // chartStatus
            // 
            chartArea2.Name = "ChartArea1";
            this.chartStatus.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartStatus.Legends.Add(legend2);
            this.chartStatus.Location = new System.Drawing.Point(449, 115);
            this.chartStatus.Name = "chartStatus";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartStatus.Series.Add(series2);
            this.chartStatus.Size = new System.Drawing.Size(422, 300);
            this.chartStatus.TabIndex = 1;
            this.chartStatus.Text = "chart1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.LightPink;
            this.btnRefresh.Location = new System.Drawing.Point(44, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
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
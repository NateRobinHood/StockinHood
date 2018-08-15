namespace StockinHood.UserControls
{
    partial class StockChart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartStockHistory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblYearToDate = new System.Windows.Forms.LinkLabel();
            this.lblSixMonths = new System.Windows.Forms.LinkLabel();
            this.lblThreeMonths = new System.Windows.Forms.LinkLabel();
            this.lblMonth = new System.Windows.Forms.LinkLabel();
            this.lblFiveDays = new System.Windows.Forms.LinkLabel();
            this.lblToday = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.chartStockHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // chartStockHistory
            // 
            this.chartStockHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartStockHistory.ChartAreas.Add(chartArea1);
            this.chartStockHistory.Location = new System.Drawing.Point(0, 0);
            this.chartStockHistory.Name = "chartStockHistory";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chartStockHistory.Series.Add(series1);
            this.chartStockHistory.Size = new System.Drawing.Size(337, 226);
            this.chartStockHistory.TabIndex = 0;
            this.chartStockHistory.Text = "chart1";
            this.chartStockHistory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartStockHistory_MouseMove);
            // 
            // lblYearToDate
            // 
            this.lblYearToDate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblYearToDate.AutoSize = true;
            this.lblYearToDate.Location = new System.Drawing.Point(31, 229);
            this.lblYearToDate.Name = "lblYearToDate";
            this.lblYearToDate.Size = new System.Drawing.Size(29, 13);
            this.lblYearToDate.TabIndex = 1;
            this.lblYearToDate.TabStop = true;
            this.lblYearToDate.Text = "YTD";
            this.lblYearToDate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChartPeriod_LinkClicked);
            // 
            // lblSixMonths
            // 
            this.lblSixMonths.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSixMonths.AutoSize = true;
            this.lblSixMonths.Location = new System.Drawing.Point(66, 229);
            this.lblSixMonths.Name = "lblSixMonths";
            this.lblSixMonths.Size = new System.Drawing.Size(51, 13);
            this.lblSixMonths.TabIndex = 2;
            this.lblSixMonths.TabStop = true;
            this.lblSixMonths.Text = "6 Months";
            this.lblSixMonths.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChartPeriod_LinkClicked);
            // 
            // lblThreeMonths
            // 
            this.lblThreeMonths.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblThreeMonths.AutoSize = true;
            this.lblThreeMonths.Location = new System.Drawing.Point(123, 229);
            this.lblThreeMonths.Name = "lblThreeMonths";
            this.lblThreeMonths.Size = new System.Drawing.Size(51, 13);
            this.lblThreeMonths.TabIndex = 3;
            this.lblThreeMonths.TabStop = true;
            this.lblThreeMonths.Text = "3 Months";
            this.lblThreeMonths.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChartPeriod_LinkClicked);
            // 
            // lblMonth
            // 
            this.lblMonth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(180, 229);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(37, 13);
            this.lblMonth.TabIndex = 4;
            this.lblMonth.TabStop = true;
            this.lblMonth.Text = "Month";
            this.lblMonth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChartPeriod_LinkClicked);
            // 
            // lblFiveDays
            // 
            this.lblFiveDays.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblFiveDays.AutoSize = true;
            this.lblFiveDays.Location = new System.Drawing.Point(223, 229);
            this.lblFiveDays.Name = "lblFiveDays";
            this.lblFiveDays.Size = new System.Drawing.Size(40, 13);
            this.lblFiveDays.TabIndex = 5;
            this.lblFiveDays.TabStop = true;
            this.lblFiveDays.Text = "5 Days";
            this.lblFiveDays.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChartPeriod_LinkClicked);
            // 
            // lblToday
            // 
            this.lblToday.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(269, 229);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(37, 13);
            this.lblToday.TabIndex = 6;
            this.lblToday.TabStop = true;
            this.lblToday.Text = "Today";
            this.lblToday.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChartPeriod_LinkClicked);
            // 
            // StockChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.lblFiveDays);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblThreeMonths);
            this.Controls.Add(this.lblSixMonths);
            this.Controls.Add(this.lblYearToDate);
            this.Controls.Add(this.chartStockHistory);
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "StockChart";
            this.Size = new System.Drawing.Size(337, 250);
            ((System.ComponentModel.ISupportInitialize)(this.chartStockHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartStockHistory;
        private System.Windows.Forms.LinkLabel lblYearToDate;
        private System.Windows.Forms.LinkLabel lblSixMonths;
        private System.Windows.Forms.LinkLabel lblThreeMonths;
        private System.Windows.Forms.LinkLabel lblMonth;
        private System.Windows.Forms.LinkLabel lblFiveDays;
        private System.Windows.Forms.LinkLabel lblToday;
    }
}

namespace StockinHood.UserControls
{
    partial class QuotePanel
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
            this.lblSymbol = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblPercentChange = new System.Windows.Forms.Label();
            this.cmdMoreInfo = new System.Windows.Forms.Label();
            this.lblLow = new System.Windows.Forms.Label();
            this.lblHigh = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.lblAvgVolume = new System.Windows.Forms.Label();
            this.layoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.layoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSymbol
            // 
            this.lblSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSymbol.Location = new System.Drawing.Point(3, 0);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(56, 35);
            this.lblSymbol.TabIndex = 0;
            this.lblSymbol.Text = "Symbol";
            this.lblSymbol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrice.Location = new System.Drawing.Point(65, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(56, 35);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChange
            // 
            this.lblChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChange.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblChange.Location = new System.Drawing.Point(127, 0);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(56, 35);
            this.lblChange.TabIndex = 2;
            this.lblChange.Text = "Change";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPercentChange
            // 
            this.lblPercentChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPercentChange.Location = new System.Drawing.Point(189, 0);
            this.lblPercentChange.Name = "lblPercentChange";
            this.lblPercentChange.Size = new System.Drawing.Size(56, 35);
            this.lblPercentChange.TabIndex = 3;
            this.lblPercentChange.Text = "% Delta";
            this.lblPercentChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdMoreInfo
            // 
            this.cmdMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMoreInfo.Location = new System.Drawing.Point(510, 5);
            this.cmdMoreInfo.Name = "cmdMoreInfo";
            this.cmdMoreInfo.Size = new System.Drawing.Size(26, 26);
            this.cmdMoreInfo.TabIndex = 4;
            this.cmdMoreInfo.Click += new System.EventHandler(this.cmdMoreInfo_Click);
            this.cmdMoreInfo.MouseEnter += new System.EventHandler(this.cmdMoreInfo_MouseEnter);
            this.cmdMoreInfo.MouseLeave += new System.EventHandler(this.cmdMoreInfo_MouseLeave);
            // 
            // lblLow
            // 
            this.lblLow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLow.Location = new System.Drawing.Point(251, 0);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new System.Drawing.Size(56, 35);
            this.lblLow.TabIndex = 5;
            this.lblLow.Text = "Low";
            this.lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHigh
            // 
            this.lblHigh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHigh.Location = new System.Drawing.Point(313, 0);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(56, 35);
            this.lblHigh.TabIndex = 6;
            this.lblHigh.Text = "High";
            this.lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVolume
            // 
            this.lblVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVolume.Location = new System.Drawing.Point(375, 0);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(56, 35);
            this.lblVolume.TabIndex = 7;
            this.lblVolume.Text = "Volume";
            this.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvgVolume
            // 
            this.lblAvgVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvgVolume.Location = new System.Drawing.Point(437, 0);
            this.lblAvgVolume.Name = "lblAvgVolume";
            this.lblAvgVolume.Size = new System.Drawing.Size(60, 35);
            this.lblAvgVolume.TabIndex = 8;
            this.lblAvgVolume.Text = "Avg Vol";
            this.lblAvgVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // layoutPanelMain
            // 
            this.layoutPanelMain.ColumnCount = 8;
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.layoutPanelMain.Controls.Add(this.lblSymbol, 0, 0);
            this.layoutPanelMain.Controls.Add(this.lblAvgVolume, 7, 0);
            this.layoutPanelMain.Controls.Add(this.lblPrice, 1, 0);
            this.layoutPanelMain.Controls.Add(this.lblVolume, 6, 0);
            this.layoutPanelMain.Controls.Add(this.lblChange, 2, 0);
            this.layoutPanelMain.Controls.Add(this.lblHigh, 5, 0);
            this.layoutPanelMain.Controls.Add(this.lblPercentChange, 3, 0);
            this.layoutPanelMain.Controls.Add(this.lblLow, 4, 0);
            this.layoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.layoutPanelMain.Name = "layoutPanelMain";
            this.layoutPanelMain.RowCount = 1;
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelMain.Size = new System.Drawing.Size(500, 35);
            this.layoutPanelMain.TabIndex = 9;
            // 
            // QuotePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutPanelMain);
            this.Controls.Add(this.cmdMoreInfo);
            this.Name = "QuotePanel";
            this.Size = new System.Drawing.Size(545, 35);
            this.layoutPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSymbol;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblPercentChange;
        private System.Windows.Forms.Label cmdMoreInfo;
        private System.Windows.Forms.Label lblLow;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblAvgVolume;
        private System.Windows.Forms.TableLayoutPanel layoutPanelMain;
    }
}

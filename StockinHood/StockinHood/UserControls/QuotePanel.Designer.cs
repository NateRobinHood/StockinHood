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
            this.SuspendLayout();
            // 
            // lblSymbol
            // 
            this.lblSymbol.Location = new System.Drawing.Point(2, 6);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(51, 23);
            this.lblSymbol.TabIndex = 0;
            this.lblSymbol.Text = "Symbol";
            this.lblSymbol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(59, 6);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(51, 23);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChange
            // 
            this.lblChange.Location = new System.Drawing.Point(116, 6);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(51, 23);
            this.lblChange.TabIndex = 2;
            this.lblChange.Text = "Change";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPercentChange
            // 
            this.lblPercentChange.Location = new System.Drawing.Point(173, 6);
            this.lblPercentChange.Name = "lblPercentChange";
            this.lblPercentChange.Size = new System.Drawing.Size(51, 23);
            this.lblPercentChange.TabIndex = 3;
            this.lblPercentChange.Text = "% Delta";
            this.lblPercentChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdMoreInfo
            // 
            this.cmdMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMoreInfo.Location = new System.Drawing.Point(460, 5);
            this.cmdMoreInfo.Name = "cmdMoreInfo";
            this.cmdMoreInfo.Size = new System.Drawing.Size(26, 26);
            this.cmdMoreInfo.TabIndex = 4;
            this.cmdMoreInfo.Click += new System.EventHandler(this.cmdMoreInfo_Click);
            this.cmdMoreInfo.MouseEnter += new System.EventHandler(this.cmdMoreInfo_MouseEnter);
            this.cmdMoreInfo.MouseLeave += new System.EventHandler(this.cmdMoreInfo_MouseLeave);
            // 
            // lblLow
            // 
            this.lblLow.Location = new System.Drawing.Point(230, 6);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new System.Drawing.Size(51, 23);
            this.lblLow.TabIndex = 5;
            this.lblLow.Text = "Low";
            this.lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHigh
            // 
            this.lblHigh.Location = new System.Drawing.Point(287, 6);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(51, 23);
            this.lblHigh.TabIndex = 6;
            this.lblHigh.Text = "High";
            this.lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVolume
            // 
            this.lblVolume.Location = new System.Drawing.Point(344, 6);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(51, 23);
            this.lblVolume.TabIndex = 7;
            this.lblVolume.Text = "Volume";
            this.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvgVolume
            // 
            this.lblAvgVolume.Location = new System.Drawing.Point(401, 6);
            this.lblAvgVolume.Name = "lblAvgVolume";
            this.lblAvgVolume.Size = new System.Drawing.Size(51, 23);
            this.lblAvgVolume.TabIndex = 8;
            this.lblAvgVolume.Text = "Avg Vol";
            this.lblAvgVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QuotePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAvgVolume);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.lblHigh);
            this.Controls.Add(this.lblLow);
            this.Controls.Add(this.cmdMoreInfo);
            this.Controls.Add(this.lblPercentChange);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblSymbol);
            this.Name = "QuotePanel";
            this.Size = new System.Drawing.Size(495, 35);
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
    }
}

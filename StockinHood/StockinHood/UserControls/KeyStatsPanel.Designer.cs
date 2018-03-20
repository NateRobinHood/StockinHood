namespace StockinHood.UserControls
{
    partial class KeyStatsPanel
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
            this.layoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblStockName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // layoutPanelMain
            // 
            this.layoutPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutPanelMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.layoutPanelMain.Location = new System.Drawing.Point(3, 26);
            this.layoutPanelMain.Name = "layoutPanelMain";
            this.layoutPanelMain.Size = new System.Drawing.Size(286, 288);
            this.layoutPanelMain.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(84, 23);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Key Statistics";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockName
            // 
            this.lblStockName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStockName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStockName.Location = new System.Drawing.Point(84, 0);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(208, 23);
            this.lblStockName.TabIndex = 2;
            this.lblStockName.Text = "Stock Name";
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KeyStatsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblStockName);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.layoutPanelMain);
            this.Name = "KeyStatsPanel";
            this.Size = new System.Drawing.Size(292, 317);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel layoutPanelMain;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblStockName;
    }
}

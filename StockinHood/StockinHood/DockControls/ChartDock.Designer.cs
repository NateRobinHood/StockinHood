namespace StockinHood.DockControls
{
    partial class ChartDock
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
            this.m_ChartControl = new StockinHood.UserControls.StockChart();
            this.SuspendLayout();
            // 
            // m_ChartControl
            // 
            this.m_ChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ChartControl.Location = new System.Drawing.Point(0, 0);
            this.m_ChartControl.Name = "m_ChartControl";
            this.m_ChartControl.Size = new System.Drawing.Size(425, 335);
            this.m_ChartControl.TabIndex = 0;
            // 
            // ChartDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 335);
            this.Controls.Add(this.m_ChartControl);
            this.Name = "ChartDock";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.StockChart m_ChartControl;
    }
}

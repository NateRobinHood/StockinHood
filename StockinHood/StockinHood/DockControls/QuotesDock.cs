using StockinHood.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace StockinHood.DockControls
{
    public class QuotesDock : DockContent
    {
        private QuotesBox m_QuotesControl;

        public QuotesDock()
        {
            InitializeComponent();

            this.Text = "Live Quotes";
        }

        private void InitializeComponent()
        {
            this.m_QuotesControl = new StockinHood.UserControls.QuotesBox();
            this.SuspendLayout();
            // 
            // m_QuotesControl
            // 
            this.m_QuotesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_QuotesControl.Location = new System.Drawing.Point(0, 0);
            this.m_QuotesControl.Name = "m_QuotesControl";
            this.m_QuotesControl.Size = new System.Drawing.Size(608, 410);
            this.m_QuotesControl.TabIndex = 0;
            // 
            // QuotesDock
            // 
            this.ClientSize = new System.Drawing.Size(608, 410);
            this.Controls.Add(this.m_QuotesControl);
            this.Name = "QuotesDock";
            this.ResumeLayout(false);

        }

        public QuotesBox QuotesControl
        {
            get
            {
                return m_QuotesControl;
            }
        }
    }
}

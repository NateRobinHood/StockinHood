using StockinHood.DockControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace StockinHood
{
    public partial class WindowMain : Form
    {
        private DockPanel m_DockPanel;
        private QuotesDock m_QuotesDock;
        private ChartDock m_ChartDock;
        private ChartListDock m_ChartListDock;

        public WindowMain()
        {
            InitializeComponent();

            this.IsMdiContainer = true;

            m_DockPanel = new DockPanel();
            m_DockPanel.Theme = new VS2015DarkTheme();
            m_DockPanel.Dock = DockStyle.Fill;
            this.Controls.Add(m_DockPanel);

            m_QuotesDock = new QuotesDock();
            m_QuotesDock.Show(m_DockPanel, DockState.Document);

            m_ChartDock = new ChartDock();
            m_ChartDock.Show(m_DockPanel, DockState.Document);

            m_ChartListDock = new ChartListDock();
            m_ChartListDock.Show(m_DockPanel, DockState.Document);
            m_ChartListDock.AddSymbol("atvi");
            m_ChartListDock.AddSymbol("ttwo");
            m_ChartListDock.AddSymbol("ea");
            m_ChartListDock.AddSymbol("amd");
            m_ChartListDock.AddSymbol("nvda");
            m_ChartListDock.AddSymbol("sne");
            m_ChartListDock.AddSymbol("glw");
            m_ChartListDock.AddSymbol("chuy");
            m_ChartListDock.AddSymbol("gluu");
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.Handle != null)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    base.OnSizeChanged(e);
                });
            }
        }
    }
}

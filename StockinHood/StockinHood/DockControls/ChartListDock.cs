using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using StockinHood.UserControls;

namespace StockinHood.DockControls
{
    public partial class ChartListDock : DockContent
    {
        public ChartListDock()
        {
            InitializeComponent();

            this.Text = "Stock Price History";
        }

        //Public Methods
        public void AddSymbol(string symbol)
        {
            StockChart newStockChart = new StockChart();

            flowLayoutPanelMain.Controls.Add(newStockChart);

            newStockChart.Width = 600;
            newStockChart.Height = 300;

            newStockChart.SetSymbol(symbol);
        }

        public void RemoveSymbol(string symbol)
        {
            StockChart remStockChart = flowLayoutPanelMain.Controls.Cast<StockChart>().Where(c => c.Symbol == symbol).FirstOrDefault();

            if (remStockChart != null)
            {
                flowLayoutPanelMain.Controls.Remove(remStockChart);
            }
        }

        #region Event Handlers

        private void flowLayoutPanelMain_Resize(object sender, EventArgs e)
        {
            foreach (Control control in flowLayoutPanelMain.Controls)
            {
                control.Width = flowLayoutPanelMain.Width - 10;
            }
        }

        #endregion
    }
}

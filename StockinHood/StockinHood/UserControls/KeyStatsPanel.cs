using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IEXRESTDAL;
using IEXRESTDAL.DataObjects;

namespace StockinHood.UserControls
{
    public partial class KeyStatsPanel : UserControl
    {
        public KeyStatsPanel()
        {
            InitializeComponent();
        }

        //Public Methods
        public void LoadStats(string symbol)
        {
            IEX_Stats keyStats = IEX_REST_DAL.GetKeyStats(symbol);

            lblStockName.Text = keyStats.companyName;

            //Year Hign/Low
            layoutPanelMain.Controls.Add(new StatItem("52 Week High", keyStats.week52high.ToString("C")));
            layoutPanelMain.Controls.Add(new StatItem("52 Week Low", keyStats.week52low.ToString("C")));

            //Beta
            layoutPanelMain.Controls.Add(new StatItem("Beta", keyStats.beta.ToString()));

            //Trailing Months % Chnaged
            layoutPanelMain.Controls.Add(new StatItem("1 Month % Changed", keyStats.month1ChangePercent.ToString("P")));
            layoutPanelMain.Controls.Add(new StatItem("3 Month % Changed", keyStats.month3ChangePercent .ToString("P")));
            layoutPanelMain.Controls.Add(new StatItem("6 Month % Changed", keyStats.month6ChangePercent.ToString("P")));
        }
    }
}

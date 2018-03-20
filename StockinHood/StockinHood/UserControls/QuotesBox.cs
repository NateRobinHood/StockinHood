using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockinHood.Components;

namespace StockinHood.UserControls
{
    public partial class QuotesBox : UserControl
    {
        private ToolStripToggleButton toolStripSymbol;
        private ToolStripToggleButton toolStripPrice;
        private ToolStripToggleButton toolStripChanged;
        private ToolStripToggleButton toolStripPercentChanged;
        private ToolStripToggleButton toolStripLow;
        private ToolStripToggleButton toolStripHigh;
        private ToolStripToggleButton toolStripVolume;
        private ToolStripToggleButton toolStripAvgVolume;


        public QuotesBox()
        {
            InitializeComponent();

            SetColorScheme();

            toolStripSymbol = new ToolStripToggleButton();
            toolStripSymbol.AutoSize = false;
            toolStripSymbol.TextAlign = ContentAlignment.MiddleCenter;
            toolStripSymbol.Text = "Symbol";
            toolStripSymbol.Width = 57;

            toolStripPrice = new ToolStripToggleButton();
            toolStripPrice.AutoSize = false;
            toolStripPrice.TextAlign = ContentAlignment.MiddleCenter;
            toolStripPrice.Text = "Price";
            toolStripPrice.Width = 57;

            toolStripChanged = new ToolStripToggleButton();
            toolStripChanged.AutoSize = false;
            toolStripChanged.TextAlign = ContentAlignment.MiddleCenter;
            toolStripChanged.Text = "Changed";
            toolStripChanged.Width = 57;

            toolStripPercentChanged = new ToolStripToggleButton();
            toolStripPercentChanged.AutoSize = false;
            toolStripPercentChanged.TextAlign = ContentAlignment.MiddleCenter;
            toolStripPercentChanged.Text = "% Changed";
            toolStripPercentChanged.Width = 57;

            toolStripLow = new ToolStripToggleButton();
            toolStripLow.AutoSize = false;
            toolStripLow.TextAlign = ContentAlignment.MiddleCenter;
            toolStripLow.Text = "Low";
            toolStripLow.Width = 57;

            toolStripHigh = new ToolStripToggleButton();
            toolStripHigh.AutoSize = false;
            toolStripHigh.TextAlign = ContentAlignment.MiddleCenter;
            toolStripHigh.Text = "High";
            toolStripHigh.Width = 57;

            toolStripVolume = new ToolStripToggleButton();
            toolStripVolume.AutoSize = false;
            toolStripVolume.TextAlign = ContentAlignment.MiddleCenter;
            toolStripVolume.Text = "Volume";
            toolStripVolume.Width = 57;

            toolStripAvgVolume = new ToolStripToggleButton();
            toolStripAvgVolume.AutoSize = false;
            toolStripAvgVolume.TextAlign = ContentAlignment.MiddleCenter;
            toolStripAvgVolume.Text = "Avg Volume";
            toolStripAvgVolume.Width = 57;

            toolStripMain.Items.Add(toolStripSymbol);
            toolStripMain.Items.Add(toolStripPrice);
            toolStripMain.Items.Add(toolStripChanged);
            toolStripMain.Items.Add(toolStripPercentChanged);

            toolStripMain.Items.Add(toolStripLow);
            toolStripMain.Items.Add(toolStripHigh);
            toolStripMain.Items.Add(toolStripVolume);
            toolStripMain.Items.Add(toolStripAvgVolume);

            Collapse();

            InitSymbols();
        }

        //Private Methods
        private void SetColorScheme()
        {
            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(ColorManager.ToolStripBeginGradient, ColorManager.ToolStripEndGradient);
            GradientRenderer.RoundedEdges = false;

            toolStripMain.Renderer = GradientRenderer;
        }

        private void Expand()
        {
            toolStripLow.Visible = true;
            toolStripHigh.Visible = true;
            toolStripVolume.Visible = true;
            toolStripAvgVolume.Visible = true;
        }

        private void Collapse()
        {
            toolStripLow.Visible = false;
            toolStripHigh.Visible = false;
            toolStripVolume.Visible = false;
            toolStripAvgVolume.Visible = false;
        }

        private void InitSymbols()
        {
            QuotePanel atvi = new QuotePanel();
            atvi.Symbol = "atvi";
            atvi.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel ttwo = new QuotePanel();
            ttwo.Symbol = "ttwo";
            ttwo.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel nvda = new QuotePanel();
            nvda.Symbol = "nvda";
            nvda.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            layoutPanelMain.Controls.Add(atvi);
            layoutPanelMain.Controls.Add(ttwo);
            layoutPanelMain.Controls.Add(nvda);
        }

        private void AnyQuotePanel_OnIsExpandedChanged(object sender, EventArgs e)
        {
            if (layoutPanelMain.Controls.Cast<Control>().Where(c => c is QuotePanel).Select(c => c as QuotePanel).Any(c => c.IsExpanded))
            {
                Expand();
            }
            else
            {
                Collapse();
            }
        }
    }
}

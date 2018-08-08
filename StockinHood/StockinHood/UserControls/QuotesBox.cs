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

            FontFamily fontFamilty = FontFamily.Families.Where(c => c.Name == "Segoe UI").FirstOrDefault();
            Font headerFont = new Font(fontFamilty, 8f, FontStyle.Regular);

            toolStripSymbol = new ToolStripToggleButton();
            toolStripSymbol.Font = headerFont;
            toolStripSymbol.AutoSize = false;
            toolStripSymbol.TextAlign = ContentAlignment.MiddleCenter;
            toolStripSymbol.Text = "Symbol";
            toolStripSymbol.Width = 62;

            toolStripPrice = new ToolStripToggleButton();
            toolStripPrice.Font = headerFont;
            toolStripPrice.AutoSize = false;
            toolStripPrice.TextAlign = ContentAlignment.MiddleCenter;
            toolStripPrice.Text = "Price";
            toolStripPrice.Width = 62;

            toolStripChanged = new ToolStripToggleButton();
            toolStripChanged.Font = headerFont;
            toolStripChanged.AutoSize = false;
            toolStripChanged.TextAlign = ContentAlignment.MiddleCenter;
            toolStripChanged.Text = "Changed";
            toolStripChanged.Width = 62;

            toolStripPercentChanged = new ToolStripToggleButton();
            toolStripPercentChanged.Font = headerFont;
            toolStripPercentChanged.AutoSize = false;
            toolStripPercentChanged.TextAlign = ContentAlignment.MiddleCenter;
            toolStripPercentChanged.Text = "%";
            toolStripPercentChanged.Width = 62;

            toolStripLow = new ToolStripToggleButton();
            toolStripLow.Font = headerFont;
            toolStripLow.AutoSize = false;
            toolStripLow.TextAlign = ContentAlignment.MiddleCenter;
            toolStripLow.Text = "Low";
            toolStripLow.Width = 62;

            toolStripHigh = new ToolStripToggleButton();
            toolStripHigh.Font = headerFont;
            toolStripHigh.AutoSize = false;
            toolStripHigh.TextAlign = ContentAlignment.MiddleCenter;
            toolStripHigh.Text = "High";
            toolStripHigh.Width = 62;

            toolStripVolume = new ToolStripToggleButton();
            toolStripVolume.Font = headerFont;
            toolStripVolume.AutoSize = false;
            toolStripVolume.TextAlign = ContentAlignment.MiddleCenter;
            toolStripVolume.Text = "Volume";
            toolStripVolume.Width = 62;

            toolStripAvgVolume = new ToolStripToggleButton();
            toolStripAvgVolume.Font = headerFont;
            toolStripAvgVolume.AutoSize = false;
            toolStripAvgVolume.TextAlign = ContentAlignment.MiddleCenter;
            toolStripAvgVolume.Text = "Avg Volume";
            toolStripAvgVolume.Width = 62;

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

            QuotePanel glw = new QuotePanel();
            glw.Symbol = "glw";
            glw.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel sne = new QuotePanel();
            sne.Symbol = "sne";
            sne.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel amd = new QuotePanel();
            amd.Symbol = "amd";
            amd.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            //OTC Markets not supports yet through IEX
            //QuotePanel ntdoy = new QuotePanel();
            //ntdoy.Symbol = "ntdoy";
            //ntdoy.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel chuy = new QuotePanel();
            chuy.Symbol = "chuy";
            chuy.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel gluu = new QuotePanel();
            gluu.Symbol = "gluu";
            gluu.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            QuotePanel ea = new QuotePanel();
            ea.Symbol = "ea";
            ea.OnIsExpandedChanged += AnyQuotePanel_OnIsExpandedChanged;

            layoutPanelMain.Controls.Add(atvi);
            layoutPanelMain.Controls.Add(ttwo);
            layoutPanelMain.Controls.Add(nvda);
            layoutPanelMain.Controls.Add(glw);
            layoutPanelMain.Controls.Add(sne);
            layoutPanelMain.Controls.Add(amd);
            layoutPanelMain.Controls.Add(chuy);
            layoutPanelMain.Controls.Add(gluu);
            layoutPanelMain.Controls.Add(ea);
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

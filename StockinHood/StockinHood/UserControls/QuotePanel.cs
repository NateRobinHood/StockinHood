using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using StockinHood.Resources;
using IEXRESTDAL;
using IEXRESTDAL.DataObjects;
using StockinHood.Components;

namespace StockinHood.UserControls
{
    public partial class QuotePanel : UserControl
    {
        //Private Variables
        private Image m_ExpandIcon;
        private Image m_CollapseIcon;
        private Image m_IncreaseIcon;
        private Image m_DecreaseIcon;
        private bool m_IsExpanded = false;
        private Timer m_TickTimer = new Timer();
        private Timer m_PriceFlickerTimer = new Timer();
        private string m_Symbol = string.Empty;
        private BackgroundWorker m_Worker = new BackgroundWorker();
        private Queue<string> m_Queue = new Queue<string>();

        //Stored Data
        private IEX_Quote m_PreviousTickData = null;
        private IEX_Quote m_CurrentTickData = null;


        //Public Events
        public event EventHandler OnIsExpandedChanged;

        //Constructors
        public QuotePanel()
        {
            InitializeComponent();

            m_Worker.DoWork += Worker_DoWork;
            m_Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            LoadImages();

            //start collapsed
            Collapse();

            m_TickTimer.Tick += TickTimer_Tick;
            m_TickTimer.Interval = 1000;
            m_TickTimer.Start();

            m_PriceFlickerTimer.Tick += PriceFlickerTimer_Tick;

            lblSymbol.TextChanged += Label_TextChanged;
            lblPrice.TextChanged += Label_TextChanged;
            lblChange.TextChanged += Label_TextChanged;
            lblPercentChange.TextChanged += Label_TextChanged;
            lblLow.TextChanged += Label_TextChanged;
            lblHigh.TextChanged += Label_TextChanged;
            lblVolume.TextChanged += Label_TextChanged;
            lblAvgVolume.TextChanged += Label_TextChanged;
        }

        //Private Methods
        private void LoadImages()
        {
            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            Stream ExpandIconImage = ThisAssembly.GetManifestResourceStream(ResourceManager.ExpandIconImage);
            Stream CollapseIconImage = ThisAssembly.GetManifestResourceStream(ResourceManager.CollapseIconImage);
            Stream GainsArrowImage = ThisAssembly.GetManifestResourceStream(ResourceManager.GainsArrowImage);
            Stream LossesArrowImage = ThisAssembly.GetManifestResourceStream(ResourceManager.LossesArrowImage);

            if (ExpandIconImage != null && CollapseIconImage != null)
            {
                m_ExpandIcon = Image.FromStream(ExpandIconImage);
                m_CollapseIcon = Image.FromStream(CollapseIconImage);
                m_IncreaseIcon = Image.FromStream(GainsArrowImage);
                m_DecreaseIcon = Image.FromStream(LossesArrowImage);

                //scale image
                Size scaledSize = new Size(26, 26);
                m_ExpandIcon = new Bitmap(m_ExpandIcon, scaledSize);
                m_CollapseIcon = new Bitmap(m_CollapseIcon, scaledSize);
                m_IncreaseIcon = new Bitmap(m_IncreaseIcon, scaledSize);
                m_DecreaseIcon = new Bitmap(m_DecreaseIcon, scaledSize);
            }
        }

        private void LoadData(string symbol)
        {
            m_PreviousTickData = m_CurrentTickData;

            IEX_Quote quoteData = IEX_REST_DAL.GetQuote(symbol);

            m_CurrentTickData = quoteData;

            if (quoteData != null)
            {
                //collapsed data
                if (lblSymbol.IsHandleCreated && lblSymbol.Text != symbol)
                    lblSymbol.Invoke((MethodInvoker)(() => lblSymbol.Text = symbol));

                if (lblPrice.IsHandleCreated && lblPrice.Text != quoteData.latestPrice.ToString("C"))
                    lblPrice.Invoke((MethodInvoker)(() => lblPrice.Text = quoteData.latestPrice.ToString("C")));

                if (lblChange.IsHandleCreated && lblChange.Text != quoteData.change.ToString("C"))
                    lblChange.Invoke((MethodInvoker)(() => lblChange.Text = quoteData.change.ToString("C")));

                if (lblPercentChange.IsHandleCreated && lblPercentChange.Text != quoteData.changePercent.ToString("P"))
                    lblPercentChange.Invoke((MethodInvoker)(() => lblPercentChange.Text = quoteData.changePercent.ToString("P")));

                if (m_IsExpanded)
                {
                    //expanded data
                    string lowValue = quoteData.low.HasValue ? quoteData.low.Value.ToString("C") : "NA";
                    if (lblLow.IsHandleCreated && lblLow.Text != lowValue)
                        lblLow.Invoke((MethodInvoker)(() => lblLow.Text = lowValue));

                    string highValue = quoteData.high.HasValue ? quoteData.high.Value.ToString("C") : "NA";
                    if (lblHigh.IsHandleCreated && lblHigh.Text != highValue)
                        lblHigh.Invoke((MethodInvoker)(() => lblHigh.Text = highValue));

                    if (lblVolume.IsHandleCreated && lblVolume.Text != quoteData.latestVolume.ToString("N0"))
                        lblVolume.Invoke((MethodInvoker)(() => lblVolume.Text = quoteData.latestVolume.ToString("N0")));

                    if (lblAvgVolume.IsHandleCreated && lblAvgVolume.Text != quoteData.avgTotalVolume.ToString("N0"))
                        lblAvgVolume.Invoke((MethodInvoker)(() => lblAvgVolume.Text = quoteData.avgTotalVolume.ToString("N0")));
                }
            }
        }

        private void Expand()
        {
            cmdMoreInfo.Image = m_CollapseIcon;
            m_IsExpanded = true;

            //adjust size
            this.Size = new Size(545, 35);
            layoutPanelMain.Size = new Size(500, 35);

            //show additional info
            lblLow.Visible = true;
            lblHigh.Visible = true;
            lblVolume.Visible = true;
            lblAvgVolume.Visible = true;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblLow)].SizeType = SizeType.Percent;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblLow)].Width = 12.5f;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblHigh)].SizeType = SizeType.Percent;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblHigh)].Width = 12.5f;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblVolume)].SizeType = SizeType.Percent;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblVolume)].Width = 12.5f;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblAvgVolume)].SizeType = SizeType.Percent;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblAvgVolume)].Width = 12.5f;
        }

        private void Collapse()
        {
            cmdMoreInfo.Image = m_ExpandIcon;
            m_IsExpanded = false;

            //hide additional info
            lblLow.Visible = false;
            lblHigh.Visible = false;
            lblVolume.Visible = false;
            lblAvgVolume.Visible = false;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblLow)].SizeType = SizeType.Absolute;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblLow)].Width = 0;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblHigh)].SizeType = SizeType.Absolute;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblHigh)].Width = 0;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblVolume)].SizeType = SizeType.Absolute;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblVolume)].Width = 0;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblAvgVolume)].SizeType = SizeType.Absolute;
            layoutPanelMain.ColumnStyles[layoutPanelMain.GetColumn(lblAvgVolume)].Width = 0;

            //adjust size
            this.Size = new Size(295, 35);
            layoutPanelMain.Size = new Size(250, 35);
        }

        private void ProcessPrice()
        {
            if (m_PreviousTickData?.latestPrice > m_CurrentTickData?.latestPrice)
            {
                lblPrice.ForeColor = ColorManager.TickDecreaseColor;
            }
            else if (m_PreviousTickData?.latestPrice < m_CurrentTickData?.latestPrice)
            {
                lblPrice.ForeColor = ColorManager.TickIncreaseColor;
            }
            m_PriceFlickerTimer.Tag = lblPrice;
            m_PriceFlickerTimer.Interval = 700;
            m_PriceFlickerTimer.Start();
        }

        private void ProcessChange()
        {
            if (m_CurrentTickData.change > 0)
            {
                lblChange.ForeColor = ColorManager.TickIncreaseColor;
                lblPercentChange.ForeColor = ColorManager.TickIncreaseColor;
                //lblChange.Image = m_IncreaseIcon;
            }
            else if (m_CurrentTickData.change < 0)
            {
                lblChange.ForeColor = ColorManager.TickDecreaseColor;
                lblPercentChange.ForeColor = ColorManager.TickDecreaseColor;
                //lblChange.Image = m_DecreaseIcon;
            }
        }


        //Public Properties
        public string Symbol
        {
            get
            {
                return m_Symbol;
            }
            set
            {
                m_Symbol = value;
                lblSymbol.Text = value;
                LoadData(value);
            }
        }

        public bool IsExpanded
        {
            get
            {
                return m_IsExpanded;
            }
        }


        //Event Hanlders
        private void cmdMoreInfo_MouseEnter(object sender, EventArgs e)
        {
            cmdMoreInfo.BorderStyle = BorderStyle.FixedSingle;
        }

        private void cmdMoreInfo_MouseLeave(object sender, EventArgs e)
        {
            cmdMoreInfo.BorderStyle = BorderStyle.None;
        }

        private void cmdMoreInfo_Click(object sender, EventArgs e)
        {
            if (m_IsExpanded)
            {
                Collapse();
            }
            else
            {
                Expand();
            }

            OnIsExpandedChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_Symbol))
            {
                if (!m_Worker.IsBusy)
                    m_Worker.RunWorkerAsync(m_Symbol);
                else
                    m_Queue.Enqueue(m_Symbol);
            }
        }

        private void Label_TextChanged(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null)
            {
                lbl.Font = new Font(lbl.Font.FontFamily, 8.25f, lbl.Font.Style);
                while (lbl.Width < System.Windows.Forms.TextRenderer.MeasureText(lbl.Text,
                    new Font(lbl.Font.FontFamily, lbl.Font.Size, lbl.Font.Style)).Width)
                {
                    lbl.Font = new Font(lbl.Font.FontFamily, lbl.Font.Size - 0.5f, lbl.Font.Style);
                }
            }

            if (lbl == lblPrice)
            {
                ProcessPrice();
            }
            if (lbl == lblChange)
            {
                ProcessChange();
            }

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (m_Queue.Count > 0)
            {
                string symbol = m_Queue.Dequeue();
                m_Worker.RunWorkerAsync(symbol);
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is string)
            {
                LoadData((string)e.Argument);
            }
        }

        private void PriceFlickerTimer_Tick(object sender, EventArgs e)
        {
            if(m_PriceFlickerTimer.Tag is Label)
            {
                ((Label)m_PriceFlickerTimer.Tag).ForeColor = ColorManager.StatItemForegroundColor;
            }
            m_PriceFlickerTimer.Stop();
        }
    }
}

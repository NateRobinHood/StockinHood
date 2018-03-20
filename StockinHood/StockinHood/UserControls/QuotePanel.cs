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

namespace StockinHood.UserControls
{
    public partial class QuotePanel : UserControl
    {
        //Private Variables
        private Image m_ExpandIcon;
        private Image m_CollapseIcon;
        private bool m_IsExpanded = false;
        private Timer m_TickTimer = new Timer();
        private string m_Symbol = string.Empty;

        //Public Events
        public event EventHandler OnIsExpandedChanged;

        //Constructors
        public QuotePanel()
        {
            InitializeComponent();

            LoadImages();

            //start collapsed
            Collapse();

            m_TickTimer.Tick += TickTimer_Tick;
            m_TickTimer.Interval = 300;
            m_TickTimer.Start();
        }


        //Private Methods
        private void LoadImages()
        {
            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            Stream ExpandIconImage = ThisAssembly.GetManifestResourceStream(ResourceManager.ExpandIconImage);
            Stream CollapseIconImage = ThisAssembly.GetManifestResourceStream(ResourceManager.CollapseIconImage);

            if (ExpandIconImage != null && CollapseIconImage != null)
            {
                m_ExpandIcon = Image.FromStream(ExpandIconImage);
                m_CollapseIcon = Image.FromStream(CollapseIconImage);

                //scale image
                Size scaledSize = new Size(26, 26);
                m_ExpandIcon = new Bitmap(m_ExpandIcon, scaledSize);
                m_CollapseIcon = new Bitmap(m_CollapseIcon, scaledSize);
            }
        }

        private void LoadData(string symbol)
        {
            IEX_Quote quoteData = IEX_REST_DAL.GetQuote(symbol);

            //collapsed data
            lblSymbol.Text = symbol;
            lblPrice.Text = quoteData.latestPrice.ToString("C");
            lblChange.Text = quoteData.change.ToString("C");
            lblPercentChange.Text = quoteData.changePercent.ToString("P");

            //expanded data
            lblLow.Text = quoteData.low.ToString("C");
            lblHigh.Text = quoteData.high.ToString("C");
            lblVolume.Text = quoteData.latestVolume.ToString("N");
            lblAvgVolume.Text = quoteData.avgTotalVolume.ToString("N");
        }

        private void Expand()
        {
            cmdMoreInfo.Image = m_CollapseIcon;
            m_IsExpanded = true;

            //adjust size
            this.Size = new Size(495, 35);

            //show additional info
            lblLow.Visible = true;
            lblHigh.Visible = true;
            lblVolume.Visible = true;
            lblAvgVolume.Visible = true;
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

            //adjust size
            this.Size = new Size(265, 35);
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
            if(!string.IsNullOrEmpty(m_Symbol))
                LoadData(m_Symbol);
        }
    }
}

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

namespace StockinHood.UserControls
{
    public partial class StatItem : UserControl
    {
        private static ImageList m_Icons;

        public StatItem()
        {
            InitializeComponent();
        }

        public StatItem(string label, string value, string icon = "")
        {
            InitializeComponent();

            Label = label;
            Value = value;
            if (!string.IsNullOrEmpty(icon))
            {
                SetIcon(icon);
            }
        }

        //Static Property
        public static ImageList Icons
        {
            get
            {
                if (m_Icons == null)
                    LoadIcons();

                return m_Icons;
            }
        }

        //Static Methods
        private static void LoadIcons()
        {
            m_Icons = new ImageList();

            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            Stream GainsArrowImage = ThisAssembly.GetManifestResourceStream(ResourceManager.GainsArrowImage);
            Stream LossesArrowImage = ThisAssembly.GetManifestResourceStream(ResourceManager.LossesArrowImage);

            if (GainsArrowImage != null && LossesArrowImage != null)
            {
                m_Icons.Images.Add(ResourceManager.GainsArrowImage, Image.FromStream(GainsArrowImage));
                m_Icons.Images.Add(ResourceManager.LossesArrowImage, Image.FromStream(LossesArrowImage));
            }
        }

        //Public Properties
        public string Label
        {
            get
            {
                return lblLabel.Text;
            }
            set
            {
                lblLabel.Text = value;
            }
        }

        public string Value
        {
            get
            {
                return lblValue.Text;
            }
            set
            {
                lblValue.Text = value;
            }
        }

        //Public Methods
        public void SetIcon(string resourceName)
        {
            if (this.lblIcon.ImageList != m_Icons)
                this.lblIcon.ImageList = m_Icons;

            this.lblIcon.ImageKey = resourceName;
        }
    }
}

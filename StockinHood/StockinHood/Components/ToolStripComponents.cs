using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockinHood.Components
{
    public class ToolStripToggleButton : ToolStripButton
    {
        public ToolStripToggleButton() : base()
        {

        }

        public enum ToggleState
        {
            ToggleOff = 0,
            Hover,
            HoverMouseDown,
            ToggleOn,
        }
    }

    public class GradientToolStripRenderer : ToolStripProfessionalRenderer
    {
        private Color m_BeginGradient;
        private Color m_EndGradient;

        public GradientToolStripRenderer(Color beginGradient, Color endGradient)
        {
            m_BeginGradient = beginGradient;
            m_EndGradient = endGradient;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            LinearGradientMode mode = LinearGradientMode.Vertical;

            using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, m_BeginGradient, m_EndGradient, mode))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is ToolStripToggleButton)
            {
                ToolStripToggleButton thisButton = e.Item as ToolStripToggleButton;
                if (thisButton != null && thisButton.Tag is ToolStripToggleButton.ToggleState)
                {
                    ToolStripToggleButton.ToggleState ButtonState = (ToolStripToggleButton.ToggleState)thisButton.Tag;

                    if (ButtonState == ToolStripToggleButton.ToggleState.Hover ||
                        ButtonState == ToolStripToggleButton.ToggleState.HoverMouseDown)
                    {
                        Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                        LinearGradientMode mode = LinearGradientMode.Vertical;

                        Color BeginGradient = Color.FromArgb(211, 211, 211);
                        Color EndGradient = Color.FromArgb(185, 185, 185);

                        BeginGradient = Color.FromArgb(201, 201, 201);
                        EndGradient = Color.FromArgb(175, 175, 175);

                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, BeginGradient, EndGradient, mode))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                            e.Graphics.DrawRectangle(Pens.DarkGray, rectangle);
                        }
                    }
                    else if (ButtonState == ToolStripToggleButton.ToggleState.ToggleOn)
                    {
                        Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                        LinearGradientMode mode = LinearGradientMode.Vertical;

                        Color BeginGradient = ColorManager.TabSelectedBeginGradient;
                        Color EndGradient = ColorManager.TabSelectedEndGradient;
                        //Color BeginGradient = Color.FromArgb(211, 211, 211);
                        //Color EndGradient = Color.FromArgb(185, 185, 185);

                        using (LinearGradientBrush b = new LinearGradientBrush(rectangle, BeginGradient, EndGradient, mode))
                        {
                            e.Graphics.FillRectangle(b, rectangle);
                            e.Graphics.DrawRectangle(Pens.DarkGray, rectangle);
                        }
                    }
                }
            }
            else
            {
                if (!e.Item.Selected)
                    base.OnRenderButtonBackground(e);
                else
                {
                    Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                    LinearGradientMode mode = LinearGradientMode.Vertical;

                    Color BeginGradient = Color.FromArgb(211, 211, 211);
                    Color EndGradient = Color.FromArgb(185, 185, 185);

                    ToolStripButton thisButton = e.Item as ToolStripButton;
                    if (thisButton != null && thisButton.Tag is bool && ((bool)thisButton.Tag) == true)
                    {
                        BeginGradient = Color.FromArgb(201, 201, 201);
                        EndGradient = Color.FromArgb(175, 175, 175);
                    }

                    using (LinearGradientBrush b = new LinearGradientBrush(rectangle, BeginGradient, EndGradient, mode))
                    {
                        e.Graphics.FillRectangle(b, rectangle);
                        e.Graphics.DrawRectangle(Pens.DarkGray, rectangle);
                    }
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (!e.Vertical || (e.Item as ToolStripSeparator) == null)
                base.OnRenderSeparator(e);
            else
            {
                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
                bounds.Y += 3;
                bounds.Height = Math.Max(0, bounds.Height - 6);
                if (bounds.Height >= 4) bounds.Inflate(0, -2);
                Pen pen = new Pen(Color.Gray);
                int x = bounds.Width / 2;
                e.Graphics.DrawLine(pen, x, bounds.Top, x, bounds.Bottom - 1);
                pen.Dispose();
                pen = new Pen(Color.LightGray);
                e.Graphics.DrawLine(pen, x + 1, bounds.Top + 1, x + 1, bounds.Bottom);
                pen.Dispose();
            }
        }

        //removes line from under the ToolStrip, official bug from microsoft --NJB
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //base.OnRenderToolStripBorder(e);
        }
    }

    public class MenuToolStripRenderer : ToolStripProfessionalRenderer
    {
        private Color m_BeginGradient;
        private Color m_EndGradient;

        public MenuToolStripRenderer()
        {
            m_BeginGradient = Color.FromArgb(200, 200, 200);
            m_EndGradient = Color.FromArgb(185, 185, 185);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.AffectedBounds.Width > 1000) //attempt to only render background for the toolstrip itself and not the dropdown items, MainForm minimum width is 1050
            {
                LinearGradientMode mode = LinearGradientMode.Vertical;

                using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, m_BeginGradient, m_EndGradient, mode))
                {
                    e.Graphics.FillRectangle(b, e.AffectedBounds);
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected)
            {
                base.OnRenderMenuItemBackground(e);
            }
            else
            {
                Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                if (e.Item.IsOnDropDown)
                {
                    rectangle = new Rectangle(2, 0, e.Item.Size.Width - 4, e.Item.Size.Height - 1);
                }
                LinearGradientMode mode = LinearGradientMode.Vertical;

                Color BeginGradient = Color.FromArgb(221, 221, 221);
                Color EndGradient = Color.FromArgb(195, 195, 195);

                using (LinearGradientBrush b = new LinearGradientBrush(rectangle, BeginGradient, EndGradient, mode))
                {
                    e.Graphics.FillRectangle(b, rectangle);
                    e.Graphics.DrawRectangle(Pens.DarkGray, rectangle);
                }
            }
        }
    }
}

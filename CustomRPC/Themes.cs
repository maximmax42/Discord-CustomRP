using System.Drawing;
using System.Windows.Forms;

namespace CustomRPC
{
    /// <summary>
    /// Color scheme based on current mode.
    /// </summary>
    public static class CurrentColors
    {
        public static Color BgInactive => Color.FromArgb(47, 49, 54);
        public static Color BgHover => Color.FromArgb(52, 55, 60);
        public static Color BgActive => Color.FromArgb(57, 60, 67);
        public static Color CheckBg => Color.FromArgb(61, 73, 162);
        public static Color CheckHover => Color.FromArgb(72, 86, 193);
        public static Color BgColor { get; private set; }
        public static Color BgTextFields { get; private set; }
        public static Color BgTextFieldsSuccess { get; private set; }
        public static Color BgTextFieldsError { get; private set; }
        public static Color TextColor { get; private set; }

        static CurrentColors()
        {
            Update();
        }

        /// <summary>
        /// Updates colors according to currently set dark mode setting.
        /// </summary>
        public static void Update()
        {
            if (Properties.Settings.Default.darkMode)
            {
                BgColor = Color.FromArgb(55, 57, 63);
                BgTextFields = Color.FromArgb(65, 68, 75);
                BgTextFieldsSuccess = Color.FromArgb(50, 150, 50);
                BgTextFieldsError = Color.FromArgb(150, 50, 50);
                TextColor = Color.White;
            }
            else
            {
                BgColor = Color.WhiteSmoke;
                BgTextFields = Color.FromName("Window");
                BgTextFieldsSuccess = Color.FromArgb(192, 255, 192);
                BgTextFieldsError = Color.FromArgb(255, 192, 192);
                TextColor = Color.FromName("ControlText");
            }
        }
    }

    internal class DarkModeColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => CurrentColors.BgHover;
        public override Color MenuItemPressedGradientBegin => CurrentColors.BgActive;
        public override Color MenuItemPressedGradientEnd => MenuItemPressedGradientBegin;
        public override Color MenuItemPressedGradientMiddle => MenuItemPressedGradientBegin;
        public override Color MenuItemSelectedGradientBegin => CurrentColors.BgHover;
        public override Color MenuItemSelectedGradientEnd => MenuItemSelectedGradientBegin;
        public override Color ImageMarginGradientBegin => CurrentColors.BgInactive;
        public override Color ImageMarginGradientMiddle => ImageMarginGradientBegin;
        public override Color ImageMarginGradientEnd => ImageMarginGradientBegin;
        public override Color CheckSelectedBackground => CurrentColors.CheckBg;
        public override Color CheckBackground => CurrentColors.CheckHover;
        public override Color MenuItemBorder => CurrentColors.BgActive;
    }

    internal class DarkModeRenderer : ToolStripProfessionalRenderer
    {
        public DarkModeRenderer() : base(new DarkModeColorTable()) { }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);

            e.Graphics.DrawRectangle(new Pen(CurrentColors.BgActive, 2), e.AffectedBounds);
            e.Graphics.FillRectangle(new SolidBrush(CurrentColors.BgInactive), e.ConnectedArea);
        }
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.ForeColor == Control.DefaultForeColor ? CurrentColors.TextColor : e.Item.ForeColor;

            base.OnRenderItemText(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            e.ToolStrip.BackColor = CurrentColors.BgInactive;

            base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (e.Image != null && e.Item.Tag != null)
                e.Item.Image = Properties.Resources.globe_white;
            base.OnRenderItemImage(e);
        }
    }

    internal class LightModeRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            e.ToolStrip.BackColor = Color.White;

            base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (e.Image != null && e.Item.Tag != null)
                e.Item.Image = Properties.Resources.globe;
            base.OnRenderItemImage(e);
        }
    }
}

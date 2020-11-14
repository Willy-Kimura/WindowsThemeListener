#region Copyright

/*
 * Developer    : Huang Zhenghui(黄正辉) All, QQ group:568015492 QQ:623128629 Email:623128629@qq.com
 * Library      : HZH_Controls
 * License      : MIT
 * 
 * Blog: https://www.cnblogs.com/bfyx
 * GitHub：https://github.com/kwwwvagaa/NetWinformControl
 * gitee：https://gitee.com/kwwwvagaa/net_winform_custom_control.git
 * 
 * Copyright (c) Huang Zhenghui(黄正辉)
 * [UPD] Improved for Windows Theme Listener.
 * 
 */

#endregion


using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace WK.Libraries.WTL.Controls
{
    /// <summary>
    /// Implements a custom toggle switch control for WinForms apps.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [ToolboxItem(true)]
    [DefaultProperty("Checked")]
    [DefaultEvent("CheckedChanged")]
    [ToolboxBitmap(typeof(RadioButton))]
    [Description("Implements a custom toggle switch control for WinForms apps.")]
    public partial class ToggleSwitch : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleSwitch" /> class.
        /// </summary>
        public ToggleSwitch()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.MouseDown += OnMouseDown;
        }

        #endregion

        #region Fields

        private bool _checked;
        private string[] _texts;

        private Color _checkedForeColor = Color.White;
        private Color _uncheckedForeColor = Color.White;
        private Color _checkedSwitchColor = Color.White;
        private Color _uncheckedSwitchColor = Color.White;
        private Color _checkedBackColor = Color.DodgerBlue;
        private Color _uncheckedBackColor = Color.FromArgb(189, 189, 189);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the checked background color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked background color.")]
        public Color CheckedBackColor
        {
            get => _checkedBackColor;
            set
            {
                _checkedBackColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the checked switch color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked switch color.")]
        public Color CheckedSwitchColor
        {
            get => _checkedSwitchColor;
            set
            {
                _checkedSwitchColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the checked fore color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked fore color.")]
        public Color CheckedForeColor
        {
            get => _checkedForeColor;
            set
            {
                _checkedForeColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the unchecked background color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the unchecked background color.")]
        public Color UncheckedBackColor
        {
            get => _uncheckedBackColor;
            set
            {
                _uncheckedBackColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the unchecked switch color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the unchecked switch color.")]
        public Color UncheckedSwitchColor
        {
            get => _uncheckedSwitchColor;
            set
            {
                _uncheckedSwitchColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the checked switch fore color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked fore color.")]
        public Color UncheckedForeColor
        {
            get => _uncheckedForeColor;
            set
            {
                _uncheckedForeColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the switch is checked.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets a value indicating whether the switch is checked.")]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;

                Refresh();

                CheckedChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Gets or sets the checked and unchecked texts respectively.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked and unchecked texts respectively.")]
        public string[] Texts
        {
            get => _texts;
            set
            {
                _texts = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the control font.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the control font.")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;

                Refresh();
            }
        }

        #endregion

        #region Events

        #region Public

        #region Event Handlers

        /// <summary>
        /// Occurs whenever the switch is checked.
        /// </summary>
        [Category("Switch Events")]
        [Description("Occurs whenever the switch is checked.")]
        public event EventHandler CheckedChanged;

        #endregion

        #endregion

        #region Private

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Checked = !Checked;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var grp = e.Graphics;

            grp.SmoothingMode = SmoothingMode.AntiAlias;
            grp.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grp.CompositingQuality = CompositingQuality.HighQuality;

            var fillColor = _checked ? _checkedBackColor : _uncheckedBackColor;
            SolidBrush fillBrush = new SolidBrush(fillColor);
            SolidBrush checkedTextBrush = new SolidBrush(_checkedForeColor);
            SolidBrush uncheckedTextBrush = new SolidBrush(_uncheckedForeColor);
            SolidBrush checkedSwitchBrush = new SolidBrush(_checkedSwitchColor);
            SolidBrush uncheckedSwitchBrush = new SolidBrush(_uncheckedSwitchColor);

            GraphicsPath path = new GraphicsPath();

            path.AddLine(new Point(Height / 2, 1), new Point(Width - Height / 2, 1));
            path.AddArc(new Rectangle(Width - Height - 1, 1, Height - 2, Height - 2), -90, 180);
            path.AddLine(new Point(Width - Height / 2, Height - 1), new Point(Height / 2, Height - 1));
            path.AddArc(new Rectangle(1, 1, Height - 2, Height - 2), 90, 180);
            grp.FillPath(fillBrush, path);

            string strText = string.Empty;

            if (_texts != null && _texts.Length == 2)
            {
                if (_checked)
                    strText = _texts[0];
                else
                    strText = _texts[1];
            }

            if (_checked)
            {
                grp.FillEllipse(checkedSwitchBrush, new Rectangle(Width - Height + 2, 4, Height - 8, Height - 8));

                if (!string.IsNullOrEmpty(strText))
                {
                    SizeF sizeF = grp.MeasureString(strText.Replace(" ", "A"), Font);
                    int intTextY = (Height - (int)sizeF.Height) / 2 + 1;
                    grp.DrawString(strText, Font, checkedTextBrush, new Point((Height - 2 - 4) / 2, intTextY));
                }
            }
            else
            {
                grp.FillEllipse(uncheckedSwitchBrush, new Rectangle(4, 4, Height - 8, Height - 8));

                if (!string.IsNullOrEmpty(strText))
                {
                    SizeF sizeF = grp.MeasureString(strText.Replace(" ", "A"), Font);
                    int intTextY = (Height - (int)sizeF.Height) / 2 + 1;
                    grp.DrawString(strText, Font, uncheckedTextBrush, new Point(Width - 2 - (Height - 6) / 2 - ((Height - 6) / 2) / 2 - (int)sizeF.Width / 2, intTextY));
                }
            }

            fillBrush.Dispose();
            checkedTextBrush.Dispose();
            uncheckedTextBrush.Dispose();
            checkedSwitchBrush.Dispose();
            uncheckedSwitchBrush.Dispose();
        }

        #endregion

        #endregion
    }
}
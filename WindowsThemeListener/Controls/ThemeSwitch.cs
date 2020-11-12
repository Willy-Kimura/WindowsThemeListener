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
    public partial class ThemeSwitch : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeSwitch" /> class.
        /// </summary>
        public ThemeSwitch()
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

        private Color _checkedTextColor = Color.White;
        private Color _checkedColor = Color.DodgerBlue;
        private Color _uncheckedForeColor = Color.White;
        private Color _uncheckedColor = Color.FromArgb(189, 189, 189);

        private SwitchTypes _type = SwitchTypes.Round;

        #endregion

        #region Enumerations

        /// <summary>
        /// Enum SwitchType
        /// </summary>
        public enum SwitchTypes
        {
            /// <summary>
            /// Round switch design.
            /// </summary>
            Round,

            /// <summary>
            /// Flat switch design.
            /// </summary>
            Flat,

            /// <summary>
            /// Line switch design.
            /// </summary>
            Line
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the checked switch color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked switch color.")]
        public Color CheckedColor
        {
            get => _checkedColor;
            set
            {
                _checkedColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the checked switch fore color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked switch fore color.")]
        public Color CheckedForeColor
        {
            get => _checkedTextColor;
            set
            {
                _checkedTextColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the unchecked switch color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the unchecked switch color.")]
        public Color UncheckedColor
        {
            get => _uncheckedColor;
            set
            {
                _uncheckedColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the checked switch fore color.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the checked switch fore color.")]
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
        /// Gets or sets the siwtch design type.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the switch design type.")]
        public SwitchTypes Type
        {
            get => _type;
            set
            {
                _type = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the switch text font.
        /// </summary>
        [Category("Switch Appearance")]
        [Description("Sets the switch text font.")]
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

            if (_type == SwitchTypes.Round)
            {
                var fillColor = _checked ? _checkedColor : _uncheckedColor;
                GraphicsPath path = new GraphicsPath();

                path.AddLine(new Point(this.Height / 2, 1), new Point(this.Width - this.Height / 2, 1));
                path.AddArc(new Rectangle(this.Width - this.Height - 1, 1, this.Height - 2, this.Height - 2), -90, 180);
                path.AddLine(new Point(this.Width - this.Height / 2, this.Height - 1), new Point(this.Height / 2, this.Height - 1));
                path.AddArc(new Rectangle(1, 1, this.Height - 2, this.Height - 2), 90, 180);
                grp.FillPath(new SolidBrush(fillColor), path);

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
                    grp.FillEllipse(Brushes.White, new Rectangle(this.Width - this.Height - 1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));

                    if (!string.IsNullOrEmpty(strText))
                    {
                        SizeF sizeF = grp.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        grp.DrawString(strText, Font, new SolidBrush(_checkedTextColor), new Point((this.Height - 2 - 4) / 2, intTextY));
                    }
                }
                else
                {
                    grp.FillEllipse(Brushes.White, new Rectangle(1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));

                    if (!string.IsNullOrEmpty(strText))
                    {
                        SizeF sizeF = grp.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        grp.DrawString(strText, Font, new SolidBrush(_uncheckedForeColor), new Point(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 - (int)sizeF.Width / 2, intTextY));
                    }
                }
            }
            else if (_type == SwitchTypes.Flat)
            {
                var fillColor = _checked ? _checkedColor : _uncheckedColor;
                GraphicsPath path = new GraphicsPath();
                int intRadius = 5;

                path.AddArc(0, 0, intRadius, intRadius, 180f, 90f);
                path.AddArc(this.Width - intRadius - 1, 0, intRadius, intRadius, 270f, 90f);
                path.AddArc(this.Width - intRadius - 1, this.Height - intRadius - 1, intRadius, intRadius, 0f, 90f);
                path.AddArc(0, this.Height - intRadius - 1, intRadius, intRadius, 90f, 90f);

                grp.FillPath(new SolidBrush(fillColor), path);

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
                    GraphicsPath path2 = new GraphicsPath();

                    path2.AddArc(this.Width - this.Height - 1 + 2, 1 + 2, intRadius, intRadius, 180f, 90f);
                    path2.AddArc(this.Width - 1 - 2 - intRadius, 1 + 2, intRadius, intRadius, 270f, 90f);
                    path2.AddArc(this.Width - 1 - 2 - intRadius, this.Height - 2 - intRadius - 1, intRadius, intRadius, 0f, 90f);
                    path2.AddArc(this.Width - this.Height - 1 + 2, this.Height - 2 - intRadius - 1, intRadius, intRadius, 90f, 90f);
                    grp.FillPath(Brushes.White, path2);

                    if (!string.IsNullOrEmpty(strText))
                    {
                        SizeF sizeF = grp.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        grp.DrawString(strText, Font, new SolidBrush(_checkedTextColor), new Point((this.Height - 2 - 4) / 2, intTextY));
                    }
                }
                else
                {
                    GraphicsPath path2 = new GraphicsPath();

                    path2.AddArc(1 + 2, 1 + 2, intRadius, intRadius, 180f, 90f);
                    path2.AddArc(this.Height - 2 - intRadius, 1 + 2, intRadius, intRadius, 270f, 90f);
                    path2.AddArc(this.Height - 2 - intRadius, this.Height - 2 - intRadius - 1, intRadius, intRadius, 0f, 90f);
                    path2.AddArc(1 + 2, this.Height - 2 - intRadius - 1, intRadius, intRadius, 90f, 90f);
                    grp.FillPath(Brushes.White, path2);

                    if (!string.IsNullOrEmpty(strText))
                    {
                        SizeF sizeF = grp.MeasureString(strText.Replace(" ", "A"), Font);
                        int intTextY = (this.Height - (int)sizeF.Height) / 2 + 2;
                        grp.DrawString(strText, Font, new SolidBrush(_uncheckedForeColor), new Point(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 - (int)sizeF.Width / 2, intTextY));
                    }
                }
            }
            else
            {
                var fillColor = _checked ? _checkedColor : _uncheckedColor;
                int intLineHeight = (this.Height - 2 - 4) / 2;

                GraphicsPath path = new GraphicsPath();

                path.AddLine(new Point(this.Height / 2, (this.Height - intLineHeight) / 2), new Point(this.Width - this.Height / 2, (this.Height - intLineHeight) / 2));
                path.AddArc(new Rectangle(this.Width - this.Height / 2 - intLineHeight - 1, (this.Height - intLineHeight) / 2, intLineHeight, intLineHeight), -90, 180);
                path.AddLine(new Point(this.Width - this.Height / 2, (this.Height - intLineHeight) / 2 + intLineHeight), new Point(this.Width - this.Height / 2, (this.Height - intLineHeight) / 2 + intLineHeight));
                path.AddArc(new Rectangle(this.Height / 2, (this.Height - intLineHeight) / 2, intLineHeight, intLineHeight), 90, 180);
                grp.FillPath(new SolidBrush(fillColor), path);

                if (_checked)
                {
                    grp.FillEllipse(new SolidBrush(fillColor), new Rectangle(this.Width - this.Height - 1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    grp.FillEllipse(Brushes.White, new Rectangle(this.Width - 2 - (this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 - 4, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                }
                else
                {
                    grp.FillEllipse(new SolidBrush(fillColor), new Rectangle(1 + 2, 1 + 2, this.Height - 2 - 4, this.Height - 2 - 4));
                    grp.FillEllipse(Brushes.White, new Rectangle((this.Height - 2 - 4) / 2 - ((this.Height - 2 - 4) / 2) / 2 + 4, (this.Height - 2 - (this.Height - 2 - 4) / 2) / 2 + 1, (this.Height - 2 - 4) / 2, (this.Height - 2 - 4) / 2));
                }
            }
        }

        #endregion

        #endregion
    }
}
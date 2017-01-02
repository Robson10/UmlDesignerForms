﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner.Components
{
    public partial class End : UserControl
    {


        List<RectangleF> Rubber = new List<RectangleF>();

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                Invalidate();
            }
        }
        public Color BackgroundColor = Color.FromArgb(200, 50, 50);
        public Color RubberColor = Color.Silver;
        public Size RubberSize = new Size(5, 5);

        public End()
        {
            InitializeComponent();
            ComponentPresets();
            Invalidate();
        }
        private void ComponentPresets()
        {
            DoubleBuffered = true;
            Size = new Size(100, 50);
            RubberCalculate();
        }
        /// <summary>
        /// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz "gumki" służące do zmiany rozmiaru kontrolki jesli jest zaznaczona;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            BackColor = Color.Transparent;
            e.Graphics.FillEllipse(new SolidBrush(BackgroundColor), 0, 0, Width - 1, Height - 1);
            Font font = new Font(FontFamily.GenericSansSerif, Height / 3, FontStyle.Bold);

            float stringWidth = e.Graphics.MeasureString(StartEndDictionary.TextEnd, font).Width;
            e.Graphics.DrawString(StartEndDictionary.TextEnd, font, new SolidBrush(Color.Black), (Width - stringWidth) / 2, Height / 4);
            e.Graphics.FillEllipse(new SolidBrush(Color.YellowGreen), Width / 2 - 4, 0, 8, 8);

            if (IsSelected)
            {
                for (int i = 0; i < Rubber.Count; i++)
                    e.Graphics.FillRectangle(new SolidBrush(RubberColor), Rubber[i]);
            }
        }


        /// <summary>
        /// Zdarzenie włączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            IsSelected = !IsSelected;
            Invalidate();
        }
        /// <summary>
        /// Zdarzenie wyłączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            IsSelected = false;
            Invalidate();
        }
        /// <summary>
        /// Zdarzenie wyłączające zaznaczenie kontrolki oraz wywołujące jej ponowne rysowanie;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            IsSelected = false;
            Invalidate();
        }
        /// <summary>
        /// Metoda mająca na celu utworzenie oraz rozmieszczenie "gumek" na kontrolce które będą służyć do zmiany rozmiaru kontrolki.
        /// metoda jest wywoływana jest w evencie OnResize()
        /// </summary>
        private void RubberCalculate()
        {
            Rubber.Clear();
            PointF topLeft = new PointF(0, 0);
            PointF topCenter = new PointF(Width / 2 - RubberSize.Width / 2, 0);
            PointF topRight = new PointF(Width - RubberSize.Width, 0);
            PointF centerLeft = new PointF(0, Height / 2 - RubberSize.Height / 2);
            PointF centerRight = new PointF(Width - RubberSize.Width, Height / 2 - RubberSize.Height / 2);
            PointF bottomLeft = new PointF(0, Height - RubberSize.Height);
            PointF bottomCenter = new PointF(Width / 2 - RubberSize.Width / 2, Height - RubberSize.Height);
            PointF bottomRight = new PointF(Width - RubberSize.Width, Height - RubberSize.Height);

            Rubber.Add(new RectangleF(topLeft, RubberSize));
            Rubber.Add(new RectangleF(topCenter, RubberSize));
            Rubber.Add(new RectangleF(topRight, RubberSize));
            Rubber.Add(new RectangleF(centerRight, RubberSize));
            Rubber.Add(new RectangleF(bottomRight, RubberSize));
            Rubber.Add(new RectangleF(bottomCenter, RubberSize));
            Rubber.Add(new RectangleF(bottomLeft, RubberSize));
            Rubber.Add(new RectangleF(centerLeft, RubberSize));
        }

    }
}

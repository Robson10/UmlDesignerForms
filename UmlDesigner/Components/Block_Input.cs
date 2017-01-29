using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UmlDesigner.Components
{
    class Block_Input:Block_Template
    {
        public Block_Input()
        {
            ComponentPresets();
            Invalidate();
        }
        /// <summary>
        /// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz gumki do zmiany rozmiaru kontrolki jesli jest zaznaczona;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Dictionary.ShapeColor_Start, ShapeInput());
        }
        /// <summary>
        /// Metoda służąca do odcięcia obszaru na zewnątrz docelowego kształtu komponentu- Przezroczystość
        /// </summary>
        protected Rectangle ShapeInput()
        {
            GraphicsPath GrPath = new GraphicsPath();
            Point[] points = new Point[]
            {
                new Point(Dictionary.RubberSize.Width*3, Dictionary.RubberSize.Height),
                new Point(Width -Dictionary.RubberSize.Width,Dictionary.RubberSize.Height),
                new Point(Width-Dictionary.RubberSize.Width*3,Height-Dictionary.RubberSize.Height),
                new Point(Dictionary.RubberSize.Width, Height-Dictionary.RubberSize.Height)
            };
            GrPath.AddLines(points);
            if (IsSelected) CutTheRubbers(ref GrPath);
            Region = new System.Drawing.Region(GrPath);

            return new Rectangle(Dictionary.RubberSize.Width, Dictionary.RubberSize.Height, ClientRectangle.Width - Dictionary.RubberSize.Width * 2, ClientRectangle.Height - Dictionary.RubberSize.Height * 2);
        }


        /// <summary>
        /// Event reagujący na zmianę rozmiaru kontrolki, 
        /// wywołuje on metode aktualizującą położenie gumek
        /// oraz poniwnie usuwa obszar ktory powinien być niewidoczny
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            UpdateRubbersLocation();
        }
    }
}

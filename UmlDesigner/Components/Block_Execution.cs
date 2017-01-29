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
    class Block_Execution:Block_Template
    {
        public Block_Execution()
        {
            ComponentPresets();
            Invalidate();
        }
        Size stringSize;
        Font font;
        /// <summary>
        /// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz gumki do zmiany rozmiaru kontrolki jesli jest zaznaczona;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Dictionary.ShapeColor_Execution, ShapeExecution());
        }
        /// <summary>
        /// Metoda służąca do odcięcia obszaru na zewnątrz docelowego kształtu komponentu- Przezroczystość
        /// </summary>
        protected Rectangle ShapeExecution()
        {
            GraphicsPath GrPath = new GraphicsPath();
            GrPath.AddRectangle( new Rectangle(new Point(Dictionary.RubberSize),ClientRectangle.Size- Dictionary.RubberSize - Dictionary.RubberSize));
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

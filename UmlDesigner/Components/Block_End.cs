using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UmlDesigner.Components
{
    public partial class Block_End : Block_Template
    {
        public Block_End()
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
            e.Graphics.FillEllipse(Dictionary.ShapeColor_End, ShapeEnd());
            FindSuitableFontAndFontSizeForText(out font, out stringSize, e, Dictionary.TextEnd);
            e.Graphics.DrawString(Dictionary.TextEnd, font, new SolidBrush(Color.Black), (Width - stringSize.Width) / 2, Height / 2 - stringSize.Height / 2);
        }
        /// <summary>
        /// Metoda służąca do odcięcia obszaru na zewnątrz docelowego kształtu komponentu- Przezroczystość
        /// </summary>
        protected Rectangle ShapeEnd()
        {
            GraphicsPath GrPath = new GraphicsPath();
            GrPath.AddEllipse(Dictionary.RubberSize.Width + 1, Dictionary.RubberSize.Height + 1, ClientSize.Width - Dictionary.RubberSize.Width * 2 - 2, ClientSize.Height - Dictionary.RubberSize.Height * 2 - 2);
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
        /// <summary>
        /// Metoda wywoływana w zdarzeniu OnPaint w celu dobrania 
        /// odpowiedniego rozmiaru czcionki dla tekstu aby ten miescił sie w Kontrolce
        /// </summary>
        /// <param name="font"></param>
        /// <param name="stringSize"></param>
        /// <param name="e"></param>
        protected void FindSuitableFontAndFontSizeForText(out Font font, out Size stringSize, PaintEventArgs e, string text)
        {
            int fontSize = (int)Math.Ceiling(Height / 3.0) + 1;
            do
            {
                fontSize--;
                font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                stringSize = e.Graphics.MeasureString(text, font).ToSize();
            } while (stringSize.Width > (Width - Dictionary.RubberSize.Width * 2) && fontSize > 1);
        }
    }
}

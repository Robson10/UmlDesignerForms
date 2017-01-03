using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner.Components
{
    public partial class End : MainUserControl
    {
        public End()
        {
            InitializeComponent();
            ComponentPresets();
            RubbersPresets();
            Invalidate();
        }
        /// <summary>
        /// Metoda rysująca kształty na kontrolce w tym wypadku elipsę oraz gumki do zmiany rozmiaru kontrolki jesli jest zaznaczona;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(StartEndDictionary.BackgroundColor), 0, 0, Width - 1, Height - 1);
            Size stringSize;
            Font font;
            FindSuitableFontAndFontSizeForText(out font, out stringSize, e, StartEndDictionary.TextEnd);
            e.Graphics.DrawString(StartEndDictionary.TextEnd, font, new SolidBrush(Color.Black), (Width - stringSize.Width) / 2, Height / 2 - stringSize.Height / 2);
            e.Graphics.FillEllipse(new SolidBrush(Color.YellowGreen), Width / 2 - 4, Height - 8, 8, 8);
        }
    }
}

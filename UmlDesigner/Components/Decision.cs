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
    class Decision: MainUserControl
    {
        public Decision()
        {
            ComponentPresets();
            RubbersPresets();
            //CutDecisionShape();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            GraphicsPath GrPath = new GraphicsPath();
            Point [] points= new Point[] {
                new Point(StartEndDictionary.RubberSize.Width, Height / 2),
                new Point(Width / 2, StartEndDictionary.RubberSize.Height),
                new Point(Width-StartEndDictionary.RubberSize.Width, Height / 2),
                new Point(Width / 2, Height-StartEndDictionary.RubberSize.Height) };

            e.Graphics.FillRectangle(new SolidBrush(Color.Green), new Rectangle(10,10,ClientSize.Width-21,ClientSize.Height-21));

            GrPath.AddLines(points);
            CutTheAllRubbers(ref GrPath);
            Region = new Region(GrPath);

            Size stringSize;
            Font font;
            FindSuitableFontAndFontSizeForText(out font, out stringSize, e, StartEndDictionary.TextDecision);
            e.Graphics.DrawString(StartEndDictionary.TextDecision, font, new SolidBrush(Color.Black), (Width - stringSize.Width) / 2, Height / 2 - stringSize.Height / 2);

        }
        protected override void OnResize(EventArgs e)
        {
            UpdateRubbersLocation();
            //CutDecisionShape();
        }
    }
}
